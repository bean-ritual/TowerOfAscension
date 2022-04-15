using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Monster :
	AIUnit
	{
	public static class MONSTER_DATA{
		public static Unit GetLevelledMonster(Game game, int level){
			return new Monster(game);
		}
	}
	private static readonly Vector3 _UI_OFFSET = new Vector3(0, 0.9f);
	public Monster(Game game){
		_ai = new ScanAI();
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Rat);
		_controller.SetSortingOrder(20);
		_controller.SetUISortingOrder(100);
		_controller.SetUIOffset(_UI_OFFSET);
		_controller.SetHealthBarActive(true);
		AddTag(game, Alive.Create());
		AddTag(game, Health.Create(5));
		AddTag(game, BasicAttack.Create());
		AddTag(game, Attackable.Create());
		AddTag(game, Value.Create(Tag.ID.Damage_Physical, 1));
	}
}
