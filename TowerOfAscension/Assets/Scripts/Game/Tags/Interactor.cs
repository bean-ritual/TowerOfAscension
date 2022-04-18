using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Interactor : 
	Tag,
	Tag.IInput<Direction>
	{
	private static Tag.ID _TAG_ID = Tag.ID.Interactor;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Direction direction){
		direction.GetTileFromUnit(game, self).GetInteractable().Interact(game, self);
		self.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, self);
	}
	public override Tag.IInput<Direction> GetIInputDirection(){
		return this;
	} 
	public static Tag Create(){
		return new Interactor();
	}
}
