using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item : 
	LevelUnit,
	Unit.IPickupable
	{
	public static class ITEM_DATA{
		public static Unit GetLevelledItem(int level){
			return new Item();
		}
	}
	public Item(){
		_controller = new WorldUnit.WorldUnitController(
			SpriteSheet.SpriteID.Potions,
			0,
			20,
			Vector3.zero, 
			0
		);
	}
	public void TryPickup(Level level, Unit unit){
		unit.GetHasInventory().GetInventory().GetPickupable().AttemptPickup(level, this);
	}
	public void DoPickup(Level level, Inventory inventory){
		GetSpawnable().Despawn(level);
		inventory.Add(this, ref _id);
	}
	public override bool CheckCollision(Level level, Unit check){
		return false;
	}
	public override IPickupable GetPickupable(){
		return this;
	}
}
