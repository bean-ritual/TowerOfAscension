using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AttackSlot : 
	Tag,
	Tag.IInput<Direction>
	{
	private const Tag.ID _TAG_ID = Tag.ID.Attack_Slot;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Direction direction){
		self.GetTag(game, Tag.ID.Active).GetIInputUnitDirection().Input(game, self, self, direction);
		self.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, self);
	}
	public override Tag.IInput<Direction> GetIInputDirection(){
		return this;
	}
	public static Tag Create(){
		return new AttackSlot();
	}
}
