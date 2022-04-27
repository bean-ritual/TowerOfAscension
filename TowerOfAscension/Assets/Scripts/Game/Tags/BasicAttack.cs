using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BasicAttack : 
	Tag,
	Tag.IInput<Unit, Direction>
	{
	private const Tag.ID _TAG_ID = Tag.ID.Active;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){}
	public void Input(Game game, Unit self, Unit caster, Direction direction){
		const int MIN_ROLL = 5;
		const int MAX_ROLL = 95;
		const int BASE_ACCURACY = 75;
		Tile tile = direction.GetTile(game, caster.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, caster));
		tile.GetHasUnits().DoUnits(game, (Tile tile, Unit target) => {
			//accuracy
			int attack = caster.GetTag(game, Tag.ID.Level).GetIGetIntValue1().GetIntValue1(game, caster);
			int defence = target.GetTag(game, Tag.ID.Level).GetIGetIntValue1().GetIntValue1(game, target);
			int accuracy = (int)Mathf.Clamp(BASE_ACCURACY + (attack - defence) * 1.5f, MIN_ROLL, MAX_ROLL);
			if(UnityEngine.Random.Range(0, 100) < accuracy){
				target.GetTag(game, Tag.ID.Attackable).GetIInput2Units().Input(game, target, caster, self);
			}else{
				target.GetTag(game, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(game, caster).InvokeTextPopupEvent("Miss!");
			}
			return true;
		});
		caster.GetTag(game, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(game, caster).InvokeMeleeAttackAnimation(direction);
		caster.GetTag(game, Tag.ID.AI).GetIClear().Clear(game, caster);
	}
	public override Tag.IInput<Unit, Direction> GetIInputUnitDirection(){
		return this;
	}
	public static Tag Create(){
		return new BasicAttack();
	}
}
