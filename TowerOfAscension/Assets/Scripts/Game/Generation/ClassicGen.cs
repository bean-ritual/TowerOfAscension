using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ClassicGen : 
	Unit,
	VisualController.IVisualController,
	Unit.ISpawnable,
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
					case 5: return new LootSpawner(x, y);
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
			void PositionMaster(Game game, Unit master);
			int CalculateDistanceCost(Game game, int x, int y);
		}
		//
		[Serializable]
		public class NullSpawner : 
			Spawner,
			IFinalize,
			IExit
			{
			public NullSpawner(){}
			public override void Spawn(Game game, Unit master){}
			public override void AddToMaster(Game game, Unit master){}
			public void AddBuild(int value = 1){}
			public void AddExitSpawner(Spawner exit){}
			public bool RemoveExitSpawner(Spawner exit){
				return false;
			}
			public void PositionMaster(Game game, Unit master){}
			public int CalculateDistanceCost(Game game, int x, int y){
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
		public abstract void Spawn(Game game, Unit master);
		public abstract void AddToMaster(Game game, Unit master);
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
		public override void Spawn(Game game, Unit master){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(_index).Spawn(game, master, _x, _y);
		}
		public override void AddToMaster(Game game, Unit master){
			master.GetClassicGen().AddStructureSpawner(this);
		}
	}
	[Serializable]
	public class ContentualBluePrintSpawner : Spawner{
		public ContentualBluePrintSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Game game, Unit master){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(master.GetClassicGen().GetContentualBluePrintIndex()).Spawn(game, master, _x, _y);
		}
		public override void AddToMaster(Game game, Unit master){
			master.GetClassicGen().AddStructureSpawner(this);
		}
	}
	//
	[Serializable]
	public class ConnectorSpawner : Spawner{
		public ConnectorSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Game game, Unit master){
			if(game.GetLevel().Get(_x, _y).GetConnectable().CanConnect(game, master, _x, _y)){
				return;
			}
			if(game.GetLevel().GetCardinals(_x, _y, (Tile tile) => {return tile.GetConnectable().CanConnect(game, master, _x, _y);}).Count == 2){
				game.GetLevel().Set(_x, _y, new PathTile(_x, _y));
				Unit.UNIT_DATA.GetLevelledDoor(game, 0).GetSpawnable().Spawn(game, _x, _y);
			}
		}
		public override void AddToMaster(Game game, Unit master){
			master.GetClassicGen().AddDetailSpawner(this);
		}
	}
	[Serializable]
	public class ExitSpawner : 
		Spawner,
		Spawner.IExit
		{
		public ExitSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Game game, Unit master){
			Unit.UNIT_DATA.GetStairs(game).GetSpawnable().Spawn(game, _x, _y);
			master.GetClassicGen().GetFinalize().AddBuild();
		}
		public virtual void PositionMaster(Game game, Unit master){
			master.GetPositionable().SetPosition(game, _x, _y);
			master.GetClassicGen().GetFinalize().RemoveExitSpawner(this);
			master.GetClassicGen().GetFinalize().AddBuild();
		}
		public virtual int CalculateDistanceCost(Game game, int x, int y){
			return game.GetLevel().CalculateDistanceCost(x, y, _x, _y);
		}
		public override void AddToMaster(Game game, Unit master){
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
		public override void Spawn(Game game, Unit master){
			if(_exits.Count <= 0){
				return;
			}
			_exits[UnityEngine.Random.Range(0, _exits.Count)].GetExit().PositionMaster(game, master);
			master.GetPositionable().GetPosition(game, out int x, out int y);
			GetExitToSpawn(game, master, x, y).Spawn(game, master);
			if(_build >= (_amount + 1)){
				master.GetClassicGen().OnFinalize(game);
			}
		}
		public override void AddToMaster(Game game, Unit master){
			//
		}
		public virtual Spawner GetExitToSpawn(Game game, Unit master, int x, int y){
			Spawner exit = GetNullSpawner();
			if(_exits.Count <= 0){
				return exit;
			}
			int amount = 0;
			for(int i = 0; i < _exits.Count; i++){
				int distance = _exits[i].GetExit().CalculateDistanceCost(game, x, y);
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
		public override void Spawn(Game game, Unit master){
			Unit.UNIT_DATA.GetLevelledMonster(game, 0).GetSpawnable().Spawn(game, _x, _y);
		}
		public override void AddToMaster(Game game, Unit master){
			master.GetClassicGen().AddDetailSpawner(this);
		}
	}
	[Serializable]
	public class LootSpawner : Spawner{
		public LootSpawner(int x, int y) : base(x, y){}
		public override void Spawn(Game game, Unit master){
			Unit.UNIT_DATA.GetLevelledItem(game, 0).GetSpawnable().Spawn(game, _x, _y);
		}
		public override void AddToMaster(Game game, Unit master){
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
	private int _x = Unit.NullUnit.GetNullX();
	private int _y = Unit.NullUnit.GetNullY();
	private VisualController _controller = VisualController.GetNullVisualController();
	private GenState _state = GenState.Null;
	private List<Spawner> _structures;
	private Queue<Spawner> _details;
	private Spawner _finalize = Spawner.GetNullSpawner();
	private Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	private int _minBuild;
	private int _build;
	public ClassicGen(int minBuild = 10, int maxExits = 1){
		_controller = new VisualController();
		_controller.SetSprite(SpriteSheet.SpriteID.Stairs, 1);
		_controller.SetSortingOrder(10);
		_structures = new List<Spawner>();
		_details = new Queue<Spawner>();
		_finalize = new FinalSpawner(maxExits);
		_minBuild = minBuild;
		_build = 0;
	}
	public VisualController GetVisualController(Game game){
		return _controller;
	}
	public bool GetWorldVisibility(Game game){
		return true;
	}
	public void Spawn(Game game, int x, int y){
		Unit.Default_Spawn(this, game, x, y);
		_state = GenState.Structure;
		_structures.Add(Spawner.SPAWNER_DATA.GetContentualBluePrintSpawner(_x, _y));
	}
	public void Despawn(Game game){
		Unit.Default_Despawn(this, game);
	}
	public void SetPosition(Game game, int x, int y, int moveSpeed = 0){
		Unit.Default_SetPosition(this, game, x, y, ref _x, ref _y, moveSpeed);
	}
	public void GetPosition(Game game, out int x, out int y){
		x = _x;
		y = _y;
	}
	public void RemovePosition(Game game){
		Unit.Default_RemovePosition(this, game, _x, _y);
	}
	public Vector3 GetPosition(Game game){
		return game.GetLevel().GetWorldPosition(_x, _y);
	}
	public Tile GetTile(Game game){
		return game.GetLevel().Get(_x, _y);
	}
	public void Add(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public void Remove(Register<Unit> register){
		register.Remove(_id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public override bool Process(Game game){
		switch(_state){
			default: break;
			case GenState.Null: break;
			case GenState.Structure:{
				StructureState(game);
				break;
			}
			case GenState.Details:{
				DetailsState(game);
				break;
			}
			case GenState.Finalize:{
				FinalizeState(game);
				break;
			}
			case GenState.Complete:{
				break;
			}
		}
		return game.GetLevel().NextTurn();
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
	public virtual void OnFinalize(Game game){
		_state = GenState.Complete;
		NukeSpawn(game);
		game.GetPlayer().GetSpawnable().Spawn(game, _x, _y);
	}
	public virtual int GetContentualBluePrintIndex(){
		if(_state == GenState.Details){
			return CLASSICGEN_DATA.GetRandomDetailBluePrintIndex();
		}
		return CLASSICGEN_DATA.GetRandomStructureBluePrintIndex();
	}
	//
	public void StructureState(Game game){
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
		_structures[index].Spawn(game, this);
		_structures.RemoveAt(index);
	}
	public void DetailsState(Game game){
		if(_details.Count <= 0){
			_state = GenState.Finalize;
			return;
		}
		_details.Dequeue().Spawn(game, this);
	}
	public void FinalizeState(Game game){
		_state = GenState.Null;
		_finalize.Spawn(game, this);
	}
	public void NukeSpawn(Game game){
		
	}
	public bool IsFinished(){
		if(_state == GenState.Complete){
			return true;
		}
		return false;
	}
	//
	public override VisualController.IVisualController GetVisualController(){
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
	public override IClassicGen GetClassicGen(){
		return this;
	}
	public override Spawner.IFinalize GetFinalize(){
		return _finalize.GetFinalize();
	}
}
