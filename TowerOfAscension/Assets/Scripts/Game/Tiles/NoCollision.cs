using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class NoCollision : 
	Block.BaseBlock,
	ICanWalk
	{
	public bool CanWalk(Game game){
		return true;
	}
	public override void Disassemble(Game game){}
	public override ICanWalk GetICanWalk(){
		return this;
	}
}
