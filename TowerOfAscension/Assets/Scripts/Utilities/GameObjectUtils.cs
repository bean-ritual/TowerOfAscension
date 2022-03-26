using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameObjectUtils{
	public static void DestroyAllChildren(Transform target){
		while(target.childCount > 0){
			Transform t = target.GetChild(0);
			t.SetParent(null);
			Object.Destroy(t.gameObject);
		}
	}
}
