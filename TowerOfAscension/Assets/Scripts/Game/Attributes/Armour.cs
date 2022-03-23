using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Armour : 
	Attribute,
	Attribute.IReducer
	{
	public interface IHasArmour{
		Attribute GetArmour();
	}
	private const int _MATH_VALUE = 100;
	private const int _MAX_VALUE = 80;
	private const int _MIN_VALUE = -80;
	private int _modifyValue;
	private int _baseValue;
	public Armour(int baseValue = 0){
		_modifyValue = 0;
		_baseValue = 0;
	}
	public override void Fortify(Level level, Unit self, int value){
		_modifyValue = (_modifyValue + value);
		AttributeUpdateEvent();
	}
	public override void Damage(Level level, Unit self, int value){
		_modifyValue = (_modifyValue - value);
		AttributeUpdateEvent();
	}
	public override int GetValue(){
		return Mathf.Clamp((_baseValue + _modifyValue), _MIN_VALUE, _MAX_VALUE);
	}
	public override int GetMaxValue(){
		return _MAX_VALUE;
	}
	public int Reduce(Level level, Unit self, int value){
		return (int)((float)GetValue() / _MATH_VALUE) * value;
	}
	public override Attribute.IReducer GetReducer(){
		return this;
	}
}
