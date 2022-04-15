using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stairs :
	Unit,
	VisualController.IVisualController,
	Unit.ISpawnable,
	Unit.ITripwire
	{
	private int _x = Unit.NullUnit.GetNullX();
	private int _y = Unit.NullUnit.GetNullY();
	private VisualController _controller = VisualController.GetNullVisualController();
	private Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	public Stairs(){
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Stairs);
		_controller.SetSortingOrder(10);
	}
	public virtual VisualController GetVisualController(Game game){
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
	public void Add(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public void Remove(Register<Unit> register){
		register.Remove(_id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public void Trip(Game game, Unit unit){
		unit.GetExitable().Exit(game);
	}
	public override VisualController.IVisualController GetVisualController(){
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
