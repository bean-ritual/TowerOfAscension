using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hero : 
	Unit,
	Unit.IProxyable
	{
	private const int _BASE_EXP = 50;
	private const float _EXP_FACTOR = 1.6f;
	public Hero(Game game) : base(game,
		new Tag[]{
			LightWorldVisual.Create(SpriteSheet.SpriteID.Hero, 0, 25),
			WorldPosition.Create(),
			TagControl.Create(),
			Experiance.Create(_BASE_EXP, _EXP_FACTOR),
			TagLevel.Create(1),
			Alive.Create(),
			Health.Create(95),
			Grave.Create(),
			Attackable.Create(),
			WeaponSlot.Create(),
			BasicAttack.Create(),
			Value.Create(Tag.ID.Damage_Physical, 1),
			TagInventory.Create(),
			Value.Create(Tag.ID.Light, 3),
			Discoverer.Create(),
			Interactor.Create(),
			Hostility.Create(),
			TagControl.Create(),
			Move.Create(10),
			Collision.Create(Tag.Collider.Basic),
			Exit.Create(),
			Log.Create(),
		}
	){}
	public override void Spawn(Game game, int x, int y){
		Unit proxy = new Player();
		proxy.Spawn(game, x, y);
		game.GetLevel().GetUnits().Swap(_id, 0);
		game.GetLevel().ResetTurn();
	}
	public override void Despawn(Game game){
		game.GetLevel().GetUnits().Get(_id).Despawn(game);
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
