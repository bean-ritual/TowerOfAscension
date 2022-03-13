using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(DungeonMaster))]
public class DungeonMasterEditor : Editor{
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();
		DungeonMaster master = (DungeonMaster)target;
		if(GUILayout.Button("Save")){
			master.Save();
		}
		if(GUILayout.Button("Load")){
			master.Load();
		}
	}
}
