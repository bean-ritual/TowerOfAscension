using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Attackable : 
	Tag,
	Tag.IInput<Unit, Unit>
	{
	private const Tag.ID _TAG_ID = Tag.ID.Attackable;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Unit attacker, Unit damage){
		int physical = damage.GetTaggable().GetTag(game, Tag.ID.Damage_Physical).GetIGetIntValue1().GetIntValue1(game, damage);
		self.GetTaggable().GetTag(game, Tag.ID.Health).GetIDamageValue1Int().DamageValue1(game, self, physical);
	}
	public override Tag.IInput<Unit, Unit> GetIInput2Units(){
		return this;
	}
	public static Tag Create(){
		return new Attackable();
	}
}
