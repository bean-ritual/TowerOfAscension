using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Generation{
	[Serializable]
	public class NullGeneration : Generation{
		public override void Process(Game game){}
		public override void Add(Spawner spawner, int index){}
		public override int GetContextualBluePrintIndex(){
			return -1;
		}
		public override void AddBuildCount(int amount){}
		public override bool IsFinished(){
			return false;
		}
		public override bool IsFailed(){
			return true;
		}
	}
	[Serializable]
	public class ClassicGeneration : Generation{
		public static class CLASSICGEN_DATA{
			private static readonly int[] _STRUCTURE_BLUEPRINTS;
			private static readonly int[] _DETAIL_BLUEPRINTS;
			static CLASSICGEN_DATA(){
				_STRUCTURE_BLUEPRINTS = new int[]{
					0,
					1,
					2,
					3,
				};
				_DETAIL_BLUEPRINTS = new int[]{
					2,
					4,
				};
			}
			public static int GetRandomStructureBluePrintIndex(){
				return _STRUCTURE_BLUEPRINTS[UnityEngine.Random.Range(0, _STRUCTURE_BLUEPRINTS.Length)];
			}
			public static int GetRandomDetailBluePrintIndex(){
				return _DETAIL_BLUEPRINTS[UnityEngine.Random.Range(0, _DETAIL_BLUEPRINTS.Length)];
			}
		}
		private enum State{
			Null,
			Initialize,
			Structure,
			Details,
			Finalize,
			Finished,
			Failed,
		};
		private int _buildCount;
		private int _minBuildCount;
		private State _state;
		private Spawner _start;
		private Spawner _exit;
		private Queue<Spawner>[] _spawners;
		public ClassicGeneration(int minBuildCount){
			_buildCount = 0;
			_minBuildCount = minBuildCount;
			_spawners = new Queue<Spawner>[]{
				new Queue<Spawner>(),
				new Queue<Spawner>(),
				new Queue<Spawner>(),
				new Queue<Spawner>(),
			};
			_start = Spawner.GetNullSpawner();
			_exit = Spawner.GetNullSpawner();
			_state = State.Initialize;
		}
		public override void Process(Game game){
			switch(_state){
				default: return;
				case State.Initialize:{
					State_Initialize(game);
					return;
				}
				case State.Structure:{
					State_Structure(game);
					return;
				}
				case State.Details:{
					State_Details(game);
					return;
				}
				case State.Finalize:{
					State_Finalize(game);
					return;
				}
			}
		}
		public override void Add(Spawner spawner, int index){
			if(index < 1 || index >= _spawners.Length){
				return;
			}
			if(index == 1 && UnityEngine.Random.Range(0, 100) < 50){
				index = 0;
			}
			_spawners[index].Enqueue(spawner);
		}
		public override int GetContextualBluePrintIndex(){
			if(_state == State.Details){
				return CLASSICGEN_DATA.GetRandomDetailBluePrintIndex();
			}else{
				return CLASSICGEN_DATA.GetRandomStructureBluePrintIndex();
			}
		}
		public override void AddBuildCount(int amount){
			_buildCount = (_buildCount + amount);
		}
		public override bool IsFinished(){
			return _state == State.Finished;
		}
		public override bool IsFailed(){
			return _state == State.Failed;
		}
		private void State_Initialize(Game game){
			game.GetMap().GetMidPoint(out int x, out int y);
			Spawner.SPAWNER_DATA.GetContextualBluePrintSpawner(x, y).Add(game);
			_state = State.Structure;
		}
		private void State_Structure(Game game){
			if(_buildCount > _minBuildCount){
				while(_spawners[0].Count > 0){
					_spawners[2].Enqueue(_spawners[0].Dequeue());
				}
				while(_spawners[1].Count > 0){
					_spawners[2].Enqueue(_spawners[1].Dequeue());
				}
				_state = State.Details;
				return;
			}
			if(_spawners[1].Count > 0){
				if(UnityEngine.Random.Range(0, 100) > 30){
					_spawners[1].Dequeue().Spawn(game);
					return;
				}
				if(_spawners[0].Count > 0){
					_spawners[0].Dequeue().Spawn(game);
					return;
				}else{
					_spawners[1].Dequeue().Spawn(game);
					return;
				}
			}
			if(_spawners[0].Count > 0){
				_spawners[0].Dequeue().Spawn(game);
				return;
			}else{
				_state = State.Failed;
				return;
			}
		}
		private void State_Details(Game game){
			if(_spawners[2].Count > 0){
				_spawners[2].Dequeue().Spawn(game);
				return;
			}else{
				_state = State.Finalize;
				return;
			}
		}
		private void State_Finalize(Game game){
			if(_spawners[3].Count >= 2){
				_start = _spawners[3].Dequeue();
				_exit = _spawners[3].Dequeue();
				int distance = game.GetMap().GetIMapmatics().CalculateDistanceCost(_start.GetX(), _start.GetY(), _exit.GetX(), _exit.GetY());
				while(_spawners[3].Count > 0){
					Spawner exit = _spawners[3].Dequeue();
					int newDist = game.GetMap().GetIMapmatics().CalculateDistanceCost(_start.GetX(), _start.GetY(), exit.GetX(), exit.GetY());
					if(newDist > distance){
						_exit = exit;
						distance = newDist;
					}
					if(UnityEngine.Random.Range(0, 100) < 30){
						Spawner swap = _start;
						_start = _exit;
						_exit = swap;
					}
				}
			}else{
				_state = State.Failed;
				return;
			}
			if(_start.IsNull() || _exit.IsNull()){
				_state = State.Failed;
				return;
			}else{
				_exit.Spawn(game);
				_start.GetXY(out int x, out int y);
				game.GetMap().Get(x, y).GetIDataTile().SetData(game, Data.DATA.CreateEnterTile(game, x, y));
				game.GetGameData().Get(0).GetBlock(game, 1).GetIWorldPosition().Spawn(game, x, y);
				return;
			}
		}
	}
	public abstract void Process(Game game);
	public abstract void Add(Spawner spawner, int index);
	public abstract int GetContextualBluePrintIndex();
	public abstract void AddBuildCount(int amount);
	public abstract bool IsFinished();
	public abstract bool IsFailed();
	private static NullGeneration _NULL_GENERATION = new NullGeneration();
	public static Generation GetNullGeneration(){
		return _NULL_GENERATION;
	}
}
