using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileTargetManager : MonoBehaviour{
	private static TileTargetManager _instance;
	private Level _level = Level.GetNullLevel();
	private Tile _tile = Tile.GetNullTile();
	[SerializeField]private Transform _target;
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private int _sortingOrder;
	private void Awake(){
		if(_instance != null){
			Destroy(gameObject);
		}
		_instance = this;
	}
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
		_renderer.sortingOrder = _sortingOrder;
		_renderer.color = Color.green;
		SetTile(_tile);
	}
	public void SetTile(Tile tile){
		_tile = tile;
		_tile.GetXY(out int x, out int y);
		_target.localPosition = _level.GetWorldPosition(x, y) + _level.GetVector3CellOffset();
	}
	public static TileTargetManager GetInstance(){
		return _instance;
	}
}
