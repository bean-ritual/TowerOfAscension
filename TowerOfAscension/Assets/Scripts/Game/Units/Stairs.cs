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
	public WorldUnit.WorldUnitController GetWorldUnitController(){
		return _controller;
	}
	public bool GetWorldVisibility(Level level){
		return true;
	}
	public void Spawn(Level level, int x, int y){
		Unit.Default_Spawn(this, level, x, y);
	}
	public void Despawn(Level level){
		Unit.Default_Despawn(this, level);
	}
	public void SetPosition(Level level, int x, int y){
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
	}
	public void GetPosition(out int x, out int y){
		x = _x;
		y = _y;
	}
	public void RemovePosition(Level level){
		Unit.Default_RemovePosition(this, level, _x, _y);
	}
	public Vector3 GetPosition(GridMap<Tile> map){
		return map.GetWorldPosition(_x, _y);
	}
	public Tile GetTile(Level level){
		return level.Get(_x, _y);
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
	public void Trip(Level level, Unit unit){
		unit.GetExitable().Exit(level);
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
