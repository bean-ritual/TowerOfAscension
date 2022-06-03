using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stats : 
	Block.BaseBlock,
	IStats,
	IKillable
	{
	private int _killer;
	private bool _alive;
	private int _level;
	private int _health;
	private int _maxHealth;
	public Stats(int level, int health){
		_killer = -1;
		_alive = true;
		_level = level;
		_health = health;
		_maxHealth = health;
	}
	public bool GetAlive(Game game){
		return _alive;
	}
	public int GetLevel(Game game){
		return _level;
	}
	public int GetHealth(Game game){
		return _health;
	}
	public int GetMaxHealth(Game game){
		return _maxHealth;
	}
	public void Kill(Game game){
		GetSelf(game).GetBlock(game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().Despawn(game);
	}
	public void SetKiller(Game game, Data killer){
		_killer = killer.GetID();
	}
	public override void Disassemble(Game game){
		
	}
	public override IStats GetIStats(){
		return this;
	}
	public override IKillable GetIKillable(){
		return this;
	}
}
