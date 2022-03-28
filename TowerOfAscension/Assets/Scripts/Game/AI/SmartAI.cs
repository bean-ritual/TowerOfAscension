using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[Serializable]
public class SmartAI : AI{
	private static Register<Unit>.ID _TARGET = Register<Unit>.ID.GetNullID();
	public override bool Process(Game game, Unit self){
		Level level = game.GetLevel();
		Unit target = level.GetUnits().Get(_TARGET);
		if(!target.GetHostileTarget().CheckHostility(game, self)){
			return level.NextTurn();
		}
		//
		self.GetPositionable().GetPosition(out int selfX, out int selfY);
		target.GetPositionable().GetPosition(out int targetX, out int targetY);
		int distance = level.CalculateDistanceCost(selfX, selfY, targetX, targetY);
		if(distance > 80){
			return level.NextTurn();
		}
		if(distance / 10 <= 1){
			UnityEngine.Debug.Log("attack");
			return level.NextTurn();
		}
		//
		return level.NextTurn();
	}
	public static void SetTarget(Register<Unit>.ID id){
		_TARGET = id;
	}
	public static Unit GetTarget(Game game){
		return game.GetLevel().GetUnits().Get(_TARGET);
	}
	public static Register<Unit>.ID GetTarget(){
		return _TARGET;
	}
}
