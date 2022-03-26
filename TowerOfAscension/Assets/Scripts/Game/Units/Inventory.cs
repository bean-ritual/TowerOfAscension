using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Inventory : Register<Unit>{
	public interface IPickupable{
		void AttemptPickup(Level level, Unit unit);
	}
	public interface IDroppable{
		void TryDrop(Level level, Register<Unit>.ID id);
	}
	[Serializable]
	public class NullInventory : Inventory{
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
	}
	[field:NonSerialized]private static readonly NullInventory _NULL_INVENTORY = new NullInventory();
	public Inventory(){}
	public virtual bool IsNull(){
		return false;
	}
	public override Unit GetNullStoreObject(){
		return Unit.GetNullUnit();
	}
	public static Inventory GetNullInventory(){
		return _NULL_INVENTORY;
	}
}
