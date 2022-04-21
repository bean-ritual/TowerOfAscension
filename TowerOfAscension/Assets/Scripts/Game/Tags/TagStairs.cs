using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagStairs : 
	Tag,
	Tag.IInput<Unit>
	{
	private static Tag.ID _TAG_ID = Tag.ID.Tripwire;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Unit trip){
		trip.GetTaggable().GetTag(game, Tag.ID.Exit).GetITrigger().Trigger(game, trip);
	}
	public override Tag.IInput<Unit> GetIInputUnit(){
		return this;
	}
	public static Tag Create(){
		return new TagStairs();
	}
}
