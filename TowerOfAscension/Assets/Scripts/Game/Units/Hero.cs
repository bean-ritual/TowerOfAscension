using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hero : 
	AIUnit,
	Unit.IDiscoverer,
	Unit.IInteractor,
	Unit.IHostileTarget,
	Unit.IExitable,
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
	public bool CheckHostility(Level level, Unit unit){
		return true;
	}
	public void Exit(Level level){
		level.SetTrigger(new NextLevel(this));
	}
	public int GetLightRange(Level level){
		return 3;
	}
	public override void Attacked(Level level, Unit unit, int attack){
		
	}
	public override Unit.IDiscoverer GetDiscoverer(){
		return this;
	}
	public override Unit.IInteractor GetInteractor(){
		return this;
	}
	public override Unit.IHostileTarget GetHostileTarget(){
		return this;
	}
	public override Unit.IExitable GetExitable(){
		return this;
	}
	public override Level.ILightSource GetLightSource(){
		return this;
	}
	
}
