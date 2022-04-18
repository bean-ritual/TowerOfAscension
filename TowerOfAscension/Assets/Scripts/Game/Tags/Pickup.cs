using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Pickup : 
	Tag,
	Tag.IAdd<Unit>,
	Tag.IRemove<Unit>
	{
    private const Tag.ID _TAG_ID = Tag.ID.Pickup;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){}
	public void Add(Game game, Unit self, Unit holder){
		holder.GetTaggable().GetTag(game, Tag.ID.Inventory).GetIAddUnit().Add(game, holder, self);
		holder.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, holder);
	}
	public void Remove(Game game, Unit self, Unit holder){
		holder.GetTaggable().GetTag(game, Tag.ID.Inventory).GetIRemoveUnitID().Remove(game, holder, self.GetRegisterable().GetID());
		holder.GetPositionable().GetPosition(game, out int x, out int y);
		self.GetSpawnable().Spawn(game, x, y);
	}
	public override Tag.IAdd<Unit> GetIAddUnit(){
		return this;
	}
	public override Tag.IRemove<Unit> GetIRemoveUnit(){
		return this;
	}
	public static Tag Create(){
		return new Pickup();
	}
}
