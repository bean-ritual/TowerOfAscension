using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClassicGen : 
	Unit,
	WorldUnit.IWorldUnit,
	Unit.ISpawnable,
	Unit.IProcessable,
	Unit.IClassicGen
	{
	public static class CLASSICGEN_DATA{
		private static readonly BluePrint.BluePrintID[] _STRUCTURE_BLUEPRINTS;
		private static readonly BluePrint.BluePrintID[] _DETAIL_BLUEPRINTS;
		static CLASSICGEN_DATA(){
			_STRUCTURE_BLUEPRINTS = new BluePrint.BluePrintID[]{
				BluePrint.BluePrintID.BasicRoom,
				BluePrint.BluePrintID.BasicHallway,
				BluePrint.BluePrintID.SmallRoom,
				BluePrint.BluePrintID.LongHallway,
			};
			_DETAIL_BLUEPRINTS = new BluePrint.BluePrintID[]{
				BluePrint.BluePrintID.SmallRoom,
				BluePrint.BluePrintID.TinyRoom,
			};
		}
		public static int GetRandomStructureBluePrintIndex(){
			return BluePrint.BLUEPRINT_DATA.GetBluePrintIndex(_STRUCTURE_BLUEPRINTS[UnityEngine.Random.Range(0, _STRUCTURE_BLUEPRINTS.Length)]);
		}
		public static int GetRandomDetailBluePrintIndex(){
			return BluePrint.BLUEPRINT_DATA.GetBluePrintIndex(_DETAIL_BLUEPRINTS[UnityEngine.Random.Range(0, _DETAIL_BLUEPRINTS.Length)]);
		}
	}
	[Serializable]
	public abstract class Spawner{
		// SPAWNER_DATA
		public static class SPAWNER_DATA{
			public static Spawner GetSpawner(int index, int x, int y){
				switch(index){
					default: return GetNullSpawner();
					case 0: return GetContentualBluePrintSpawner(x, y);
					case 1: return GetPossibleConnectorSpawner(x, y);
					case 2: return new ConnectorSpawner(x, y);
					case 3: return new ExitSpawner(x, y);
					case 4: return new MonsterSpawner(x, y);
					case 10: return GetRandomBluePrintSpawner(x, y);
					case 11: return GetStructureBluePrintSpawner(x, y);
					case 12: return GetDetailBluePrintSpawner(x, y);
				}
			}
			public static Spawner GetRandomBluePrintSpawner(int x, int y){
				return new BluePrintSpawner(x, y, BluePrint.BLUEPRINT_DATA.GetRandomIndex());
			}
			public static Spawner GetStructureBluePrintSpawner(int x, int y){
				return new BluePrintSpawner(x, y, CLASSICGEN_DATA.GetRandomStructureBluePrintIndex());
			}
			public static Spawner GetDetailBluePrintSpawner(int x, int y){
				return new BluePrintSpawner(x, y, CLASSICGEN_DATA.GetRandomDetailBluePrintIndex());
			}
			public static Spawner GetContentualBluePrintSpawner(int x, int y){
				return new ContentualBluePrintSpawner(x, y);
			}
			public static Spawner GetPossibleConnectorSpawner(int x, int y){
				if(UnityEngine.Random.Range(0, 100) < 10){
					return new ConnectorSpawner(x, y);
				}
				return GetNullSpawner();
			}
		}
		public interface IFinalize{
			void AddBuild(int value = 1);
			void AddExitSpawner(Spawner exit);
			bool RemoveExitSpawner(Spawner exit);
		}
		public interface IExit{
			void PositionMaster(Level level, Unit master);
			int CalculateDistanceCost(Level level, int x, int y);
		}
		//
		[Serializable]
		public class NullSpawner : 
			Spawner,
			IFinalize,
			IExit
			{
			public NullSpawner(){}
			public override void Spawn(Level level, Unit master){}
			public override void AddToMaster(Level level, Unit master){}
			public void AddBuild(int value = 1){}
			public void AddExitSpawner(Spawner exit){}
			public bool RemoveExitSpawner(Spawner exit){
				return false;
			}
			public void PositionMaster(Level level, Unit master){}
			public int CalculateDistanceCost(Level level, int x, int y){
				return -1;
			}
		}
		[field:NonSerialized]private static readonly NullSpawner _NULL_SPAWNER = new NullSpawner();
		protected int _x;
		protected int _y;
		public Spawner(int x, int y){
			_x = x;
			_y = y;
		}
		public Spawner(){}
		public abstract void Spawn(Level level, Unit master);
		public abstract void AddToMaster(Level level, Unit master);
		public virtual IFinalize GetFinalize(){
			return _NULL_SPAWNER;
		}
		public virtual IExit GetExit(){
			return _NULL_SPAWNER;
		}
		public static Spawner GetNullSpawner(){
			return _NULL_SPAWNER;
		}
	}
	//
	[Serializable]
	public class BluePrintSpawner : Spawner{
		private readonly int _index;
		public BluePrintSpawner(int x, int y, int index) : base(x, y){
			_index = index;
		}
		public override void Spawn(Level level, Unit master){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(_index).Spawn(level, master, _x, _y);
		}
		public override void AddToMaster(Level level, Unit master){
			master.GetClassicGen().AddStructureSpawner(this);
		}
	}
	[Serializable]
	public class ContentualBluePrintSpawner : Spawner{
		public ContentualBluePrintSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Level level, Unit master){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(master.GetClassicGen().GetContentualBluePrintIndex()).Spawn(level, master, _x, _y);
		}
		public override void AddToMaster(Level level, Unit master){
			master.GetClassicGen().AddStructureSpawner(this);
		}
	}
	//
	[Serializable]
	public class ConnectorSpawner : Spawner{
		public ConnectorSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Level level, Unit master){
			if(level.Get(_x, _y).GetConnectable().CanConnect(level, master, _x, _y)){
				return;
			}
			if(level.GetCardinals(_x, _y, (Tile tile) => {return tile.GetConnectable().CanConnect(level, master, _x, _y);}).Count == 2){
				level.Set(_x, _y, new PathTile(_x, _y));
				Unit door = Door.DOOR_DATA.GetLevelledDoor(0);
				door.GetSpawnable().Spawn(level, _x, _y);
			}
		}
		public override void AddToMaster(Level level, Unit master){
			master.GetClassicGen().AddDetailSpawner(this);
		}
	}
	[Serializable]
	public class ExitSpawner : 
		Spawner,
		Spawner.IExit
		{
		public ExitSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Level level, Unit master){
			Unit stairs = new Stairs();
			stairs.GetSpawnable().Spawn(level, _x, _y);
			master.GetClassicGen().GetFinalize().AddBuild();
		}
		public virtual void PositionMaster(Level level, Unit master){
			master.GetPositionable().SetPosition(level, _x, _y);
			master.GetClassicGen().GetFinalize().RemoveExitSpawner(this);
			master.GetClassicGen().GetFinalize().AddBuild();
		}
		public virtual int CalculateDistanceCost(Level level, int x, int y){
			return level.CalculateDistanceCost(x, y, _x, _y);
		}
		public override void AddToMaster(Level level, Unit master){
			master.GetClassicGen().GetFinalize().AddExitSpawner(this);
		}
		public override Spawner.IExit GetExit(){
			return this;
		}
	}
	[Serializable]
	public class FinalSpawner : 
		Spawner,
		Spawner.IFinalize
		{
		private List<Spawner> _exits;
		private int _amount;
		private int _build;
		public FinalSpawner(int amount){
			_exits = new List<Spawner>();
			_amount = amount;
			_build = 0;
		}
		public override void Spawn(Level level, Unit master){
			if(_exits.Count <= 0){
				return;
			}
			_exits[UnityEngine.Random.Range(0, _exits.Count)].GetExit().PositionMaster(level, master);
			master.GetPositionable().GetPosition(out int x, out int y);
			GetExitToSpawn(level, master, x, y).Spawn(level, master);
			if(_build >= (_amount + 1)){
				master.GetClassicGen().OnFinalize(level);
			}
		}
		public override void AddToMaster(Level level, Unit master){
			//
		}
		public virtual Spawner GetExitToSpawn(Level level, Unit master, int x, int y){
			Spawner exit = GetNullSpawner();
			if(_exits.Count <= 0){
				return exit;
			}
			int amount = 0;
			for(int i = 0; i < _exits.Count; i++){
				int distance = _exits[i].GetExit().CalculateDistanceCost(level, x, y);
				if(distance > amount){
					exit = _exits[i];
					amount = distance;
				}
			}
			return exit;
		}
		public virtual void AddBuild(int value = 1){
			_build = (_build + value);
		}
		public virtual void AddExitSpawner(Spawner exit){
			_exits.Add(exit);
		}
		public virtual bool RemoveExitSpawner(Spawner exit){
			return _exits.Remove(exit);
		}
		public override Spawner.IFinalize GetFinalize(){
			return this;
		}
	}
	//
	[Serializable]
	public class MonsterSpawner : Spawner{
		public MonsterSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Level level, Unit master){
			Monster.MONSTER_DATA.GetLevelledMonster(0).GetSpawnable().Spawn(level, _x, _y);
		}
		public override void AddToMaster(Level level, Unit master){
			master.GetClassicGen().AddDetailSpawner(this);
		}
	}
	//
	public enum GenState{
		Null,
		Initialize,
		Structure,
		Details,
		Finalize,
		Complete,
	};
	[field:NonSerialized]public event EventHandler<EventArgs> OnWorldUnitUpdate;
	private int _x;
	private int _y;
	private Unit _player;
	private GenState _state;
	private List<Spawner> _structures;
	private Queue<Spawner> _details;
	private Spawner _finalize;
	private Register<Unit>.ID _id;
	private int _minBuild;
	private int _build;
	public ClassicGen(Unit player, int minBuild = 10, int maxExits = 1){
		_x = Unit.NullUnit.GetNullX();
		_y = Unit.NullUnit.GetNullY();
		_player = player;
		_state = GenState.Null;
		_structures = new List<Spawner>();
		_details = new Queue<Spawner>();
		_finalize = new FinalSpawner(maxExits);
		_id = Register<Unit>.ID.GetNullID();
		_minBuild = minBuild;
		_build = 0;
	}	
	public Sprite GetSprite(){
		return SpriteSheet.SPRITESHEET_DATA.GetSprite(SpriteSheet.SpriteID.Stairs, 1);
	}
	public int GetSortingOrder(){
		return 10;
	}
	public bool GetWorldVisibility(Level level){
		return true;
	}
	public void Spawn(Level level, int x, int y){
		Unit.Default_Spawn(this, level, x, y);
		_state = GenState.Structure;
		_structures.Add(Spawner.SPAWNER_DATA.GetContentualBluePrintSpawner(_x, _y));
	}
	public void Despawn(Level level){
		
	}
	public void SetPosition(Level level, int x, int y){
		Unit.Default_SetPosition(this, level, x, y, ref _x, ref _y);
		OnWorldUnitUpdate?.Invoke(this, EventArgs.Empty);
	}
	public void GetPosition(out int x, out int y){
		x = _x;
		y = _y;
	}
	public Vector3 GetPosition(GridMap<Tile> map){
		return map.GetWorldPosition(_x, _y);
	}
	public Tile GetTile(Level level){
		return level.Get(_x, _y);
	}
	public void AddToRegister(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public bool Process(Level level){
		switch(_state){
			default: break;
			case GenState.Null: break;
			case GenState.Structure:{
				StructureState(level);
				break;
			}
			case GenState.Details:{
				DetailsState(level);
				break;
			}
			case GenState.Finalize:{
				FinalizeState(level);
				break;
			}
			case GenState.Complete:{
				break;
			}
		}
		return level.NextTurn();
	}
	//
	public virtual void AddStructureSpawner(Spawner spawner){
		_structures.Add(spawner);
	}
	public virtual void AddDetailSpawner(Spawner spawner){
		_details.Enqueue(spawner);
	}
	public virtual void AddBuild(int value = 1){
		_build = (_build + value);
	}
	public virtual void OnFinalize(Level level){
		_state = GenState.Complete;
		NukeSpawn(level);
		_player.GetSpawnable().Spawn(level, _x, _y);
	}
	public virtual int GetContentualBluePrintIndex(){
		if(_state == GenState.Details){
			return CLASSICGEN_DATA.GetRandomDetailBluePrintIndex();
		}
		return CLASSICGEN_DATA.GetRandomStructureBluePrintIndex();
	}
	//
	public void StructureState(Level level){
		if(_build > _minBuild || _structures.Count <= 0){
			_state = GenState.Details;
			for(int i = 0; i < _structures.Count; i++){
				_details.Enqueue(_structures[i]);
			}
			_structures.Clear();
			return;
		}
		//
		int index = (_structures.Count - 1);
		if(UnityEngine.Random.Range(0, 100) < 50){
			index = UnityEngine.Random.Range(0, _structures.Count);
		}
		_structures[index].Spawn(level, this);
		_structures.RemoveAt(index);
	}
	public void DetailsState(Level level){
		if(_details.Count <= 0){
			_state = GenState.Finalize;
			return;
		}
		_details.Dequeue().Spawn(level, this);
	}
	public void FinalizeState(Level level){
		_state = GenState.Null;
		_finalize.Spawn(level, this);
	}
	public void NukeSpawn(Level level){
		
	}
	//
	public override WorldUnit.IWorldUnit GetWorldUnit(){
		return this;
	}
	public override Unit.ISpawnable GetSpawnable(){
		return this;
	}
	public override Unit.IPositionable GetPositionable(){
		return this;
	}
	public override Register<Unit>.IRegisterable GetRegisterable(){
		return this;
	}
	public override Unit.IProcessable GetProcessable(){
		return this;
	}
	public override IClassicGen GetClassicGen(){
		return this;
	}
	public override Spawner.IFinalize GetFinalize(){
		return _finalize.GetFinalize();
	}
}
