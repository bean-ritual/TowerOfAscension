using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Collision : 
	Tag,
	Tag.ICondition<Tag.Collider>
	{
	private static Queue<Collision> _POOL = new Queue<Collision>();
	private const Tag.ID _TAG_ID = Tag.ID.Collision;
	private Tag.Collider _value = Tag.Collider.Null;
	public void Setup(Tag.Collider value){
		_value = value;
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
