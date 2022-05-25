using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Ground : 
	Block.BaseBlock,
	IListData,
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
	public void AddData(Game game, Data data){
		_data.Add(data.GetID());
		FireBlockUpdateEvent(game);
	}
	public void RemoveData(Game game, Data data){
		_data.Remove(data.GetID());
		FireBlockUpdateEvent(game);
		//data.GetMap().FireTileUpdateEvent(_x, _y);
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
	public bool CanPrint(Game game){
		return false;
	}
	public bool CanConnect(Game game){
		if(_data.Count > 0){
			return false;
		}else{
			return true;
		}
	}
	public bool CanWalk(Game game){
		return !(_data.Count > 0);
	}
	public int GetAtlasIndex(Game game){
		return 1;
	}
	public int GetUVFactor(Game game){
		return 1;
	}
	public override void Disassemble(Game game){
		
	}
	public override IListData GetIListData(){
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
