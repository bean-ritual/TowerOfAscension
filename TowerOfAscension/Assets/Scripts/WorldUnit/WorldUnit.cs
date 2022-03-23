using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnit : MonoBehaviour{
	public class UnitAnimateEventArgs : EventArgs{
		public Unit unit;
		public Vector3 position;
		public UnitAnimateEventArgs(Unit unit, Vector3 position){
			this.unit = unit;
			this.position = position;
		}
	}
	public interface IWorldUnit{
		event EventHandler<EventArgs> OnWorldUnitUpdate;
		Sprite GetSprite();
		int GetSortingOrder();
		bool GetWorldVisibility(Level level);
	}
	public interface IWorldUnitUI{
		event EventHandler<EventArgs> OnWorldUnitUIUpdate;
		Vector3 GetUIOffset();
		int GetUISortingOrder();
		bool GetHealthBar();
	}
	public interface IWorldUnitAnimations{
		event EventHandler<UnitAnimateEventArgs> OnMoveAnimation;
		event EventHandler<UnitAnimateEventArgs> OnAttackAnimation;
	}
	private Unit _unit = Unit.GetNullUnit();
	private Level _level = Level.GetNullLevel();
	[SerializeField]private GameObject _offset;
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private WorldUnitUI _worldUnitUI;
	private void OnDisable(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		UnsubscribeFromEvents();
		_unit = unit;
		_unit.GetWorldUnit().OnWorldUnitUpdate += OnWorldUnitUpdate;
		_unit.GetWorldUnitAnimations().OnMoveAnimation += OnMoveAnimation;
		_unit.GetWorldUnitAnimations().OnAttackAnimation += OnAttackAnimation;
		_level = DungeonMaster.GetInstance().GetLevel();
		_level.OnLightUpdate += OnLightUpdate;
		_worldUnitUI.Setup(_unit);
		RefreshAll();
	}
	public void RefreshAll(){
		_renderer.sprite = _unit.GetWorldUnit().GetSprite();
		_renderer.sortingOrder = _unit.GetWorldUnit().GetSortingOrder();
		_offset.transform.localPosition = _level.GetVector3CellOffset();
		this.transform.localPosition = _unit.GetPositionable().GetPosition(_level);
		_worldUnitUI.Refresh();
		RefreshVisibility();
	}
	public void RefreshVisibility(){
		_offset.SetActive(_unit.GetWorldUnit().GetWorldVisibility(_level));
	}
	public IEnumerator LerpToTarget(Vector3 target, float duration, Action OnAnimationComplete){
		float time = 0f;
		Vector3 start = transform.localPosition;
		while(time < duration){
			transform.localPosition = Vector3.Lerp(start, target, time / duration);
			time += Time.deltaTime;
			WorldUnitManager.GetInstance().OnFrameAnimation();
			yield return null;
		}
		transform.localPosition = target;
		OnAnimationComplete?.Invoke();
		WorldUnitManager.GetInstance().OnFrameAnimation();
	}
	public void MoveAnimation(Vector3 target){
		const float MOVE_DURATION = 0.1f;
		StartCoroutine(LerpToTarget(target, MOVE_DURATION, null));
	}
	public void AttackAnimation(Vector3 target){
		const float ATTACK_DURATION = 0.08f;
		Vector3 position = transform.localPosition;
		StartCoroutine(LerpToTarget(target, ATTACK_DURATION, () => {
			StartCoroutine(LerpToTarget(position, ATTACK_DURATION, null));
		}));
	}
	public void UnitDestroy(){
		Destroy(gameObject);
	}
	private void UnsubscribeFromEvents(){
		_unit.GetWorldUnit().OnWorldUnitUpdate -= OnWorldUnitUpdate;
		_unit.GetWorldUnitAnimations().OnMoveAnimation -= OnMoveAnimation;
		_unit.GetWorldUnitAnimations().OnAttackAnimation -= OnAttackAnimation;
		_level.OnLightUpdate -= OnLightUpdate;
	}
	private void OnWorldUnitUpdate(object sender, EventArgs e){
		RefreshAll();
	}
	private void OnMoveAnimation(object sender, UnitAnimateEventArgs e){
		if(!_offset.activeSelf){
			transform.localPosition = e.position;
			return;
		}
		WorldUnitManager.GetInstance().QueueAnimation(() => MoveAnimation(e.position));
	}
	private void OnAttackAnimation(object sender, UnitAnimateEventArgs e){
		if(!_offset.activeSelf){
			return;
		}
		WorldUnitManager.GetInstance().QueueAnimation(() => AttackAnimation(e.position));
	}
	private void OnLightUpdate(object sender, EventArgs e){
		RefreshVisibility();
	}
}
