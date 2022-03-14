using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WallTile : 
	Tile,
	LevelMeshManager.ITileMeshData,
	Tile.IPrintable
	{
	public WallTile(int x, int y) : base(x, y){}
	public int GetAtlasIndex(){
		const int PATH_INDEX = 2;
		return PATH_INDEX;
	}
	public int GetUVFactor(){
		const int PATH_FACTOR = 1;
		return PATH_FACTOR;
	}
	public void Print(Level level, ClassicGen master, BluePrint.Print print, Tile tile){
		level.Set(_x, _y, tile);
		print.OnSpawn(level, master, tile);
	}
	public bool Check(Level level, ClassicGen master){
		return true;
	}
	public override LevelMeshManager.ITileMeshData GetTileMeshData(){
		return this;
	}
	public override Tile.IPrintable GetPrintable(){
		return this;
	}
}
