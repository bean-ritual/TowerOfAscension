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
		_unit.GetTag(game, Tag.ID.Equippable).GetIRemoveUnit().Remove(game, _unit, self);
		if(!_unit.IsNull()){
			return;
		}
		equip.Despawn(game);
		self.GetTag(game, Tag.ID.Inventory).GetIRemoveUnitID().Remove(game, self, equip.GetID());
		_unit = equip;
		_unit.GetTag(game, Tag.ID.UIUnit).GetISetValue1Bool().SetValue1(game, self, true);
		self.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, self);
		TagUpdateEvent();
	}
	public void Remove(Game game, Unit self, Unit equip){
		if(equip != _unit){
			return;
		}
		_unit.GetTag(game, Tag.ID.UIUnit).GetISetValue1Bool().SetValue1(game, self, false);
		_unit = Unit.GetNullUnit();
		self.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, self).GetXY(out int x, out int y);
		equip.Spawn(game, x, y);
		self.GetTag(game, Tag.ID.Inventory).GetIAddUnit().Add(game, self, equip);
		self.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, self);
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
