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
		self.GetPositionable().GetPosition(game, out int x, out int y);
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
			self.GetTaggable().GetTag(game, Tag.ID.Move).GetIInputDirection().Input(game, self, Direction.GetRandomDirection());
			return level.NextTurn();
		}
		Tile tile = targets[UnityEngine.Random.Range(0, targets.Count)];
		tile.GetXY(out int finalX, out int finalY);
		if((level.CalculateDistanceCost(x, y, finalX, finalY) / 10) <= 1){
			self.GetTaggable().GetTag(game, Tag.ID.Basic_Attack).GetIInputDirection().Input(game, self, Direction.IntToDirection(x, y, finalX, finalY));
			return level.NextTurn();
		}
		List<Tile> route = level.FindPath(x, y, finalX, finalY, (Tile tile) => tile.GetWalkable().CanWalk(game, self));
		//
		if(route.Count < 1){
			self.GetTaggable().GetTag(game, Tag.ID.Move).GetIInputDirection().Input(game, self, Direction.GetRandomDirection());
			return level.NextTurn();
		}
		route[1].GetXY(out int walkX, out int walkY);
		self.GetTaggable().GetTag(game, Tag.ID.Move).GetIInputDirection().Input(game, self, Direction.IntToDirection(x, y, walkX, walkY));
		return level.NextTurn();
	}
	public override Tag.IProcess GetIProcess(){
		return this;
	}
	public static Tag Create(){
		return new TagAI();
	}
}
