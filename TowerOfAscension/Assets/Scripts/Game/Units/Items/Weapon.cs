using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Weapon : 
	Item,
	Unit.IUseable,
	Unit.IAttacker,
	Unit.IEquippable,
	Unit.IItemToolTip
	{
	private int _attack;
	private bool _equipped;
	public Weapon(){
		_controller = new VisualController();
		_controller.SetSprite(SpriteSheet.SpriteID.Swords, UnityEngine.Random.Range(0, 5));
		_controller.SetSortingOrder(20);
		_attack = UnityEngine.Random.Range(5, 50);
		_equipped = false;
	}
	public void TryUse(Game game, Unit unit){
		if(_equipped){
			TryUnequip(game, unit);
		}else{
			TryEquip(game, unit);
		}
	}
	public void TryAttack(Game game, Direction direction){
		
	}
	public void DoAttack(Game game, Unit skills, Unit target){
		//target.GetHasHealth().GetHealth(game).Damage(game, target, _attack);
	}
	public void TryEquip(Game game, Unit unit){
		Unit.Default_TryEquipWeapon(game, unit, ref _id);
	}
	public void DoEquip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){
		Unit.Default_DoEquip(this, game, unit, ref id, ref _id, ref _equipped);
	}
	public void TryUnequip(Game game, Unit unit){
		Unit.Default_TryUnequipWeapon(game, unit, ref _id);
	}
	public void DoUnequip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){
		Unit.Default_DoUnequip(this, game, unit, ref id, ref _equipped);
	}
	public string GetToolTip(Game game){
		return "Weapon\n\n" + _attack + " Damage";
	}
	public override IUseable GetUseable(){
		return this;
	}
	public override IAttacker GetAttacker(){
		return this;
	}
	public override IEquippable GetEquippable(){
		return this;
	}
	public override IItemToolTip GetItemToolTip(){
		return this;
	}
}
