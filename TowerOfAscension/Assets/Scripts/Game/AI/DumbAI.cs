using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DumbAI : AI{
	public override bool Process(Level level, Unit self){
		Default_Wander(self, level);
		return level.NextTurn();
	}
}
