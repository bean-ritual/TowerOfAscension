using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Loot : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.ITrigger
	{
	public static class LOOT_DATA{
		
	}
	private static Queue<Loot> _POOL = new Queue<Loot>();
	private static Tag.ID _TAG_ID = Tag.ID.Loot;
	private int _loottable;
	public void Setup(int loottable){
		_loottable = loottable;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public void SetValue1(Game game, Unit self, int value){
		_loottable = value;
	}
	public void Trigger(Game game, Unit self){
		//
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.ITrigger GetITrigger(){
		return this;
	}
	public static Tag Create(int loottable){
		Loot tag = new Loot();
		tag.Setup(loottable);
		return tag;
	}
}
