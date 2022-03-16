using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(DungeonMaster))]
public class DungeonMasterEditor : Editor{
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();
		DungeonMaster master = (DungeonMaster)target;
		/*
		if(GUILayout.Button("Process")){
			master.Process();
		}
		if(GUILayout.Button("Play")){
			master.Play();
		}
		if(GUILayout.Button("Pause")){
			master.Pause();
		}
		*/
		if(GUILayout.Button("Save")){
			master.Save();
		}
		if(GUILayout.Button("Load")){
			master.Load();
		}
	}
}
