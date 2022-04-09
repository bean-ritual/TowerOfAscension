using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Item : 
	LevelUnit,
	Unit.IPickupable,
	Unit.IDroppable
	{
	public static class ITEM_DATA{
		public static Unit GetLevelledItem(int level){
			if(UnityEngine.Random.Range(0, 100) < 50){
				return new Weapon();
			}
			return new Chestplate();
		}
	}
	public void TryPickup(Game game, Unit unit){
		Unit.Default_TryPickup(this, game, unit);
	}
	public void DoPickup(Game game, Unit unit, Inventory inventory){
		Unit.Default_DoPickup(this, game, unit, ref _id);
	}
	public void TryDrop(Game game, Unit unit){
		Unit.Default_TryDrop(this, game, unit, ref _id);
	}
	public void DoDrop(Game game, Unit unit, Inventory inventory){
		Unit.Default_DoDrop(this, game, unit, ref _id);
	}
	public override bool CheckCollision(Game game, Unit check){
		return false;
	}
	public override IPickupable GetPickupable(){
		return this;
	}
	public override IDroppable GetDroppable(){
		return this;
	}
}
