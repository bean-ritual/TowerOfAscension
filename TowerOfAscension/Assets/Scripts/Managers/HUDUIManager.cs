using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDUIManager : MonoBehaviour{
	private static HUDUIManager _INSTANCE;
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	[SerializeField]private HealthBarManager _healthBar;
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
		}
		_INSTANCE = this;
		Refresh();
	}
	private void Start(){
		_local = DungeonMaster.GetInstance().GetLocalGame();
	}
	public void SetUnit(Unit unit){
		_unit = unit;
		Refresh();
	}
	public void Refresh(){
		_healthBar.SetHealth(_unit.GetHasHealth().GetHealth(_local));
	}
	public static HUDUIManager GetInstance(){
		return _INSTANCE;
	}
}
