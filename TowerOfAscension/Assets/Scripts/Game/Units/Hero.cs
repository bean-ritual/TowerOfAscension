using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hero : 
	AIUnit,
	Unit.IProxyable,
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
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Hero);
		_controller.SetSortingOrder(20);
		_health = new Health(95);
		_inventory = new Equipment();
	}
	public override void Spawn(Game game, int x, int y){
		Unit proxy = new Player();
		proxy.GetSpawnable().Spawn(game, x, y);
		game.GetLevel().GetUnits().Swap(_id, 0);
		game.GetLevel().ResetTurn();
	}
	public override void Despawn(Game game){
		game.GetLevel().GetUnits().Get(_id).GetSpawnable().Despawn(game);
	}
	public void SetProxyID(Game game, Register<Unit>.ID id){
		_id = id;
	}
	public void RemoveProxyID(Game game){
		_id = Register<Unit>.ID.GetNullID();
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
	public Inventory GetInventory(Game game){
		return _inventory;
	}
	public int GetLightRange(Game game){
		return 3;
	}
	public override IProxyable GetProxyable(){
		return this;
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
