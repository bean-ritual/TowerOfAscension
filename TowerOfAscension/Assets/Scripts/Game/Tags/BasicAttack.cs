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
	private Tag.ID _skill = Tag.ID.Null;
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){}
	public void Input(Game game, Unit self, Unit caster, Direction direction){
		const int MIN_ROLL = 0;
		const int MAX_ROLL = 100;
		Tile tile = direction.GetTile(game, caster.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, caster));
		tile.GetHasUnits().DoUnits(game, (Tile tile, Unit target) => {
			//accuracy
			int accuracy = (int)Mathf.Clamp(
				(UnityEngine.Random.Range(MIN_ROLL, MAX_ROLL) +
				(float)target.GetTag(game, Tag.ID.Level).GetIReduceInt().Reduce(game, target, caster.GetTag(game, Tag.ID.Level).GetIGetIntValue1().GetIntValue1(game, caster)))
				/ 2,
				MIN_ROLL, 
				MAX_ROLL 
			);
			if(UnityEngine.Random.Range(MIN_ROLL, MAX_ROLL) < accuracy){
				target.GetTag(game, Tag.ID.WorldUnit).GetIGetWorldUnitController().GetWorldUnitController(game, caster).InvokeTextPopupEvent("Miss!");
				return true;
			}
			target.GetTag(game, Tag.ID.Attackable).GetIInput2Units().Input(game, target, caster, self);
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
