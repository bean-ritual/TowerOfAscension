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
		_ai = new ScanAI();
		_controller = new WorldUnit.WorldUnitController(
			SpriteSheet.SpriteID.Rat,
			0,
			20,
			Vector3.zero, 
			0
		);
		_health = new Health(5);
	}
}
