using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Door : 
	LevelUnit
	{
	// DOOR_DATA
	public static class DOOR_DATA{
		public static Unit GetLevelledDoor(int level){
			if(UnityEngine.Random.Range(0, 100) < 80){
				return new Door();
			}
			return Unit.GetNullUnit();
		}
	}
	public Door(){
		_spriteID = SpriteSheet.SpriteID.Door;
		_sortingOrder = 30;
	}
}
