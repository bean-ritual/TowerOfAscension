using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AI{
	public interface ITurnControl{
		void StartTurn(Level level, Unit self);
		void EndTurn(Level level, Unit self);
	}
	[Serializable]
	public class NullAI : 
		AI,
		AI.ITurnControl
		{
		public override bool Process(Level level, Unit self){
			return level.NextTurn();
		}
		public void StartTurn(Level level, Unit self){}
		public void EndTurn(Level level, Unit self){}
	}
	[field:NonSerialized]private static readonly NullAI _NULL_AI = new NullAI();
	public abstract bool Process(Level level, Unit self);
	public virtual ITurnControl GetTurnControl(){
		return _NULL_AI;
	}
	public static void Default_Wander(Unit self, Level level){
		self.GetPositionable().GetPosition(out int x, out int y);
		List<Tile> tiles = level.GetNeighbours(x, y);
		tiles[UnityEngine.Random.Range(0, tiles.Count)].GetXY(out int targetX, out int targetY);
		self.GetMoveable().Move(level, Unit.IntToDirection(x, y, targetX, targetY));
	}
	public static AI GetNullAI(){
		return _NULL_AI;
	}
}
