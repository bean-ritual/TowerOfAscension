using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBarManager : MonoBehaviour{
	private Attribute _health = Attribute.GetNullAttribute();
	[SerializeField]private Slider _slider;
	[SerializeField]private GameObject _control;
	[SerializeField]private TextMeshProUGUI _text;
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
		int value = _health.GetValue();
		int maxValue = _health.GetMaxValue();
		_control.SetActive(maxValue > 0);
		_slider.maxValue = maxValue;
		_slider.value = value;
		if(_text != null){
			_text.text = "HP: " + value + "/" + maxValue;
		}
	}
	public void UnsubscribeFromEvents(){
		_health.OnAttributeUpdate -= OnAttributeUpdate;
	}
	public void OnAttributeUpdate(object sender, EventArgs e){
		Refresh();
	}
}
