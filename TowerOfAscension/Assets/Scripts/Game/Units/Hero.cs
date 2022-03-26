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
	Unit.IHasInventory,
	Level.ILightSource
	{
	private Inventory _inventory = Inventory.GetNullInventory();
	public Hero(){
		_ai = new PlayerControl();
		_controller = new WorldUnit.WorldUnitController(
			SpriteSheet.SpriteID.Hero,
			0,
			20,
			Vector3.zero, 
			0
		);
		_health = new Health(95);
		_inventory = new Inventory();
	}
	public override bool GetHealthBar(){
		return false;
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
	public Inventory GetInventory(){
		return _inventory;
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
	public override Unit.IHostileTarget GetHostileTarget(){
		return this;
	}
	public override Unit.IExitable GetExitable(){
		return this;
	}
	public override Unit.IHasInventory GetHasInventory(){
		return this;
	}
	public override Level.ILightSource GetLightSource(){
		return this;
	}
	
}
