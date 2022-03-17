using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Door : 
	LevelUnit,
	Unit.IInteractable,
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
	private bool _locked;
	public Door(){
		_locked = true;
		_spriteID = SpriteSheet.SpriteID.Door;
		_sortingOrder = 30;
	}
	public void Interact(Level level, Unit unit){
		_locked ^= true;
		RefreshSpriteIndex();
	}
	public override bool CheckCollision(Level level, Unit check){
		return _locked;
	}
	public override bool GetWorldVisibility(Level level){
		return true;
	}
	public bool CheckTransparency(Level level){
		return !_locked;
	}
	public void RefreshSpriteIndex(){
		int index = 0;
		if(!_locked){
			index = 1;
		}
		SetSpriteIndex(index);
	}
	public override Unit.IInteractable GetInteractable(){
		return this;
	}
	public override Level.ILightControl GetLightControl(){
		return this;
	}
}
