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
		self.GetMoveable().Move(game, Direction.GetRandomDirection());
	}
	public static AI GetNullAI(){
		return _NULL_AI;
	}
}
