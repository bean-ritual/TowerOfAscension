using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClassicGen : 
	Unit,
	WorldUnit.IWorldUnit,
	Unit.ISpawnable,
	Unit.IProcessable
	{
	public static class CLASSICGEN_DATA{
		private static readonly int[] _STRUCTURE_BLUEPRINTS;
		private static readonly int[] _DETAIL_BLUEPRINTS;
		static CLASSICGEN_DATA(){
			_STRUCTURE_BLUEPRINTS = new int[]{
				0,
				1,
			};
			_DETAIL_BLUEPRINTS = new int[]{
				2,
			};
		}
		public static int GetRandomStructureBluePrintIndex(){
			return _STRUCTURE_BLUEPRINTS[UnityEngine.Random.Range(0, _STRUCTURE_BLUEPRINTS.Length)];
		}
		public static int GetRandomDetailBluePrintIndex(){
			return _DETAIL_BLUEPRINTS[UnityEngine.Random.Range(0, _DETAIL_BLUEPRINTS.Length)];
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
		//
		[Serializable]
		public class NullSpawner : Spawner{
			public NullSpawner(){}
			public override void Spawn(Level level, ClassicGen master){}
			public override void AddToMaster(Level level, ClassicGen master){}
		}
		[field:NonSerialized]private static readonly NullSpawner _NULL_SPAWNER = new NullSpawner();
		protected int _x;
		protected int _y;
		public Spawner(int x, int y){
			_x = x;
			_y = y;
		}
		public Spawner(){}
		public abstract void Spawn(Level level, ClassicGen master);
		public abstract void AddToMaster(Level level, ClassicGen master);
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
		public override void Spawn(Level level, ClassicGen master){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(_index).Spawn(level, master, _x, _y);
		}
		public override void AddToMaster(Level level, ClassicGen master){
			const ClassicGen.GenState BLUEPRINT_STATE = ClassicGen.GenState.Structure;
			master.AddSpawner(this, BLUEPRINT_STATE);
		}
	}
	[Serializable]
	public class ContentualBluePrintSpawner : Spawner{
		public ContentualBluePrintSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Level level, ClassicGen master){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(master.GetContentualBluePrintIndex()).Spawn(level, master, _x, _y);
		}
		public override void AddToMaster(Level level, ClassicGen master){
			const ClassicGen.GenState BLUEPRINT_STATE = ClassicGen.GenState.Structure;
			master.AddSpawner(this, BLUEPRINT_STATE);
		}
	}
	//
	[Serializable]
	public class ConnectorSpawner : Spawner{
		public ConnectorSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Level level, ClassicGen master){
			if(level.Get(_x, _y).GetConnectable().CanConnect(level, master, _x, _y)){
				return;
			}
			if(level.GetCardinals(_x, _y, (Tile tile) => {return tile.GetConnectable().CanConnect(level, master, _x, _y);}).Count >= 2){
				level.Set(_x, _y, new PathTile(_x, _y));
				Unit door = Door.DOOR_DATA.GetLevelledDoor(0);
				door.GetSpawnable().Spawn(level, _x, _y);
			}
		}
		public override void AddToMaster(Level level, ClassicGen master){
			const ClassicGen.GenState CONNECTOR_STATE = ClassicGen.GenState.Details;
			master.AddSpawner(this, CONNECTOR_STATE);
		}
	}
	[Serializable]
	public class ExitSpawner : Spawner{
		public ExitSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Level level, ClassicGen master){
			Unit stairs = new Stairs();
			stairs.GetSpawnable().Spawn(level, _x, _y);
		}
		public virtual void PositionMaster(Level level, ClassicGen master){
			master.SetPosition(level, _x, _y);
			master.RemoveExit(this);
		}
		public virtual int CalculateDistanceCost(Level level, int x, int y){
			return level.CalculateDistanceCost(x, y, _x, _y);
		}
		public override void AddToMaster(Level level, ClassicGen master){
			master.AddExit(this);
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
	public event EventHandler<EventArgs> OnWorldUnitUpdate;
	private int _x = Unit.NullUnit.GetNullX();
	private int _y = Unit.NullUnit.GetNullY();
	private GenState _state = GenState.Null;
	private Queue<Spawner> _structures = new Queue<Spawner>();
	private Queue<Spawner> _details = new Queue<Spawner>();
	private List<ExitSpawner> _exits = new List<ExitSpawner>();
	private Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	//
	private int _maxBuild;
	private int _build = 0;
	public ClassicGen(int maxBuild){
		_maxBuild = maxBuild;
	}	
	public virtual void AddSpawner(Spawner spawner, GenState state){
		GetSpawner(state).Enqueue(spawner);
	}
	public virtual void AddExit(ExitSpawner exit){
		_exits.Add(exit);
	}
	public virtual void RemoveExit(ExitSpawner exit){
		_exits.Remove(exit);
	}
	public virtual Queue<Spawner> GetSpawner(GenState state){
		switch(state){
			default: return new Queue<Spawner>();
			case GenState.Null: return new Queue<Spawner>();
			case GenState.Structure: return _structures;
			case GenState.Details: return _details;
		}
	}
	public virtual void AddBuild(int build = 1){
		_build = (_build + build);
	}
	public virtual int GetContentualBluePrintIndex(){
		if(_state == GenState.Details){
			return CLASSICGEN_DATA.GetRandomDetailBluePrintIndex();
		}
		return CLASSICGEN_DATA.GetRandomStructureBluePrintIndex();
	}
	public Sprite GetSprite(){
		return SpriteSheet.SPRITESHEET_DATA.GetSprite(SpriteSheet.SpriteID.Stairs, 1);
	}
	public int GetSortingOrder(){
		return 10;
	}
	public void Spawn(Level level, int x, int y){
		Unit.Default_Spawn(this, level, x, y);
		_state = GenState.Structure;
		_structures.Enqueue(Spawner.SPAWNER_DATA.GetContentualBluePrintSpawner(_x, _y));
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
	public void StructureState(Level level){
		if(_build > _maxBuild || _structures.Count <= 0){
			_state = GenState.Details;
			while(_structures.Count > 0){
				_details.Enqueue(_structures.Dequeue());
			}
			return;
		}
		_structures.Dequeue().Spawn(level, this);
	}
	public void DetailsState(Level level){
		if(_details.Count <= 0){
			_state = GenState.Finalize;
			return;
		}
		_details.Dequeue().Spawn(level, this);
	}
	public void FinalizeState(Level level){
		const int MIN_EXITS = 1;
		if(_exits.Count <= MIN_EXITS){
			_state = GenState.Null;
			return;
		}
		_exits[UnityEngine.Random.Range(0, _exits.Count)].PositionMaster(level, this);
		int index = 0;
		int amount = 0;
		for(int i = 0; i < _exits.Count; i++){
			int newAmount = _exits[i].CalculateDistanceCost(level, _x, _y);
			if(newAmount > amount){
				amount = newAmount;
				index = i;
			}
		}
		_exits[index].Spawn(level, this);
		_state = GenState.Complete;
	}
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
}
