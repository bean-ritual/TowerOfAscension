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
	private Unit _unit;
	private Level _level;
	[SerializeField]private GameObject _offset;
	[SerializeField]private SpriteRenderer _renderer;
	private void OnDisable(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		if(_unit != null){
			UnsubscribeFromEvents();
		}
		_unit = unit;
		_level = DungeonMaster.GetInstance().GetLevel();
		_unit.GetWorldUnit().OnWorldUnitUpdate += OnWorldUnitUpdate;
		Refresh();
	}
	public void Refresh(){
		_renderer.sprite = _unit.GetWorldUnit().GetSprite();
		_renderer.sortingOrder = _unit.GetWorldUnit().GetSortingOrder();
		_offset.transform.localPosition = _level.GetVector3CellOffset();
		this.transform.localPosition = _unit.GetPositionable().GetPosition(_level);
	}
	private void UnsubscribeFromEvents(){
		_unit.GetWorldUnit().OnWorldUnitUpdate -= OnWorldUnitUpdate;
	}
	private void OnWorldUnitUpdate(object sender, EventArgs e){
		Refresh();
	}
}
