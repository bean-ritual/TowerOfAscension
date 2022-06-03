using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MonsterAI : 
	Block.BaseBlock,
	IDoTurn
	{
	public MonsterAI(){}
	public bool DoTurn(Game game){
		Map.IMapmatics map = game.GetMap().GetIMapmatics();
		Block move = GetSelf(game).GetBlock(game, 1);
		move.GetIWorldPosition().GetTile(game).GetXY(out int startX, out int startY);
		game.GetPlayer().GetBlock(game, 1).GetIWorldPosition().GetTile(game).GetXY(out int targetX, out int targetY);
		if(!map.RaycastHit(startX, startY, targetX, targetY, (Map.Tile tile) => (!tile.GetIDataTile().GetBlock(game, 4).GetICanOpaque().CanOpaque(game)))){
			move.GetIMovement().Move(game, Direction.GetRandomDirection());
			return true;
		}
		if((map.CalculateDistanceCost(startX, startY, targetX, targetY) / 10) <= 1){
			//self.GetTag(game, Tag.ID.Attack_Slot).GetIInputDirection().Input(game, self, Direction.IntToDirection(x, y, finalX, finalY));
			return true;
		}
		List<Map.Tile> route = map.FindPath(startX, startY, targetX, targetY, (Map.Tile tile) => tile.GetIDataTile().GetBlock(game, 0).GetICanWalk().CanWalk(game));
		//
		if(route.Count < 1){
			move.GetIMovement().Move(game, Direction.GetRandomDirection());
			return true;
		}
		route[1].GetXY(out int walkX, out int walkY);
		move.GetIMovement().Move(game, Direction.IntToDirection(startX, startY, walkX, walkY));
		return true;
	}
	public override void Disassemble(Game game){}
	public override IDoTurn GetIDoTurn(){
		return this;
	}
}
