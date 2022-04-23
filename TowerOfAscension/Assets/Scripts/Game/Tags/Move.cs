using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Move : 
	Tag,
	Tag.IInput<Direction>,
	Tag.IGetIntValue1
	{
	private const Tag.ID _TAG_ID = Tag.ID.Move;
	private int _moveSpeed;
	public void Setup(int moveSpeed){
		_moveSpeed = moveSpeed;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Direction direction){
		Tile tile = direction.GetTile(game, self.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, self));
		if(tile.GetWalkable().CanWalk(game, self)){
			tile.GetXY(out int x, out int y);
			self.GetTag(game, Tag.ID.Position).GetISetValuesInt().SetValues(game, self, x, y);
			self.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, self);
			tile.GetWalkable().Walk(game, self);
		}
	}
	public int GetIntValue1(Game game, Unit self){
		return _moveSpeed;
	}
	public override IInput<Direction> GetIInputDirection(){
		return this;
	}
	public override Tag.IGetIntValue1 GetIGetIntValue1(){
		return this;
	}
	public static Tag Create(int moveSpeed){
		Move tag = new Move();
		tag.Setup(moveSpeed);
		return tag;
	}
}
