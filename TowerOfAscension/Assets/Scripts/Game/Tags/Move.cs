using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Move : Tag{
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
			self.GetPositionable().SetPosition(game, x, y);
			self.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, self);
		}
	}
	public static Tag Create(){
		return _MOVE;
	}
}
