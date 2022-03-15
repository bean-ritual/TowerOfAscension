using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SpriteSheet{
	public static class SPRITESHEET_DATA{
		private static readonly SpriteSheet[] _SPRITESHEETS;
		static SPRITESHEET_DATA(){
			_SPRITESHEETS = new SpriteSheet[]{
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Misc.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Hero.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Door.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Rat.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Stairs.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Swords.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Potions.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Chestplates.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Boots.ToString())),
				SafeCreateSpriteSheet(Resources.LoadAll<Sprite>(SpriteID.Gold.ToString())),
			};
		}
		public static SpriteSheet SafeCreateSpriteSheet(Sprite[] sprites){
			if(sprites.Length > 0){
				return new SpriteSheet(sprites);
			}
			return SpriteSheet.GetNullSpriteSheet();
		}
		public static Sprite GetSprite(SpriteID id, int index = 0){
			return GetSpriteSheet(id).GetSprite(index);
		}
		public static int GetRandomSpriteIndex(SpriteID id){
			return GetSpriteSheet(id).GetRandomSpriteIndex();
		}
		public static SpriteSheet GetSpriteSheet(SpriteID id){
			switch(id){
				default: return SpriteSheet.GetNullSpriteSheet();
				case SpriteID.Null: return SpriteSheet.GetNullSpriteSheet();
				case SpriteID.Misc: return _SPRITESHEETS[0];
				case SpriteID.Hero: return _SPRITESHEETS[1];
				case SpriteID.Door: return _SPRITESHEETS[2];
				case SpriteID.Rat: return _SPRITESHEETS[3];
				case SpriteID.Stairs: return _SPRITESHEETS[4];
				case SpriteID.Swords: return _SPRITESHEETS[5];
				case SpriteID.Potions: return _SPRITESHEETS[6];
				case SpriteID.Chestplates: return _SPRITESHEETS[7];
				case SpriteID.Boots: return _SPRITESHEETS[8];
				case SpriteID.Gold: return _SPRITESHEETS[9];
			}
		}
	}
	[Serializable]
	public class NullSpriteSheet : SpriteSheet{
		private static readonly Sprite _NULL_SPRITE = Sprite.Create(Texture2D.blackTexture, Rect.zero, Vector2.zero);
		public NullSpriteSheet(){}
		public override Sprite GetSprite(){
			return _NULL_SPRITE;
		}
		public override Sprite GetSprite(int index){
			return _NULL_SPRITE;
		}
		public override Sprite GetRandomSprite(){
			return _NULL_SPRITE;
		}
		public override int GetRandomSpriteIndex(){
			return 0;
		}
		public override bool CheckArrayBounds(int index){
			return false;
		}
		public static Sprite GetNullSprite(){
			return _NULL_SPRITE;
		}
	}
	public enum SpriteID{
		Null,
		Misc,
		Hero,
		Door,
		Rat,
		Stairs,
		Swords,
		Potions,
		Chestplates,
		Boots,
		Gold,
	};
	[field:NonSerialized]private static readonly NullSpriteSheet _NULL_SPRITE_SHEET = new NullSpriteSheet();
	private const int _DEFAULT_INDEX = 0;
	[SerializeField]private Sprite[] _sprites;
	public SpriteSheet(Sprite[] sprites){
		_sprites = sprites;
	}
	public SpriteSheet(){}
	public virtual Sprite GetSprite(){
		return _sprites[_DEFAULT_INDEX];
	}
	public virtual Sprite GetSprite(int index){
		if(!CheckArrayBounds(index)){
			index = _DEFAULT_INDEX;
		}
		return _sprites[index];
	}
	public virtual Sprite GetRandomSprite(){
		return _sprites[UnityEngine.Random.Range(0, _sprites.Length)];
	}
	public virtual int GetRandomSpriteIndex(){
		return UnityEngine.Random.Range(0, _sprites.Length);
	}
	public virtual bool CheckArrayBounds(int index){
		if(index < 0){
			return false;
		}
		if(index > _sprites.Length){
			return false;
		}
		return true;
	}
	public static SpriteSheet GetNullSpriteSheet(){
		return _NULL_SPRITE_SHEET;
	}
}