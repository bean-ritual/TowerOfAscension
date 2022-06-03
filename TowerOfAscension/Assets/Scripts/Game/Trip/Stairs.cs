using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stairs : 
	Block.BaseBlock,
	ITripwire
	{
	public void Trip(Game game, Data data){
		if(data.GetID() == 0){
			data.GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().Despawn(game);
			game.NextFloor();
		}
	}
	public override void Disassemble(Game game){
		
	}
	public override ITripwire GetITripwire(){
		return this;
	}
}
