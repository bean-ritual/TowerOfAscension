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
	public void Test(){
		UnityEngine.Debug.Log(
			_local.GetPlayer().GetHasArmour().GetArmour(_local).GetValue() + ""
		);
	}
}
