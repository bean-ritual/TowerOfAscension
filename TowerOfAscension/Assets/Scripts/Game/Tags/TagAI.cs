using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagAI : 
	Tag,
	Tag.IProcess
	{
	private static Tag.ID _TAG_ID = Tag.ID.AI;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Process(Game game, Unit self){
		
	}
	public override Tag.IProcess GetIProcess(){
		return this;
	}
	public static Tag Create(){
		return new TagAI();
	}
}
