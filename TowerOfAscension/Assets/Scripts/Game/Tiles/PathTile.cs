using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PathTile : 
	Tile,
	LevelMeshManager.ITileMeshData,
	Tile.IPrintable,
	Tile.IConnectable
	{
	public PathTile(int x, int y) : base(x, y){}
	public int GetAtlasIndex(){
		const int PATH_INDEX = 1;
		return PATH_INDEX;
	}
	public int GetUVFactor(){
		const int PATH_FACTOR = 1;
		return PATH_FACTOR;
	}
	public void Print(Level level, ClassicGen master, BluePrint.Print print, Tile tile, int x, int y){}
	public bool CanPrint(Level level, ClassicGen master, int x, int y){
		return false;
	}
	public bool CanConnect(Level level, ClassicGen master, int x, int y){
		return true;
	}
	public override LevelMeshManager.ITileMeshData GetTileMeshData(){
		return this;
	}
	public override Tile.IPrintable GetPrintable(){
		return this;
	}
	public override Tile.IConnectable GetConnectable(){
		return this;
	}
}
