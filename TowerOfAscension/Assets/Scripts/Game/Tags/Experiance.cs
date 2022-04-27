using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Experiance : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.IGetIntValue1,
	Tag.IInput<Unit>
	{
	private static Tag.ID _TAG_ID = Tag.ID.Experiance;
	private int _value;
	public void Setup(int value){
		_value = value;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void SetValue1(Game game, Unit self, int value){
		_value = value;
	}
	public int GetIntValue1(Game game, Unit self){
		return _value;
	}
	public void Input(Game game, Unit self, Unit value){
		UnityEngine.Debug.Log("EXP GAINED TEST " + _value);
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IInput<Unit> GetIInputUnit(){
		return this;
	}
	public static Tag Create(int value){
		Experiance tag = new Experiance();
		tag.Setup(value);
		return tag;
	}
}
