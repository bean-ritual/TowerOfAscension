using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equipment : 
	Inventory,
	Inventory.IPickupable,
	Inventory.IDroppable,
	Inventory.IWeaponEquippable
	{
	private Register<Unit>.ID _weapon = Register<Unit>.ID.GetNullID();
	public Equipment(){}
	public void TryPickup(Game game, Unit holder, Unit item){
		item.GetPickupable().DoPickup(game, holder, this);
	}
	public void TryDrop(Game game, Unit holder, Register<Unit>.ID id){
		Get(id).GetDroppable().DoDrop(game, holder, this);
	}
	public void EquipWeapon(Game game, Unit self, Register<Unit>.ID id){
		Get(_weapon).GetEquippable().TryUnequip(game, self);
		if(_weapon.IsNull()){
			UnityEngine.Debug.Log("test :: null");
			Get(id).GetEquippable().DoEquip(game, self, this, ref id);
		}
	}
	public void UnequipWeapon(Game game, Unit self, Register<Unit>.ID id){
		UnityEngine.Debug.Log("test");
		if(_weapon.Equals(id)){
			Get(id).GetEquippable().DoUnequip(game, self, this, ref id);
		}
	}
	public Unit GetWeapon(Game game, Unit self){
		return Get(_weapon);
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
}
