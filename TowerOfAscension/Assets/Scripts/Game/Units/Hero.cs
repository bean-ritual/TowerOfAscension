using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hero : 
	AIUnit,
	Unit.IDiscoverer,
	Unit.IInteractor,
	Level.ILightSource
	{
	public Hero(){
		_ai = new PlayerControl();
		_spriteID = SpriteSheet.SpriteID.Hero;
		_sortingOrder = 20;
	}
	public void Discover(Level level, Tile tile){
		tile.GetDiscoverable().Discover(level, this);
	}
	public void Interact(Level level, Direction direction){
		direction.GetTile(level, _x, _y).GetInteractable().Interact(level, this);
		_ai.GetTurnControl().EndTurn(level, this);
	}
	public int GetLightRange(Level level){
		return 3;
	}
	public override Unit.IDiscoverer GetDiscoverer(){
		return this;
	}
	public override Unit.IInteractor GetInteractor(){
		return this;
	}
	public override Level.ILightSource GetLightSource(){
		return this;;
	}
	
}
