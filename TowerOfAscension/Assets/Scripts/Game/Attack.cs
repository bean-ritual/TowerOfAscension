using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack{
	public static void Create(Game game, Direction direction, Unit attacker, Unit damage){
		Tile tile = direction.GetTile(game, attacker.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, attacker));
		tile.GetHasUnits().DoUnits(game, (Tile tile, Unit unit) => {
			unit.GetTag(game, Tag.ID.Attackable).GetIInput2Units().Input(game, unit, attacker, damage);
			return true;
		});
	}
}
