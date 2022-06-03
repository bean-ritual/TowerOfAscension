using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class GameBlocks{
	[Serializable]
	public class NullGameBlocks : 
		GameBlocks,
		ITick,
		IProcess,
		IEndTurn,
		IReset
		{
		public bool Tick(Game game){
			return true;
		}
		public void Process(Game game){}
		public void EndTurn(Game game){}
		public void Reset(Game game){}
		//
		public override void Add(Game game, int id, Block block){}
		public override void Remove(Game game, int id){}
		public override Block Get(Game game, int id){
			return Block.GetNullBlock();
		}
		public override Block GetIndex(Game game, int index){
			return Block.GetNullBlock();
		}
		public override int GetBlockID(){
			return -1;
		}
		public override int GetCount(){
			return 0;
		}
		public override void Disassemble(Game game){}
		public override bool IsNull(){
			return true;
		}
		public override void FireGameBlockUpdateEvent(){}
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
		public override Block GetIndex(Game game, int index){
			if(_blocks.TryGetValue(index, out Block block)){
				return block;
			}else{
				return Block.GetNullBlock();
			}
		}
		public override int GetBlockID(){
			return _id;
		}
		public override int GetCount(){
			return _blocks.Count;
		}
		public override void Disassemble(Game game){
			List<int> ids = new List<int>(_blocks.Count);
			foreach(KeyValuePair<int, Block> keyValue in _blocks){
				ids.Add(keyValue.Key);
			}
			for(int i = 0; i < ids.Count; i++){
				game.GetGameData().Get(ids[i]).Disassemble(game);
			}
			ids = null;
		}
		public override bool IsNull(){
			return false;
		}
	}
	public class GameBlockUpdateEventArgs : EventArgs{
		public int blockID;
		public GameBlockUpdateEventArgs(int blockID){
			this.blockID = blockID;
		}
	}
	//
	[field:NonSerialized]public event EventHandler<GameBlockUpdateEventArgs> OnGameBlockUpdate;
	public abstract void Add(Game game, int id, Block block);
	public abstract void Remove(Game game, int id);
	public abstract Block Get(Game game, int id);
	public abstract Block GetIndex(Game game, int index);
	public abstract int GetBlockID();
	public abstract int GetCount();
	public abstract void Disassemble(Game game);
	public abstract bool IsNull();
	//
	public virtual void FireGameBlockUpdateEvent(){
		OnGameBlockUpdate?.Invoke(this, new GameBlockUpdateEventArgs(GetBlockID()));
	}
	//
	public virtual ITick GetITick(){
		return _NULL_GAME_BLOCKS;
	}
	public virtual IProcess GetIProcess(){
		return _NULL_GAME_BLOCKS;
	}
	public virtual IEndTurn GetIEndTurn(){
		return _NULL_GAME_BLOCKS;
	}
	public virtual IReset GetIReset(){
		return _NULL_GAME_BLOCKS;
	}
	//
	private static NullGameBlocks _NULL_GAME_BLOCKS = new NullGameBlocks();
	public static GameBlocks GetNullGameBlocks(){
		return _NULL_GAME_BLOCKS;
	}
}
