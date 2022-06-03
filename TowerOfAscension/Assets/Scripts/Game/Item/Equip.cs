using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equip : 
	Block.BaseBlock
	{
	private int _holderID;
	private int _blockID;
	private bool _held;
	private bool _equipped;
	public Equip(){
		_holderID = -1;
		_blockID = -1;
		_held = false;
		_equipped = false;
	}
	public void Pickup(Game game, Data holder){
		if(!_held){
			//Despawn
			holder.GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).GetIListData().AddData(game, GetSelf(game));
			_held = true;
			_blockID = Game.TOAGame.BLOCK_INVENTORY;
			_holderID = holder.GetID();
		}
	}
	public void Drop(Game game){
		if(_held){
			if(_equipped){
				
			}else{
				//holder.GetBlock(game, Game.TOAGame.BLOCK_INVENTORY).GetIListData().RemoveData(game, GetSelf(game));
			}
			
			_held = false;
			_blockID = -1;
			_holderID = -1;
			//Spawn
		}
	}
	public void Equipp(Game game){
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

	}

}
