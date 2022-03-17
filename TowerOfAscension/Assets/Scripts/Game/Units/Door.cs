using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Door : 
	LevelUnit,
	Level.ILightControl
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
	public override bool GetWorldVisibility(Level level){
		return true;
	}
	public bool CheckTransparency(Level level){
		return false;
	}
	public override Level.ILightControl GetLightControl(){
		return this;
	}
}
