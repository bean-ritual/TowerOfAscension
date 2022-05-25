using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MonsterAI : 
	Block.BaseBlock,
	IDoTurn
	{
	public MonsterAI(){}
	public bool DoTurn(Game game){
		return true;
	}
	public override void Disassemble(Game game){}
	public override IDoTurn GetIDoTurn(){
		return this;
	}
}
