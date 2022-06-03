using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Murderer : 
	Block.BaseBlock,
	IActive
	{
	public void Trigger(Game game, Direction direction){
		IListData targets = direction.GetTile(game.GetMap(), GetSelf(game).GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetTile(game)).GetIDataTile().GetBlock(game, Game.TOAGame.BLOCK_TILE).GetIListData();
		for(int i = 0; i < targets.GetDataCount(); i++){
			IKillable kill = targets.GetData(game, i).GetBlock(game, Game.TOAGame.BLOCK_STATS).GetIKillable();
			kill.SetKiller(game, GetSelf(game));
			kill.Kill(game);
		}
		GetSelf(game).GetBlock(game, Game.TOAGame.BLOCK_DOTURN).GetIConclude().Conclude(game);
	}
	public override void Disassemble(Game game){
		
	}
	public override IActive GetIActive(){
		return this;
	}
}
