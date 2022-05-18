using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Block{
	[Serializable]
	public class NullBlock : 
		Block
		{
		//
		//
		public override void SetIDs(int unitID, int blockID){}
		public override Unit GetSelf(Game game){
			return Unit.GetNullUnit();
		}
		public override GameBlocks GetGameBlocks(Game game){
			return GameBlocks.GetNullGameBlocks();
		}
		public override void Disassemble(Game game){}
		public override bool IsNull(){
			return true;
		}
	}
	[Serializable]
	public abstract class BaseBlock : Block{
		private int _unitID;
		private int _blockID;
		public override void SetIDs(int unitID, int blockID){
			_unitID = unitID;
			_blockID = blockID;
		}
		public override Unit GetSelf(Game game){
			return game.GetGameUnits().Get(_unitID);
		}
		public override GameBlocks GetGameBlocks(Game game){
			return game.GetGameBlocks(_blockID);
		}
		public override bool IsNull(){
			return false;
		}
		protected void FireBlockUpdateEvent(Game game){
			//GetSelf(game).FireBlockUpdateEvent(_blockID);
		}
	}
	public abstract void SetIDs(int unitID, int blockID);
	public abstract Unit GetSelf(Game game);
	public abstract GameBlocks GetGameBlocks(Game game);
	public abstract void Disassemble(Game game);
	public abstract bool IsNull();
	//
	//
	private static NullBlock _NULL_BLOCK = new NullBlock();
	public static Block GetNullBlock(){
		return _NULL_BLOCK;
	}
}
