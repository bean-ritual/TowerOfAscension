using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equipment : 
	Inventory,
	Inventory.ISortable,
	Inventory.IPickupable,
	Inventory.IDroppable,
	Inventory.IWeaponEquippable,
	Inventory.IChestplateEquippable,
	Inventory.IBootsEquippable,
	EquipSlots.IHasEquipSlots
	{
	[field:NonSerialized]public event EventHandler<EventArgs> OnInventorySort;
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
	public void Sort(Game game){
		Inventory.Default_Sort(this, game, _equips);
	}
	public void TryPickup(Game game, Unit holder, Unit item){
		Inventory.Default_TryPickup(this, game, holder, item);
	}
	public void TryDrop(Game game, Unit holder, Register<Unit>.ID id){
		Inventory.Default_TryDrop(this, game, holder, id);
	}
	public void EquipWeapon(Game game, Unit self, Register<Unit>.ID id){
		Inventory.Default_Equip(this, game, self, id, ref _equips[0], Attribute.GetNullAttribute());
	}
	public void UnequipWeapon(Game game, Unit self, Register<Unit>.ID id){
		Inventory.Default_Unequip(this, game, self, id, ref _equips[0]);
	}
	public void EquipChestplate(Game game, Unit self, Register<Unit>.ID id){
		Inventory.Default_Equip(this, game, self, id, ref _equips[1], _equipSlots);
	}
	public void UnequipChestplate(Game game, Unit self, Register<Unit>.ID id){
		Inventory.Default_Unequip(this, game, self, id, ref _equips[1]);
	}
	public void EquipBoots(Game game, Unit self, Register<Unit>.ID id){
		Inventory.Default_Equip(this, game, self, id, ref _equips[2], _equipSlots);
	}
	public void UnequipBoots(Game game, Unit self, Register<Unit>.ID id){
		Inventory.Default_Unequip(this, game, self, id, ref _equips[2]);
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
	public override ISortable GetSortable(){
		return this;
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
