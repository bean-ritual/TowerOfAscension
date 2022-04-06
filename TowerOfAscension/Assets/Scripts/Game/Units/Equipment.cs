using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equipment : 
	Inventory,
	Inventory.IPickupable,
	Inventory.IDroppable,
	Inventory.IWeaponEquippable,
	Inventory.IChestplateEquippable,
	Inventory.IBootsEquippable,
	EquipSlots.IHasEquipSlots
	{
	private Attribute _equipSlots = Attribute.GetNullAttribute();
	private Register<Unit>.ID[] _equips = {
		Register<Unit>.ID.GetNullID(),
		Register<Unit>.ID.GetNullID(),
		Register<Unit>.ID.GetNullID(),
		Register<Unit>.ID.GetNullID(),
		Register<Unit>.ID.GetNullID()
	};
	public Equipment(){
		_equipSlots = new EquipSlots();
	}
	public void TryPickup(Game game, Unit holder, Unit item){
		item.GetPickupable().DoPickup(game, holder, this);
	}
	public void TryDrop(Game game, Unit holder, Register<Unit>.ID id){
		Get(id).GetDroppable().DoDrop(game, holder, this);
	}
	public void EquipWeapon(Game game, Unit self, Register<Unit>.ID id){
		Equip(game, self, id, 0);
	}
	public void UnequipWeapon(Game game, Unit self, Register<Unit>.ID id){
		Unequip(game, self, id, 0);
	}
	public void EquipChestplate(Game game, Unit self, Register<Unit>.ID id){
		if(_equipSlots.GetValue() >= _equipSlots.GetMaxValue()){
			return;
		}
		Equip(game, self, id, 1);
	}
	public void UnequipChestplate(Game game, Unit self, Register<Unit>.ID id){
		Unequip(game, self, id, 1);
	}
	public void EquipBoots(Game game, Unit self, Register<Unit>.ID id){
		if(_equipSlots.GetValue() >= _equipSlots.GetMaxValue()){
			return;
		}
		Equip(game, self, id, 2);
	}
	public void UnequipBoots(Game game, Unit self, Register<Unit>.ID id){
		Unequip(game, self, id, 2);
	}
	private void Equip(Game game, Unit self, Register<Unit>.ID id, int index){
		Get(_equips[index]).GetEquippable().TryUnequip(game, self);
		if(_equips[index].IsNull()){
			Get(id).GetEquippable().DoEquip(game, self, this, ref _equips[index]);
		}
	}
	private void Unequip(Game game, Unit self, Register<Unit>.ID id, int index){
		if(_equips[index].Equals(id)){
			Get(id).GetEquippable().DoUnequip(game, self, this, ref _equips[index]);
		}
	}
	public Unit GetWeapon(Game game, Unit self){
		return Get(_equips[0]);
	}
	public Unit GetChestplate(Game game, Unit self){
		return Get(_equips[1]);
	}
	public Unit GetBoots(Game game, Unit self){
		return Get(_equips[2]);
	}
	public Attribute GetEquipSlots(Game game){
		return _equipSlots;
	}
	public override IPickupable GetPickupable(){
		return this;
	}
	public override IDroppable GetDroppable(){
		return this;
	}
	public override IWeaponEquippable GetWeaponEquippable(){
		return this;
	}
	public override IChestplateEquippable GetChestplateEquippable(){
		return this;
	}
	public override IBootsEquippable GetBootsEquippable(){
		return this;
	}
	public override EquipSlots.IHasEquipSlots GetHasEquipSlots(){
		return this;
	}
}
