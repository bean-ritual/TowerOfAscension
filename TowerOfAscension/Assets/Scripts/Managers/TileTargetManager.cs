using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileTargetManager : 
	MonoBehaviour,
	TileTargetManager.ITileTargetManager
	{
	public interface ITileTargetManager{
		void SetTile(Map.Tile tile);
	}
	[Serializable]
	public class NullTileTargetManager : ITileTargetManager{
		public void SetTile(Map.Tile tile){}
	}
	//
	private static TileTargetManager _INSTANCE;
	private Game _game = Game.GetNullGame();
	private Map _map = Map.GetNullMap();
	private Map.Tile _tile = Map.Tile.GetNullTile();
	[SerializeField]private Transform _target;
	[SerializeField]private SpriteRenderer _renderer;
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
		}
		_INSTANCE = this;
	}
	private void Start(){
		_game = DungeonMaster.GetInstance().GetGame();
		_map = _game.GetMap();
		_renderer.color = Color.green;
		SetTile(_tile);
	}
	public void SetTile(Map.Tile tile){
		_tile = tile;
		_target.localPosition = _tile.GetPosition(_map) + _map.GetVector3TileOffset();
	}
	//
	private static NullTileTargetManager _NULL_TILE_TARGET_MANAGER = new NullTileTargetManager();
	public static ITileTargetManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_TILE_TARGET_MANAGER;
		}else{
			return _INSTANCE;
		}
	}
}
