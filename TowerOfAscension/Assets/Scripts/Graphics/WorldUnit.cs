using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnit : MonoBehaviour{
	public interface IWorldUnit{
		event EventHandler<EventArgs> OnWorldUnitUpdate;
		Sprite GetSprite();
		int GetSortingOrder();
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
		_level = DungeonMaster.GetInstance().GetLevel();
		_unit.GetWorldUnit().OnWorldUnitUpdate += OnWorldUnitUpdate;
		_unit.GetMoveable().OnMoveEvent += OnMoveEvent;
		Refresh();
	}
	public void Refresh(){
		_renderer.sprite = _unit.GetWorldUnit().GetSprite();
		_renderer.sortingOrder = _unit.GetWorldUnit().GetSortingOrder();
		_offset.transform.localPosition = _level.GetVector3CellOffset();
		this.transform.localPosition = _unit.GetPositionable().GetPosition(_level);
	}
	public IEnumerator MoveAnimation(int x, int y){
		float time = 0f;
		float duration = 0.1f;
		Vector3 start = transform.position;
		Vector3 target = _level.GetWorldPosition(x, y);
		while(time < duration){
			transform.position = Vector3.Lerp(start, target, time / duration);
			time += Time.deltaTime;
			WorldUnitManager.GetInstance().OnFrameAnimation();
			yield return null;
		}
		transform.position = target;
		WorldUnitManager.GetInstance().OnFrameAnimation();
	}
	private void UnsubscribeFromEvents(){
		_unit.GetWorldUnit().OnWorldUnitUpdate -= OnWorldUnitUpdate;
		_unit.GetMoveable().OnMoveEvent -= OnMoveEvent;
	}
	private void OnWorldUnitUpdate(object sender, EventArgs e){
		Refresh();
	}
	private void OnMoveEvent(object sender, EventArgs e){
		_unit.GetPositionable().GetPosition(out int x, out int y);
		StartCoroutine(MoveAnimation(x, y));
	}
}
