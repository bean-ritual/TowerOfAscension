using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileTargetManager : MonoBehaviour{
	private static TileTargetManager _INSTANCE;
	private Level _level = Level.GetNullLevel();
	private Tile _tile = Tile.GetNullTile();
	[SerializeField]private Transform _target;
	[SerializeField]private Transform _offset;
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private int _sortingOrder;
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
		}
		_INSTANCE = this;
	}
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
		_offset.localPosition = _level.GetVector3CellOffset();
		_renderer.sortingOrder = _sortingOrder;
		_renderer.color = Color.green;
		SetTile(_tile);
	}
	public void SetTile(Tile tile){
		_tile = tile;
		_tile.GetXY(out int x, out int y);
		_target.localPosition = _level.GetWorldPosition(x, y);
	}
	public static TileTargetManager GetInstance(){
		return _INSTANCE;
	}
}
