using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelMeshManager : MonoBehaviour{
	private static LevelMeshManager _instance;
	private Level _level;
	private Mesh _mesh;
	private UVAtlas _atlas;
	private Vector3[] _vertices;
	private Vector2[] _uv;
	private int[] _triangles;
	[SerializeField]private MeshFilter _meshFilter;
	[SerializeField]private MeshRenderer _meshRenderer;
	[SerializeField]private UVAtlas.AtlasID _atlasID;
	[SerializeField]private Material _material;
	[SerializeField]private int _sortingOrder;
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
		_atlas = UVAtlas.UVATLAS_DATA.GetUVAtlas(_atlasID);
		_meshRenderer.material = _material;
		_meshRenderer.material.mainTexture = _atlas.GetTexture();
		_meshRenderer.sortingOrder = _sortingOrder;
		_mesh = MeshUtils.CreateEmptyMesh();
		_meshFilter.mesh = _mesh;
		MeshUtils.CreateEmptyMeshArrays(_level.GetArea(), out _vertices, out _uv, out _triangles);
		Build();
	}
	public void Build(){
		for(int x = 0; x < _level.GetWidth(); x++){
			for(int y = 0; y < _level.GetHeight(); y++){
				int index = x * _level.GetHeight() + y;
				CalculateUV(_level.Get(x, y), out Vector2 uv00, out Vector2 uv11, out Vector3 quad);
				MeshUtils.ModifyMeshAtIndex(index, _vertices, _uv, _triangles, _level.GetWorldPosition(x, y) + (quad * _level.GetCellOffset()), quad, uv00, uv11);
			}
		}
		MeshUtils.SetFinalMeshArrays(_mesh, _vertices, _uv, _triangles);
	}
	private void CalculateUV(Tile tile, out Vector2 uv00, out Vector2 uv11, out Vector3 quad){
		if(tile.GetTerrain() == Tile.Terrain.Null){
			quad = Vector3.zero;
		}else{
			quad = _level.GetVector3CellSize();
		}
		_atlas.GetUVNormal(tile.GetAtlasIndex(), out uv00, out uv11);
	}
	public static LevelMeshManager GetInstance(){
		return _instance;
	}
}
