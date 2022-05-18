using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagAI : 
	Tag,
	Tag.IProcess
	{
	private static Tag.ID _TAG_ID = Tag.ID.AI;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public bool Process(Game game, Unit self){
		Level level = game.GetLevel();
		self.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, self).GetXY(out int x, out int y);
		List<Tile> targets = new List<Tile>();
		level.CalculateFov(x, y, 5, (int range, Tile tile) => {
			if(!tile.GetLightControl().CheckTransparency(game)){
				return false;
			}
			if(tile.GetHostileTarget().CheckHostility(game, self)){
				targets.Add(tile);
			}
			return true;
		});
		//
		if(targets.Count <= 0){
			self.GetTag(game, Tag.ID.Move).GetIInputDirection().Input(game, self, Direction.GetRandomDirection());
			return level.NextTurn(game);
		}
		Tile tile = targets[UnityEngine.Random.Range(0, targets.Count)];
		tile.GetXY(out int finalX, out int finalY);
		if((level.CalculateDistanceCost(x, y, finalX, finalY) / 10) <= 1){
			self.GetTag(game, Tag.ID.Attack_Slot).GetIInputDirection().Input(game, self, Direction.IntToDirection(x, y, finalX, finalY));
			return level.NextTurn(game);
		}
		List<Tile> route = level.FindPath(x, y, finalX, finalY, (Tile tile) => tile.GetWalkable().CanWalk(game, self));
		//
		if(route.Count < 1){
			self.GetTag(game, Tag.ID.Move).GetIInputDirection().Input(game, self, Direction.GetRandomDirection());
			return level.NextTurn(game);
		}
		route[1].GetXY(out int walkX, out int walkY);
		self.GetTag(game, Tag.ID.Move).GetIInputDirection().Input(game, self, Direction.IntToDirection(x, y, walkX, walkY));
		return level.NextTurn(game);
	}
	public override Tag.IProcess GetIProcess(){
		return this;
	}
	public static Tag Create(){
		return new TagAI();
	}
}
