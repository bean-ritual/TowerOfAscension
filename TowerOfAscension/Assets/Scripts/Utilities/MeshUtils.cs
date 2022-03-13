using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class MeshUtils{
	public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uv, out int[] triangles){
		vertices = new Vector3[4 * quadCount];
		uv = new Vector2[4 * quadCount];
		triangles = new int[6 *  quadCount]; 
	}
	public static void GetMeshArrays(Mesh mesh, out Vector3[] vertices, out Vector2[] uv, out int[] triangles){
		vertices = mesh.vertices;
		uv = mesh.uv;
		triangles = mesh.triangles;
	}
	public static Mesh CreateEmptyMesh(){
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[0];
		mesh.uv = new Vector2[0];
		mesh.triangles = new int[0];
		return mesh;
	}
	public static Mesh CreateMesh(Vector3 position, Vector3 baseSize, Vector2 uv00, Vector2 uv11){
		return AddToMesh(null, position, baseSize, uv00, uv11);
	}
	public static Mesh AddToMesh(Mesh mesh, Vector3 position, Vector3 baseSize, Vector2 uv00, Vector2 uv11){
		if(mesh == null){
			mesh = CreateEmptyMesh();
		}
		Vector3[] vertices = new Vector3[4 + mesh.vertices.Length];
		Vector2[] uv = new Vector2[4 + mesh.uv.Length];
		int[] triangles = new int[6 + mesh.triangles.Length];
		mesh.vertices.CopyTo(vertices, 0);
		mesh.uv.CopyTo(uv, 0);
		mesh.triangles.CopyTo(triangles, 0);
		int index = (vertices.Length / 4) - 1;
		int vIndex = index * 4;
		int vIndex0 = vIndex;
		int vIndex1 = vIndex + 1;
		int vIndex2 = vIndex + 2;
		int vIndex3 = vIndex + 3;
		baseSize *= 0.5f;
		vertices[vIndex0] = position + new Vector3(-baseSize.x, baseSize.y);
		vertices[vIndex1] = position + new Vector3(-baseSize.x, -baseSize.y);
		vertices[vIndex2] = position + new Vector3(baseSize.x, -baseSize.y);
		vertices[vIndex3] = position + baseSize;
		uv[vIndex0] = new Vector2(uv00.x, uv11.y);
		uv[vIndex1] = new Vector2(uv00.x, uv00.y);
		uv[vIndex2] = new Vector2(uv11.x, uv00.y);
		uv[vIndex3] = new Vector2(uv11.x, uv11.y);
		int tIndex = index * 6;
		triangles[tIndex + 0] = vIndex0;
		triangles[tIndex + 1] = vIndex3;
		triangles[tIndex + 2] = vIndex1;
		triangles[tIndex + 3] = vIndex1;
		triangles[tIndex + 4] = vIndex3;
		triangles[tIndex + 5] = vIndex2;
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		return mesh;
	}
	public static void ModifyMeshAtIndex(int index, Vector3[] vertices, Vector2[] uv, int[] triangles, Vector3 position, Vector3 baseSize, Vector2 uv00, Vector2 uv11){
		int vIndex = index * 4;
		int vIndex0 = vIndex;
		int vIndex1 = vIndex + 1;
		int vIndex2 = vIndex + 2;
		int vIndex3 = vIndex + 3;
		baseSize *= 0.5f;
		vertices[vIndex0] = position + new Vector3(-baseSize.x, baseSize.y);
		vertices[vIndex1] = position + new Vector3(-baseSize.x, -baseSize.y);
		vertices[vIndex2] = position + new Vector3(baseSize.x, -baseSize.y);
		vertices[vIndex3] = position + baseSize;
		uv[vIndex0] = new Vector2(uv00.x, uv11.y);
		uv[vIndex1] = new Vector2(uv00.x, uv00.y);
		uv[vIndex2] = new Vector2(uv11.x, uv00.y);
		uv[vIndex3] = new Vector2(uv11.x, uv11.y);
		int tIndex = index * 6;
		triangles[tIndex + 0] = vIndex0;
		triangles[tIndex + 1] = vIndex3;
		triangles[tIndex + 2] = vIndex1;
		triangles[tIndex + 3] = vIndex1;
		triangles[tIndex + 4] = vIndex3;
		triangles[tIndex + 5] = vIndex2;
	}
	public static void SetFinalMeshArrays(Mesh mesh, Vector3[] vertices, Vector2[] uv, int[] triangles){
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
	}
}
