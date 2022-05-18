using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClampedValue : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.ISetValue2<int>,
	Tag.IFortifyValue1<int>,
	Tag.IDamageValue1<int>,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2
	{
	private static Queue<ClampedValue> _POOL = new Queue<ClampedValue>();
	private const int _MIN_VALUE = 0;
	private Tag.ID _tagID = Tag.ID.Null;
	private int _value;
	private int _maxValue;
	public void Setup(Tag.ID tagID, int value){
		_tagID = tagID;
		_value = value;
		_maxValue = value;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public override void BuildString(StringBuilder builder){
		builder.Append(_tagID.ToString() + " " + _value + "/" + _maxValue).Append(System.Environment.NewLine);
	}
	public void SetValue1(Game game, Unit self, int value){
		_value = Mathf.Clamp((_value + value), _MIN_VALUE, _maxValue);
		TagUpdateEvent();
	}
	public void SetValue2(Game game, Unit self, int value){
		_maxValue = value;
		SetValue1(game, self, _value);
		TagUpdateEvent();
	}
	public void FortifyValue1(Game game, Unit self, int value){
		_value = Mathf.Clamp((_value + value), _MIN_VALUE, _maxValue);
		TagUpdateEvent();
	}
	public void DamageValue1(Game game, Unit self, int value){
		_value = Mathf.Clamp((_value - value), _MIN_VALUE, _maxValue);
		TagUpdateEvent();
	}
	public int GetIntValue1(Game game, Unit self){
		return _value;
	}
	public int GetIntValue2(Game game, Unit self){
		return _maxValue;
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.ISetValue2<int> GetISetValue2Int(){
		return this;
	}
	public override Tag.IFortifyValue1<int> GetIFortifyValue1Int(){
		return this;
	}
	public override Tag.IDamageValue1<int> GetIDamageValue1Int(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public static Tag Create(Tag.ID tagID, int value){
		ClampedValue tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new ClampedValue();
		}
		tag.Setup(tagID, value);
		return tag;
	}
}
