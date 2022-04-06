using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equip : 
	Item,
	Unit.IUseable,
	Unit.IEquippable,
	Unit.IItemToolTip
	{
	public enum EquipSlot{
		Null,
		Chestplate,
		Boots,
	};
	private EquipSlot _slot;
	private int _armour;
	private bool _equipped;
	public Equip(){
		_controller = new VisualController();
		_controller.SetSprite(SpriteSheet.SpriteID.Chestplates, UnityEngine.Random.Range(0, 5));
		_controller.SetSortingOrder(20);
		_armour = UnityEngine.Random.Range(5, 50);
	}
	public void TryUse(Game game, Unit unit){
		if(_equipped){
			TryUnequip(game, unit);
		}else{
			TryEquip(game, unit);
		}
	}
	public void TryEquip(Game game, Unit unit){
		unit.GetHasInventory().GetInventory(game).GetWeaponEquippable().EquipWeapon(game, unit, _id);
	}
	public void DoEquip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){
		id = _id;
		_equipped = true;
		_controller.SetItemBorder(_equipped);
	}
	public void TryUnequip(Game game, Unit unit){
		unit.GetHasInventory().GetInventory(game).GetWeaponEquippable().UnequipWeapon(game, unit, _id);
	}
	public void DoUnequip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){
		id = Register<Unit>.ID.GetNullID();
		_equipped = false;
		_controller.SetItemBorder(_equipped);
	}
	public string GetToolTip(Game game){
		return "Chestplate\n\n" + _armour + " Armour";
	}
	public override IUseable GetUseable(){
		return this;
	}
	public override IEquippable GetEquippable(){
		return this;
	}
	public override IItemToolTip GetItemToolTip(){
		return this;
	}
}
