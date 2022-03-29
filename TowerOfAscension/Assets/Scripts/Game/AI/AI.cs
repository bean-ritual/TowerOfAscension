using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AI{
	public interface ITurnControl{
		void StartTurn(Game game, Unit self);
		void EndTurn(Game game, Unit self);
	}
	[Serializable]
	public class NullAI : 
		AI,
		AI.ITurnControl
		{
		public override bool Process(Game game, Unit self){
			return game.GetLevel().NextTurn();
		}
		public void StartTurn(Game game, Unit self){}
		public void EndTurn(Game game, Unit self){}
	}
	[field:NonSerialized]private static readonly NullAI _NULL_AI = new NullAI();
	public abstract bool Process(Game game, Unit self);
	public virtual ITurnControl GetTurnControl(){
		return _NULL_AI;
	}
	public static void Default_Wander(Unit self, Game game){
		self.GetPositionable().GetPosition(game, out int x, out int y);
		List<Tile> tiles = game.GetLevel().GetNeighbours(x, y);
		tiles[UnityEngine.Random.Range(0, tiles.Count)].GetXY(out int targetX, out int targetY);
		self.GetMoveable().Move(game, Direction.IntToDirection(x, y, targetX, targetY));
	}
	public static AI GetNullAI(){
		return _NULL_AI;
	}
}
