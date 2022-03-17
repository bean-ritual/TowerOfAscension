using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hero : 
	AIUnit,
	Level.ILightSource
	{
	public Hero(){
		_ai = new PlayerControl();
		_spriteID = SpriteSheet.SpriteID.Hero;
		_sortingOrder = 20;
	}
	public int GetLightRange(Level level){
		return 5;
	}
	public override Level.ILightSource GetLightSource(){
		return this;;
	}
}
