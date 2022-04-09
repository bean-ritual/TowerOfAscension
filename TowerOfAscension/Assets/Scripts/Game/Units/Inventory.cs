using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Inventory : Register<Unit>{
	public interface ISortable{
		event EventHandler<EventArgs> OnInventorySort;
		void Sort(Game game);
	}
	public interface IPickupable{
		void TryPickup(Game game, Unit holder, Unit item);
	}
	public interface IDroppable{
		void TryDrop(Game game, Unit holder, Register<Unit>.ID id);
	}
	public interface IWeaponEquippable{
		void EquipWeapon(Game game, Unit self, Register<Unit>.ID id);
		void UnequipWeapon(Game game, Unit self, Register<Unit>.ID id);
		Unit GetWeapon(Game game, Unit self);
	}
	public interface IChestplateEquippable{
		void EquipChestplate(Game game, Unit self, Register<Unit>.ID id);
		void UnequipChestplate(Game game, Unit self, Register<Unit>.ID id);
		Unit GetChestplate(Game game, Unit self);
	}
	public interface IBootsEquippable{
		void EquipBoots(Game game, Unit self, Register<Unit>.ID id);
		void UnequipBoots(Game game, Unit self, Register<Unit>.ID id);
		Unit GetBoots(Game game, Unit self);
	}
	[Serializable]
	public class NullInventory : 
		Inventory,
		Inventory.ISortable,
		Inventory.IPickupable,
		Inventory.IDroppable,
		Inventory.IWeaponEquippable,
		Inventory.IChestplateEquippable,
		Inventory.IBootsEquippable,
		EquipSlots.IHasEquipSlots
		{
		[field:NonSerialized]public event EventHandler<EventArgs> OnInventorySort;
		public NullInventory(){}
		public override bool IsNull(){
			return true;
		}
		public override void Add(Unit value, ref Register<Unit>.ID id){}
		public override bool Remove(ID id){
			return false;
		}
		public override Unit Get(Register<Unit>.ID id){
			return Unit.GetNullUnit();
		}
		public override Unit Get(int index){
			return Unit.GetNullUnit();
		}
		public override Register<Unit>.ID GetID(int index){
			return Register<Unit>.ID.GetNullID();
		}
		public override bool Contains(Register<Unit>.ID id){
			return false;
		}
		public override bool Contains(Unit unit){
			return false;
		}
		public override bool Contains(int index){
			return false;
		}
		public override bool CheckBounds(int index){
			return false;
		}
		public override List<Unit> GetAll(){
			return new List<Unit>();
		}
		public override List<Unit> GetMultiple(List<Register<Unit>.ID> ids){
			return new List<Unit>();
		}
		public override List<Unit> GetMultiple(Register<Unit>.ID[] ids){
			return new List<Unit>();
		}
		public override int GetCounter(){
			return 0;
		}
		public override int GetCount(){
			return 0;
		}
		public void Sort(Game game){}
		public void TryPickup(Game game, Unit holder, Unit item){}
		public void TryDrop(Game game, Unit holder, Register<Unit>.ID id){}
		public void EquipWeapon(Game game, Unit self, Register<Unit>.ID id){}
		public void UnequipWeapon(Game game, Unit self, Register<Unit>.ID id){}
		public Unit GetWeapon(Game game, Unit self){
			return Unit.GetNullUnit();
		}
		public void EquipChestplate(Game game, Unit self, Register<Unit>.ID id){}
		public void UnequipChestplate(Game game, Unit self, Register<Unit>.ID id){}
		public Unit GetChestplate(Game game, Unit self){
			return Unit.GetNullUnit();
		}
		public void EquipBoots(Game game, Unit self, Register<Unit>.ID id){}
		public void UnequipBoots(Game game, Unit self, Register<Unit>.ID id){}
		public Unit GetBoots(Game game, Unit self){
			return Unit.GetNullUnit();
		}
		public Attribute GetEquipSlots(Game game){
			return Attribute.GetNullAttribute();
		}
	}
	[field:NonSerialized]private static readonly NullInventory _NULL_INVENTORY = new NullInventory();
	public Inventory(){}
	public virtual bool IsNull(){
		return false;
	}
	public override Unit GetNullStoreObject(){
		return Unit.GetNullUnit();
	}
	public virtual ISortable GetSortable(){
		return _NULL_INVENTORY;
	}
	public virtual IPickupable GetPickupable(){
		return _NULL_INVENTORY;
	}
	public virtual IDroppable GetDroppable(){
		return _NULL_INVENTORY;
	}
	public virtual IWeaponEquippable GetWeaponEquippable(){
		return _NULL_INVENTORY;
	}
	public virtual IChestplateEquippable GetChestplateEquippable(){
		return _NULL_INVENTORY;
	}
	public virtual IBootsEquippable GetBootsEquippable(){
		return _NULL_INVENTORY;
	}
	public virtual EquipSlots.IHasEquipSlots GetHasEquipSlots(){
		return _NULL_INVENTORY;
	}
	public static void Default_Sort(Inventory self, Game game, Register<Unit>.ID[] ids){
		for(int i = (ids.Length - 1); i >= 0; i--){
			self.Insert(ids[i], 0);
		}
	}
	public static void Default_TryPickup(Inventory self, Game game, Unit holder, Unit item){
		item.GetPickupable().DoPickup(game, holder, self);
	}
	public static void Default_TryDrop(Inventory self, Game game, Unit holder, Register<Unit>.ID id){
		self.Get(id).GetDroppable().DoDrop(game, holder, self);
	}
	public static void Default_Equip(Inventory self, Game game, Unit holder, Register<Unit>.ID id, ref Register<Unit>.ID slot, Attribute slots){
		self.Get(slot).GetEquippable().TryUnequip(game, holder);
		if(slots.GetConditionable().IsMaxed()){
			return;
		}
		if(slot.IsNull()){
			self.Get(id).GetEquippable().DoEquip(game, holder, self, ref slot);
		}
	}
	public static void Default_Unequip(Inventory self, Game game, Unit holder, Register<Unit>.ID id, ref Register<Unit>.ID slot){
		if(slot.Equals(id)){
			self.Get(id).GetEquippable().DoUnequip(game, holder, self, ref slot);
		}
	}
	public static Inventory GetNullInventory(){
		return _NULL_INVENTORY;
	}
}
