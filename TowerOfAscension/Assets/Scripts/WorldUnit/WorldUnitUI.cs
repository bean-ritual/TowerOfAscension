using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldUnitUI : MonoBehaviour{
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	[SerializeField]private Canvas _uiCanvas;
	[SerializeField]private RectTransform _uiOffset;
	[SerializeField]private HealthBarManager _health;
	[SerializeField]private GameObject _healthContent;
	public void Setup(Unit unit){
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_unit = unit;
		Refresh();
	}
	public void Refresh(){
		_uiCanvas.sortingOrder = _unit.GetWorldUnitUI().GetUISortingOrder(_local);
		_uiOffset.localPosition = _unit.GetWorldUnitUI().GetUIOffset(_local);
		_health.SetHealth(_unit.GetHasHealth().GetHealth(_local));
		_healthContent.SetActive(_unit.GetWorldUnitUI().GetHealthBar(_local));
	}

}
