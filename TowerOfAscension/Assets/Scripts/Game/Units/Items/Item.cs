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
	public Item(){}
	public void TryPickup(Level level, Unit unit){
		
	}
	public void DoPickup(Level level, Inventory inventory){
		
	}
	public override IPickupable GetPickupable(){
		return this;
	}
}
