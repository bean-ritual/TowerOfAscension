using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnit : 
	MonoBehaviour,
	WorldUnitCollider.IToolTip
	{
	public interface IWorldUnitController{
		event EventHandler<MeleeAttackEventArgs> OnMeleeAttackAnimation;
		event EventHandler<TextPopupEventArgs> OnTextPopupEvent;
		void InvokeMeleeAttackAnimation(Direction direction);
		void InvokeTextPopupEvent(string text);
		void InvokeTextPopupEvent(string text, TextPopup.TextColour colour);
	}
	public class MeleeAttackEventArgs : EventArgs{
		public Direction direction;
		public MeleeAttackEventArgs(Direction direction){
			this.direction = direction;
		}
	}
	public class TextPopupEventArgs : EventArgs{
		public string text;
		public TextPopup.TextColour colour = TextPopup.TextColour.White;
		public TextPopupEventArgs(string text){
			this.text = text;
		}
		public TextPopupEventArgs(string text, TextPopup.TextColour colour){
			this.text = text;
			this.colour = colour;
		}
	}
	public class NullWorldUnitController : 
		IWorldUnitController
		{
		[field:NonSerialized]public	event EventHandler<MeleeAttackEventArgs> OnMeleeAttackAnimation;
		[field:NonSerialized]public	event EventHandler<TextPopupEventArgs> OnTextPopupEvent;
		public void InvokeMeleeAttackAnimation(Direction direction){}
		public void InvokeTextPopupEvent(string text){}
		public void InvokeTextPopupEvent(string text, TextPopup.TextColour colour){}
		private void Silence(){
			OnMeleeAttackAnimation?.Invoke(this, new MeleeAttackEventArgs(Direction.GetNullDirection()));
			OnTextPopupEvent?.Invoke(this, new TextPopupEventArgs(""));
		}
	}
	private static NullWorldUnitController _NULL_WORLD_UNIT_CTRL = new NullWorldUnitController();
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	[SerializeField]private GameObject _visual;
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private WorldUnitUI _worldUnitUI;
	[SerializeField]private WorldUnitCollider _worldUnitCollider;
	private void OnDestroy(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit, Camera camera){
		UnsubscribeFromEvents();
		_unit = unit;
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_unit.GetTag(_local, Tag.ID.Position).OnTagUpdate += OnWorldPositionUpdate;
		_unit.GetTag(_local, Tag.ID.WorldUnit).OnTagUpdate += OnWorldUnitTagUpdate;
		_unit.GetTag(_local, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(_local, _unit).OnMeleeAttackAnimation += OnMeleeAttackAnimation;
		_unit.GetTag(_local, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(_local, _unit).OnTextPopupEvent += OnTextPopupEvent;
		_local.GetLevel().OnLightUpdate += OnLightUpdate;
		_worldUnitUI.Setup(_unit, camera);
		_worldUnitCollider.Setup(this);
		RefreshAll();
	}
	public void RefreshAll(){
		Vector3 offset = _local.GetLevel().GetVector3CellOffset();
		_visual.transform.localPosition = offset;
		_worldUnitCollider.SetPosition(offset);
		this.transform.localPosition = _unit.GetTag(_local, Tag.ID.Position).GetIGetVector().GetVector(_local, _unit);
		RefreshSprite();
		RefreshVisibility();
		_worldUnitUI.RefreshAll();
	}
	public void RefreshSprite(){
		Tag tag = _unit.GetTag(_local, Tag.ID.WorldUnit);
		_renderer.sprite = tag.GetIGetSprite().GetSprite(_local, _unit);
		_renderer.sortingOrder = tag.GetIGetIntValue2().GetIntValue2(_local, _unit);
	}
	public bool CheckVisibility(){
		return _visual.activeSelf || _unit.GetTag(_local, Tag.ID.WorldUnit).GetICondition().Check(_local, _unit);
	}
	public void RefreshVisibility(){
		_visual.SetActive(_unit.GetTag(_local, Tag.ID.WorldUnit).GetICondition().Check(_local, _unit));
		_worldUnitCollider.SetActive(_unit.GetTag(_local, Tag.ID.Tooltip).GetICondition().Check(_local, _unit));
	}
	public void UpdateWorldPosition(){
		Vector3 position = _unit.GetTag(_local, Tag.ID.Position).GetIGetVector().GetVector(_local, _unit);
		int moveSpeed = _unit.GetTag(_local, Tag.ID.Move).GetIGetIntValue1().GetIntValue1(_local, _unit);
		if(moveSpeed > 0 && CheckVisibility()){
			DungeonMaster.GetInstance().QueueAction(() => MoveAnimation(position, moveSpeed));
		}else{
			this.transform.localPosition = position;
			RefreshVisibility();
		}
	}
	public string GetToolTip(){
		return _unit.GetTag(_local, Tag.ID.Tooltip).GetIGetStringValue1().GetStringValue1(_local, _unit);
	}
	public IEnumerator LerpToTarget(Vector3 target, float duration, Action OnAnimationComplete){
		float time = 0f;
		Vector3 start = this.transform.localPosition;
		while(time < duration){
			this.transform.localPosition = Vector3.Lerp(start, target, time / duration);
			time += Time.deltaTime;
			DungeonMaster.GetInstance().BusyFrame();
			yield return null;
		}
		this.transform.localPosition = target;
		OnAnimationComplete?.Invoke();
		DungeonMaster.GetInstance().BusyFrame();
	}
	public void MoveAnimation(Vector3 target, int moveSpeed){
		const float MOVE_FACTOR = 100f;
		StartCoroutine(LerpToTarget(target, moveSpeed / MOVE_FACTOR, RefreshVisibility));
	}
	public void AttackAnimation(Vector3 target){
		const float ATTACK_SPEED = 0.1f;
		Vector3 position = this.transform.localPosition;
		StartCoroutine(LerpToTarget(target, ATTACK_SPEED, () => {
			StartCoroutine(LerpToTarget(position, ATTACK_SPEED, null));
		}));
	}
	public void UnitDestroy(){
		StopAllCoroutines();
		Destroy(gameObject);
	}
	private void UnsubscribeFromEvents(){
		_unit.GetTag(_local, Tag.ID.Position).OnTagUpdate -= OnWorldPositionUpdate;
		_unit.GetTag(_local, Tag.ID.WorldUnit).OnTagUpdate -= OnWorldUnitTagUpdate;
		_unit.GetTag(_local, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(_local, _unit).OnMeleeAttackAnimation -= OnMeleeAttackAnimation;
		_unit.GetTag(_local, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(_local, _unit).OnTextPopupEvent -= OnTextPopupEvent;
		_local.GetLevel().OnLightUpdate -= OnLightUpdate;
	}
	private void OnWorldPositionUpdate(object sender, EventArgs e){
		UpdateWorldPosition();
	}
	private void OnWorldUnitTagUpdate(object sender, EventArgs e){
		RefreshSprite();
	}
	private void OnMeleeAttackAnimation(object sender, MeleeAttackEventArgs e){
		if(CheckVisibility()){
			DungeonMaster.GetInstance().QueueAction(() => AttackAnimation((e.direction.GetWorldDirection(_local) / 2) + this.transform.localPosition));
		}
	}
	private void OnTextPopupEvent(object sender, TextPopupEventArgs e){
		TextPopupManager.GetInstance().PopText(
			e.text, 
			(_unit.GetTag(_local, Tag.ID.Position).GetIGetVector().GetVector(_local, _unit) + _local.GetLevel().GetVector3CellOffset()), 
			e.colour
		);
	}
	private void OnLightUpdate(object sender, EventArgs e){
		RefreshVisibility();
	}
	public static IWorldUnitController GetNullWorldUnitController(){
		return _NULL_WORLD_UNIT_CTRL;
	}
}
