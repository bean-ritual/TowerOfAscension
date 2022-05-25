using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WorldPosition : 
	Block.BaseBlock,
	IWorldPosition
	{
	private int _x;
	private int _y;
	private bool _spawned;
	public WorldPosition(){
		_x = -1;
		_y = -1;
		_spawned = false;
	}
	public void Spawn(Game game, int x, int y){
		if(!_spawned){
			game.GetGameWorld().Spawn(game, GetSelf(game).GetID());
			_spawned = true;
			SetPosition(game, x, y);
		}
	}
	public void Despawn(Game game){
		if(_spawned){
			game.GetGameWorld().Despawn(game, GetSelf(game).GetID());
			_spawned = false;
			ClearPosition(game);
		}
	}
	public void SetPosition(Game game, int x, int y){
		ClearPosition(game);
		_x = x;
		_y = y;
		game.GetMap().Get(x, y).GetIDataTile().GetData(game).GetBlock(game, 0).GetIListData().AddData(game, GetSelf(game));
		WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.PositionUpdateAnimation(game, GetSelf(game)));
	}
	public void ClearPosition(Game game){
		game.GetMap().Get(_x, _y).GetIDataTile().GetData(game).GetBlock(game, 0).GetIListData().RemoveData(game, GetSelf(game));
		_x = -1;
		_y = -1;
	}
	public Map.Tile GetTile(Game game){
		return game.GetMap().Get(_x, _y);
	}
	public Vector3 GetPosition(Game game){
		return game.GetMap().GetWorldPosition(_x, _y);
	}
	public int GetX(){
		return _x;
	}
	public int GetY(){
		return _y;
	}
	public override void Disassemble(Game game){
		Despawn(game);
	}
	public override IWorldPosition GetIWorldPosition(){
		return this;
	}
}
