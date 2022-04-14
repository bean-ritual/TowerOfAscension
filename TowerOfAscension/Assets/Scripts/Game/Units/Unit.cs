using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Unit{
	public interface IProcessable{
		bool Process(Game game);
	}
	public interface ITaggable{
		void AddTag(Game game, Tag tag);
		void RemoveTag(Game game, Tag.ID id);
		Tag GetTag(Game game, Tag.ID id);
	}
	public interface IPositionable{
		void SetPosition(Game game, int x, int y);
		void GetPosition(Game game, out int x, out int y);
		void RemovePosition(Game game);
		Vector3 GetPosition(Game game);
		Tile GetTile(Game game);
	}
	public interface ITileable{
		Tile GetTile(Game game);
		Tile GetTileFrom(Game game, int x, int y);
	}
	public interface ISpawnable : IPositionable, Register<Unit>.IRegisterable{
		void Spawn(Game game, int x, int y);
		void Despawn(Game game);
	}
	public interface IMoveable : IPositionable{
		void Move(Game game, Direction direction);
		void OnMove(Game game, Tile tile);
	}
	public interface ICollideable{
		bool CheckCollision(Game game, Unit check);
	}
	public interface IControllable{
		void SetAI(Game game, AI ai);
		AI GetAI(Game game);
	}
	public interface IDiscoverer{
		void Discover(Game game, Tile tile);
	}
	public interface IInteractable{
		void Interact(Game game, Unit unit);
	}
	public interface IInteractor{
		void Interact(Game game, Direction direction);
	}
	public interface IAttacker{
		void TryAttack(Game game, Direction direction);
		void DoAttack(Game game, Unit skills, Unit target);
	}
	public interface IAttackable{
		void CheckAttack(Game game, Unit skills, Unit attack);
	}
	public interface IKillable : ISpawnable{
		void Kill(Game game);
		void OnKill(Game game);
	}
	public interface IPickupable{
		void TryPickup(Game game, Unit unit);
		void DoPickup(Game game, Unit unit, Inventory inventory);
	}
	public interface IDroppable{
		void TryDrop(Game game, Unit unit);
		void DoDrop(Game game, Unit unit, Inventory inventory);
	}
	public interface IExitable{
		void Exit(Game game);
	}
	public interface ITripwire{
		void Trip(Game game, Unit unit);
	}
	public interface IHasInventory{
		Inventory GetInventory(Game game);
	}
	public interface IHostileTarget{
		bool CheckHostility(Game game, Unit unit);
	}
	public interface IUseable{
		void TryUse(Game game, Unit unit);
	}
	public interface IEquippable{
		void TryEquip(Game game, Unit unit);
		void DoEquip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id);
		void TryUnequip(Game game, Unit unit);
		void DoUnequip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id);
	}
	public interface IItemToolTip{
		string GetToolTip(Game game);
	}
	public interface IPlayable{
		void SetPlayer(Game game);
		void RemovePlayer(Game game);
	}
	public interface IProxyable{
		void SetProxyID(Game game, Register<Unit>.ID id);
		void RemoveProxyID(Game game);
	}
	public interface IClassicGen{
		void AddStructureSpawner(ClassicGen.Spawner spawner);
		void AddDetailSpawner(ClassicGen.Spawner spawner);
		void AddBuild(int value = 1);
		void OnFinalize(Game game);
		int GetContentualBluePrintIndex();
		ClassicGen.Spawner.IFinalize GetFinalize();
	}
	[Serializable]
	public class NullUnit : 
		Unit,
		Register<Unit>.IRegisterable,
		VisualController.IVisualController,
		Unit.IProcessable,
		Unit.ITaggable,
		Unit.IPositionable,
		Unit.ITileable,
		Unit.ISpawnable,
		Unit.IMoveable,
		Unit.ICollideable,
		Unit.IControllable,
		Unit.IDiscoverer,
		Unit.IInteractable,
		Unit.IInteractor,
		Unit.IAttacker,
		Unit.IAttackable,
		Unit.IKillable,
		Unit.IPickupable,
		Unit.IDroppable,
		Unit.IExitable,
		Unit.ITripwire,
		Unit.IHasInventory,
		Unit.IHostileTarget,
		Unit.IUseable,
		Unit.IEquippable,
		Unit.IItemToolTip,
		Unit.IPlayable,
		Unit.IProxyable,
		Level.ILightControl,
		Level.ILightSource,
		Unit.IClassicGen,
		ClassicGen.Spawner.IFinalize
		{
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
		public VisualController GetVisualController(Game game){
			return VisualController.GetNullVisualController();
		}
		public bool GetWorldVisibility(Game game){
			return false;
		}
		public bool Process(Game game){
			return game.GetLevel().NextTurn();
		}
		public void AddTag(Game game, Tag tag){}
		public void RemoveTag(Game game, Tag.ID id){}
		public Tag GetTag(Game game, Tag.ID id){
			return Tag.GetNullTag();
		}
		public void SetPosition(Game game, int x, int y){}
		public void GetPosition(Game game, out int x, out int y){
			x = _NULL_X;
			y = _NULL_Y;
		}
		public void RemovePosition(Game game){}
		public Vector3 GetPosition(Game game){
			return Vector3.zero;
		}
		public Tile GetTile(Game game){
			return Tile.GetNullTile();
		}
		public Tile GetTileFrom(Game game, int x, int y){
			return Tile.GetNullTile();
		}
		public void Spawn(Game game, int x, int y){}
		public void Despawn(Game game){}
		public void Move(Game game, Direction direction){}
		public void OnMove(Game game, Tile tile){}
		public bool CheckCollision(Game game, Unit check){
			return false;
		}
		public void SetAI(Game game, AI ai){}
		public AI GetAI(Game game){
			return AI.GetNullAI();
		}
		public void Discover(Game game, Tile tile){}
		public void Interact(Game game, Unit unit){}
		public void Interact(Game game, Direction direction){}
		public void TryAttack(Game game, Direction direction){}
		public void DoAttack(Game game, Unit skills, Unit target){}
		public void CheckAttack(Game game, Unit skills, Unit attack){}
		public void Kill(Game game){}
		public void OnKill(Game game){}
		public void TryPickup(Game game, Unit unit){}
		public void DoPickup(Game game, Unit unit, Inventory inventory){}
		public void TryDrop(Game game, Unit unit){}
		public void DoDrop(Game game, Unit unit, Inventory inventory){}
		public void Exit(Game game){}
		public void Trip(Game game, Unit unit){}
		public Inventory GetInventory(Game game){
			return Inventory.GetNullInventory();
		}
		public bool CheckHostility(Game game, Unit unit){
			return false;
		}
		public void TryUse(Game game, Unit unit){}
		public void TryEquip(Game game, Unit unit){}
		public void DoEquip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){}
		public void TryUnequip(Game game, Unit unit){}
		public void DoUnequip(Game game, Unit unit, Inventory inventory, ref Register<Unit>.ID id){}
		public string GetToolTip(Game game){
			return "";
		}
		public void SetPlayer(Game game){}
		public void RemovePlayer(Game game){}
		public void SetProxyID(Game game, Register<Unit>.ID id){}
		public void RemoveProxyID(Game game){}
		public bool CheckTransparency(Game game){
			return true;
		}
		public int GetLightRange(Game game){
			return 0;
		}
		public void AddStructureSpawner(ClassicGen.Spawner spawner){}
		public void AddDetailSpawner(ClassicGen.Spawner spawner){}
		public void AddBuild(int value = 1){}
		public void OnFinalize(Game game){}
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
	public virtual bool IsNull(){
		return false;
	}
	public virtual Register<Unit>.IRegisterable GetRegisterable(){
		return _NULL_UNIT;
	}
	public virtual VisualController.IVisualController GetVisualController(){
		return _NULL_UNIT;
	}
	public virtual IProcessable GetProcessable(){
		return _NULL_UNIT;
	}
	public virtual Unit.ITaggable GetTaggable(){
		return _NULL_UNIT;
	}
	public virtual IPositionable GetPositionable(){
		return _NULL_UNIT;
	}
	public virtual ITileable GetTileable(){
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
	public virtual IAttacker GetAttacker(){
		return _NULL_UNIT;
	}
	public virtual IAttackable GetAttackable(){
		return _NULL_UNIT;
	}
	public virtual IKillable GetKillable(){
		return _NULL_UNIT;
	}
	public virtual IPickupable GetPickupable(){
		return _NULL_UNIT;
	}
	public virtual IDroppable GetDroppable(){
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
	public virtual IUseable GetUseable(){
		return _NULL_UNIT;
	}
	public virtual IEquippable GetEquippable(){
		return _NULL_UNIT;
	}
	public virtual IItemToolTip GetItemToolTip(){
		return _NULL_UNIT;
	}
	public virtual IPlayable GetPlayable(){
		return _NULL_UNIT;
	}
	public virtual IProxyable GetProxyable(){
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
	public static void Default_Spawn(Unit self, Game game, int x, int y){
		self.GetRegisterable().AddToRegister(game.GetLevel().GetUnits());
		self.GetPlayable().SetPlayer(game);
		self.GetPositionable().SetPosition(game, x, y);
		game.GetLevel().LightUpdate(game, self);
	}
	public static void Default_Despawn(Unit self, Game game){
		self.GetPositionable().RemovePosition(game);
		self.GetPlayable().RemovePlayer(game);
		self.GetRegisterable().RemoveFromRegister(game.GetLevel().GetUnits());
	}
	public static void Default_SetPosition(Unit self, Game game, int newX, int newY, ref int x, ref int y, int moveSpeed = 0){
		self.GetPositionable().RemovePosition(game);
		game.GetLevel().Get(newX, newY).GetHasUnits().AddUnit(game, self.GetRegisterable().GetID());
		x = newX;
		y = newY;
		self.GetVisualController().GetVisualController(game).SetWorldPosition(game.GetLevel().GetWorldPosition(x, y), moveSpeed);
	}
	public static void Default_RemovePosition(Unit self, Game game, int x, int y){
		game.GetLevel().Get(x, y).GetHasUnits().RemoveUnit(game, self.GetRegisterable().GetID());
	}
	public static void Default_Move(Unit self, Game game, Direction direction){
		direction.GetTileFromUnit(game, self).GetWalkable().Walk(game, self);
	}
	public static void Default_Kill(Unit self, Game game){
		self.GetSpawnable().Despawn(game);
		self.GetKillable().OnKill(game);
	}
	public static void Default_SetPlayer(Game game, ref Register<Unit>.ID id){
		game.GetPlayer().GetProxyable().SetProxyID(game, id);
	}
	public static void Default_RemovePlayer(Game game){
		game.GetPlayer().GetProxyable().RemoveProxyID(game);
	}
	public static void Default_TryPickup(Unit self, Game game, Unit unit){
		unit.GetHasInventory().GetInventory(game).GetPickupable().TryPickup(game, unit, self);
	}
	public static void Default_DoPickup(Unit self, Game game, Unit unit, ref Register<Unit>.ID id){
		self.GetSpawnable().Despawn(game);
		unit.GetHasInventory().GetInventory(game).Add(self, ref id);
		unit.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, unit);
	}
	public static void Default_TryDrop(Unit self, Game game, Unit unit, ref Register<Unit>.ID id){
		unit.GetHasInventory().GetInventory(game).GetDroppable().TryDrop(game, unit, id);
	}
	public static void Default_DoDrop(Unit self, Game game, Unit unit, ref Register<Unit>.ID id){
		self.GetEquippable().TryUnequip(game, unit);
		unit.GetHasInventory().GetInventory(game).Remove(id);
		id = Register<Unit>.ID.GetNullID();
		unit.GetPositionable().GetPosition(game, out int x, out int y);
		self.GetSpawnable().Spawn(game, x, y);
		unit.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, unit);
	}
	public static void Default_TryEquipWeapon(Game game, Unit unit, ref Register<Unit>.ID id){
		unit.GetHasInventory().GetInventory(game).GetWeaponEquippable().EquipWeapon(game, unit, id);
	}
	public static void Default_TryUnequipWeapon(Game game, Unit unit, ref Register<Unit>.ID id){
		unit.GetHasInventory().GetInventory(game).GetWeaponEquippable().UnequipWeapon(game, unit, id);
	}
	public static void Default_TryEquipChestplate(Game game, Unit unit, ref Register<Unit>.ID id){
		unit.GetHasInventory().GetInventory(game).GetChestplateEquippable().EquipChestplate(game, unit, id);
	}
	public static void Default_TryUnequipChestplate(Game game, Unit unit, ref Register<Unit>.ID id){
		unit.GetHasInventory().GetInventory(game).GetChestplateEquippable().UnequipChestplate(game, unit, id);
	}
	public static void Default_TryEquipBoots(Game game, Unit unit, ref Register<Unit>.ID id){
		unit.GetHasInventory().GetInventory(game).GetBootsEquippable().EquipBoots(game, unit, id);
	}
	public static void Default_TryUnequipBoots(Game game, Unit unit, ref Register<Unit>.ID id){
		unit.GetHasInventory().GetInventory(game).GetBootsEquippable().UnequipBoots(game, unit, id);
	}
	public static void Default_DoEquip(Unit self, Game game, Unit unit, ref Register<Unit>.ID slot, ref Register<Unit>.ID id, ref bool equipped){
		slot = id;
		equipped = true;
		self.GetVisualController().GetVisualController(game).SetItemBorder(equipped);
		unit.GetHasInventory().GetInventory(game).GetSortable().Sort(game);
		unit.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, unit);
	}
	public static void Default_DoUnequip(Unit self, Game game, Unit unit, ref Register<Unit>.ID slot, ref bool equipped){
		slot = Register<Unit>.ID.GetNullID();
		equipped = false;
		self.GetVisualController().GetVisualController(game).SetItemBorder(equipped);
		unit.GetHasInventory().GetInventory(game).GetSortable().Sort(game);
		unit.GetControllable().GetAI(game).GetTurnControl().EndTurn(game, unit);
	}
	public static Unit GetNullUnit(){
		return _NULL_UNIT;
	}
}
