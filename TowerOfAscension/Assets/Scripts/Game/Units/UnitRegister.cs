using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UnitRegister : Register<Unit>{
	[Serializable]
	public class NullUnitRegister : UnitRegister{
		public NullUnitRegister(){}
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
	[field:NonSerialized]private static readonly NullUnitRegister _NULL_UNIT_REGISTER = new NullUnitRegister();
	public UnitRegister(){}
	public override Unit GetNullStoreObject(){
		return Unit.GetNullUnit();
	}
	public static Register<Unit> GetNullUnitRegister(){
		return _NULL_UNIT_REGISTER;
	}
}
