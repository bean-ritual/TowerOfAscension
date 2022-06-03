using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Wall : 
	Block.BaseBlock,
	ITile,
	MapMeshManager.ITileMeshData
	{
	private int _x;
	private int _y;
	public Wall(int x, int y){
		_x = x;
		_y = y;
	}
	public Map.Tile GetTile(Game game){
		return game.GetMap().Get(_x, _y);
	}
	public int GetAtlasIndex(Game game){
		return 2;
	}
	public int GetUVFactor(Game game){
		return 1;
	}
	public override void Disassemble(Game game){
		game.GetMap().Get(_x, _y).GetIDataTile().ClearData(game);
	}
	public override ITile GetITile(){
		return this;
	}
	public override MapMeshManager.ITileMeshData GetITileMeshData(){
		return this;
	}
}
