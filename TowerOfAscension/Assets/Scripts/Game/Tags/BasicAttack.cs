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
		Attack.Create(game, direction, self, self);
		self.GetVisualController().GetVisualController(game).InvokeAttackAnimation(direction);
		self.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, self);
	}
	public override Tag.IInput<Direction> GetIInputDirection(){
		return this;
	}
	public static Tag Create(){
		return new BasicAttack();
	}
}
