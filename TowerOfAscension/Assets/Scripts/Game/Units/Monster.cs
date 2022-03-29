using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Monster :
	AIUnit
	{
	public static class MONSTER_DATA{
		public static Unit GetLevelledMonster(int level){
			return new Monster();
		}
	}
	private static readonly Vector3 _UI_OFFSET = new Vector3(0, 0.9f);
	public Monster(){
		_ai = new ScanAI();
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Rat);
		_controller.SetSortingOrder(20);
		_controller.SetUISortingOrder(100);
		_controller.SetUIOffset(_UI_OFFSET);
		_controller.SetHealthBarActive(true);
		_health = new Health(5);
	}
}
