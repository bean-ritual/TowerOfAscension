using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DumbAI : AI{
	public override bool Process(Game game, Unit self){
		Default_Wander(self, game);
		return game.GetLevel().NextTurn();
	}
}
