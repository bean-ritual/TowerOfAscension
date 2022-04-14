using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack{
	public static void Create(Game game, Direction direction, Unit attacker, Unit damage){
		Tile tile = direction.GetTileFromUnit(game, attacker);
		tile.GetHasUnits().DoUnits(game, (Tile tile, Unit unit) => {
			int physical = damage.GetTaggable().GetTag(game, Tag.ID.Damage_Physical).GetIGetIntValue1().GetIntValue1(game, damage);
			if(physical > 0){
				unit.GetTaggable().GetTag(game, Tag.ID.Health).GetIDamageValue1Int().DamageValue1(game, unit, physical);
			}
			attacker.GetVisualController().GetVisualController(game).InvokeAttackAnimation((attacker.GetPositionable().GetPosition(game) + unit.GetPositionable().GetPosition(game)) / 2);
			return true;
		});
		attacker.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, attacker);
	}
}
