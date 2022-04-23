using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldUnitUI : MonoBehaviour{
	private const int _UI_SORTING_ORDER = 100;
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	[SerializeField]private Canvas _uiCanvas;
	[SerializeField]private GameObject _uiContent;
	[SerializeField]private RectTransform _uiRect;
	[SerializeField]private HealthBarManager _health;
	private void OnDestroy(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		UnsubscribeFromEvents();
		_unit = unit;
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_uiContent.SetActive(!_unit.GetTag(_local, Tag.ID.WorldUnitUI).IsNull());
		_unit.GetTag(_local, Tag.ID.WorldUnit).OnTagUpdate += OnWorldUnitTagUpdate;
		_unit.GetTag(_local, Tag.ID.WorldUnitUI).OnTagUpdate += OnWorldUnitUITagUpdate;
		_health.SetHealth(_unit, _unit.GetTag(_local, Tag.ID.Health));
		RefreshAll();
	}
	public void RefreshAll(){
		RefreshSortingOrder();
		RefreshOffsetPosition();
	}
	public void RefreshSortingOrder(){
		_uiCanvas.sortingOrder = _unit.GetTag(_local, Tag.ID.WorldUnit).GetIGetIntValue2().GetIntValue2(_local, _unit) + _UI_SORTING_ORDER;
	}
	public void RefreshOffsetPosition(){
		_uiRect.localPosition = _unit.GetTag(_local, Tag.ID.WorldUnitUI).GetIGetVector().GetVector(_local, _unit);
	}
	public void UnsubscribeFromEvents(){
		_unit.GetTag(_local, Tag.ID.WorldUnit).OnTagUpdate -= OnWorldUnitTagUpdate;
		_unit.GetTag(_local, Tag.ID.WorldUnitUI).OnTagUpdate -= OnWorldUnitUITagUpdate;
	}
	private void OnWorldUnitTagUpdate(object sender, EventArgs e){
		RefreshSortingOrder();
	}
	private void OnWorldUnitUITagUpdate(object sender, EventArgs e){
		RefreshOffsetPosition();
	}

}
