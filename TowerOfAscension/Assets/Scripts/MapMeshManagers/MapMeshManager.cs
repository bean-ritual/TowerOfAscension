using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapMeshManager : MonoBehaviour{
	public interface ITileMeshData{
		int GetAtlasIndex(Game game);
		int GetUVFactor(Game game);
	}
	//
	private Game _game = Game.GetNullGame();
	private Map _map = Map.GetNullMap();
	//
	private Mesh _mesh;
	private UVAtlas _atlas;
	private Vector3[] _vertices;
	private Vector2[] _uv;
	private int[] _triangles;
	private bool _update;
	[SerializeField]private MeshFilter _meshFilter;
	[SerializeField]private MeshRenderer _meshRenderer;
	[SerializeField]private Material _material;
	[SerializeField]private int _atlasID;
	[SerializeField]private int _blockID;
	private void OnDestroy(){
		_map.OnMapUpdate -= OnMapUpdate;
		_map.OnTileUpdate -= OnTileUpdate;
		_game.GetGameBlocks(_blockID).OnGameBlockUpdate -= OnGameBlockUpdate;
	}
	private void Start(){
		_game = DungeonMaster.GetInstance().GetGame();
		_map = _game.GetMap();
		_atlas = UVAtlas.UVATLAS_DATA.GetUVAtlas(_atlasID);
		_meshRenderer.material = _material;
		_meshRenderer.material.mainTexture = _atlas.GetTexture();
		_mesh = MeshUtils.CreateEmptyMesh();
		_meshFilter.mesh = _mesh;
		MeshUtils.CreateEmptyMeshArrays(_map.GetArea(), out _vertices, out _uv, out _triangles);
		_map.OnMapUpdate += OnMapUpdate;
		_map.OnTileUpdate += OnTileUpdate;
		_game.GetGameBlocks(_blockID).OnGameBlockUpdate += OnGameBlockUpdate;
		Build();
	}
	private void LateUpdate(){
		if(_update){
			MeshUtils.SetFinalMeshArrays(_mesh, _vertices, _uv, _triangles);
			_update = false;
		}
	}
	public void Build(){
		for(int x = 0; x < _map.GetWidth(); x++){
			for(int y = 0; y < _map.GetHeight(); y++){
				int index = x * _map.GetHeight() + y;
				ITileMeshData tile = _map.Get(x, y).GetIDataTile().GetBlock(_game, _blockID).GetITileMeshData();
				Vector3 quad = _map.GetVector3TileSize() * tile.GetUVFactor(_game);
				_atlas.GetUVNormal(tile.GetAtlasIndex(_game), out Vector2 uv00, out Vector2 uv11);
				MeshUtils.ModifyMeshAtIndex(index, _vertices, _uv, _triangles, _map.GetWorldPosition(x, y) + (quad * _map.GetTileOffset()), quad, uv00, uv11);
			}
		}
		_update = true;
	}
	public void Build(int x, int y){
		int index = x * _map.GetHeight() + y;
		ITileMeshData tile = _map.Get(x, y).GetIDataTile().GetBlock(_game, _blockID).GetITileMeshData();
		Vector3 quad = _map.GetVector3TileSize() * tile.GetUVFactor(_game);
		_atlas.GetUVNormal(tile.GetAtlasIndex(_game), out Vector2 uv00, out Vector2 uv11);
		MeshUtils.ModifyMeshAtIndex(index, _vertices, _uv, _triangles, _map.GetWorldPosition(x, y) + (quad * _map.GetTileOffset()), quad, uv00, uv11);
		_update = true;
	}
	private void OnMapUpdate(object sender, EventArgs e){
		Build();
	}
	private void OnTileUpdate(object sender, Map.OnTileUpdateEventArgs e){
		Build(e.x, e.y);
	}
	private void OnGameBlockUpdate(object sender, GameBlocks.GameBlockUpdateEventArgs e){
		Build();
	}
}
