using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EquipSlots : 
	Attribute,
	Attribute.IConditionable
	{
	public interface IHasEquipSlots{
		Attribute GetEquipSlots(Game game);
	}
	private const int _MIN_VALUE = 0;
	private const int _MAX_VALUE = 4;
	private int _value;
	public EquipSlots(){
		_value = 0;
	}
	public override void Fortify(Game game, Unit self, int value){
		_value = (_value + value);
		AttributeUpdateEvent();
	}
	public override void Damage(Game game, Unit self, int value){
		_value = (_value - value);
		AttributeUpdateEvent();
	}
	public override int GetValue(){
		return _value;
	}
	public override int GetMaxValue(){
		return _MAX_VALUE;
	}
	public bool IsMinned(){
		return _MIN_VALUE >= _value;
	}
	public bool IsMaxed(){
		return _MAX_VALUE <= _value;
	}
	public override IConditionable GetConditionable(){
		return this;
	}
}
