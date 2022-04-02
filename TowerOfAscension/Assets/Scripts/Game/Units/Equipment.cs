using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equipment : 
	Inventory,
	Inventory.IPickupable,
	Inventory.IDroppable
	{
	private Register<Unit>.ID _weapon;
	public void TryPickup(Game game, Unit holder, Unit item){
		item.GetPickupable().DoPickup(game, holder, this);
	}
	public void TryDrop(Game game, Unit holder, Unit item){
		if(Contains(item)){
			item.GetDroppable().DoDrop(game, holder, this);
		}
	}
	public void TryEquipWeapon(Game game, Unit self, Register<Unit>.ID id){
		Get(id).GetWeaponable().TryEquipWeapon(game, self);
	}
	public void DoEquipWeapon(Game game, Unit self, Register<Unit>.ID id){
		Get(_weapon).GetWeaponable().TryUnequipWeapon(game, self);
		_weapon = id;
	}
	public void DoUnequipWeapon(Game game, Unit self, Register<Unit>.ID id){
		_weapon = Register<Unit>.ID.GetNullID();
	}
	public override IPickupable GetPickupable(){
		return this;
	}
	public override IDroppable GetDroppable(){
		return this;
	}
}
