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
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Potions);
		_controller.SetSortingOrder(20);
	}
	public void TryPickup(Game game, Unit unit){
		unit.GetHasInventory().GetInventory(game).GetPickupable().AttemptPickup(game, this);
	}
	public void DoPickup(Game game, Inventory inventory){
		GetSpawnable().Despawn(game);
		inventory.Add(this, ref _id);
	}
	public override bool CheckCollision(Game game, Unit check){
		return false;
	}
	public override IPickupable GetPickupable(){
		return this;
	}
}
