using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Exit : 
	Tag,
	Tag.ITrigger
	{
	private const Tag.ID _TAG_ID = Tag.ID.Exit;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Trigger(Game game, Unit self){
		game.NextLevel();
	}
	public override Tag.ITrigger GetITrigger(){
		return this;
	}
	public static Tag Create(){
		return new Exit();
	}
}
