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
		Data data = tile.GetIDataTile().GetData(game);
		//
		if(data.GetBlock(game, Game.TOAGame.BLOCK_TILE).GetICanWalk().CanWalk(game)){
			Data self = GetSelf(game);
			WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.MoveWorldAnimation(game, self.GetID(), tile, _moveSpeed));
			SetPosition(game, tile.GetX(), tile.GetY());
			//
			data.GetBlock(game, Game.TOAGame.BLOCK_TRIP).GetITripwire().Trip(game, self);
			IListData listData = data.GetBlock(game, Game.TOAGame.BLOCK_TILE).GetIListData();
			for(int i = 0; i < listData.GetDataCount(); i++){
				listData.GetData(game, i).GetBlock(game, Game.TOAGame.BLOCK_TRIP).GetITripwire().Trip(game, self);
			}
			self.GetBlock(game, Game.TOAGame.BLOCK_DOTURN).GetIConclude().Conclude(game);
		}
	}
	public override IMovement GetIMovement(){
		return this;
	}
}
