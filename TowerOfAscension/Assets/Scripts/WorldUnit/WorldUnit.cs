using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class WorldUnit : 
	MonoBehaviour
	{
	public interface IWorldUnitController{
		event EventHandler<MeleeAttackEventArgs> OnMeleeAttackAnimation;
		event EventHandler<TextPopupEventArgs> OnTextPopupEvent;
		void InvokeMeleeAttackAnimation(Direction direction);
		void InvokeTextPopupEvent(string text);
	}
	public class MeleeAttackEventArgs : EventArgs{
		public Direction direction;
		public MeleeAttackEventArgs(Direction direction){
			this.direction = direction;
		}
	}
	public class TextPopupEventArgs : EventArgs{
		public string text;
		public TextPopupEventArgs(string text){
			this.text = text;
		}
	}
	public class NullWorldUnitController : 
		IWorldUnitController
		{
		[field:NonSerialized]public	event EventHandler<MeleeAttackEventArgs> OnMeleeAttackAnimation;
		[field:NonSerialized]public	event EventHandler<TextPopupEventArgs> OnTextPopupEvent;
		public void InvokeMeleeAttackAnimation(Direction direction){}
		public void InvokeTextPopupEvent(string text){}
		private void Silence(){
			OnMeleeAttackAnimation?.Invoke(this, new MeleeAttackEventArgs(Direction.GetNullDirection()));
			OnTextPopupEvent?.Invoke(this, new TextPopupEventArgs(""));
		}
	}
	private static NullWorldUnitController _NULL_WORLD_UNIT_CTRL = new NullWorldUnitController();
	private Unit _unit = Unit.GetNullUnit();
	private Game _local = Game.GetNullGame();
	[SerializeField]private GameObject _offset;
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private WorldUnitUI _worldUnitUI;
	private void OnDisable(){
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
		RefreshAll();
	}
	public void RefreshAll(){
		_offset.transform.localPosition = _local.GetLevel().GetVector3CellOffset();
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
	public void RefreshVisibility(){
		_offset.SetActive(_unit.GetTag(_local, Tag.ID.WorldUnit).GetICondition().Check(_local, _unit));
	}
	public void UpdateWorldPosition(){
		RefreshVisibility();
		Vector3 position = _unit.GetTag(_local, Tag.ID.Position).GetIGetVector().GetVector(_local, _unit);
		int moveSpeed = _unit.GetTag(_local, Tag.ID.Move).GetIGetIntValue1().GetIntValue1(_local, _unit);
		if(moveSpeed > 0 && _offset.activeSelf){
			DungeonMaster.GetInstance().QueueAction(() => MoveAnimation(position, moveSpeed));
		}else{
			this.transform.localPosition = position;
		}
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
		const float MOVE_FACTOR = 10f;
		StartCoroutine(LerpToTarget(target, moveSpeed / MOVE_FACTOR, null));
	}
	public void AttackAnimation(Vector3 target){
		const float ATTACK_SPEED = 0.1f;
		Vector3 position = this.transform.localPosition;
		StartCoroutine(LerpToTarget(target, ATTACK_SPEED, () => {
			StartCoroutine(LerpToTarget(position, ATTACK_SPEED, null));
		}));
	}
	public void UnitDestroy(){
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
		RefreshVisibility();
		if(!_offset.activeSelf){
			return;
		}
		DungeonMaster.GetInstance().QueueAction(() => AttackAnimation((e.direction.GetWorldDirection(_local) / 2) + this.transform.localPosition));
	}
	private void OnTextPopupEvent(object sender, TextPopupEventArgs e){
		TextPopupManager.GetInstance().PopText(e.text, (_unit.GetTag(_local, Tag.ID.Position).GetIGetVector().GetVector(_local, _unit) + _local.GetLevel().GetVector3CellOffset()));
	}
	private void OnLightUpdate(object sender, EventArgs e){
		RefreshVisibility();
	}
	public static IWorldUnitController GetNullWorldUnitController(){
		return _NULL_WORLD_UNIT_CTRL;
	}
}
