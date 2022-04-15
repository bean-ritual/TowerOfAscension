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
	public Hero(Game game){
		_ai = new PlayerControl();
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Hero);
		_controller.SetSortingOrder(25);
		//_inventory = new Equipment();
		AddTag(game, Alive.Create());
		AddTag(game, Health.Create(95));
		AddTag(game, Grave.Create());
		AddTag(game, Attackable.Create());
		AddTag(game, BasicAttack.Create());
		AddTag(game, Value.Create(Tag.ID.Damage_Physical, 1));
		AddTag(game, TagInventory.Create());
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
	public override void OnKill(Game game){
		HeroGrave grave = new HeroGrave();
		grave.Spawn(game, _x, _y);
	}
	public void SetProxyID(Game game, Register<Unit>.ID id){
		_id = id;
	}
	public void RemoveProxyID(Game game){
		_id = Register<Unit>.ID.GetNullID();
	}
	public void Discover(Game game, Tile tile){
		tile.GetDiscoverable().Discover(game, this);
	}
	public void Interact(Game game, Direction direction){
		direction.GetTileFromUnit(game, this).GetInteractable().Interact(game, this);
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
