using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LightControl : 
	Tag,
	Tag.ISetValue1<bool>,
	Tag.ICondition
	{
	private static Tag.ID _TAG_ID = Tag.ID.Opacity;
	private bool _opaque = true;
	public void Setup(){
		_opaque = true;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void SetValue1(Game game, Unit self, bool value){
		_opaque = value;
	}
	public bool Check(Game game, Unit self){
		return _opaque;
	}
	public override Tag.ISetValue1<bool> GetISetValue1Bool(){
		return this;
	}
	public override Tag.ICondition GetICondition(){
		return this;
	} 
	public static Tag Create(){
		LightControl tag = new LightControl();
		tag.Setup();
		return tag;
	}
}
