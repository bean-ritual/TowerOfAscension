using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemPickup : 
	Block.BaseBlock,
	IPickup
	{
	private int _holderID;
	private bool _held;
	public ItemPickup(){
		_holderID = -1;
		_held = false;
	}
	public void Pickup(Game game, Data holder){
		if(!_held && !holder.GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).IsNull()){
			Data self = GetSelf(game);
			self.GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().Despawn(game);
			holder.GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).GetIListData().AddData(game, self);
			_held = true;
			_holderID = holder.GetID();
		}
	}
	public void Drop(Game game){
		if(_held){
			Data self = GetSelf(game);
			Data holder = game.GetGameData().Get(_holderID);
			holder.GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).GetIListData().RemoveData(game, self);
			_held = false;
			_holderID = -1;
			Map.Tile tile = holder.GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetTile(game);
			tile.GetXY(out int x, out int y);
			self.GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().Spawn(game, x, y);
		}
	}
	public override void Disassemble(Game game){
		if(_held){
			game.GetGameData().Get(_holderID).GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).GetIListData().RemoveData(game, GetSelf(game));
			_held = false;
			_holderID = -1;
		}
	}
	public override IPickup GetIPickup(){
		return this;
	}
}
