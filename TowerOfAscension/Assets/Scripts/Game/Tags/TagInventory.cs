using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TagInventory : 
	Tag,
	Tag.IAdd<Unit>,
	Tag.IRemove<Register<Unit>.ID>,
	Tag.IGetRegisterEvents
	{
	private static Queue<TagInventory> _POOL = new Queue<TagInventory>();
	private const Tag.ID _TAG_ID = Tag.ID.Inventory;
	private Inventory _inventory = Inventory.GetNullInventory();
	public void Setup(){
		_inventory = new Inventory();
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void Add(Game game, Unit self, Unit item){
		item.GetSpawnable().Despawn(game);
		item.GetRegisterable().Add(_inventory);
	}
	public void Remove(Game game, Unit self, Register<Unit>.ID itemID){
		_inventory.Get(itemID).GetRegisterable().Remove(_inventory);
	}
	public Register<Unit>.IRegisterEvents GetRegisterEvents(Game game, Unit self){
		return _inventory;
	}
	public override Tag.IAdd<Unit> GetIAddUnit(){
		return this;
	}
	public override Tag.IRemove<Register<Unit>.ID> GetIRemoveUnitID(){
		return this;
	}
	public override Tag.IGetRegisterEvents GetIGetRegisterEvents(){
		return this;
	}
	public static Tag Create(){
		TagInventory tag = new TagInventory();
		tag.Setup();
		return tag;
	}
}
