using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Chestplate : 
	Item,
	Unit.IUseable,
	Unit.IEquippable,
	Unit.IItemToolTip
	{
	private int _armour;
	private bool _equipped;
	public Chestplate(){
		_controller = new VisualController();
		_controller.SetSprite(SpriteSheet.SpriteID.Chestplates, UnityEngine.Random.Range(0, 5));
		_controller.SetSortingOrder(20);
		_armour = UnityEngine.Random.Range(5, 50);
		_equipped = false;
	}
	public void TryUse(Game game, Unit unit){
		if(_equipped){
			TryUnequip(game, unit);
		}else{
			TryEquip(game, unit);
		}
	}
	public void TryEquip(Game game, Unit unit){
		Unit.Default_TryEquipChestplate(game, unit, ref _id);
	}
	public void DoEquip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){
		Unit.Default_DoEquip(this, game, unit, ref id, ref _id, ref _equipped);
		unit.GetHasArmour().GetArmour(game).Fortify(game, unit, _armour);
	}
	public void TryUnequip(Game game, Unit unit){
		Unit.Default_TryUnequipChestplate(game, unit, ref _id);
	}
	public void DoUnequip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){
		Unit.Default_DoUnequip(this, game, unit, ref id, ref _equipped);
		unit.GetHasArmour().GetArmour(game).Damage(game, unit, _armour);
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
