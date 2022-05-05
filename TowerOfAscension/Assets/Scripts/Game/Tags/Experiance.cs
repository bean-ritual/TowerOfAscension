using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Experiance : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.ISetValue1<float>,
	Tag.ISetValue2<int>,
	Tag.IFortifyValue1<int>,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2,
	Tag.IClear
	{
	private static Tag.ID _TAG_ID = Tag.ID.Experiance;
	private int _value;
	private int _baseValue;
	private float _factor;
	public void Setup(int baseValue, float factor){
		_baseValue = baseValue;
		_factor = factor;
		_value = 0;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void SetValue1(Game game, Unit self, int value){
		_value = value;
		if(_value > GetIntValue2(game, self)){
			self.GetTag(game, Tag.ID.Level).GetIFortifyValue1().FortifyValue1(game, self);
			_value = 0;
		}
	}
	public void SetValue1(Game game, Unit self, float value){
		_factor = value;
	}
	public void SetValue2(Game game, Unit self, int value){
		_baseValue = value;
	}
	public void FortifyValue1(Game game, Unit self, int value){
		const string EXPERIANCE_GAIN = "You gain {0} EXP!";
		self.GetTag(game, Tag.ID.PlayerLog).GetIInputString().Input(game, self, String.Format(EXPERIANCE_GAIN, value));
		_value = (_value + value);
		if(_value > GetIntValue2(game, self)){
			self.GetTag(game, Tag.ID.Level).GetIFortifyValue1().FortifyValue1(game, self);
			_value = 0;
		}
	}
	public int GetIntValue1(Game game, Unit self){
		return _value;
	}
	public int GetIntValue2(Game game, Unit self){
		return (int)(_baseValue + Mathf.Pow(self.GetTag(game, Tag.ID.Level).GetIGetIntValue1().GetIntValue1(game, self), _factor));
	}
	public void Clear(Game game, Unit self){
		_value = 0;
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.ISetValue1<float> GetISetValue1Float(){
		return this;
	}
	public override Tag.ISetValue2<int> GetISetValue2Int(){
		return this;
	}
	public override Tag.IFortifyValue1<int> GetIFortifyValue1Int(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public override Tag.IClear GetIClear(){
		return this;
	}
	public static Tag Create(int baseValue, float factor){
		Experiance tag = new Experiance();
		tag.Setup(baseValue, factor);
		return tag;
	}
}
