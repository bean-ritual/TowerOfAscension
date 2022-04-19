using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Move : 
	Tag,
	Tag.IInput<Direction>
	{
	private static readonly Move _MOVE = new Move();
	private const Tag.ID _TAG_ID = Tag.ID.Move;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Input(Game game, Unit self, Direction direction){
		Tile tile = direction.GetTileFromUnit(game, self);
		if(tile.GetWalkable().CanWalk(game, self)){
			tile.GetXY(out int x, out int y);
			self.GetPositionable().SetPosition(game, x, y, 1);
			self.GetTaggable().GetTag(game, Tag.ID.AI).GetIClear().Clear(game, self);
			tile.GetWalkable().Walk(game, self);
		}
	}
	public override IInput<Direction> GetIInputDirection(){
		return this;
	}
	public static Tag Create(){
		return _MOVE;
	}
}
