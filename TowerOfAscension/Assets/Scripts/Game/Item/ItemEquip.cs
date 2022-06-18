using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemEquip : 
	Block.BaseBlock,
	IPickup
	{
	private int _holderID;
	private int _blockID;
	private bool _held;
	private bool _equipped;
	private int _equipSlot;
	public ItemEquip(int equipSlot){
		_holderID = -1;
		_blockID = -1;
		_held = false;
		_equipped = false;
	}
	public void Pickup(Game game, Data holder){
		if(!_held){
			Data self = GetSelf(game);
			self.GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().Despawn(game);
			holder.GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).GetIListData().AddData(game, self);
			_held = true;
			_blockID = Game.TOAGame.BLOCK_INVENTORY;
			_holderID = holder.GetID();
		}
	}
	public void Drop(Game game){
		if(_held){
			Data self = GetSelf(game);
			Data holder = game.GetGameData().Get(_holderID);
			holder.GetBlock(game, _blockID).GetIListData().RemoveData(game, self);
			_held = false;
			_blockID = -1;
			_holderID = -1;
			Map.Tile tile = holder.GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetTile(game);
			tile.GetXY(out int x, out int y);
			self.GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().Spawn(game, x, y);
		}
	}
	public void Equip(Game game){
		if(_held){
			//holder.GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).GetIListData().RemoveData(game, GetSelf(game));
			_blockID = Game.TOAGame.BLOCK_INVENTORY;
			//holder.GetBlock(game, Game.TOAGame.BLOCK_EQUIPMENT)
		}
	}
	public void Unequip(Game game){
		if(_held){
			
		}
	}
	public override void Disassemble(Game game){
		if(_held){
			game.GetGameData().Get(_holderID).GetBlock(game, _blockID).GetIListData().RemoveData(game, GetSelf(game));
			_held = false;
			_blockID = -1;
			_holderID = -1;
		}
	}
}
