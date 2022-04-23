using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Alive : 
	Tag,
	Tag.ICondition,
	Tag.IDamageValue1
	{
	private static Queue<Alive> _POOL = new Queue<Alive>();
	private static Tag.ID _TAG_ID = Tag.ID.Alive;
	private bool _value;
	public void Setup(){
		_value = true;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public bool Check(Game game, Unit self){
		return _value;
	}
	public void DamageValue1(Game game, Unit self){
		_value = false;
		self.GetTag(game, Tag.ID.Loot).GetITrigger().Trigger(game, self);
		self.Despawn(game);
	}
	public override Tag.ICondition GetICondition(){
		return this;
	}
	public override Tag.IDamageValue1 GetIDamageValue1(){
		return this;
	}
	public static Tag Create(){
		Alive tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new Alive();
		}
		tag.Setup();
		return tag;
	}
}
