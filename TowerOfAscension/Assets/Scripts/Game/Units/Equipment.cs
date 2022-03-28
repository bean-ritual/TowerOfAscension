using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equipment : 
	Inventory,
	Inventory.IPickupable
	{
	public void AttemptPickup(Game game, Unit unit){
		unit.GetPickupable().DoPickup(game, this);
	}
	public override IPickupable GetPickupable(){
		return this;
	}
}
