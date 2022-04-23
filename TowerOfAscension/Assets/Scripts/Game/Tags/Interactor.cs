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
		direction.GetTile(game, self.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, self)).GetInteractable().Interact(game, self);
		self.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, self);
	}
	public override Tag.IInput<Direction> GetIInputDirection(){
		return this;
	} 
	public static Tag Create(){
		return new Interactor();
	}
}
