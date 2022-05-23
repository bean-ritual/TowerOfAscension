using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class SpriteSheet{
	public static class SPRITESHEET_DATA{
		private static SpriteSheet _SPRITESHEET;
		static SPRITESHEET_DATA(){
			_SPRITESHEET = SafeCreateSpriteSheet(Resources.LoadAll<Sprite>("SpriteSheet"));
		}
		public static SpriteSheet SafeCreateSpriteSheet(Sprite[] sprites){
			if(sprites.Length > 0){
				return new RealSpriteSheet(sprites);
			}
			return SpriteSheet.GetNullSpriteSheet();
		}
		public static Sprite GetSprite(int index){
			return _SPRITESHEET.GetSprite(index);
		}
	}
	[Serializable]
	public abstract class SpriteMap{
		[Serializable]
		public class NullSpriteMap : SpriteMap{}
		//
		private static NullSpriteMap _NULL_SPRITE_MAP = new NullSpriteMap();
		public static SpriteMap GetNullSpriteMap(){
			return _NULL_SPRITE_MAP;
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
	}
	[Serializable]
	public class RealSpriteSheet : SpriteSheet{
		private Sprite[] _sprites;
		public RealSpriteSheet(Sprite[] sprites){
			_sprites = sprites;
		}
		public override Sprite GetSprite(){
			return _sprites[0];
		}
		public override Sprite GetSprite(int index){
			if(index < 0 || index >= _sprites.Length){
				return _sprites[0];
			}else{
				return _sprites[index];
			}
		}
		public override Sprite GetRandomSprite(){
			return _sprites[UnityEngine.Random.Range(0, _sprites.Length)];
		}
	}
	//
	public abstract Sprite GetSprite();
	public abstract Sprite GetSprite(int index);
	public abstract Sprite GetRandomSprite();
	//
	private static readonly NullSpriteSheet _NULL_SPRITE_SHEET = new NullSpriteSheet();
	public static SpriteSheet GetNullSpriteSheet(){
		return _NULL_SPRITE_SHEET;
	}
}