using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Condition : 
	Tag,
	Tag.ISetValue1<bool>,
	Tag.ICondition
	{
	private Tag.ID _tagID;
	private bool _value;
	public void Setup(Tag.ID tagID, bool value){
		_tagID = tagID;
		_value = value;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public override void Disassemble(){
		//
	}
	public void SetValue1(Game game, Unit self, bool value){
		_value = value;
	}
	public bool Check(Game gamea, Unit self){
		return _value;
	}
	public override Tag.ISetValue1<bool> GetISetValue1Bool(){
		return this;
	}
	public override Tag.ICondition GetICondition(){
		return this;
	}
	public static Tag Create(Tag.ID tagID, bool value){
		Condition tag = new Condition();
		tag.Setup(tagID, value);
		return tag;
	}
}
