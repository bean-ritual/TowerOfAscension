using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestingManager : MonoBehaviour{
	private Game _local = Game.GetNullGame();
	private void Start(){
		_local = DungeonMaster.GetInstance().GetLocalGame();
	}
	private void Update(){
		if(Input.GetKeyDown(KeyCode.T)){
			Test();
		}
	}
	private void Test(){
		//_local.GameOver();
		_local.GetPlayer().GetHasHealth().GetHealth(_local).Damage(_local, _local.GetPlayer(), 100);
	}
}
