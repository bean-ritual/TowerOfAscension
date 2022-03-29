using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class LevelUnit : 
	Unit,
	WorldUnit.IWorldUnit,
	Unit.ISpawnable,
	Unit.IProcessable,
	Unit.ICollideable
	{
	protected WorldUnit.WorldUnitController _controller = WorldUnit.WorldUnitController.GetNullWorldUnitController();
	protected int _x = Unit.NullUnit.GetNullX();
	protected int _y = Unit.NullUnit.GetNullY();
	protected Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	public LevelUnit(){}
	public virtual WorldUnit.WorldUnitController GetWorldUnitController(Game game){
		return _controller;
	}
	public virtual bool GetWorldVisibility(Game game){
		return game.GetLevel().Get(_x, _y).GetLightable().GetLight() > 0;
	}
	public virtual void Spawn(Game game, int x, int y){
		Unit.Default_Spawn(this, game, x, y);
	}
	public virtual void Despawn(Game game){
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
	public virtual bool Process(Game game){
		return game.GetLevel().NextTurn();
	}
	public virtual bool CheckCollision(Game game, Unit check){
		return true;
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
	public override IProcessable GetProcessable(){
		return this;
	}
	public override Unit.ICollideable GetCollideable(){
		return this;
	}
}
