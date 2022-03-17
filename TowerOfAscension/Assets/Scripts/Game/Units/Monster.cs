using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Monster :
	AIUnit
	{
	public Monster(){
		_ai = new DumbAI();
		_spriteID = SpriteSheet.SpriteID.Rat;
		_sortingOrder = 20;
	}
}
