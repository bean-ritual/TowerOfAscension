using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LanternSlot : 
	Tag,
	Tag.IAdd<Unit>,
	Tag.IRemove<Unit>,
	Tag.IGetUnit,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2,
	Tag.IEndTurn
	{
	private const Tag.ID _TAG_ID = Tag.ID.Light;
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
	public int GetIntValue1(Game game, Unit self){
		return _unit.GetTag(game, Tag.ID.Light).GetIGetIntValue1().GetIntValue1(game, _unit);
	}
	public int GetIntValue2(Game game, Unit self){
		return _unit.GetTag(game, Tag.ID.Light).GetIGetIntValue2().GetIntValue2(game, _unit);
	}
	public void OnEndTurn(Game game, Unit self){
		const int TORCH = 1;
		_unit.GetTag(game, Tag.ID.Fuel).GetIDamageValue1Int().DamageValue1(game, _unit, TORCH);
		game.GetLevel().LightUpdate(game, self);
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
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public override Tag.IEndTurn GetIEndTurn(){
		return this;
	}
	public static Tag Create(){
		return new LanternSlot();
	}
}
