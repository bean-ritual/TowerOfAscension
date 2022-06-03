using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DataTile : 
	Map.Tile.BaseTile,
	DataTile.IDataTile,
	MapMeshManager.ITileMeshData
	{
	public interface IDataTile{
		void SetData(Game game, Data data);
		void ClearData(Game game);
		Data GetData(Game game);
		Block GetBlock(Game game, int blockID);
	}
	private int _dataID;
	public DataTile(int x, int y) : base(x, y){
		_dataID = -1;
	}
	public void SetData(Game game, Data data){
		game.GetGameData().Get(_dataID).Disassemble(game);
		_dataID = data.GetID();
		GetXY(out int x, out int y);
		data.GetBlock(game, 1).GetIWorldPosition().Spawn(game, x, y);
		game.GetMap().FireTileUpdateEvent(x, y);
	}
	public void ClearData(Game game){
		_dataID = -1;
		game.GetMap().FireTileUpdateEvent(GetX(), GetY());
	}
	public Data GetData(Game game){
		return game.GetGameData().Get(_dataID);
	}
	public Block GetBlock(Game game, int blockID){
		return game.GetGameBlocks(blockID).Get(game, _dataID);
	}
	public int GetAtlasIndex(Game game){
		return game.GetGameBlocks(0).Get(game, _dataID).GetITileMeshData().GetAtlasIndex(game);
	}
	public int GetUVFactor(Game game){
		return game.GetGameBlocks(0).Get(game, _dataID).GetITileMeshData().GetUVFactor(game);
	}
	public override DataTile.IDataTile GetIDataTile(){
		return this;
	}
	public override MapMeshManager.ITileMeshData GetITileMeshData(){
		return this;
	}
}
