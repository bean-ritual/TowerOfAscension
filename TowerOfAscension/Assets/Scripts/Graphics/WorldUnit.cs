using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnit : MonoBehaviour{
	public interface IWorldUnit{
		event EventHandler<EventArgs> OnWorldUnitUpdate;
		Sprite GetSprite();
		int GetSortingOrder();
		bool GetWorldVisibility(Level level);
	}
	private Unit _unit = Unit.GetNullUnit();
	private Level _level = Level.GetNullLevel();
	[SerializeField]private GameObject _offset;
	[SerializeField]private SpriteRenderer _renderer;
	private void OnDisable(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		UnsubscribeFromEvents();
		_unit = unit;
		_unit.GetWorldUnit().OnWorldUnitUpdate += OnWorldUnitUpdate;
		_unit.GetMoveable().OnMoveEvent += OnMoveEvent;
		_level = DungeonMaster.GetInstance().GetLevel();
		_level.OnLightUpdate += OnLightUpdate;
		RefreshAll();
	}
	public void RefreshAll(){
		_renderer.sprite = _unit.GetWorldUnit().GetSprite();
		_renderer.sortingOrder = _unit.GetWorldUnit().GetSortingOrder();
		_offset.transform.localPosition = _level.GetVector3CellOffset();
		this.transform.localPosition = _unit.GetPositionable().GetPosition(_level);
		RefreshVisibility();
	}
	public void RefreshVisibility(){
		_offset.SetActive(_unit.GetWorldUnit().GetWorldVisibility(_level));
	}
	public IEnumerator LerpToXY(int x, int y, Action OnAnimationComplete){
		float time = 0f;
		float duration = 0.1f;
		Vector3 start = transform.localPosition;
		Vector3 target = _level.GetWorldPosition(x, y);
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
	public void MoveAnimation(int x, int y){
		StartCoroutine(LerpToXY(x, y, null));
	}
	public void AttackAnimation(int x, int y){
		_unit.GetPositionable().GetPosition(out int oldX, out int oldY);
		StartCoroutine(LerpToXY(x, y, () => {
			StartCoroutine(LerpToXY(oldX, oldY, null));
		}));
	}
	public void UnitDestroy(){
		Destroy(gameObject);
	}
	private void UnsubscribeFromEvents(){
		_unit.GetWorldUnit().OnWorldUnitUpdate -= OnWorldUnitUpdate;
		_unit.GetMoveable().OnMoveEvent -= OnMoveEvent;
		_level.OnLightUpdate -= OnLightUpdate;
	}
	private void OnWorldUnitUpdate(object sender, EventArgs e){
		RefreshAll();
	}
	private void OnMoveEvent(object sender, EventArgs e){
		_unit.GetPositionable().GetPosition(out int x, out int y);
		if(!_offset.activeSelf){
			transform.localPosition = _level.GetWorldPosition(x, y);
			return;
		}
		WorldUnitManager.GetInstance().QueueAnimation(() => MoveAnimation(x, y));
	}
	private void OnAttackEvent(object sender, EventArgs e){
		//
	}
	private void OnLightUpdate(object sender, EventArgs e){
		RefreshVisibility();
	}
}
