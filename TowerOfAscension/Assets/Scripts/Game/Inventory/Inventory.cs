using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Inventory : 
	Block.BaseBlock,
	IListData
	{
	private List<int> _items;
	public Inventory(){
		_items = new List<int>();
	}
	public void AddData(Game game, Data data){
		int id = data.GetID();
		_items.Add(id);
		FireBlockDataAddEvent(game, id);
	}
	public void RemoveData(Game game, Data data){
		int id = data.GetID();
		_items.Remove(id);
		FireBlockDataRemoveEvent(game, id);
	}
	public Data GetData(Game game, int index){
		if(index < 0 || index >= _items.Count){
			return Data.GetNullData();
		}else{
			return game.GetGameData().Get(_items[index]);
		}
	}
	public int GetDataCount(){
		return _items.Count;
	}
	public override void Disassemble(Game game){
		int[] temp = _items.ToArray();
		for(int i = 0; i < temp.Length; i++){
			game.GetGameData().Get(temp[i]).Disassemble(game);
		}
		temp = null;
	}
	public override IListData GetIListData(){
		return this;
	}
}
