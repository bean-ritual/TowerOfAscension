using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Alive : 
	Tag,
	Tag.ISetValue1<Register<Unit>.ID>,
	Tag.ICondition,
	Tag.IDamageValue1,
	Tag.IGetUnit
	{
	private static Queue<Alive> _POOL = new Queue<Alive>();
	private static Tag.ID _TAG_ID = Tag.ID.Alive;
	private Register<Unit>.ID _killer = Register<Unit>.ID.GetNullID();
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
	public void SetValue1(Game game, Unit self, Register<Unit>.ID value){
		_killer = value;
	}
	public bool Check(Game game, Unit self){
		return _value;
	}
	public void DamageValue1(Game game, Unit self){
		_value = false;
		self.GetTag(game, Tag.ID.Loot).GetITrigger().Trigger(game, self);
		self.GetTag(game, Tag.ID.Experiance).GetIInputUnit().Input(game, self, game.GetLevel().GetUnits().Get(_killer));
		self.Despawn(game);
	}
	public Unit GetUnit(Game game, Unit self){
		return game.GetLevel().GetUnits().Get(_killer);
	}
	public override Tag.ISetValue1<Register<Unit>.ID> GetISetValue1UnitID(){
		return this;
	}
	public override Tag.ICondition GetICondition(){
		return this;
	}
	public override Tag.IDamageValue1 GetIDamageValue1(){
		return this;
	}
	public override Tag.IGetUnit GetIGetUnit(){
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
