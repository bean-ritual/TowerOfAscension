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
		IListDataLogic listLogic = tile.GetIDataTile().GetData(game).GetBlock(game, Game.TOAGame.BLOCK_TILE).GetIListDataLogic();
		if(listLogic.DoData(game, (Data data) => {
			return data.GetBlock(game, Game.TOAGame.BLOCK_TILE).GetICanWalk().CanWalk(game);
		})){
			Data self = GetSelf(game);
			WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.MoveWorldAnimation(game, self.GetID(), tile, _moveSpeed));
			SetPosition(game, tile.GetX(), tile.GetY());
			listLogic.DoData(game, (Data data) => {
				data.GetBlock(game, Game.TOAGame.BLOCK_TRIP).GetITripwire().Trip(game, self);
				return true;
			});
			self.GetBlock(game, Game.TOAGame.BLOCK_DOTURN).GetIConclude().Conclude(game);
		}
	}
	public override IMovement GetIMovement(){
		return this;
	}
}
