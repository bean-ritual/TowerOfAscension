using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightMeshManager : MonoBehaviour{
	public interface ILightMeshData{
		int GetLightAtlasIndex();
		int GetLightUVFactor();
	}
	private static LightMeshManager _instance;
	private Level _level;
	private Mesh _mesh;
	private UVAtlas _atlas;
	private Vector3[] _vertices;
	private Vector2[] _uv;
	private int[] _triangles;
	private bool _update;
	[SerializeField]private MeshFilter _meshFilter;
	[SerializeField]private MeshRenderer _meshRenderer;
	[SerializeField]private UVAtlas.AtlasID _atlasID;
	[SerializeField]private Material _material;
	[SerializeField]private int _sortingOrder;
	private void OnDestroy(){
		_level.OnGridMapChanged -= OnGridMapChanged;
		_level.OnLightUpdate -= OnLightUpdate;
	}
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
		_atlas = UVAtlas.UVATLAS_DATA.GetUVAtlas(_atlasID);
		_meshRenderer.material = _material;
		_meshRenderer.material.mainTexture = _atlas.GetTexture();
		_meshRenderer.sortingOrder = _sortingOrder;
		_mesh = MeshUtils.CreateEmptyMesh();
		_meshFilter.mesh = _mesh;
		MeshUtils.CreateEmptyMeshArrays(_level.GetArea(), out _vertices, out _uv, out _triangles);
		_level.OnGridMapChanged += OnGridMapChanged;
		_level.OnLightUpdate += OnLightUpdate;
		_update = true;
	}
	private void LateUpdate(){
		if(_update){
			Build();
		}
	}
	public void Build(){
		for(int x = 0; x < _level.GetWidth(); x++){
			for(int y = 0; y < _level.GetHeight(); y++){
				int index = x * _level.GetHeight() + y;
				Tile tile = _level.Get(x, y);
				Vector3 quad = _level.GetVector3CellSize() * tile.GetLightMeshData().GetLightUVFactor();
				_atlas.GetUVNormal(tile.GetLightMeshData().GetLightAtlasIndex(), out Vector2 uv00, out Vector2 uv11);
				MeshUtils.ModifyMeshAtIndex(index, _vertices, _uv, _triangles, _level.GetWorldPosition(x, y) + (quad * _level.GetCellOffset()), quad, uv00, uv11);
			}
		}
		MeshUtils.SetFinalMeshArrays(_mesh, _vertices, _uv, _triangles);
		_update = false;
	}
	private void OnGridMapChanged(object sender, GridMap<Tile>.OnGridMapChangedEventArgs e){
		_update = true;
	}
	private void OnLightUpdate(object sender, EventArgs e){
		_update = true;
	}
	public static LightMeshManager GetInstance(){
		return _instance;
	}
}
