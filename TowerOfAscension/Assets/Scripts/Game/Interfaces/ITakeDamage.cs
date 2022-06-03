using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITakeDamage{
	void TakeTrueDamage(Game game, int damage);
	//
	void TakeSlashingDamage(Game game, int damage);
	void TakeCrushingDamage(Game game, int damage);
	void TakePiercingDamage(Game game, int damage);
	//
	void TakeMagicDamage(Game game, int damage);
	void TakePoisonDamage(Game game, int damage);
	//
	void TakeWoodDamage(Game game, int damage);
	void TakeFireDamage(Game game, int damage);
	void TakeWaterDamage(Game game, int damage);
	void TakeEarthDamage(Game game, int damage);
	//void TakeMetalDamage(Game game, int damage);
}
