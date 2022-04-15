using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Pickup : 
	Tag,
	Tag.IInput<Unit>
	{
    private const Tag.ID _TAG_ID = Tag.ID.Pickup;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){}
	public void Input(Game game, Unit self, Unit holder){
		holder.GetTaggable().GetTag(game, Tag.ID.Inventory).GetIAddUnit().Add(game, holder, self);
		holder.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, holder);
	}
	public override Tag.IInput<Unit> GetIInputUnit(){
		return this;
	}
	public static Tag Create(){
		return new Pickup();
	}
}
