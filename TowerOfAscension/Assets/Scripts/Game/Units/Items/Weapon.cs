using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Weapon : 
	Item,
	Unit.IUseable,
	Unit.IEquippable
	{
	private bool _equipped = false;
	public Weapon(){
		_controller = new VisualController();
		_controller.SetSprite(SpriteSheet.SpriteID.Swords, UnityEngine.Random.Range(0, 5));
		_controller.SetSortingOrder(20);
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
		UnityEngine.Debug.Log("test :: doequip");
		id = _id;
		_equipped = true;
		_controller.SetItemBorder(_equipped);
	}
	public void TryUnequip(Game game, Unit unit){
		unit.GetHasInventory().GetInventory(game).GetWeaponEquippable().UnequipWeapon(game, unit, _id);
	}
	public void DoUnequip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){
		UnityEngine.Debug.Log("test :: dounequip");
		id = Register<Unit>.ID.GetNullID();
		_equipped = false;
		_controller.SetItemBorder(_equipped);
	}
	public override IUseable GetUseable(){
		return this;
	}
	public override IEquippable GetEquippable(){
		return this;
	}
}
