using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class SerializationUtils{
	public class Vector3SerializationSurrogate : ISerializationSurrogate{
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
			Vector3 vector3 = (Vector3)obj;
			info.AddValue("x", vector3.x);
			info.AddValue("y", vector3.y);
			info.AddValue("z", vector3.z);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
			Vector3 vector3 = (Vector3)obj;
			vector3.x = (float)info.GetValue("x", typeof(float));
			vector3.y = (float)info.GetValue("y", typeof(float));
			vector3.z = (float)info.GetValue("z", typeof(float));
			obj = vector3;
			return obj;
		}
	}
	public class Vector2SerializationSurrogate : ISerializationSurrogate{
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
			Vector2 vector2 = (Vector2)obj;
			info.AddValue("x", vector2.x);
			info.AddValue("y", vector2.y);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
			Vector2 vector2 = (Vector2)obj;
			vector2.x = (float)info.GetValue("x", typeof(float));
			vector2.y = (float)info.GetValue("y", typeof(float));
			obj = vector2;
			return obj;
		}
	}
	public class Vector2IntSerializationSurrogate : ISerializationSurrogate{
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context){
			Vector2Int vector2 = (Vector2Int)obj;
			info.AddValue("x", vector2.x);
			info.AddValue("y", vector2.y);
		}
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector){
			Vector2Int vector2 = (Vector2Int)obj;
			vector2.x = (int)info.GetValue("x", typeof(int));
			vector2.y = (int)info.GetValue("y", typeof(int));
			obj = vector2;
			return obj;
		}
	}
	public static bool SaveSerialized<T>(string file, T data, BinaryFormatter formatter, string directory, string extension = ".save"){
		if(!Directory.Exists(directory)){
			Directory.CreateDirectory(directory);
			Debug.Log("Save serialized directory created at: " + directory);
		}
		string path = Path.Combine(directory, file + extension);
		using(FileStream stream = new FileStream(path, FileMode.Create)){
			formatter.Serialize(stream, data);
		}
		Debug.Log("Save serialized complete at: " + path);
		return true;
	}
	public static bool LoadSerialized<T>(string file, out T data, BinaryFormatter formatter, string directory, string extension = ".save"){
		string path = Path.Combine(directory, file + extension);
		if(!File.Exists(path)){
			Debug.Log("File name does not exist at: " + path);
			data = default;
			return false;
		}
		if(formatter == null){
			Debug.Log("test");
		}
		try{
			using(FileStream stream = new FileStream(path, FileMode.Open)){
				data = (T)formatter.Deserialize(stream);
			}
			Debug.Log("Load serialized complete at: " + path);
			return true;
		}catch(Exception e){
			Debug.Log("Failed to load file at: " + path);
			Debug.Log(e.Message);
			data = default;
			return false;
		}
	}
	public static T CloneSerialized<T>(T clone, BinaryFormatter formatter){
		using(MemoryStream stream = new MemoryStream()){
			formatter.Serialize(stream, clone);
			stream.Position = 0;
			return (T)formatter.Deserialize(stream);
		}
	}
	public static BinaryFormatter BinaryFormatter(){
		BinaryFormatter formatter = new BinaryFormatter();
		SurrogateSelector surrogate = new SurrogateSelector();
		surrogate.AddSurrogate(
			typeof(Vector3), 
			new StreamingContext(StreamingContextStates.All), 
			new Vector3SerializationSurrogate()
		);
		surrogate.AddSurrogate(
			typeof(Vector2), 
			new StreamingContext(StreamingContextStates.All), 
			new Vector2SerializationSurrogate()
		);
		surrogate.AddSurrogate(
			typeof(Vector2Int), 
			new StreamingContext(StreamingContextStates.All), 
			new Vector2IntSerializationSurrogate()
		);
		formatter.SurrogateSelector = surrogate;
		return formatter;
	}
}