using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Attribute{
	public interface IReducer{
		int Reduce(Game game, Unit self, int value);
	}
	public interface IModifiableMaxes{
		void FortifyMax(Game game, Unit self, int value);
		void DamageMax(Game game, Unit self, int value);
	}
	[Serializable]
	public class NullAttribute : 
		Attribute,
		IReducer,
		IModifiableMaxes
		{
		public override void Fortify(Game game, Unit self, int value){}
		public override void Damage(Game game, Unit self, int value){}
		public override int GetValue(){
			return 0;
		}
		public override int GetMaxValue(){
			return 0;
		}
		public override void AttributeUpdateEvent(){}
		public int Reduce(Game game, Unit self, int value){
			return value;
		}
		public void FortifyMax(Game game, Unit self, int value){}
		public void DamageMax(Game game, Unit self, int value){}
	}
	[field:NonSerialized]public event EventHandler<EventArgs> OnAttributeUpdate;
	[field:NonSerialized]private static readonly NullAttribute _NULL_ATTRIBUTE = new NullAttribute();
	public Attribute(){}
	public abstract void Fortify(Game game, Unit self, int value);
	public abstract void Damage(Game game, Unit self, int value);
	public abstract int GetValue();
	public abstract int GetMaxValue();
	public virtual void AttributeUpdateEvent(){
		OnAttributeUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual IReducer GetReducer(){
		return _NULL_ATTRIBUTE;
	}
	public virtual IModifiableMaxes GetModifiableMaxes(){
		return _NULL_ATTRIBUTE;
	}
	public static Attribute GetNullAttribute(){
		return _NULL_ATTRIBUTE;
	}
}
