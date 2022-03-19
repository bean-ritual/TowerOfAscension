using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[Serializable]
public class SmartAI : AI{
	public override bool Process(Level level, Unit self){
		self.GetPositionable().GetPosition(out int x, out int y);
		List<Tile> targets = new List<Tile>();
		level.CalculateFov(x, y, 5, (int range, Tile tile) => {
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
		List<Tile> route = level.FindPath(x, y, finalX, finalY, (Tile tile) => tile.GetWalkable().CanWalk(level, self));
		//
		if(route.Count < 1){
			Default_Wander(self, level);
			return level.NextTurn();
		}
		UnityEngine.Debug.Log("test");
		route[1].GetXY(out int walkX, out int walkY);
		self.GetMoveable().Move(level, Direction.IntToDirection(x, x, walkX, walkY));
		return level.NextTurn();
	}
}
