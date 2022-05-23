using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UVAtlas{
	public static class UVATLAS_DATA{
		public static readonly UVAtlas[] _UV_ATLASES;
		static UVATLAS_DATA(){
			_UV_ATLASES = new UVAtlas[]{
				SafeCreateUVAtlas(
					Resources.Load<Texture>("Colours"),
					() => {
						return new UVAtlas.UVPixel[]{
							new UVAtlas.UVPixel(new Vector2Int(0,0), new Vector2Int(1,1)),
							new UVAtlas.UVPixel(new Vector2Int(1,0), new Vector2Int(2,1)),
							new UVAtlas.UVPixel(new Vector2Int(2,0), new Vector2Int(3,1)),
							new UVAtlas.UVPixel(new Vector2Int(3,0), new Vector2Int(4,1)),
							new UVAtlas.UVPixel(new Vector2Int(4,0), new Vector2Int(5,1)),
							new UVAtlas.UVPixel(new Vector2Int(5,0), new Vector2Int(6,1)),
							new UVAtlas.UVPixel(new Vector2Int(6,0), new Vector2Int(7,1)),
							new UVAtlas.UVPixel(new Vector2Int(7,0), new Vector2Int(8,1))
						};
					}
				),
				SafeCreateUVAtlas(
					Resources.Load<Texture>("Tilemap"),
					() => {
						return new UVAtlas.UVPixel[]{
							new UVAtlas.UVPixel(new Vector2Int(0,0), new Vector2Int(32,32)),
							new UVAtlas.UVPixel(new Vector2Int(32,0), new Vector2Int(64,32)),
							new UVAtlas.UVPixel(new Vector2Int(64,0), new Vector2Int(96,32)),
							new UVAtlas.UVPixel(new Vector2Int(96,0), new Vector2Int(128,32)),
						};
					}
				),
				SafeCreateUVAtlas(
					Resources.Load<Texture>("Shadowmap"),
					() => {
						return new UVAtlas.UVPixel[]{
							new UVAtlas.UVPixel(new Vector2Int(0,0), new Vector2Int(1,1)),
							new UVAtlas.UVPixel(new Vector2Int(1,0), new Vector2Int(2,1)),
							new UVAtlas.UVPixel(new Vector2Int(2,0), new Vector2Int(3,1)),
						};
					}
				),
			};
			
		}
		public static UVAtlas SafeCreateUVAtlas(Texture texture, Func<UVPixel[]> SetUVPixels){
			if(texture == null){
				return UVAtlas.GetNullUVAtlas();
			}
			return new UVAtlas(texture, SetUVPixels);
		}
		public static UVAtlas GetUVAtlas(int id){
			if(id < 0 || id >= _UV_ATLASES.Length){
				return UVAtlas.GetNullUVAtlas();
			}else{
				return _UV_ATLASES[id];
			}
		}
	}
	[Serializable]
	public class NullUVAtlas : UVAtlas{
		private static readonly Vector2 _NULL_VECTOR2 = Vector2.zero;
		private static readonly UVNormal _NULL_UV_NORMAL = new UVNormal(Vector2.zero, Vector2.zero);
		public NullUVAtlas(){}
		public override UVNormal GetUVNormal(int index){
			return _NULL_UV_NORMAL;
		}
		public override void GetUVNormal(int index, out Vector2 uv00, out Vector2 uv11){
			uv00 = _NULL_VECTOR2;
			uv11 = _NULL_VECTOR2;
		}
		public override Texture GetTexture(){
			return Texture2D.blackTexture;
		}
		public override bool CheckBounds(int index){
			return false;
		}
	}
	[Serializable]
	public struct UVNormal{
		public Vector2 uv00;
		public Vector2 uv11;
		public UVNormal(Vector2 uv00, Vector2 uv11){
			this.uv00 = uv00;
			this.uv11 = uv11;
		}
	}
	[Serializable]
	public struct UVPixel{
		public Vector2Int uv00;
		public Vector2Int uv11;
		public UVPixel(Vector2Int uv00, Vector2Int uv11){
			this.uv00 = uv00;
			this.uv11 = uv11;
		}
	}
	[field:NonSerialized]private static readonly NullUVAtlas _NULL_UV_ATLAS = new NullUVAtlas();
	private Texture _texture;
	private UVPixel[] _pixels;
	private UVNormal[] _uvs;
	public UVAtlas(Texture texture, Func<UVPixel[]> SetUVPixels){
		_texture = texture;
		float width = texture.width;
		float height = texture.height;
		_pixels = SetUVPixels();
		_uvs = new UVNormal[(_pixels.Length + 1)];
		for(int i = 0; i < _pixels.Length; i++){
			UVPixel pixel = _pixels[i];
			_uvs[i] = new UVNormal(
				new Vector2(pixel.uv00.x / width, pixel.uv00.y / height),
				new Vector2(pixel.uv11.x / width, pixel.uv11.y / height)
			);
		}
	}
	public UVAtlas(){}
	public virtual UVNormal GetUVNormal(int index){
		const int DEFAULT_INDEX = 0;
		if(!CheckBounds(index)){
			index = DEFAULT_INDEX;
		}
		return _uvs[index];
	}
	public virtual void GetUVNormal(int index, out Vector2 uv00, out Vector2 uv11){
		UVNormal normal = GetUVNormal(index);
		uv00 = normal.uv00;
		uv11 = normal.uv11;
	}
	public virtual Texture GetTexture(){
		return _texture;
	}
	public virtual bool CheckBounds(int index){
		if(index < 0){
			return false;
		}
		if(index > _pixels.Length){
			return false;
		}
		return true;
	}	
	public static UVAtlas GetNullUVAtlas(){
		return _NULL_UV_ATLAS;
	}
}
