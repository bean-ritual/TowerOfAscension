using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Health :
	Attribute,
	Attribute.IModifiableMaxes
	{
	public interface IHasHealth{
		Attribute GetHealth();
	}
	private const int _MAX_MAX_VALUE = 999;
	private const int _MIN_MAX_VALUE = 10;
	private const int _MIN_VALUE = 0;
	private int _value;
	private int _maxValue;
	public Health(int baseValue){
		_value = baseValue;
		_maxValue = baseValue;
	}
	public override void Fortify(Game game, Unit self, int value){
		_value = (_value + value);
		_value = Mathf.Clamp(_value, _MIN_VALUE, GetMaxValue());
		AttributeUpdateEvent();
	}
	public override void Damage(Game game, Unit self, int value){
		_value = (_value - value);
		if(_value <= 0){
			self.GetKillable().Kill(game);
		}
		AttributeUpdateEvent();
	}
	public override int GetValue(){
		return _value;
	}
	public override int GetMaxValue(){
		return _maxValue;
	}
	public void FortifyMax(Game game, Unit self, int value){
		_maxValue = (_maxValue + value);
		_maxValue = Mathf.Clamp(_maxValue, _MIN_MAX_VALUE, _MAX_MAX_VALUE);
		AttributeUpdateEvent();
	}
	public void DamageMax(Game game, Unit self, int value){
		_maxValue = (_maxValue - value);
		_maxValue = Mathf.Clamp(_maxValue, _MIN_MAX_VALUE, _MAX_MAX_VALUE);
		if(_value > _maxValue){
			_value = _maxValue;
		}
		AttributeUpdateEvent();
	}
	public override IModifiableMaxes GetModifiableMaxes(){
		return this;
	}
}
