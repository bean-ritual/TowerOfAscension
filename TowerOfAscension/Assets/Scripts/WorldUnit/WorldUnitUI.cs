using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldUnitUI : MonoBehaviour{
	private Game _local = Game.GetNullGame();
	private VisualController _controller = VisualController.GetNullVisualController();
	private Unit _unit = Unit.GetNullUnit();
	[SerializeField]private Canvas _uiCanvas;
	[SerializeField]private RectTransform _uiOffset;
	[SerializeField]private HealthBarManager _health;
	[SerializeField]private GameObject _healthContent;
	private void OnDestroy(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		UnsubscribeFromEvents();
		_unit = unit;
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_controller = unit.GetVisualController().GetVisualController(_local);
		_controller.OnUIOffsetUpdate += OnUIOffsetUpdate;
		_controller.OnUISortingOrderUpdate += OnUISortingOrderUpdate;
		_controller.OnHealthBarActiveUpdate += OnHealthBarActiveUpdate;
		_health.SetHealth(_unit.GetHasHealth().GetHealth(_local));
		RefreshAll();
	}
	public void RefreshAll(){
		RefreshSortingOrder();
		RefreshOffsetPosition();
		RefreshHealthBarActive();
	}
	public void RefreshSortingOrder(){
		_uiCanvas.sortingOrder = _controller.GetUISortingOrder();
	}
	public void RefreshOffsetPosition(){
		_uiOffset.localPosition = _controller.GetUIOffset();
	}
	public void RefreshHealthBarActive(){
		_healthContent.SetActive(_controller.GetHealthBarActive());
	}
	public void UnsubscribeFromEvents(){
		_controller.OnUIOffsetUpdate -= OnUIOffsetUpdate;
		_controller.OnUISortingOrderUpdate -= OnUISortingOrderUpdate;
		_controller.OnHealthBarActiveUpdate -= OnHealthBarActiveUpdate;
	}
	private void OnUIOffsetUpdate(object sender, EventArgs e){
		RefreshOffsetPosition();
	}
	private void OnUISortingOrderUpdate(object sender, EventArgs e){
		RefreshSortingOrder();
	}
	private void OnHealthBarActiveUpdate(object sender, EventArgs e){
		RefreshHealthBarActive();
	}

}
