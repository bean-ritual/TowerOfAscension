using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class SaveSystem{
	private static readonly string _DIRECTORY = Application.dataPath + "/" + "Saves";
	private static readonly string _FILE_NAME = "Save";
	private static readonly string _EXTENSION = ".save";
	public static bool Save(Game file){
		SettingsSystem.Save();
		return SerializationUtils.SaveSerialized<Game>(
			_FILE_NAME, 
			file, 
			SerializationUtils.BinaryFormatter(), 
			_DIRECTORY, 
			_EXTENSION
		);
	}
	public static bool Load(out Game file){
		SettingsSystem.Load();
		return SerializationUtils.LoadSerialized<Game>(
			_FILE_NAME, 
			out file, 
			SerializationUtils.BinaryFormatter(), 
			_DIRECTORY, 
			_EXTENSION
		);
	}
}