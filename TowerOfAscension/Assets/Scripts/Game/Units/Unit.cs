using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Unit{
	public static class UNIT_DATA{
		public static Unit GetStairs(Game game){
			VisualController controller = new VisualController();
			controller.SetSpriteID(SpriteSheet.SpriteID.Stairs);
			controller.SetSortingOrder(10);
			return new LevelUnit(
				game,
				controller,
				new Tag[]{
					TagStairs.Create(),
					WorldVisual.Create(SpriteSheet.SpriteID.Stairs, 0, 10),
				}
			);
		}
		public static Unit GetLevelledDoor(Game game, int level){
			VisualController controller = new VisualController();
			controller.SetSpriteID(SpriteSheet.SpriteID.Door);
			controller.SetSortingOrder(30);
			return new LevelUnit(
				game,
				controller,
				new Tag[]{
					WorldVisual.Create(SpriteSheet.SpriteID.Door, 0, 30),
					LightControl.Create(),
					Open.Create(),
					Collision.Create(Tag.Collider.Basic),
				}
			);
		}
		public static Unit GetLevelledMonster(Game game, int level){
			VisualController controller = new VisualController();
			controller.SetSpriteID(SpriteSheet.SpriteID.Rat);
			controller.SetSortingOrder(20);
			controller.SetUISortingOrder(100);
			controller.SetUIOffset(new Vector3(0, 0.9f));
			controller.SetHealthBarActive(true);
			return new LevelUnit(
				game,
				controller,
				new Tag[]{
					LightWorldVisual.Create(SpriteSheet.SpriteID.Rat, 0, 20),
					TagAI.Create(),
					Alive.Create(),
					Health.Create(5),
					BasicAttack.Create(),
					Attackable.Create(),
					Value.Create(Tag.ID.Damage_Physical, 1),
					Move.Create(),
					Collision.Create(Tag.Collider.Basic),
				}
			);
		}
		public static Unit GetLevelledItem(Game game, int level){
			VisualController controller = new VisualController();
			controller.SetSprite(SpriteSheet.SpriteID.Swords, UnityEngine.Random.Range(0, 5));
			controller.SetSortingOrder(10);
			return new LevelUnit(
				game,
				controller,
				new Tag[]{
					LightWorldVisual.Create(SpriteSheet.SpriteID.Swords, UnityEngine.Random.Range(0, 5), 10),
					Condition.Create(Tag.ID.UIUnit, false),
					Pickup.Create(),
					Equippable.Create(Tag.ID.Weapon),
				}
			);
		}
	}
	public interface ITaggable{
		void AddTag(Game game, Tag tag);
		void RemoveTag(Game game, Tag.ID id);
		Tag GetTag(Game game, Tag.ID id);
	}
	public interface IPositionable{
		void SetPosition(Game game, int x, int y, int moveSpeed = 0);
		void GetPosition(Game game, out int x, out int y);
		void RemovePosition(Game game);
		Vector3 GetPosition(Game game);
		Tile GetTile(Game game);
	}
	public interface ITileable{
		Tile GetTile(Game game);
		Tile GetTileFrom(Game game, int x, int y);
	}
	public interface ISpawnable : 
		IPositionable, 
		Register<Unit>.IRegisterable
		{
		void Spawn(Game game, int x, int y);
		void Despawn(Game game);
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
		Unit.ITaggable,
		Unit.IPositionable,
		Unit.ITileable,
		Unit.ISpawnable,
		Unit.IPlayable,
		Unit.IProxyable,
		Unit.IClassicGen,
		ClassicGen.Spawner.IFinalize
		{
		private const int _NULL_X = -1;
		private const int _NULL_Y = -1;
		public NullUnit(){}
		public override bool IsNull(){
			return true;
		}
		public void Add(Register<Unit> register){}
		public void Remove(Register<Unit> register){}
		public Register<Unit>.ID GetID(){
			return Register<Unit>.ID.GetNullID();
		}
		public VisualController GetVisualController(Game game){
			return VisualController.GetNullVisualController();
		}
		public bool GetWorldVisibility(Game game){
			return false;
		}
		public override bool Process(Game game){
			return game.GetLevel().NextTurn();
		}
		public void AddTag(Game game, Tag tag){}
		public void RemoveTag(Game game, Tag.ID id){}
		public Tag GetTag(Game game, Tag.ID id){
			return Tag.GetNullTag();
		}
		public void SetPosition(Game game, int x, int y, int moveSpeed = 0){}
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
		public void SetPlayer(Game game){}
		public void RemovePlayer(Game game){}
		public void SetProxyID(Game game, Register<Unit>.ID id){}
		public void RemoveProxyID(Game game){}
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
	public virtual bool Process(Game game){
		return GetTaggable().GetTag(game, Tag.ID.AI).GetIProcess().Process(game, this);
	}
	public virtual bool IsNull(){
		return false;
	}
	public virtual Register<Unit>.IRegisterable GetRegisterable(){
		return _NULL_UNIT;
	}
	public virtual VisualController.IVisualController GetVisualController(){
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
	public virtual IPlayable GetPlayable(){
		return _NULL_UNIT;
	}
	public virtual IProxyable GetProxyable(){
		return _NULL_UNIT;
	}
	public virtual IClassicGen GetClassicGen(){
		return _NULL_UNIT;
	}
	public virtual ClassicGen.Spawner.IFinalize GetFinalize(){
		return _NULL_UNIT;
	}
	public static void Default_Spawn(Unit self, Game game, int x, int y){
		self.GetRegisterable().Add(game.GetLevel().GetUnits());
		self.GetPlayable().SetPlayer(game);
		self.GetPositionable().SetPosition(game, x, y);
		game.GetLevel().LightUpdate(game, self);
	}
	public static void Default_Despawn(Unit self, Game game){
		self.GetPositionable().RemovePosition(game);
		self.GetPlayable().RemovePlayer(game);
		self.GetRegisterable().Remove(game.GetLevel().GetUnits());
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
	public static void Default_SetPlayer(Game game, ref Register<Unit>.ID id){
		game.GetPlayer().GetProxyable().SetProxyID(game, id);
	}
	public static void Default_RemovePlayer(Game game){
		game.GetPlayer().GetProxyable().RemoveProxyID(game);
	}
	public static Unit GetNullUnit(){
		return _NULL_UNIT;
	}
}
