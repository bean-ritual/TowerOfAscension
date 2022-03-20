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
		Vector3 GetPosition(GridMap<Tile> map);
		Tile GetTile(Level level);
	}
	public interface ISpawnable : IPositionable, Register<Unit>.IRegisterable{
		void Spawn(Level level, int x, int y);
		void Despawn(Level level);
	}
	public interface IMoveable : IPositionable{
		event EventHandler<EventArgs> OnMoveEvent;
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
		void OnAttacked();
	}
	public interface IAttacker{
		void Attack(Level level, Direction direction);
		void OnAttack();
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
		Unit.IHostileTarget,
		Level.ILightControl,
		Level.ILightSource,
		Unit.IClassicGen,
		ClassicGen.Spawner.IFinalize
		{
		[field:NonSerialized]public event EventHandler<EventArgs> OnWorldUnitUpdate;
		[field:NonSerialized]public event EventHandler<EventArgs> OnMoveEvent;
		private const int _NULL_X = -1;
		private const int _NULL_Y = -1;
		public NullUnit(){}
		public void AddToRegister(Register<Unit> register){}
		public Register<Unit>.ID GetID(){
			return Register<Unit>.ID.GetNullID();
		}
		public Sprite GetSprite(){
			return SpriteSheet.NullSpriteSheet.GetNullSprite();
		}
		public int GetSortingOrder(){
			return 0;
		}
		public bool GetWorldVisibility(Level level){
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
		public void OnAttacked(){}
		public void Attack(Level level, Direction direction){}
		public void OnAttack(){}
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
	}
	[field:NonSerialized]private static readonly NullUnit _NULL_UNIT = new NullUnit();
	public Unit(){}
	public virtual Register<Unit>.IRegisterable GetRegisterable(){
		return _NULL_UNIT;
	}
	public virtual WorldUnit.IWorldUnit GetWorldUnit(){
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
	public static void Default_Spawn(Unit self, Level level, int x, int y){
		self.GetRegisterable().AddToRegister(level.GetUnits());
		self.GetPositionable().SetPosition(level, x, y);
		level.LightUpdate(self);
	}
	public static void Default_SetPosition(Unit self, Level level, int newX, int newY, ref int x, ref int y){
		self.GetPositionable().GetPosition(out int oldX, out int oldY);
		level.Get(oldX, oldY).GetHasUnits().RemoveUnit(self.GetRegisterable().GetID());
		level.Get(newX, newY).GetHasUnits().AddUnit(self.GetRegisterable().GetID());
		x = newX;
		y = newY;
	}
	public static void Default_Move(Unit self, Level level, Direction direction){
		self.GetPositionable().GetPosition(out int oldX, out int oldY);
		direction.GetTile(level, oldX, oldY).GetWalkable().Walk(level, self);
	}
	public static Unit GetNullUnit(){
		return _NULL_UNIT;
	}
}
