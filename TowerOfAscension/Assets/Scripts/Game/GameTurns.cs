using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameTurns : 
	GameBlocks,
	ITick,
	IProcess,
	IEndTurn
	{
	private int _id;
	private int _index;
	private List<int> _indexes;
	private Dictionary<int, Block> _blocks;
	public GameTurns(int id){
		_id = id;
		_index = 0;
		_indexes = new List<int>();
		_blocks = new Dictionary<int, Block>();
	}
	public void Process(Game game){
		Get(game, _indexes[_index]).GetIProcess().Process(game);
	}
	public bool Tick(Game game){
		Get(game, _indexes[_index]).GetIProcess().Process(game);
		return _index >= 0;
	}
	public void EndTurn(Game game){
		int count = _blocks.Count;
		if(count > 0){
			_index = (_index + 1) % count;
		}
	}
	public override void Add(Game game, int id, Block block){
		if(!_blocks.ContainsKey(id)){
			block.SetIDs(id, _id);
			_blocks.Add(id, block);
			_indexes.Add(id);
		}
	}
	public override void Remove(Game game, int id){
		if(_blocks.TryGetValue(id, out Block block)){
			block.Disassemble(game);
			_blocks.Remove(id);
			_indexes.Remove(id);
		}
	}
	public override Block Get(Game game, int id){
		if(_blocks.TryGetValue(id, out Block block)){
			return block;
		}else{
			return Block.GetNullBlock();
		}
	}
	public override int GetCount(){
		return _blocks.Count;
	}
	public override bool IsNull(){
		return false;
	}
	public override ITick GetITick(){
		return this;
	}
	public override IProcess GetIProcess(){
		return this;
	}
	public override IEndTurn GetIEndTurn(){
		return this;
	}
}
