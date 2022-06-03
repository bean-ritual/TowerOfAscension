using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TileLight : 
	Block.BaseBlock,
	ITileLight,
	ICanOpaque,
	MapMeshManager.ITileMeshData
	{
	private int _lightLevel;
	private bool _discovered;
	public TileLight(){
		_lightLevel = 0;
		_discovered = false;
	}
	public void SetLight(Game game, int lightLevel){
		_lightLevel = lightLevel;
	}
	public int GetLight(Game game){
		return _lightLevel;
	}
	public void Discover(Game game){
		_discovered = true;
	}
	public bool GetDiscovered(Game game){
		return _discovered;
	}
	public virtual bool CanOpaque(Game game){
		IListData listData = GetSelf(game).GetBlock(game, 0).GetIListData();
		for(int i = 0; i < listData.GetDataCount(); i++){
			if(listData.GetData(game, i).GetBlock(game, 4).GetICanOpaque().CanOpaque(game)){
				return true;
			}
		}
		return false;
	}
	public int GetAtlasIndex(Game game){
		if(_discovered){
			return 1;
		}
		return 0;
	}
	public int GetUVFactor(Game game){
		if(_lightLevel > 0){
			return 0;
		}
		return 1;
	}
	public override void Disassemble(Game game){
		
	}
	public override ITileLight GetITileLight(){
		return this;
	}
	public override ICanOpaque GetICanOpaque(){
		return this;
	}
	public override MapMeshManager.ITileMeshData GetITileMeshData(){
		return this;
	}
}
