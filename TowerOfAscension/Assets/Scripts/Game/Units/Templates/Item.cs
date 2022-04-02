using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item : 
	LevelUnit,
	Unit.IPickupable,
	Unit.IDroppable
	{
	public static class ITEM_DATA{
		public static Unit GetLevelledItem(int level){
			return new Item();
		}
	}
	public Item(){
		_controller = new VisualController();
		_controller.SetSpriteID(SpriteSheet.SpriteID.Potions);
		_controller.SetSortingOrder(20);
	}
	public void TryPickup(Game game, Unit unit){
		unit.GetHasInventory().GetInventory(game).GetPickupable().TryPickup(game, unit, this);
	}
	public void DoPickup(Game game, Unit unit, Inventory inventory){
		GetSpawnable().Despawn(game);
		inventory.Add(this, ref _id);
		unit.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, unit);
	}
	public void TryDrop(Game game, Unit unit){
		unit.GetHasInventory().GetInventory(game).GetDroppable().TryDrop(game, unit, this);
	}
	public void DoDrop(Game game, Unit unit, Inventory inventory){
		inventory.Remove(_id);
		_id = Register<Unit>.ID.GetNullID();
		unit.GetPositionable().GetPosition(game, out int x, out int y);
		GetSpawnable().Spawn(game, x, y);
		unit.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, unit);
	}
	public override bool CheckCollision(Game game, Unit check){
		return false;
	}
	public override IPickupable GetPickupable(){
		return this;
	}
	public override IDroppable GetDroppable(){
		return this;
	}
}
