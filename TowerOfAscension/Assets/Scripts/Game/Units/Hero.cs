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
		_inventory = new Equipment();
	}
	public override void Spawn(Game game, int x, int y){
		base.Spawn(game, x, y);
		Level level = game.GetLevel();
		level.GetUnits().Swap(_id, 0);
		level.ResetTurn();
	}
	public override bool GetHealthBar(){
		return false;
	}
	public override void Attack(Game game, Direction direction){
		direction.GetTile(game.GetLevel(), _x, _y).GetAttackable().Attacked(game, this, 100);
	}
	public void Discover(Game game, Tile tile){
		tile.GetDiscoverable().Discover(game, this);
	}
	public void Interact(Game game, Direction direction){
		direction.GetTile(game.GetLevel(), _x, _y).GetInteractable().Interact(game, this);
		_ai.GetTurnControl().EndTurn(game, this);
	}
	public bool CheckHostility(Game game, Unit unit){
		return true;
	}
	public void Exit(Game game){
		game.NextLevel();
	}
	public Inventory GetInventory(){
		return _inventory;
	}
	public int GetLightRange(Game game){
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
