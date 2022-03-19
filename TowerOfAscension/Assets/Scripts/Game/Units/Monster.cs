using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Monster :
	AIUnit
	{
	public static class MONSTER_DATA{
		public static Unit GetLevelledMonster(int level){
			return new Monster();
		}
	}
	public Monster(){
		_ai = new SmartAI();
		_spriteID = SpriteSheet.SpriteID.Rat;
		_sortingOrder = 20;
	}
}
