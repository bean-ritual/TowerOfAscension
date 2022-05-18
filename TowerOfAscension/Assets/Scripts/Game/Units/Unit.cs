using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Unit{
	public static class UNIT_DATA{
		public static Unit GetGrave(Game game){
			return new Unit(
				game,
				new Tag[]{
					WorldVisual.Create(SpriteSheet.SpriteID.Grave, 0, 25),
					WorldPosition.Create(),
					GameOver.Create(),
				}
			);
		}
		public static Unit GetStairs(Game game){
			return new Unit(
				game,
				new Tag[]{
					WorldVisual.Create(SpriteSheet.SpriteID.Stairs, 0, 10),
					WorldPosition.Create(),
					TagStairs.Create(),
				}
			);
		}
		public static Unit GetLevelledDoor(Game game, int level){
			return new Unit(
				game,
				new Tag[]{
					WorldVisual.Create(SpriteSheet.SpriteID.Door, 0, 30),
					WorldPosition.Create(),
					LightControl.Create(),
					Open.Create(),
					Collision.Create(Tag.Collider.Basic),
				}
			);
		}
		public static Unit GetLevelledMonster(Game game, int level){
			if(UnityEngine.Random.Range(0, 100) < 50){
				return GetLevelledRat(game, level);
			}else{
				return GetLevelledSkeleton(game, level);
			}
		}
		public static Unit GetLevelledRat(Game game, int level){
			return new Unit(
				game,
				new Tag[]{
					LightWorldVisual.Create(SpriteSheet.SpriteID.Rat, 0, 20),
					WorldPosition.Create(),
					WorldVisualUI.Create(new Vector3(0, 0.9f)),
					TagAI.Create(),
					Alive.Create(),
					Value.Create(Tag.ID.Level, level),
					Health.Create(5, 1),
					AttackSlot.Create(),
					BasicAttack.Create(),
					Attackable.Create(),
					Loot.Create(1),
					ExpDrop.Create(2),
					RangeValue.Create(Tag.ID.Damage_Physical, 1, 5),
					Move.Create(8),
					Collision.Create(Tag.Collider.Basic),
					Text.Create(Tag.ID.Name, "Rat"),
					Tooltip.Create(),
				}
			);
		}
		public static Unit GetLevelledSkeleton(Game game, int level){
			return new Unit(
				game,
				new Tag[]{
					LightWorldVisual.Create(SpriteSheet.SpriteID.Skeleton, 0, 20),
					WorldPosition.Create(),
					WorldVisualUI.Create(new Vector3(0, 1)),
					TagAI.Create(),
					Alive.Create(),
					Value.Create(Tag.ID.Level, level),
					Health.Create(10, 2),
					AttackSlot.Create(),
					BasicAttack.Create(),
					Attackable.Create(),
					Loot.Create(1),
					ExpDrop.Create(7),
					RangeValue.Create(Tag.ID.Damage_Physical, 1, 7),
					Move.Create(10),
					Collision.Create(Tag.Collider.Basic),
					Text.Create(Tag.ID.Name, "Skeleton"),
					Tooltip.Create(),
				}
			);
		}
		public static Unit GetLevelledItem(Game game, int level){
			return new Unit(
				game,
				new Tag[]{
					LightWorldVisual.Create(SpriteSheet.SpriteID.Swords, UnityEngine.Random.Range(0, 5), 10),
					WorldPosition.Create(),
					Condition.Create(Tag.ID.UIUnit, false),
					Pickup.Create(),
					BasicAttack.Create(),
					RangeValue.Create(Tag.ID.Damage_Physical, 3, 9),
					Equippable.Create(Tag.ID.Attack_Slot),
					Text.Create(Tag.ID.Name, "Sword"),
					Tooltip.Create(),
				}
			);
		}
		public static Unit GetLantern(Game game){
			return new Unit(
				game,
				new Tag[]{
					LightWorldVisual.Create(SpriteSheet.SpriteID.Lamp, 0, 10),
					WorldPosition.Create(),
					Condition.Create(Tag.ID.UIUnit, false),
					Pickup.Create(),
					Equippable.Create(Tag.ID.Light),
					Torchlight.Create(5),
					ClampedValue.Create(Tag.ID.Fuel, 250),
					Text.Create(Tag.ID.Name, "Lantern"),
					Tooltip.Create(),
				}
			);
		}
		public static Unit GetLevelledUnit(Game game, int level){
			switch(level){
				default: return Unit.GetNullUnit();
				case 0: return GetLevelledMonster(game, level);
			}
		}
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
		public override void Add(Register<Unit> register){}
		public override void Remove(Register<Unit> register){}
		public override Register<Unit>.ID GetID(){
			return Register<Unit>.ID.GetNullID();
		}
		public override bool Process(Game game){
			return game.GetLevel().NextTurn(game);
		}
		public override void EndTurn(Game game){}
		public override void AddTag(Game game, Tag tag){}
		public override void RemoveTag(Game game, Tag.ID id){}
		public override Tag GetTag(Game game, Tag.ID id){
			return Tag.GetNullTag();
		}
		public override Tag GetRealTag(Game game, Tag.ID id){
			return Tag.GetNullTag();
		}
		public override void Spawn(Game game, int x, int y){}
		public override void Despawn(Game game){}
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
	[Serializable]
	public class RealUnit : Unit{
		private int _id;
		public RealUnit(int id){
			_id = id;
		}
	}
	private static readonly NullUnit _NULL_UNIT = new NullUnit();
	protected Dictionary<Tag.ID, Tag> _tags;
	protected Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	public Unit(){
		_tags = new Dictionary<Tag.ID, Tag>();
	}
	public Unit(Game game, Tag[] array){
		_tags = new Dictionary<Tag.ID, Tag>(array.Length);
		for(int i = 0; i < array.Length; i++){
			AddTag(game, array[i]);
		}
	}
	public Unit(Game game, List<Tag> list){
		_tags = new Dictionary<Tag.ID, Tag>(list.Count);
		for(int i = 0; i < list.Count; i++){
			AddTag(game, list[i]);
		}
	}
	public virtual bool Process(Game game){
		return GetTag(game, Tag.ID.AI).GetIProcess().Process(game, this);
	}
	public virtual void EndTurn(Game game){
		GetTag(game, Tag.ID.Light).GetIEndTurn().OnEndTurn(game, this);
	}
	public virtual void Spawn(Game game, int x, int y){
		Add(game.GetLevel().GetUnits());
		this.GetPlayable().SetPlayer(game);
		GetTag(game, Tag.ID.Position).GetISetValuesInt().SetValues(game, this, x, y);
		EndTurn(game);
	}
	public virtual void Despawn(Game game){
		GetTag(game, Tag.ID.Position).GetIClear().Clear(game, this);
		this.GetPlayable().RemovePlayer(game);
		Remove(game.GetLevel().GetUnits());
	}
	public virtual void Add(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public virtual void Remove(Register<Unit> register){
		register.Remove(_id);
	}
	public virtual Register<Unit>.ID GetID(){
		return _id;
	}
	public virtual void AddTag(Game game, Tag tag){
		tag.Add(game, this, _tags);
	}
	public virtual void RemoveTag(Game game, Tag.ID id){
		GetTag(game, id).Remove(game, this, _tags);
	}
	public virtual Tag GetTag(Game game, Tag.ID id){
		if(!_tags.TryGetValue(id, out Tag tag)){
			tag = Tag.GetNullTag();
		}
		return tag;
		//return tag.GetSelf(game, this);
	}
	public virtual Tag GetRealTag(Game game, Tag.ID id){
		if(!_tags.TryGetValue(id, out Tag tag)){
			tag = Tag.GetNullTag();
		}
		return tag;
	}
	public virtual void UpdateAllTags(Game game){
		foreach(KeyValuePair<Tag.ID, Tag> keyValue in _tags){
			keyValue.Value.TagUpdateEvent();
		}
	}
	public virtual void RefreshAllTags(Game game){
		foreach(KeyValuePair<Tag.ID, Tag> keyValue in _tags){
			keyValue.Value.GetIRefresh().Refresh(game, this);
		}
	}
	public virtual bool IsNull(){
		return false;
	}
	public virtual IClassicGen GetClassicGen(){
		return _NULL_UNIT;
	}
	public virtual ClassicGen.Spawner.IFinalize GetFinalize(){
		return _NULL_UNIT;
	}
	public virtual Unit.IPlayable GetPlayable(){
		return _NULL_UNIT;
	}
	public virtual Unit.IProxyable GetProxyable(){
		return _NULL_UNIT;
	}
	/*
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
	*/
	public static Unit GetNullUnit(){
		return _NULL_UNIT;
	}
}
