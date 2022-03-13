using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor{
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();
		GameManager manager = (GameManager)target;
		if(GUILayout.Button("Reload")){
			manager.Reload();
		}
	}
}
