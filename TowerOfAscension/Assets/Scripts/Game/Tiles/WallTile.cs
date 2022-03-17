using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WallTile : 
	Tile,
	LevelMeshManager.ITileMeshData,
	LightMeshManager.ILightMeshData,
	Tile.IPrintable,
	Tile.ILightable,
	Level.ILightControl,
	Tile.IDiscoverable
	{
	private int _light;
	private bool _discovered;
	public WallTile(int x, int y) : base(x, y){
		_light = 0;
		_discovered = false;
	}
	public int GetAtlasIndex(){
		const int PATH_INDEX = 2;
		return PATH_INDEX;
	}
	public int GetUVFactor(){
		const int PATH_FACTOR = 1;
		return PATH_FACTOR;
	}
	public int GetLightAtlasIndex(){
		const int DARKNESS = 0;
		const int SHADE = 1;
		if(_discovered){
			return SHADE;
		}
		return DARKNESS;
	}
	public int GetLightUVFactor(){
		const int LIT_FACTOR = 0;
		const int UNLIT_FACTOR = 1;
		if(_light > 0){
			return LIT_FACTOR;
		}
		return UNLIT_FACTOR;
	}
	public void Print(Level level, Unit master, BluePrint.Print print, Tile tile, int x, int y){
		level.Set(x, y, tile);
		print.OnSpawn(level, master, tile, x, y);
	}
	public bool CanPrint(Level level, Unit master, int x, int y){
		return true;
	}
	public void SetLight(int light){
		_light = light;
	}
	public int GetLight(){
		return _light;
	}
	public bool CheckTransparency(Level level){
		return false;
	}
	public void Discover(Level level, Unit unit){
		_discovered = true;
	}
	public override LevelMeshManager.ITileMeshData GetTileMeshData(){
		return this;
	}
	public override LightMeshManager.ILightMeshData GetLightMeshData(){
		return this;
	}
	public override Tile.IPrintable GetPrintable(){
		return this;
	}
	public override Tile.ILightable GetLightable(){
		return this;
	}
	public override Level.ILightControl GetLightControl(){
		return this;
	}
	public override Tile.IDiscoverable GetDiscoverable(){
		return this;
	}
}
