using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hostility : 
	Tag,
	Tag.ICondition
	{
	private static Tag.ID _TAG_ID = Tag.ID.Hostility;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public bool Check(Game game, Unit self){
		return true;
	}
	public override Tag.ICondition GetICondition(){
		return this;
	} 
	public static Tag Create(){
		return new Hostility();
	}
}
