using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ListedBlocks : 
	GameBlocks
	{
	private int _id;
	private List<int> _indexes;
	private Dictionary<int, Block> _blocks;
	public ListedBlocks(int id){
		_id = id;
		_indexes = new List<int>();
		_blocks = new Dictionary<int, Block>();
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
	public override Block GetIndex(Game game, int index){
		if(index < 0 || index >= _indexes.Count){
			return Block.GetNullBlock();
		}else{
			if(_blocks.TryGetValue(_indexes[index], out Block block)){
				return block;
			}else{
				return Block.GetNullBlock();
			}
		}
	}
	public override int GetBlockID(){
		return _id;
	}
	public override int GetCount(){
		return _indexes.Count;
	}
	public override void Disassemble(Game game){
		foreach(int id in _indexes.ToArray()){
			game.GetGameData().Get(id).Disassemble(game);
		}
	}
	public override bool IsNull(){
		return false;
	}
}
