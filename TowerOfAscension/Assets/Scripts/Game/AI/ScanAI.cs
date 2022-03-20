using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[Serializable]
public class ScanAI : AI{
	public override bool Process(Level level, Unit self){
		self.GetPositionable().GetPosition(out int x, out int y);
		List<Tile> targets = new List<Tile>();
		level.CalculateFov(x, y, 5, (int range, Tile tile) => {
			if(!tile.GetLightControl().CheckTransparency(level)){
				return false;
			}
			if(tile.GetHostileTarget().CheckHostility(level, self)){
				targets.Add(tile);
			}
			return true;
		});
		//
		if(targets.Count <= 0){
			Default_Wander(self, level);
			return level.NextTurn();
		}
		Tile tile = targets[UnityEngine.Random.Range(0, targets.Count)];
		tile.GetXY(out int finalX, out int finalY);
		if((level.CalculateDistanceCost(x, y, finalX, finalY) / 10) <= 1){
			self.GetAttacker().Attack(level, Direction.IntToDirection(x, y, finalX, finalY));
			return level.NextTurn();
		}
		List<Tile> route = level.FindPath(x, y, finalX, finalY, (Tile tile) => tile.GetWalkable().CanWalk(level, self));
		//
		if(route.Count < 1){
			Default_Wander(self, level);
			return level.NextTurn();
		}
		route[1].GetXY(out int walkX, out int walkY);
		self.GetMoveable().Move(level, Direction.IntToDirection(x, y, walkX, walkY));
		return level.NextTurn();
	}
}
