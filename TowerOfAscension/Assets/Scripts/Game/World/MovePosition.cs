using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MovePosition : 
	WorldPosition, 
	IMovement
	{
	private int _moveSpeed;
	public MovePosition(int moveSpeed) : base(){
		_moveSpeed = moveSpeed;
	}
	public void Move(Game game, Direction direction){
		Map.Tile tile = direction.GetTile(game.GetMap(), game.GetMap().Get(GetX(), GetY()));
		if(tile.GetIDataTile().GetBlock(game, 0).GetICanWalk().CanWalk(game)){
			WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.MoveWorldAnimation(game, GetSelf(game).GetID(), tile, _moveSpeed));
			SetPosition(game, tile.GetX(), tile.GetY());
			GetSelf(game).GetBlock(game, 3).GetIConclude().Conclude(game);
		}
	}
	public override IMovement GetIMovement(){
		return this;
	}
}
