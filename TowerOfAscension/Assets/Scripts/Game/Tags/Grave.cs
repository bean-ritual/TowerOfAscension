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
		self.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, self).GetXY(out int x, out int y);
		UnityEngine.Debug.Log(x + "" + y);
		Unit.UNIT_DATA.GetGrave(game).Spawn(game, x, y);
	}
	public override Tag.ITrigger GetITrigger(){
		return this;
	}
	public static Tag Create(){
		return new Grave();
	}
}
