using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroGrave : 
	LevelUnit
	{
	public HeroGrave(){
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Grave);
		_controller.SetSortingOrder(20);
	}
	public override bool Process(Game game){
		game.GameOver();
		return true;
	}
	public override void Spawn(Game game, int x, int y){
		base.Spawn(game, x, y);
		game.GetLevel().GetUnits().Swap(_id, 0);
		game.GetLevel().ResetTurn();
	}
}
