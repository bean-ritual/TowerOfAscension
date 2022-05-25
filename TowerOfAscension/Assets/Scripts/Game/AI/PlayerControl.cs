using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerControl : 
	Block.BaseBlock,
	IDoTurn,
	IConclude
	{
	private bool _turn;
	public PlayerControl(){
		_turn = false;
	}
	public bool DoTurn(Game game){
		_turn = true;
		return false;
	}
	public void Conclude(Game game){
		_turn = false;
		game.GetGameWorld().EndTurn(game);
	}
	public override void Disassemble(Game game){}
	public override IDoTurn GetIDoTurn(){
		return this;
	}
	public override IConclude GetIConclude(){
		return this;
	}
}
