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
		_controller = new WorldUnit.WorldUnitController(
			SpriteSheet.SpriteID.Door,
			0,
			30,
			Vector3.zero, 
			0
		);
	}
	public void Interact(Game game, Unit unit){
		_locked ^= true;
		RefreshSpriteIndex();
	}
	public override bool CheckCollision(Game game, Unit check){
		return _locked;
	}
	public override bool GetWorldVisibility(Game game){
		return true;
	}
	public bool CheckTransparency(Game game){
		return !_locked;
	}
	public void RefreshSpriteIndex(){
		int index = 0;
		if(!_locked){
			index = 1;
		}
		_controller.SetSpriteIndex(index);
	}
	public override Unit.IInteractable GetInteractable(){
		return this;
	}
	public override Level.ILightControl GetLightControl(){
		return this;
	}
}
