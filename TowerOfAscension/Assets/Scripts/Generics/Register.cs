using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Register<TStoreObject> : 
	Register<TStoreObject>.IRegisterEvents
	{
	public interface IRegisterable{
		void Add(Register<TStoreObject> register);
		void Remove(Register<TStoreObject> register);
		Register<TStoreObject>.ID GetID();
	}
	public interface IRegisterEvents{
		event EventHandler<OnObjectChangedEventArgs> OnObjectAdded;
		event EventHandler<OnObjectChangedEventArgs> OnObjectRemoved;
		List<TStoreObject> GetAll();
	}
	[Serializable]
	public class ID{
		[Serializable]
		public class NullID : ID{
			public const int _NULL = -1;
			public NullID(){}
			public override int GetID(){
				return _NULL;
			}
			public override bool Equals(System.Object obj){
				return false;
			}
			public override int GetHashCode(){
				return _NULL;
			}
			public override bool IsNull(){
				return true;
			}	
		}
		[field:NonSerialized]private static readonly NullID _NULL_ID = new NullID();
		private readonly int _id;
		public ID(int id){
			_id = id;
		}
		public ID(){}
		public virtual int GetID(){
			return _id;
		}
		public override bool Equals(System.Object obj){
			if((obj == null) || !this.GetType().Equals(obj.GetType())){
				return false;
			}else{
				ID id = (ID)obj;
				return _id == id.GetID();
			}
		}
		public override int GetHashCode(){
			return _id;
		}
		public virtual bool IsNull(){
			return false;
		}
		public static ID GetNullID(){
			return _NULL_ID;
		}
	}
	[field:NonSerialized]public event EventHandler<OnObjectChangedEventArgs> OnObjectAdded;
	[field:NonSerialized]public event EventHandler<OnObjectChangedEventArgs> OnObjectRemoved;
	public class OnObjectChangedEventArgs : EventArgs{
		public ID id;
		public TStoreObject value;
		public OnObjectChangedEventArgs(ID id, TStoreObject value){
			this.id = id;
			this.value = value;
		}
	}
	public class OnObjectSwappedEventArgs : EventArgs{
		public ID id1;
		public ID id2;
		public TStoreObject value1;
		public TStoreObject value2;
	}
	private int _idCounter;
	private List<ID> _ids;
	protected Dictionary<ID, TStoreObject> _register;
	public Register(){
		_idCounter = 0;
		_ids = new List<ID>();
		_register = new Dictionary<ID, TStoreObject>();
	}
	public abstract TStoreObject GetNullStoreObject();
	public virtual void Add(TStoreObject value, ref ID id){
		id = CreateID();
		_ids.Add(id);
		_register.Add(id, value);
		OnObjectAdded?.Invoke(this, new OnObjectChangedEventArgs(id, value));
	}
	public virtual bool Remove(ID id){
		_ids.Remove(id);
		_register.TryGetValue(id, out TStoreObject value);
		OnObjectRemoved?.Invoke(this, new OnObjectChangedEventArgs(id, value));
		return _register.Remove(id);
	}
	public virtual TStoreObject Get(ID id){
		if(_register.TryGetValue(id, out TStoreObject value)){
			return value;
		}
		return GetNullStoreObject();
	}
	public virtual TStoreObject Get(int index){
		if(!CheckBounds(index)){
			return GetNullStoreObject();
		}
		return Get(_ids[index]);
	}
	public virtual ID GetID(int index){
		if(!CheckBounds(index)){
			return ID.GetNullID();
		}
		return _ids[index];
	}
	public virtual bool Contains(ID id){
		return _register.ContainsKey(id);
	}
	public virtual bool Contains(TStoreObject value){
		return _register.ContainsValue(value);
	}
	public virtual bool Contains(int index){
		return _register.ContainsKey(_ids[index]);
	}
	public virtual bool CheckBounds(int index){
		if(index < 0){
			return false;
		}
		if(index >= _ids.Count){
			return false;
		}
		return true;
	}
	public virtual void Swap(ID id, int index){
		if(!CheckBounds(index)){
			return;
		}
		ID id2 = _ids[index];
		for(int i = 0; i < _ids.Count; i++){
			if(_ids[i].Equals(id)){
				_ids[index] = id;
				_ids[i] = id2;
				return;
			}	
		}
	}
	public virtual void Insert(ID id, int index){
		if(!CheckBounds(index)){
			return;
		}
		if(_ids.Remove(id)){
			_ids.Insert(index, id);
		}
	}
	public virtual List<TStoreObject> GetAll(){
		List<TStoreObject> values = new List<TStoreObject>(_ids.Count);
		for(int i = 0; i < _ids.Count; i++){
			values.Add(Get(i));
		}
		return values;
	}
	public virtual List<TStoreObject> GetMultiple(List<ID> ids){
		List<TStoreObject> values = new List<TStoreObject>(ids.Count);
		for(int i = 0; i < ids.Count; i++){
			values.Add(Get(ids[i]));
		}
		return values;
	}
	public virtual List<TStoreObject> GetMultiple(ID[] ids){
		List<TStoreObject> values = new List<TStoreObject>(ids.Length);
		for(int i = 0; i < ids.Length; i++){
			values.Add(Get(ids[i]));
		}
		return values;
	}
	public virtual int GetCounter(){
		return _idCounter;
	}
	public virtual int GetCount(){
		return _ids.Count;
	}
	public virtual IRegisterEvents GetEvents(){
		return this;
	}
	private ID CreateID(){
		const int COUNTER = 1;
		return new ID(_idCounter += COUNTER);
	}
}
