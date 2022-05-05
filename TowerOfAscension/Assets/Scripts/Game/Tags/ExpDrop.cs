using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ExpDrop : 
	Tag,
	Tag.ISetValue1<int>,
	Tag.IInput<Unit>
	{
	public static class LOOT_DATA{
		
	}
	private static Queue<ExpDrop> _POOL = new Queue<ExpDrop>();
	private static Tag.ID _TAG_ID = Tag.ID.ExpDrop;
	private int _experiance;
	public void Setup(int experiance){
		_experiance = experiance;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public void SetValue1(Game game, Unit self, int value){
		_experiance = value;
	}
	public void Input(Game game, Unit self, Unit killer){
		killer.GetTag(game, Tag.ID.Experiance).GetIFortifyValue1Int().FortifyValue1(game, killer, _experiance);
	}
	public override Tag.ISetValue1<int> GetISetValue1Int(){
		return this;
	}
	public override Tag.IInput<Unit> GetIInputUnit(){
		return this;
	}
	public static Tag Create(int experiance){
		ExpDrop tag = new ExpDrop();
		tag.Setup(experiance);
		return tag;
	}
}
