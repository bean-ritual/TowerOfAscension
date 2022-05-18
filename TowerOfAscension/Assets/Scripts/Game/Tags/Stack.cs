using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stack : 
	Tag,
	Tag.IFortifyValue1<int>,
	Tag.IDamageValue1<int>,
	Tag.ISubtractValue1<int>,
	Tag.IInput<Inventory>,
	Tag.IGetStringValue1
	{
	private const Tag.ID _TAG_ID = Tag.ID.Stack;
	private const int _MIN_STACK = 0;
	private int _stack;
	private int _maxStack;
	private string _stackID;
	public void Setup(string stackID){
		_stackID = stackID;
	}
	public override Tag.ID GetTagID(){
		return _TAG_ID;
	}
	public override void Disassemble(){
		//
	}
	public void FortifyValue1(Game game, Unit self, int value){
		_stack = Mathf.Clamp((_stack + value), _MIN_STACK, _maxStack);
		TagUpdateEvent();
	}
	public void DamageValue1(Game game, Unit self, int value){
		_stack = Mathf.Clamp((_stack - value), _MIN_STACK, _maxStack);
		TagUpdateEvent();
	}
	public int SubtractValue1(Game game, Unit self, int value){
		int newStack = Mathf.Clamp((_stack - value), _MIN_STACK, _maxStack);
		int subtract = (_stack - newStack);
		_stack = newStack;
		return subtract;
	}
	public void Input(Game game, Unit self, Inventory inventory){
		/*
		for(int i = 0; i < inventory.GetCount(); i++){
			Unit item = inventory.Get(i);
			if(item == self){
				continue;
			}
			item.GetTag(game, Tag.ID.Stack).GetIInputUnit().Input(game, item, self);
		}
		*/
	}
	public void Input(Game game, Unit self, Unit item){
		/*
		Tag stack = item.GetTag(Tag.ID.Stack);
		if(stack.GetIGetStringValue1().GetStringValue1(game, item) == _stackID){
			_stack = Mathf.Clamp(_stack + stack.GetI)
		}
		*/
	}
	public string GetStringValue1(Game game, Unit self){
		return _stackID;
	}
	public override Tag.IFortifyValue1<int> GetIFortifyValue1Int(){
		return this;
	}
	public override Tag.IDamageValue1<int> GetIDamageValue1Int(){
		return this;
	}
	public override Tag.ISubtractValue1<int> GetISubtractValue1Int(){
		return this;
	}
	public override Tag.IInput<Inventory> GetIInputInventory(){
		return this;
	}
	public override Tag.IGetStringValue1 GetIGetStringValue1(){
		return this;
	}
	public static Tag Create(string stackID){
		return new Stack();
	}
}
