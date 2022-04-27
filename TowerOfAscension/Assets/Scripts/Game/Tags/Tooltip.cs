using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Tooltip : 
	Tag,
	Tag.IGetStringValue1,
	Tag.ICondition
	{
	private static Tag.ID _TAG_ID = Tag.ID.Tooltip;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public string GetStringValue1(Game game, Unit self){
		StringBuilder builder = new StringBuilder();
		self.GetTag(game, Tag.ID.Name).BuildString(builder);
		self.GetTag(game, Tag.ID.Level).BuildString(builder);
		self.GetTag(game, Tag.ID.Damage_Physical).BuildString(builder);
		return builder.ToString();
	}
	public bool Check(Game game, Unit self){
		return self.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, self).GetLightable().GetLight() > 0;
	}
	public override Tag.IGetStringValue1 GetIGetStringValue1(){
		return this;
	}
	public override Tag.ICondition GetICondition(){
		return this;
	}
	public static Tag Create(){
		return new Tooltip();
	}
}
