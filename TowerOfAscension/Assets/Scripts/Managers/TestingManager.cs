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
		_local.GetPlayer().GetTag(_local, Tag.ID.Health).GetIDamageValue1Int().DamageValue1(_local, _local.GetPlayer(), 999);
		//UnityEngine.Debug.Log(_local.GetPlayer().GetTag(_local, Tag.ID.Position).GetIGetVector().GetVector(_local, _local.GetPlayer()));
	}
}
