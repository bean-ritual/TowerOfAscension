using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stat : 
	Tag,
	Tag.IFortifyValue1<int>,
	Tag.IDamageValue1<int>,
	Tag.IReduce<int>,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2
	{
	private static Queue<Stat> _POOL = new Queue<Stat>();
	private const int _MATH_VALUE = 100;
	private const int _MAX_VALUE = 80;
	private const int _MIN_VALUE = -80;
	private Tag.ID _tagID = Tag.ID.Null;
	private int _modifyValue;
	private int _baseValue;
	public void Setup(Tag.ID tagID, int baseValue = 0){
		_tagID = tagID;
		_modifyValue = 0;
		_baseValue = 0;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public void FortifyValue1(Game game, Unit self, int value){
		_modifyValue = (_modifyValue + value);
		TagUpdateEvent();
	}
	public void DamageValue1(Game game, Unit self, int value){
		_modifyValue = (_modifyValue - value);
		TagUpdateEvent();
	}
	public int Reduce(Game game, Unit self, int value){
		return (int)((float)GetIntValue1() / _MATH_VALUE) * value;
	}
	public int GetIntValue1(Game game, Unit self){
		return GetIntValue1();
	}
	public int GetIntValue2(Game game, Unit self){
		return _MAX_VALUE;
	}
	public int GetIntValue1(){
		return Mathf.Clamp((_baseValue + _modifyValue), _MIN_VALUE, _MAX_VALUE);
	}
	public override Tag.IFortifyValue1<int> GetIFortifyValue1Int(){
		return this;
	}
	public override Tag.IDamageValue1<int> GetIDamageValue1Int(){
		return this;
	}
	public override Tag.IReduce<int> GetIReduceInt(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public static Tag Create(Tag.ID tagID, int baseValue = 0){
		Stat tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new Stat();
		}
		tag.Setup(tagID, baseValue);
		return tag;
	}
}
