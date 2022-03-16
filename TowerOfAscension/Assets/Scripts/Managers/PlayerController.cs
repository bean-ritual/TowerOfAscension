using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour{
	private Level _level;
	private Unit _player;
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
		_player = Unit.GetNullUnit();
	}
}
