using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Loot : 
	Tag
	{
	public static class LOOT_DATA{
		
	}
	private static Queue<Loot> _POOL = new Queue<Loot>();
	private static Tag.ID _TAG_ID = Tag.ID.Loot;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
}
