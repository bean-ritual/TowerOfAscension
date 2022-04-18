using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EquipSlot : 
	Tag,
	Tag.IAdd<Unit>,
	Tag.IRemove<Unit>,
	Tag.IGetUnit
	{
	private Tag.ID _tagID = Tag.ID.Null;
	private Unit _unit = Unit.GetNullUnit();
	public void Setup(Tag.ID tagID){
		_tagID = tagID;
	}
	public override Tag.ID GetTagID(){
		return _tagID;
	}
	public void Add(Game game, Unit self, Unit equip){
		_unit.GetTaggable().GetTag(game, Tag.ID.Equippable).GetIRemoveUnit().Remove(game, _unit, self);
		if(!_unit.IsNull()){
			return;
		}
		equip.GetSpawnable().Despawn(game);
		self.GetTaggable().GetTag(game, Tag.ID.Inventory).GetIRemoveUnitID().Remove(game, self, equip.GetRegisterable().GetID());
		_unit = equip;
		_unit.GetVisualController().GetVisualController(game).SetItemBorder(true);
		TagUpdateEvent();
	}
	public void Remove(Game game, Unit self, Unit equip){
		if(equip != _unit){
			return;
		}
		_unit.GetVisualController().GetVisualController(game).SetItemBorder(false);
		_unit = Unit.GetNullUnit();
		self.GetPositionable().GetPosition(game, out int x, out int y);
		equip.GetSpawnable().Spawn(game, x, y);
		self.GetTaggable().GetTag(game, Tag.ID.Inventory).GetIAddUnit().Add(game, self, equip);
		TagUpdateEvent();
	}
	public Unit GetUnit(Game game, Unit self){
		return _unit;
	}
	public override void Disassemble(){
		//
	}
	public override Tag.IAdd<Unit> GetIAddUnit(){
		return this;
	}
	public override Tag.IRemove<Unit> GetIRemoveUnit(){
		return this;
	}
	public override Tag.IGetUnit GetIGetUnit(){
		return this;
	}
	public static Tag Create(Tag.ID tagID){
		EquipSlot tag = new EquipSlot();
		tag.Setup(tagID);
		return tag;
	}
}
