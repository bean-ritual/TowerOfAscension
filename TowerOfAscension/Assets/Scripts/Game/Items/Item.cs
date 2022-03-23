using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Item{
	[Serializable]
	public class NullItem : Item{
		
	}
	[field:NonSerialized]private static readonly NullItem _NULL_ITEM = new NullItem();
	public static Item GetNullItem(){
		return _NULL_ITEM;
	}
}
