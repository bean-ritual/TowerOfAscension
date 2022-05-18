using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBarManager : MonoBehaviour{
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	private Tag _health = Tag.GetNullTag();
	[SerializeField]private Slider _slider;
	[SerializeField]private GameObject _control;
	private void Start(){
		_local = DungeonMaster.GetInstance().GetLocalGame();
		SetHealth(_unit, _health);
	}
	private void OnDestroy(){
		UnsubscribeFromEvents();
	}
	public void SetHealth(Unit unit, Tag health){
		UnsubscribeFromEvents();
		_unit = unit;
		_health = health;
		_health.OnTagUpdate += OnTagUpdate;
		Refresh();
	}
	public void Refresh(){
		int value = _health.GetIGetIntValue1().GetIntValue1(_local, _unit);
		int maxValue = _health.GetIGetIntValue2().GetIntValue2(_local, _unit);
		_control.SetActive(maxValue > 0);
		_slider.maxValue = maxValue;
		_slider.value = value;
	}
	public void UnsubscribeFromEvents(){
		_health.OnTagUpdate -= OnTagUpdate;
	}
	public void OnTagUpdate(object sender, EventArgs e){
		Refresh();
	}
}
