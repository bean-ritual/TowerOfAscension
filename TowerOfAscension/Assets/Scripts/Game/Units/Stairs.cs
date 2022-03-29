using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stairs :
	Unit,
	WorldUnit.IWorldUnit,
	Unit.ISpawnable,
	Unit.ITripwire
	{
	private int _x = Unit.NullUnit.GetNullX();
	private int _y = Unit.NullUnit.GetNullY();
	private WorldUnit.WorldUnitController _controller = WorldUnit.WorldUnitController.GetNullWorldUnitController();
	private Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	public Stairs(){
		_controller = new WorldUnit.WorldUnitController(
			SpriteSheet.SpriteID.Stairs,
			0,
			10,
			Vector3.zero, 
			0
		);
	}
	public WorldUnit.WorldUnitController GetWorldUnitController(Game game){
		return _controller;
	}
	public bool GetWorldVisibility(Game game){
		return true;
	}
	public void Spawn(Game game, int x, int y){
		Unit.Default_Spawn(this, game, x, y);
	}
	public void Despawn(Game game){
		Unit.Default_Despawn(this, game);
	}
	public void SetPosition(Game game, int x, int y){
		Unit.Default_SetPosition(this, game, x, y, ref _x, ref _y);
	}
	public void GetPosition(Game game, out int x, out int y){
		x = _x;
		y = _y;
	}
	public void RemovePosition(Game game){
		Unit.Default_RemovePosition(this, game, _x, _y);
	}
	public Vector3 GetPosition(Game game){
		return game.GetLevel().GetWorldPosition(_x, _y);
	}
	public Tile GetTile(Game game){
		return game.GetLevel().Get(_x, _y);
	}
	public void AddToRegister(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public void RemoveFromRegister(Register<Unit> register){
		register.Remove(_id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public void Trip(Game game, Unit unit){
		unit.GetExitable().Exit(game);
	}
	public override WorldUnit.IWorldUnit GetWorldUnit(){
		return this;
	}
	public override Unit.ISpawnable GetSpawnable(){
		return this;
	}
	public override Unit.IPositionable GetPositionable(){
		return this;
	}
	public override Register<Unit>.IRegisterable GetRegisterable(){
		return this;
	}
	public override Unit.ITripwire GetTripwire(){
		return this;
	}
}
