using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestingManager : MonoBehaviour{
	private Level _level = Level.GetNullLevel();
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
	}
	private void Update(){
		if(Input.GetKeyDown(KeyCode.T)){
			Test();
		}
	}
	private void Test(){

	}
}
