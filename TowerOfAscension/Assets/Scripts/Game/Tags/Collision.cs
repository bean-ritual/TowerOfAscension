using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Collision : 
	Tag,
	Tag.ISetValue1<Tag.Collider>,
	Tag.IGetCollider,
	Tag.ICondition<Tag.Collider>
	{
	private static Queue<Collision> _POOL = new Queue<Collision>();
	private const Tag.ID _TAG_ID = Tag.ID.Collision;
	private Tag.Collider _value = Tag.Collider.Null;
	public void Setup(Tag.Collider value){
		_value = value;
	}
	public void SetValue1(Game game, Unit self, Tag.Collider value){
		_value = value;
	}
	public Tag.Collider GetCollider(Game game, Unit self){
		return _value;
	}
	public bool Check(Game game, Unit self, Tag.Collider collider){
		if(collider == Tag.Collider.Null){
			return false;
		}
		if(collider != _value){
			return false;
		}
		return true;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		_POOL.Enqueue(this);
	}
	public override Tag.ISetValue1<Tag.Collider> GetISetValue1Collider(){
		return this;
	}
	public override Tag.IGetCollider GetIGetCollider(){
		return this;
	}
	public override Tag.ICondition<Tag.Collider> GetIConditionCollider(){
		return this;
	}
	public static Tag Create(Tag.Collider value){
		Collision tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new Collision();
		}
		tag.Setup(value);
		return tag;
	}
}
