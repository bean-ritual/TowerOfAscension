using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BasicAttack : 
	Tag,
	Tag.IInput<Direction>
	{
	private const Tag.ID _TAG_ID = Tag.ID.Basic_Attack;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){}
	public void Input(Game game, Unit self, Direction direction){
		/*
		Tag weapon = self.GetTag(game, Tag.ID.Weapon).GetIGetUnit().GetUnit(game, self).GetTag(game, Tag.ID.Active);
		if(!weapon.IsNull()){
			
		}else{
			Attack.Create(game, direction, self, self);
		}
		attacker.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, attacker);
		Attack.Create(game, direction, attacker, self);
		//self.GetVisualController().GetVisualController(game).InvokeAttackAnimation(direction);
		attacker.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, attacker);
		*/
	}
	public override Tag.IInput<Direction> GetIInputDirection(){
		return this;
	}
	public static Tag Create(){
		return new BasicAttack();
	}
}
