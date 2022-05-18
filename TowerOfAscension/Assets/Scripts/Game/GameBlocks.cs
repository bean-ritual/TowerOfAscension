using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class GameBlocks{
	[Serializable]
	public class NullGameBlocks : GameBlocks{
		public override void Add(Game game, int id, Block block){}
		public override void Remove(Game game, int id){}
		public override Block Get(Game game, int id){
			return Block.GetNullBlock();
		}
		public override bool IsNull(){
			return true;
		}
	}
	[Serializable]
	public class RealGameBlocks : GameBlocks{
		private int _id;
		private Dictionary<int, Block> _blocks;
		public RealGameBlocks(int id){
			_id = id;
			_blocks = new Dictionary<int, Block>();
		}
		public override void Add(Game game, int id, Block block){
			if(!_blocks.ContainsKey(id)){
				block.SetIDs(id, _id);
				_blocks.Add(id, block);
			}
		}
		public override void Remove(Game game, int id){
			if(_blocks.TryGetValue(id, out Block block)){
				block.Disassemble(game);
				_blocks.Remove(id);
			}
		}
		public override Block Get(Game game, int id){
			if(_blocks.TryGetValue(id, out Block block)){
				return block;
			}else{
				return Block.GetNullBlock();
			}
		}
		public override bool IsNull(){
			return false;
		}
	}
	//
	public abstract void Add(Game game, int id, Block block);
	public abstract void Remove(Game game, int id);
	public abstract Block Get(Game game, int id);
	public abstract bool IsNull();
	//
	private static NullGameBlocks _NULL_GAME_BLOCKS = new NullGameBlocks();
	public static GameBlocks GetNullGameBlocks(){
		return _NULL_GAME_BLOCKS;
	}
}
