using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class RangeValue : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.ISetValue2<int>,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2,
	Tag.IGetIntValue3
	{
	private static Queue<RangeValue> _POOL = new Queue<RangeValue>();
	private Tag.ID _tagID = Tag.ID.Null;
	private int _lowValue;
	private int _highValue;
	public void Setup(Tag.ID tagID, int value1, int value2){
		_tagID = tagID;
		_lowValue = value1;
		_highValue = value2;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public override void BuildString(StringBuilder builder){
		builder.Append(_tagID.ToString() + " " + _lowValue + " - " + _highValue).Append(System.Environment.NewLine);
	}
	public void SetValue1(Game game, Unit self, int value){
		_lowValue = value;
	}
	public void SetValue2(Game game, Unit self, int value){
		_highValue = value;
	}
	public int GetIntValue1(Game game, Unit self){
		return UnityEngine.Random.Range(_lowValue, (_highValue + 1));
	}
	public int GetIntValue2(Game game, Unit self){
		return _lowValue;
	}
	public int GetIntValue3(Game game, Unit self){
		return _highValue;
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.ISetValue2<int> GetISetValue2Int(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public override Tag.IGetIntValue3 GetIGetIntValue3(){
		return this;
	}
	public static Tag Create(Tag.ID tagID, int value1, int value2){
		RangeValue tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new RangeValue();
		}
		tag.Setup(tagID, value1, value2);
		return tag;
	}
}
