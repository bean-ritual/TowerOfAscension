using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[Serializable]
public class ScanAI : AI{
	public override bool Process(Game game, Unit self){
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
			Default_Wander(self, game);
			return level.NextTurn();
		}
		Tile tile = targets[UnityEngine.Random.Range(0, targets.Count)];
		tile.GetXY(out int finalX, out int finalY);
		if((level.CalculateDistanceCost(x, y, finalX, finalY) / 10) <= 1){
			self.GetAttacker().Attack(game, Direction.IntToDirection(x, y, finalX, finalY));
			return level.NextTurn();
		}
		List<Tile> route = level.FindPath(x, y, finalX, finalY, (Tile tile) => tile.GetWalkable().CanWalk(game, self));
		//
		if(route.Count < 1){
			Default_Wander(self, game);
			return level.NextTurn();
		}
		route[1].GetXY(out int walkX, out int walkY);
		self.GetMoveable().Move(game, Direction.IntToDirection(x, y, walkX, walkY));
		return level.NextTurn();
	}
}
