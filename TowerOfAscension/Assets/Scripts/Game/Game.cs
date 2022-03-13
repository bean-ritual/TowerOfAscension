using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Game{
	[Serializable]
	public class NullGame : Game{
		public NullGame(){}
		public override bool Process(){
			return false;
		}
		public override void NewLevel(){}
		public override Level GetLevel(){
			return Level.GetNullLevel();
		}
	}
	[field:NonSerialized]private static readonly NullGame _NULL_GAME = new NullGame();
	private Level _level;
	public Game(){
		_level = Level.GetNullLevel();
	}
	public virtual bool Process(){
		return _level.Process();
	}
	public virtual void NewLevel(){
		_level = new Level(100, 100);
	}
	public virtual Level GetLevel(){
		return _level;
	}
	public static Game GetNullGame(){
		return _NULL_GAME;
	}
}
