using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Wall : 
	Block.BaseBlock,
	MapMeshManager.ITileMeshData
	{
	private int _x;
	private int _y;
	public Wall(int x, int y){
		_x = x;
		_y = y;
	}
	public int GetAtlasIndex(Game game){
		return 2;
	}
	public int GetUVFactor(Game game){
		return 1;
	}
	public override void Disassemble(Game game){
		
	}
	public override MapMeshManager.ITileMeshData GetITileMeshData(){
		return this;
	}
}
