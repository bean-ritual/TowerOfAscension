using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour{
	private Attribute _health = Attribute.GetNullAttribute();
	[SerializeField]private Slider _slider;
	[SerializeField]private GameObject _control;
	private void Awake(){
		SetHealth(_health);
	}
	private void OnDestroy(){
		UnsubscribeFromEvents();
	}
	public void SetHealth(Attribute health){
		UnsubscribeFromEvents();
		_health = health;
		_health.OnAttributeUpdate += OnAttributeUpdate;
		Refresh();
	}
	public void Refresh(){
		int maxValue = _health.GetMaxValue();
		_control.SetActive(maxValue > 0);
		_slider.maxValue = maxValue;
		_slider.value = _health.GetValue();
	}
	public void UnsubscribeFromEvents(){
		_health.OnAttributeUpdate -= OnAttributeUpdate;
	}
	public void OnAttributeUpdate(object sender, EventArgs e){
		Refresh();
	}
}
