using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equipment : 
	Inventory,
	Inventory.IPickupable
	{
	public void AttemptPickup(Level level, Unit unit){
		unit.GetPickupable().DoPickup(level, this);
	}
	public override IPickupable GetPickupable(){
		return this;
	}
}
