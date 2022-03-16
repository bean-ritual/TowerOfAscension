using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AI{
	[Serializable]
	public class NullAI : AI{
		public override bool Process(Level level, Unit self){
			return level.NextTurn();
		}
	}
	[field:NonSerialized]private static readonly NullAI _NULL_AI = new NullAI();
	public abstract bool Process(Level level, Unit self);
	public static AI GetNullAI(){
		return _NULL_AI;
	}
}
