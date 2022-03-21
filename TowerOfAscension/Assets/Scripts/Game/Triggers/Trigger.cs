using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Trigger{
	[Serializable]
	public class NullTrigger : Trigger{
		public NullTrigger(){}
		public override void Process(Game game){}
	}
	private static readonly NullTrigger _NULL_TRIGGER = new NullTrigger();
	public abstract void Process(Game game);
	public static void Default_ResetTrigger(Level level){
		level.SetTrigger(GetNullTrigger());
	}
	public static Trigger GetNullTrigger(){
		return _NULL_TRIGGER;
	}
}
