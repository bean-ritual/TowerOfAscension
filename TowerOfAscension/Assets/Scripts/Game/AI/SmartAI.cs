using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[Serializable]
public class SmartAI : AI{
	private static Register<Unit>.ID _TARGET = Register<Unit>.ID.GetNullID();
	public override bool Process(Level level, Unit self){
		Unit target = level.GetUnits().Get(_TARGET);
		if(!target.GetHostileTarget().CheckHostility(level, self)){
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
	public static Unit GetTarget(Level level){
		return level.GetUnits().Get(_TARGET);
	}
	public static Register<Unit>.ID GetTarget(){
		return _TARGET;
	}
}
