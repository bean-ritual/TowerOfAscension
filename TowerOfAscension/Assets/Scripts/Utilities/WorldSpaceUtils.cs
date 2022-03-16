using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class WorldSpaceUtils{
	public static Vector3 MouseToWorldSpace(Camera cam){
		return cam.ScreenToWorldPoint(Input.mousePosition);
	}
	public static Vector3Int MouseToWorldSpaceV3Int(Camera cam){
		return Vector3Int.FloorToInt(MouseToWorldSpace(cam));
	}
}

