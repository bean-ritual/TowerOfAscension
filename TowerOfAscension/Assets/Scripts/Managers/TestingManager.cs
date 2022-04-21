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
		_local.GetPlayer().GetPositionable().GetPosition(_local, out int x, out int y);
		UnityEngine.Debug.Log(_local.GetPlayer().GetTaggable().GetTag(_local, Tag.ID.WorldUnit).GetICondition().Check(_local, _local.GetPlayer()) + "");
	}
}
