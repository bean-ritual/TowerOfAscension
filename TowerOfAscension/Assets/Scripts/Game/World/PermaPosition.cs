using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PermaPosition : 
	Block.BaseBlock,
	IWorldPosition
	{
	private int _x;
	private int _y;
	private bool _spawned;
	public PermaPosition(){
		_x = -1;
		_y = -1;
		_spawned = false;
	}
	public void Spawn(Game game, int x, int y){
		if(!_spawned){
			game.GetGameWorld().Spawn(game, GetSelf(game).GetID());
			_spawned = true;
			_x = x;
			_y = y;
		}
	}
	public void Despawn(Game game){
		if(_spawned){
			game.GetGameWorld().Despawn(game, GetSelf(game).GetID());
			_spawned = false;
			_x = -1;
			_y = -1;
		}
	}
	public void SetPosition(Game game, int x, int y){}
	public void ClearPosition(Game game){}
	public Map.Tile GetTile(Game game){
		return game.GetMap().Get(_x, _y);
	}
	public Vector3 GetPosition(Game game){
		return game.GetMap().GetWorldPosition(_x, _y);
	}
	public override void Disassemble(Game game){
		Despawn(game);
	}
	public override IWorldPosition GetIWorldPosition(){
		return this;
	}
}
