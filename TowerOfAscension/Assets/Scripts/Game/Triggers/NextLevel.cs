using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class NextLevel : Trigger{
	private Unit _player;
	public NextLevel(Unit player){
		_player = player;
	}
	public override void Process(Game game){
		Default_ResetTrigger(game.GetLevel());
		game.SetPlayer(_player);
		game.NextLevel();
	}
}
