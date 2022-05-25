using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestingManager : MonoBehaviour{
	//private int _value = 0;
	private Game _local = Game.GetNullGame();
	private void Start(){
		_local = DungeonMaster.GetInstance().GetGame();
	}
	private void Update(){
		if(Input.GetKeyDown(KeyCode.T)){
			Test();
		}
	}
	public void Test(){
		int active = PlayerController.GetInstance().GetPlayer().GetID();
		UnityEngine.Debug.Log(_local.GetGameWorld().GetCurrentDataID(_local) + " " + active);
	}
}
