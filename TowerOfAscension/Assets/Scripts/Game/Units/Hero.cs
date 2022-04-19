using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hero : 
	LevelUnit,
	Unit.IProxyable
	{
	public Hero(Game game){
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Hero);
		_controller.SetSortingOrder(25);
		AddTag(game, TagControl.Create());
		AddTag(game, Alive.Create());
		AddTag(game, Health.Create(95));
		AddTag(game, Grave.Create());
		AddTag(game, Attackable.Create());
		AddTag(game, BasicAttack.Create());
		AddTag(game, Value.Create(Tag.ID.Damage_Physical, 100));
		AddTag(game, TagInventory.Create());
		AddTag(game, EquipSlot.Create(Tag.ID.Weapon));
		AddTag(game, Value.Create(Tag.ID.Light, 3));
		AddTag(game, Discoverer.Create());
		AddTag(game, Interactor.Create());
		AddTag(game, Hostility.Create());
		AddTag(game, TagControl.Create());
		AddTag(game, Move.Create());
		AddTag(game, Collision.Create(Tag.Collider.Basic));
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
	public override IProxyable GetProxyable(){
		return this;
	}
}
