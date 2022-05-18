using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Torchlight : 
	Tag,
	Tag.IGetIntValue1,
	Tag.IGetIntValue2
	{
	private static Queue<Torchlight> _POOL = new Queue<Torchlight>();
	private const Tag.ID _TAG_ID = Tag.ID.Light;
	private int _maxLight;
	public void Setup(int maxLight){
		_maxLight = maxLight;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public int GetIntValue1(Game game, Unit self){
		return (int)Mathf.Ceil(_maxLight * (self.GetTag(game, Tag.ID.Fuel).GetIGetIntValue1().GetIntValue1(game, self) / (float)self.GetTag(game, Tag.ID.Fuel).GetIGetIntValue2().GetIntValue2(game, self)));
	}
	public int GetIntValue2(Game game, Unit self){
		return _maxLight;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public override Tag.IGetIntValue2 GetIGetIntValue2(){
		return this;
	}
	public static Tag Create(int maxLight){
		Torchlight tag;
		if(_POOL.Count > 0){
			tag = _POOL.Dequeue();
		}else{
			tag = new Torchlight();
		}
		tag.Setup(maxLight);
		return tag;
	}
}
