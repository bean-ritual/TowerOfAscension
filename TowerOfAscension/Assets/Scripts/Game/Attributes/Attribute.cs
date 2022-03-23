using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Attribute{
	public interface IReducer{
		int Reduce(Level level, Unit self, int value);
	}
	public interface IModifiableMaxes{
		void FortifyMax(Level level, Unit self, int value);
		void DamageMax(Level level, Unit self, int value);
	}
	[Serializable]
	public class NullAttribute : 
		Attribute,
		IReducer,
		IModifiableMaxes
		{
		public override void Fortify(Level level, Unit self, int value){}
		public override void Damage(Level level, Unit self, int value){}
		public override int GetValue(){
			return 0;
		}
		public override int GetMaxValue(){
			return 0;
		}
		public override void AttributeUpdateEvent(){}
		public int Reduce(Level level, Unit self, int value){
			return value;
		}
		public void FortifyMax(Level level, Unit self, int value){}
		public void DamageMax(Level level, Unit self, int value){}
	}
	[field:NonSerialized]public event EventHandler<EventArgs> OnAttributeUpdate;
	[field:NonSerialized]private static readonly NullAttribute _NULL_ATTRIBUTE = new NullAttribute();
	public Attribute(){}
	public abstract void Fortify(Level level, Unit self, int value);
	public abstract void Damage(Level level, Unit self, int value);
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
