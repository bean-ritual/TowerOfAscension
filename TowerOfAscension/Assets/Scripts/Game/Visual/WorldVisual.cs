using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WorldVisual : 
	Block.BaseBlock,
	IVisual
	{
	private int _sprite;
	private int _sortingOrder;
	public WorldVisual(int sprite, int sortingOrder){
		_sprite = sprite;
		_sortingOrder = sortingOrder;
	}
	public void PlayAnimation(Game game, WorldAnimation animation){
		if(GetRender(game)){
			WorldDataManager.GetInstance().PlayAnimation(animation);
		}
	}
	public int GetSprite(Game game){
		return _sprite;
	}
	public int GetSortingOrder(Game game){
		return _sortingOrder;
	}
	public virtual bool GetRender(Game game){
		return true;
	}
	public override void Disassemble(Game game){
		//
	}
	public override IVisual GetIVisual(){
		return this;
	}
}
