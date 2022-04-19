using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Grave : 
	Tag,
	Tag.ITrigger
	{
	private const Tag.ID _TAG_ID = Tag.ID.Loot;
    public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Trigger(Game game, Unit self){
		self.GetPositionable().GetPosition(game, out int x, out int y);
		HeroGrave grave = new HeroGrave();
		grave.Spawn(game, x, y);
	}
	public override Tag.ITrigger GetITrigger(){
		return this;
	}
	public static Tag Create(){
		return new Grave();
	}
}
