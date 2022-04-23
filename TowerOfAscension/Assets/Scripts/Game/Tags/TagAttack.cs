using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagAttack : 
	Tag,
	Tag.IInput<Unit, Direction>
	{
	private const Tag.ID _TAG_ID = Tag.ID.Active;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){}
	public void Input(Game game, Unit self, Unit holder, Direction direction){
		Attack.Create(game, direction, holder, self);
		holder.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, holder);
	}
	public override Tag.IInput<Unit, Direction> GetIInputUnitDirection(){
		return this;
	}
	public static Tag Create(){
		return new TagAttack();
	}
}
