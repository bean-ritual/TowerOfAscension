using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackgroundMeshManager : MonoBehaviour{
	private Mesh _mesh;
	private UVAtlas _atlas;
	private Vector3[] _vertices;
	private Vector2[] _uv;
	private int[] _triangles;
	[SerializeField]private CameraManager _camera;
	[SerializeField]private MeshFilter _meshFilter;
	[SerializeField]private MeshRenderer _meshRenderer;
	[SerializeField]private Material _material;
	[SerializeField]private int _width;
	[SerializeField]private int _height;
	[SerializeField]private int _atlasID;
	[SerializeField]private int _uvIndex;
	[SerializeField]private Vector3 _cellSize;
	[SerializeField]private float _offset;
	private void Start(){
		_camera.Setup(new Vector3((_width * 0.5f), (_height * 0.5f)), 0, true);
		_atlas = UVAtlas.UVATLAS_DATA.GetUVAtlas(_atlasID);
		_meshRenderer.material = _material;
		_meshRenderer.material.mainTexture = _atlas.GetTexture();
		_mesh = MeshUtils.CreateEmptyMesh();
		_meshFilter.mesh = _mesh;
		MeshUtils.CreateEmptyMeshArrays((_width * _height), out _vertices, out _uv, out _triangles);
		Build();
	}
	public void Build(){
		Vector3 offset = _cellSize * _offset;
		_atlas.GetUVNormal(_uvIndex, out Vector2 uv00, out Vector2 uv11);
		for(int x = 0; x < _width; x++){
			for(int y = 0; y < _height; y++){
				int index = x * _height + y;
				MeshUtils.ModifyMeshAtIndex(index, _vertices, _uv, _triangles, new Vector3(x, y) + offset, _cellSize, uv00, uv11);
			}
		}
		MeshUtils.SetFinalMeshArrays(_mesh, _vertices, _uv, _triangles);
	}
}
