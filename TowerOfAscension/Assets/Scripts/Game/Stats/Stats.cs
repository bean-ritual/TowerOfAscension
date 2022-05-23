using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stats : 
	Block.BaseBlock,
	IStats
	{
	private bool _alive;
	private int _level;
	private int _health;
	private int _baseHealth;
	private int _scaleHealth;
	public Stats(Game game, int level, int baseHealth, int scaleHealth){
		_alive = true;
		_baseHealth = baseHealth;
		_scaleHealth = scaleHealth;
		SetLevel(game, level);
		SetHealth(game, GetMaxHealth(game));
	}
	public void SetLevel(Game game, int level){
		const int MIN_LEVEL = 1;
		const int MAX_LEVEL = 99;
		_level = Mathf.Clamp(level, MIN_LEVEL, MAX_LEVEL);
		SetHealth(game, GetHealth(game));
	}
	public void SetHealth(Game game, int health){
		const int MIN_HEALTH = 0;
		_health = Mathf.Clamp(health, MIN_HEALTH, GetMaxHealth(game));
		if(_health <= MIN_HEALTH){
			_alive = false;
		}
		FireBlockUpdateEvent(game);
	}
	public int GetHealth(Game game){
		return _health;
	}
	public int GetMaxHealth(Game game){
		return _baseHealth + (_scaleHealth * _level);
	}
	public override void Disassemble(Game game){
		
	}
	public override IStats GetIStats(){
		return this;
	}
}
