using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Ground : 
	Block.BaseBlock,
	ITile,
	IListData,
	IListDataLogic,
	ICanPrint,
	ICanConnect,
	ICanWalk,
	MapMeshManager.ITileMeshData
	{
	private int _x;
	private int _y;
	private List<int> _data;
	public Ground(int x, int y){
		_x = x;
		_y = y;
		_data = new List<int>();
	}
	public Map.Tile GetTile(Game game){
		return game.GetMap().Get(_x, _y);
	}
	public void AddData(Game game, Data data){
		int id = data.GetID();
		_data.Add(id);
		FireBlockDataAddEvent(game, id);
	}
	public void RemoveData(Game game, Data data){
		int id = data.GetID();
		_data.Remove(id);
		FireBlockDataRemoveEvent(game, id);
	}
	public Data GetData(Game game, int index){
		if(index < 0 || index >= _data.Count){
			return Data.GetNullData();
		}else{
			return game.GetGameData().Get(_data[index]);
		}
	}
	public int GetDataCount(){
		return _data.Count;
	}
	public bool DoData(Game game, Func<Data, bool> DoLogic){
		if(!DoLogic(GetSelf(game))){
			return false;
		}
		for(int i = 0; i < _data.Count; i++){
			if(!DoLogic(game.GetGameData().Get(_data[i]))){
				return false;
			}
		}
		return true;
	}
	public bool DoData(Game game, Func<Data, bool> DoLogic, out Data hit){
		hit = GetSelf(game);
		if(!DoLogic(hit)){
			return false;
		}
		for(int i = 0; i < _data.Count; i++){
			hit = game.GetGameData().Get(_data[i]);
			if(!DoLogic(hit)){
				return false;
			}
		}
		hit = Data.GetNullData();
		return true;
	}
	public bool CanPrint(Game game){
		return false;
	}
	public bool CanConnect(Game game){
		return true;
		/*
		if(_data.Count > 0){
			return false;
		}else{
			return true;
		}
		*/
	}
	public bool CanWalk(Game game){
		return true;
	}
	public int GetAtlasIndex(Game game){
		return 1;
	}
	public int GetUVFactor(Game game){
		return 1;
	}
	public override void Disassemble(Game game){
		game.GetMap().Get(_x, _y).GetIDataTile().ClearData(game);
		int[] temp = _data.ToArray();
		_data.Clear();
		for(int i = 0; i < temp.Length; i++){
			game.GetGameData().Get(temp[i]).Disassemble(game);
		}
		temp = null;
	}
	public override ITile GetITile(){
		return this;
	}
	public override IListData GetIListData(){
		return this;
	}
	public override IListDataLogic GetIListDataLogic(){
		return this;
	}
	public override ICanPrint GetICanPrint(){
		return this;
	}
	public override ICanConnect GetICanConnect(){
		return this;
	}
	public override ICanWalk GetICanWalk(){
		return this;
	}
	public override MapMeshManager.ITileMeshData GetITileMeshData(){
		return this;
	}
}
