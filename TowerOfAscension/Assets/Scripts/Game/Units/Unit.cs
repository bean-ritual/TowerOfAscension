using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Unit{
	public interface IProcessable{
		bool Process(Level level);
	}
	public interface IPositionable{
		void SetPosition(Level level, int x, int y);
		void GetPosition(out int x, out int y);
		void RemovePosition(Level level);
		Vector3 GetPosition(GridMap<Tile> map);
		Tile GetTile(Level level);
	}
	public interface ISpawnable : IPositionable, Register<Unit>.IRegisterable{
		void Spawn(Level level, int x, int y);
		void Despawn(Level level);
	}
	public interface IMoveable : IPositionable{
		void Move(Level level, Direction direction);
		void OnMove(Level level, Tile tile);
	}
	public interface ICollideable{
		bool CheckCollision(Level level, Unit check);
	}
	public interface IControllable{
		void SetAI(AI ai);
		AI GetAI();
	}
	public interface IDiscoverer{
		void Discover(Level level, Tile tile);
	}
	public interface IInteractable{
		void Interact(Level level, Unit unit);
	}
	public interface IInteractor{
		void Interact(Level level, Direction direction);
	}
	public interface IDamageable{
		void TakeDamage(Level level, Unit unit, int damage);
	}
	public interface IAttackable{
		void Attacked(Level level, Unit unit, int attack);
	}
	public interface IAttacker{
		void Attack(Level level, Direction direction);
		void OnAttack(Level level, Tile tile);
	}
	public interface IKillable : ISpawnable{
		void Kill(Level level);
		void OnKill(Level level);
	}
	public interface IPickupable{
		void TryPickup(Level level, Unit unit);
		void DoPickup(Level level, Inventory inventory);
	}
	public interface IDroppable{
		void DoDrop(Level level, Inventory inventory);
	}
	public interface IExitable{
		void Exit(Level level);
	}
	public interface ITripwire{
		void Trip(Level level, Unit unit);
	}
	public interface IHasInventory{
		Inventory GetInventory();
	}
	public interface IHostileTarget{
		bool CheckHostility(Level level, Unit unit);
	}
	public interface IClassicGen{
		void AddStructureSpawner(ClassicGen.Spawner spawner);
		void AddDetailSpawner(ClassicGen.Spawner spawner);
		void AddBuild(int value = 1);
		void OnFinalize(Level level);
		int GetContentualBluePrintIndex();
		ClassicGen.Spawner.IFinalize GetFinalize();
	}
	[Serializable]
	public class NullUnit : 
		Unit,
		Register<Unit>.IRegisterable,
		WorldUnit.IWorldUnit,
		WorldUnit.IWorldUnitUI,
		WorldUnit.IWorldUnitAnimations,
		Unit.IProcessable,
		Unit.IPositionable,
		Unit.ISpawnable,
		Unit.IMoveable,
		Unit.ICollideable,
		Unit.IControllable,
		Unit.IDiscoverer,
		Unit.IInteractable,
		Unit.IInteractor,
		Unit.IDamageable,
		Unit.IAttackable,
		Unit.IAttacker,
		Unit.IKillable,
		Unit.IPickupable,
		Unit.IExitable,
		Unit.ITripwire,
		Unit.IHasInventory,
		Unit.IHostileTarget,
		Level.ILightControl,
		Level.ILightSource,
		Unit.IClassicGen,
		ClassicGen.Spawner.IFinalize,
		Health.IHasHealth,
		Armour.IHasArmour
		{
		[field:NonSerialized]public event EventHandler<EventArgs> OnWorldUnitUIUpdate;
		[field:NonSerialized]public event EventHandler<WorldUnit.UnitAnimateEventArgs> OnAttackAnimation;
		private const int _NULL_X = -1;
		private const int _NULL_Y = -1;
		public NullUnit(){}
		public override bool IsNull(){
			return true;
		}
		public void AddToRegister(Register<Unit> register){}
		public void RemoveFromRegister(Register<Unit> register){}
		public Register<Unit>.ID GetID(){
			return Register<Unit>.ID.GetNullID();
		}
		public WorldUnit.WorldUnitController GetWorldUnitController(){
			return WorldUnit.WorldUnitController.GetNullWorldUnitController();
		}
		public bool GetWorldVisibility(Level level){
			return false;
		}
		public Vector3 GetUIOffset(){
			return Vector3.zero;
		}
		public int GetUISortingOrder(){
			return 0;
		}
		public bool GetHealthBar(){
			return false;
		}
		public bool Process(Level level){
			return level.NextTurn();
		}
		public void SetPosition(Level level, int x, int y){}
		public void GetPosition(out int x, out int y){
			x = _NULL_X;
			y = _NULL_Y;
		}
		public void RemovePosition(Level level){}
		public Vector3 GetPosition(GridMap<Tile> map){
			return Vector3.zero;
		}
		public Tile GetTile(Level level){
			return Tile.GetNullTile();
		}
		public void Spawn(Level level, int x, int y){}
		public void Despawn(Level level){}
		public void Move(Level level, Direction direction){}
		public void OnMove(Level level, Tile tile){}
		public bool CheckCollision(Level level, Unit check){
			return false;
		}
		public void SetAI(AI ai){}
		public AI GetAI(){
			return AI.GetNullAI();
		}
		public void Discover(Level level, Tile tile){}
		public void Interact(Level level, Unit unit){}
		public void Interact(Level level, Direction direction){}
		public void TakeDamage(Level level, Unit unit, int damage){}
		public void Attacked(Level level, Unit unit, int attack){}
		public void Attack(Level level, Direction direction){}
		public void OnAttack(Level level, Tile tile){}
		public void Kill(Level level){}
		public void OnKill(Level level){}
		public void TryPickup(Level level, Unit unit){}
		public void DoPickup(Level level, Inventory inventory){}
		public void Exit(Level level){}
		public void Trip(Level level, Unit unit){}
		public Inventory GetInventory(){
			return Inventory.GetNullInventory();
		}
		public bool CheckHostility(Level level, Unit unit){
			return false;
		}
		public bool CheckTransparency(Level level){
			return true;
		}
		public int GetLightRange(Level level){
			return 0;
		}
		public void AddStructureSpawner(ClassicGen.Spawner spawner){}
		public void AddDetailSpawner(ClassicGen.Spawner spawner){}
		public void AddBuild(int value = 1){}
		public void OnFinalize(Level level){}
		public int GetContentualBluePrintIndex(){
			return -1;
		}
		//public void AddBuild(int value = 1){}
		public void AddExitSpawner(ClassicGen.Spawner exit){}
		public bool RemoveExitSpawner(ClassicGen.Spawner exit){
			return false;
		}
		public static int GetNullX(){
			return _NULL_X;
		}
		public static int GetNullY(){
			return _NULL_Y;
		}
		public Attribute GetHealth(){
			return Attribute.GetNullAttribute();
		}
		public Attribute GetArmour(){
			return Attribute.GetNullAttribute();
		}
	}
	[field:NonSerialized]private static readonly NullUnit _NULL_UNIT = new NullUnit();
	public Unit(){}
	public virtual bool IsNull(){
		return false;
	}
	public virtual Register<Unit>.IRegisterable GetRegisterable(){
		return _NULL_UNIT;
	}
	public virtual WorldUnit.IWorldUnit GetWorldUnit(){
		return _NULL_UNIT;
	}
	public virtual WorldUnit.IWorldUnitUI GetWorldUnitUI(){
		return _NULL_UNIT;
	}
	public virtual WorldUnit.IWorldUnitAnimations GetWorldUnitAnimations(){
		return _NULL_UNIT;
	}
	public virtual IProcessable GetProcessable(){
		return _NULL_UNIT;
	}
	public virtual IPositionable GetPositionable(){
		return _NULL_UNIT;
	}
	public virtual ISpawnable GetSpawnable(){
		return _NULL_UNIT;
	}
	public virtual IMoveable GetMoveable(){
		return _NULL_UNIT;
	}
	public virtual ICollideable GetCollideable(){
		return _NULL_UNIT;
	}
	public virtual IControllable GetControllable(){
		return _NULL_UNIT;
	}
	public virtual IDiscoverer GetDiscoverer(){
		return _NULL_UNIT;
	}
	public virtual IInteractable GetInteractable(){
		return _NULL_UNIT;
	}
	public virtual IInteractor GetInteractor(){
		return _NULL_UNIT;
	}
	public virtual IDamageable GetDamageable(){
		return _NULL_UNIT;
	}
	public virtual IAttackable GetAttackable(){
		return _NULL_UNIT;
	}
	public virtual IAttacker GetAttacker(){
		return _NULL_UNIT;
	}
	public virtual IKillable GetKillable(){
		return _NULL_UNIT;
	}
	public virtual IPickupable GetPickupable(){
		return _NULL_UNIT;
	}
	public virtual IExitable GetExitable(){
		return _NULL_UNIT;
	}
	public virtual ITripwire GetTripwire(){
		return _NULL_UNIT;
	}
	public virtual IHasInventory GetHasInventory(){
		return _NULL_UNIT;
	}
	public virtual IHostileTarget GetHostileTarget(){
		return _NULL_UNIT;
	}
	public virtual Level.ILightControl GetLightControl(){
		return _NULL_UNIT;
	}
	public virtual Level.ILightSource GetLightSource(){
		return _NULL_UNIT;
	}
	public virtual IClassicGen GetClassicGen(){
		return _NULL_UNIT;
	}
	public virtual ClassicGen.Spawner.IFinalize GetFinalize(){
		return _NULL_UNIT;
	}
	public virtual Health.IHasHealth GetHasHealth(){
		return _NULL_UNIT;
	}
	public virtual Armour.IHasArmour GetHasArmour(){
		return _NULL_UNIT;
	}
	public static void Default_Spawn(Unit self, Level level, int x, int y){
		self.GetRegisterable().AddToRegister(level.GetUnits());
		self.GetPositionable().SetPosition(level, x, y);
		level.LightUpdate(self);
	}
	public static void Default_Despawn(Unit self, Level level){
		self.GetRegisterable().RemoveFromRegister(level.GetUnits());
		self.GetPositionable().RemovePosition(level);
	}
	public static void Default_SetPosition(Unit self, Level level, int newX, int newY, ref int x, ref int y, int moveSpeed = 0){
		self.GetPositionable().RemovePosition(level);
		level.Get(newX, newY).GetHasUnits().AddUnit(self.GetRegisterable().GetID());
		x = newX;
		y = newY;
		self.GetWorldUnit().GetWorldUnitController().SetWorldPosition(level.GetWorldPosition(x, y), moveSpeed);
	}
	public static void Default_RemovePosition(Unit self, Level level, int x, int y){
		level.Get(x, y).GetHasUnits().RemoveUnit(self.GetRegisterable().GetID());
	}
	public static void Default_Move(Unit self, Level level, Direction direction){
		self.GetPositionable().GetPosition(out int oldX, out int oldY);
		direction.GetTile(level, oldX, oldY).GetWalkable().Walk(level, self);
	}
	public static void Default_Kill(Unit self, Level level){
		self.GetSpawnable().Despawn(level);
		self.GetKillable().OnKill(level);
	}
	public static Unit GetNullUnit(){
		return _NULL_UNIT;
	}
}
