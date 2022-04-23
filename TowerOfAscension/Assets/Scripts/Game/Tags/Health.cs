using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Health : 
	Tag,
	Tag.IStringValueEvent,
	Tag.IFortifyValue1<int>,
	Tag.IFortifyValue2<int>,
	Tag.IDamageValue1<int>,
	Tag.IDamageValue2<int>,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2
	{
	[field:NonSerialized]public	event EventHandler<ValueEventArgs<string>> OnStringValueEvent;
	private static Queue<Health> _POOL = new Queue<Health>();
	private const Tag.ID _TAG_ID = Tag.ID.Health;
	private const int _MAX_MAX_VALUE = 999;
	private const int _MIN_MAX_VALUE = 10;
	private const int _MIN_VALUE = 0;
	private int _value;
	private int _maxValue;
	public void Setup(int value){
		_value = value;
		_maxValue = value;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public void FortifyValue1(Game game, Unit self, int value){
		OnStringValueEvent?.Invoke(this, new Tag.ValueEventArgs<string>("+" + value));
		_value = (_value + value);
		_value = Mathf.Clamp(_value, _MIN_VALUE, _maxValue);
		TagUpdateEvent();
	}
	public void FortifyValue2(Game game, Unit self, int value){
		_maxValue = (_maxValue + value);
		_maxValue = Mathf.Clamp(_maxValue, _MIN_MAX_VALUE, _MAX_MAX_VALUE);
		TagUpdateEvent();
	}
	public void DamageValue1(Game game, Unit self, int value){
		OnStringValueEvent?.Invoke(this, new Tag.ValueEventArgs<string>("-" + value));
		_value = (_value - value);
		if(_value <= 0){
			self.GetTag(game, Tag.ID.Alive).GetIDamageValue1().DamageValue1(game, self);
		}
		TagUpdateEvent();
	}
	public void DamageValue2(Game game, Unit self, int value){
		_maxValue = (_maxValue - value);
		_maxValue = Mathf.Clamp(_maxValue, _MIN_MAX_VALUE, _MAX_MAX_VALUE);
		if(_value > _maxValue){
			_value = _maxValue;
		}
		TagUpdateEvent();
	}
	public int GetIntValue1(Game game, Unit self){
		return _value;
	}
	public int GetIntValue2(Game game, Unit self){
		return _maxValue;
	}
	public override Tag.IStringValueEvent GetIStringValueEvent(){
		return this;
	}
	public override Tag.IFortifyValue1<int> GetIFortifyValue1Int(){
		return this;
	}
	public override Tag.IFortifyValue2<int> GetIFortifyValue2Int(){
		return this;
	}
	public override Tag.IDamageValue1<int> GetIDamageValue1Int(){
		return this;
	}
	public override Tag.IDamageValue2<int> GetIDamageValue2Int(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public static Tag Create(int value){
		Health tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new Health();
		}
		tag.Setup(value);
		return tag;
	}
}
