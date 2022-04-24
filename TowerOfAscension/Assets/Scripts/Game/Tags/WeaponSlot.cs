using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WeaponSlot : 
	Tag,
	Tag.IAdd<Unit>,
	Tag.IRemove<Unit>,
	Tag.IInput<Direction>,
	Tag.IGetUnit
	{
	private const Tag.ID _TAG_ID = Tag.ID.Attack_Slot;
	private Unit _unit = Unit.GetNullUnit();
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
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
		TagUpdateEvent();
	}
	public void Input(Game game, Unit self, Direction direction){
		Unit attack = _unit;
		if(_unit.IsNull()){
			attack = self;
		}
		attack.GetTag(game, Tag.ID.Active).GetIInputUnitDirection().Input(game, attack, self, direction);
		self.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, self);
	}
	public Unit GetUnit(Game game, Unit self){
		return _unit;
	}
	public override Tag.IAdd<Unit> GetIAddUnit(){
		return this;
	}
	public override Tag.IRemove<Unit> GetIRemoveUnit(){
		return this;
	}
	public override Tag.IInput<Direction> GetIInputDirection(){
		return this;
	}
	public override Tag.IGetUnit GetIGetUnit(){
		return this;
	}
	public static Tag Create(){
		return new WeaponSlot();
	}
}
