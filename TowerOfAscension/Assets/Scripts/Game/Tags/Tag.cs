using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Tag{
	public enum ID{
		Null,
		Alive,
		Position,
		WorldUnit,
		WorldUnitUI,
		UIUnit,
		Health,
		Armour,
		Move,
		Weapon,
		Chestplate,
		Boots,
		Inventory,
		Pickup,
		Drop,
		Equippable,
		Light,
		Opacity,
		Collision,
		Loot,
		Discoverer,
		Interactor,
		Interactable,
		Hostility,
		Tripwire,
		Exit,
		AI,
		Attackable,
		Active,
		Basic_Attack,
		Damage_Physical,
		Damage_Magic,
		Damage_Poison,
		Fortify_Health,
		Fortify_Armour,
	};
	public enum Collider{
		Null,
		Basic,
	};
	public class ValueEventArgs<TValue> : EventArgs{
		public TValue value;
		public ValueEventArgs(TValue value){
			this.value = value;
		}
	}
	public class ValuesEventArgs<TValue1, TValue2> : EventArgs{
		public TValue1 value1;
		public TValue2 value2;
		public ValuesEventArgs(TValue1 value1, TValue2 value2){
			this.value1 = value1;
			this.value2 = value2;
		}
	}
	public interface IStringValueEvent{
		event EventHandler<ValueEventArgs<string>> OnStringValueEvent;
	}
	public interface IDirectionValueEvent{
		event EventHandler<ValueEventArgs<Direction>> OnDirectionValueEvent;
	}
	public interface IWorldAnimationEvent{
		event EventHandler<ValuesEventArgs<Vector3, int>> OnWorldAnimationEvent;
	}
	public interface IProcess{
		bool Process(Game game, Unit self);
	}
	public interface ICondition{
		bool Check(Game game, Unit self);
	}
	public interface ICondition<TValue>{
		bool Check(Game game, Unit self, TValue value);
	}
	public interface IClear{
		void Clear(Game game, Unit self);
	}
	public interface IFortifyValue1{
		void FortifyValue1(Game game, Unit self);
	}
	public interface IFortifyValue1<TValue>{
		void FortifyValue1(Game game, Unit self, TValue value);
	}
	public interface IFortifyValue2<TValue>{
		void FortifyValue2(Game game, Unit self, TValue value);
	}
	public interface IDamageValue1{
		void DamageValue1(Game game, Unit self);
	}
	public interface IDamageValue1<TValue>{
		void DamageValue1(Game game, Unit self, TValue value);
	}
	public interface IDamageValue2<TValue>{
		void DamageValue2(Game game, Unit self, TValue value);
	}
	public interface ISetValue1<TValue>{
		void SetValue1(Game game, Unit self, TValue value);
	}
	public interface ISetValue2<TValue>{
		void SetValue2(Game game, Unit self, TValue value);
	}
	public interface ISetValue3<TValue>{
		void SetValue3(Game game, Unit self, TValue value);
	}
	public interface ISetValues<TValue1, TValue2>{
		void SetValues(Game game, Unit self, TValue1 value1, TValue2 value2);
	}
	public interface IGetIntValue1{
		int GetIntValue1(Game game, Unit self);
	}
	public interface IGetIntValue2{
		int GetIntValue2(Game game, Unit self);
	}
	public interface IGetIntValue3{
		int GetIntValue3(Game game, Unit self);
	}
	public interface IGetUnit{
		Unit GetUnit(Game game, Unit self);
	}
	public interface IGetTile{
		Tile GetTile(Game game, Unit self);
	}
	public interface IGetCollider{
		Collider GetCollider(Game game, Unit self);
	}
	public interface IGetSprite{
		Sprite GetSprite(Game game, Unit self);
	}
	public interface IGetVector{
		Vector3 GetVector(Game game, Unit self);
	}
	public interface IGetRegisterEvents{
		Register<Unit>.IRegisterEvents GetRegisterEvents(Game game, Unit self);
	}
	public interface ITrigger{
		void Trigger(Game game, Unit self);
	}
	public interface ITrigger<TValue>{
		void Trigger(Game game, Unit self, TValue value);
	}
	public interface IInput<TValue>{
		void Input(Game game, Unit self, TValue value);
	}
	public interface IInput<TValue1, TValue2>{
		void Input(Game game, Unit self, TValue1 value1, TValue2 value2);
	}
	public interface IAdd<TValue>{
		void Add(Game game, Unit self, TValue value);
	}
	public interface IRemove<TValue>{
		void Remove(Game game, Unit self, TValue value);
	}
	public interface IRemove<TValue1, TValue2>{
		void Remove(Game game, Unit self, TValue1 value1, TValue2 value2);
	}
	public interface IReduce<TValue>{
		TValue Reduce(Game game, Unit self, TValue value);
	}
	public class NullTag : 
		Tag,
		Tag.IStringValueEvent,
		Tag.IDirectionValueEvent,
		Tag.IWorldAnimationEvent,
		Tag.IProcess,
		Tag.ITrigger,
		Tag.ICondition,
		Tag.ICondition<Tag.Collider>,
		Tag.IClear,
		Tag.IFortifyValue1,
		Tag.IFortifyValue1<int>,
		Tag.IFortifyValue2<int>,
		Tag.IDamageValue1,
		Tag.IDamageValue1<int>,
		Tag.IDamageValue2<int>,
		Tag.ISetValue1<Tag.Collider>,
		Tag.ISetValue1<SpriteSheet.SpriteID>,
		Tag.ISetValue1<int>,
		Tag.ISetValue1<bool>,
		Tag.ISetValue2<int>,
		Tag.ISetValue3<int>,
		Tag.ISetValues<int, int>,
		Tag.IReduce<int>,
		Tag.IGetIntValue1,
		Tag.IGetIntValue2,
		Tag.IGetIntValue3,
		Tag.IGetUnit,
		Tag.IGetTile,
		Tag.IGetCollider,
		Tag.IGetSprite,
		Tag.IGetVector,
		Tag.IGetRegisterEvents,
		Tag.IInput<Direction>,
		Tag.IInput<Unit>,
		Tag.IInput<Tile>,
		Tag.IInput<Unit, Unit>,
		Tag.IInput<Unit, Direction>,
		Tag.IAdd<Unit>,
		Tag.IRemove<Unit>,
		Tag.IRemove<Unit, Unit>,
		Tag.IRemove<Register<Unit>.ID>
		{
		[field:NonSerialized]public	event EventHandler<ValueEventArgs<string>> OnStringValueEvent;
		[field:NonSerialized]public	event EventHandler<ValueEventArgs<Direction>> OnDirectionValueEvent;
		[field:NonSerialized]public event EventHandler<ValuesEventArgs<Vector3, int>> OnWorldAnimationEvent;
		private void ShutupUnityEventWarning(){
			OnStringValueEvent?.Invoke(this, new ValueEventArgs<string>(""));
			OnDirectionValueEvent?.Invoke(this, new ValueEventArgs<Direction>(Direction.GetNullDirection()));
			OnWorldAnimationEvent?.Invoke(this, new ValuesEventArgs<Vector3, int>(Vector3.zero, 0));
		}
		//
		public bool Process(Game game, Unit self){
			return game.GetLevel().NextTurn();
		}
		public void Trigger(Game game, Unit self){}
		public bool Check(Game game, Unit self){
			return false;
		}
		public bool Check(Game game, Unit self, Collider collider){
			return false;
		}
		public void Clear(Game game, Unit self){}
		public void FortifyValue1(Game game, Unit self){}
		public void FortifyValue1(Game game, Unit self, int value){}
		public void FortifyValue2(Game game, Unit self, int value){}
		public void DamageValue1(Game game, Unit self){}
		public void DamageValue1(Game game, Unit self, int value){}
		public void DamageValue2(Game game, Unit self, int value){}
		public void SetValue1(Game game, Unit self, Tag.Collider value){}
		public void SetValue1(Game game, Unit self, SpriteSheet.SpriteID value){}
		public void SetValue1(Game game, Unit self, int value){}
		public void SetValue1(Game game, Unit self, bool value){}
		public void SetValue2(Game game, Unit self, int value){}
		public void SetValue3(Game game, Unit self, int value){}
		public void SetValues(Game game, Unit self, int value1, int value2){}
		public int Reduce(Game game, Unit self, int value){
			return value;
		}
		public int GetIntValue1(Game game, Unit self){
			return 0;
		}
		public int GetIntValue2(Game game, Unit self){
			return 0;
		}
		public int GetIntValue3(Game game, Unit self){
			return 0;
		}
		public Unit GetUnit(Game game, Unit self){
			return Unit.GetNullUnit();
		}
		public Tile GetTile(Game game, Unit self){
			return Tile.GetNullTile();
		}
		public Tag.Collider GetCollider(Game game, Unit self){
			return Tag.Collider.Null;
		}
		public Sprite GetSprite(Game game, Unit self){
			return SpriteSheet.NullSpriteSheet.GetNullSprite();
		}
		public Vector3 GetVector(Game game, Unit self){
			return Vector3.zero;
		}
		public Register<Unit>.IRegisterEvents GetRegisterEvents(Game game, Unit self){
			return Inventory.GetNullInventory();
		}
		public void Input(Game game, Unit self, Direction direction){}
		public void Input(Game game, Unit self, Unit unit){}
		public void Input(Game game, Unit self, Tile tile){}
		public void Input(Game game, Unit self, Unit unit1, Unit unit2){}
		public void Input(Game game, Unit self, Unit unit, Direction direction){}
		public void Add(Game game, Unit self, Unit unit){}
		public void Remove(Game game, Unit self, Unit unit){}
		public void Remove(Game game, Unit self, Unit unit1, Unit unit2){}
		public void Remove(Game game, Unit self, Register<Unit>.ID unit){}
		//
		public override Tag.ID GetTagID(){
			const Tag.ID TAG_ID = Tag.ID.Null;
			return TAG_ID;
		}
		public override void Disassemble(){}
		public override bool Add(Dictionary<ID, Tag> tags){
			return false;
		}
		public override bool Remove(Dictionary<ID, Tag> tags){
			return false;
		}
		public override bool IsNull(){
			return true;
		}
		protected override void TagUpdateEvent(){}
	}
	private static readonly NullTag _NULL_TAG = new NullTag();
	[field:NonSerialized]public event EventHandler<EventArgs> OnTagUpdate;
	public virtual bool IsNull(){
		return false;
	}
	//
	public abstract Tag.ID GetTagID();
	public abstract void Disassemble();
	//
	public virtual bool Add(Dictionary<Tag.ID, Tag> tags){
		Tag.ID id = GetTagID();
		if(id == ID.Null){
			return false;
		}
		if(tags.ContainsKey(id)){
			tags[id] = this;
		}else{
			tags.Add(id, this);
		}
		return true;
	}
	public virtual bool Remove(Dictionary<ID, Tag> tags){
		return tags.Remove(GetTagID());
	}
	public virtual void BuildString(StringBuilder builder){}
	protected virtual void TagUpdateEvent(){
		OnTagUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual Tag.IStringValueEvent GetIStringValueEvent(){
		return _NULL_TAG;
	}
	public virtual Tag.IDirectionValueEvent GetIDirectionValueEvent(){
		return _NULL_TAG;
	}
	public virtual Tag.IWorldAnimationEvent GetIWorldAnimationEvent(){
		return _NULL_TAG;
	}
	public virtual Tag.IProcess GetIProcess(){
		return _NULL_TAG;
	}
	public virtual Tag.ITrigger GetITrigger(){
		return _NULL_TAG;
	}
	public virtual Tag.ICondition GetICondition(){
		return _NULL_TAG;
	}
	public virtual Tag.ICondition<Collider> GetIConditionCollider(){
		return _NULL_TAG;
	}
	public virtual Tag.IClear GetIClear(){
		return _NULL_TAG;
	}
	public virtual Tag.IFortifyValue1 GetIFortifyValue1(){
		return _NULL_TAG;
	}
	public virtual Tag.IFortifyValue1<int> GetIFortifyValue1Int(){
		return _NULL_TAG;
	}
	public virtual Tag.IFortifyValue2<int> GetIFortifyValue2Int(){
		return _NULL_TAG;
	}
	public virtual Tag.IDamageValue1 GetIDamageValue1(){
		return _NULL_TAG;
	}
	public virtual Tag.IDamageValue1<int> GetIDamageValue1Int(){
		return _NULL_TAG;
	}
	public virtual Tag.IDamageValue2<int> GetIDamageValue2Int(){
		return _NULL_TAG;
	}
	public virtual Tag.ISetValue1<Tag.Collider> GetISetValue1Collider(){
		return _NULL_TAG;
	}
	public virtual Tag.ISetValue1<SpriteSheet.SpriteID> GetISetValue1SpriteID(){
		return _NULL_TAG;
	}
	public virtual Tag.ISetValue1<int> GetISetValue1Int(){
		return _NULL_TAG;
	}
	public virtual Tag.ISetValue1<bool> GetISetValue1Bool(){
		return _NULL_TAG;
	}
	public virtual Tag.ISetValue2<int> GetISetValue2Int(){
		return _NULL_TAG;
	}
	public virtual Tag.ISetValue3<int> GetISetValue3Int(){
		return _NULL_TAG;
	}
	public virtual Tag.ISetValues<int, int> GetISetValuesInt(){
		return _NULL_TAG;
	}
	public virtual Tag.IReduce<int> GetIReduceInt(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetIntValue1 GetIGetIntValue1(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetIntValue2 GetIGetIntValue2(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetIntValue3 GetIGetIntValue3(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetUnit GetIGetUnit(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetTile GetIGetTile(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetCollider GetIGetCollider(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetSprite GetIGetSprite(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetVector GetIGetVector(){
		return _NULL_TAG;
	}
	public virtual Tag.IGetRegisterEvents GetIGetRegisterEvents(){
		return _NULL_TAG;
	}
	public virtual Tag.IInput<Direction> GetIInputDirection(){
		return _NULL_TAG;
	}
	public virtual Tag.IInput<Unit> GetIInputUnit(){
		return _NULL_TAG;
	}
	public virtual Tag.IInput<Tile> GetIInputTile(){
		return _NULL_TAG;
	}
	public virtual Tag.IInput<Unit, Unit> GetIInput2Units(){
		return _NULL_TAG;
	}
	public virtual Tag.IInput<Unit, Direction> GetIInputUnitDirection(){
		return _NULL_TAG;
	}
	public virtual Tag.IAdd<Unit> GetIAddUnit(){
		return _NULL_TAG;
	}
	public virtual Tag.IRemove<Unit> GetIRemoveUnit(){
		return _NULL_TAG;
	}
	public virtual Tag.IRemove<Unit, Unit> GetIRemove2Units(){
		return _NULL_TAG;
	}
	public virtual Tag.IRemove<Register<Unit>.ID> GetIRemoveUnitID(){
		return _NULL_TAG;
	}
	public static Tag GetNullTag(){
		return _NULL_TAG;
	}
}
