using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Hero : 
	LevelUnit
	{
	private AI _ai = AI.GetNullAI();
	public Hero(){
		_ai = new PlayerControl();
		_spriteID = SpriteSheet.SpriteID.Hero;
		_sortingOrder = 20;
	}
	public void Move(Level level, Direction direction){
		Unit.Default_Move(this, level, direction);
	}
	public void OnMove(Level level, Tile tile){
		tile.GetXY(out int x, out int y);
		SetPosition(level, x, y);
	}
	public override bool Process(Level level){
		return _ai.Process(level, this);
	}
}
