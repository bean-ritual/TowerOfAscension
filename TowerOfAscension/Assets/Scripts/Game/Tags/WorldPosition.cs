using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WorldPosition : 
	Tag,
	Tag.ISetValues<int, int>,
	Tag.IClear,
	Tag.IGetTile,
	Tag.IGetVector
	{
	private const Tag.ID _TAG_ID = Tag.ID.Position;
	private const int _NULL_XY = -1;
	private int _x = _NULL_XY;
	private int _y = _NULL_XY;
	private Vector3 _position = Vector3.zero;
    public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void SetValues(Game game, Unit self, int x, int y){
		Clear(game, self);
		game.GetLevel().Get(x, y).GetHasUnits().AddUnit(game, self.GetID());
		_x = x;
		_y = y;
		_position = game.GetLevel().GetWorldPosition(x, y);
		TagUpdateEvent();
	}
	public void Clear(Game game, Unit self){
		game.GetLevel().Get(_x, _y).GetHasUnits().RemoveUnit(game, self.GetID());
		_x = _NULL_XY;
		_y = _NULL_XY;
	}
	public Tile GetTile(Game game, Unit self){
		return game.GetLevel().Get(_x, _y);
	}
	public Vector3 GetVector(Game game, Unit self){
		return _position;
	}
	public override Tag.ISetValues<int, int> GetISetValuesInt(){
		return this;
	}
	public override Tag.IClear GetIClear(){
		return this;
	}
	public override Tag.IGetTile GetIGetTile(){
		return this;
	}
	public override Tag.IGetVector GetIGetVector(){
		return this;
	}
	public static Tag Create(){
		return new WorldPosition();
	}
}
