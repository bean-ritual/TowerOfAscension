using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestingManager : MonoBehaviour{
	//private int _value = 0;
	private Game _test = Game.GetNullGame();
	private void Start(){
		_test = DungeonMaster.GetInstance().GetGame();
	}
	private void Update(){
		if(Input.GetKeyDown(KeyCode.T)){
			Test();
		}
	}
	public void Test(){
		_test.GameOver();
	}
}
