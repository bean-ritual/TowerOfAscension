using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Value : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.IGetIntValue1,
	Tag.IReduce<int>
	{
	private static Queue<Value> _POOL = new Queue<Value>();
	private Tag.ID _tagID = Tag.ID.Null;
	private int _value;
	public void Setup(Tag.ID tagID, int value){
		_tagID = tagID;
		_value = value;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public void SetValue1(Game game, Unit self, int value){
		_value = value;
	}
	public int GetIntValue1(Game game, Unit self){
		return _value;
	}
	public int Reduce(Game game, Unit self, int value){
		return value - _value;
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IReduce<int> GetIReduceInt(){
		return this;
	}
	public static Tag Create(Tag.ID tagID, int value){
		Value tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new Value();
		}
		tag.Setup(tagID, value);
		return tag;
	}
}
