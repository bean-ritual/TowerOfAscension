using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equippable : 
	Tag,
	Tag.IAdd<Unit>,
	Tag.IRemove<Unit>
	{
	private const Tag.ID _TAG_ID = Tag.ID.Equippable;
	private Tag.ID _slot = Tag.ID.Null;
	public void Setup(Tag.ID slot){
		_slot = slot;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public void Add(Game game, Unit self, Unit holder){
		holder.GetTag(game, _slot).GetIAddUnit().Add(game, holder, self);
	}
	public void Remove(Game game, Unit self, Unit holder){
		holder.GetTag(game, _slot).GetIRemoveUnit().Remove(game, holder, self);
	}
	public override void Disassemble(){
		
	}
	public override Tag.IAdd<Unit> GetIAddUnit(){
		return this;
	}
	public override Tag.IRemove<Unit> GetIRemoveUnit(){
		return this;
	}
	public static Tag Create(Tag.ID slot){
		Equippable tag = new Equippable();
		tag.Setup(slot);
		return tag;
	}
}
