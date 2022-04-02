using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class SettingsSystem{
	[Serializable]
	public class Config{
		public bool menuState;
		public UIWindowManager.UISizeData inventory;
		public UIWindowManager.UISizeData minimap;
		public UIWindowManager.UISizeData ground;
		public Config(){
			this.menuState = false;
			this.inventory = new UIWindowManager.UISizeData(
				false,
				new Vector2(0, (Screen.height / 2)),
				new Vector2(250, 215),
				new Vector2(250, 215),
				new Vector2(1000, 1000)
			);
			this.minimap = new UIWindowManager.UISizeData(
				false,
				new Vector2(Screen.width, Screen.height),
				new Vector2(350, 350),
				new Vector2(300, 300),
				new Vector2(1000, 1000)
			);
			this.ground = new UIWindowManager.UISizeData(
				false,
				new Vector2((Screen.width / 1.5f), (Screen.height / 1.5f)), 
				new Vector2(250, 215),
				new Vector2(250, 215),
				new Vector2(1000, 1000)
			);
		}
	}
	private static readonly string _DIRECTORY = Application.dataPath + "/" + "Settings";
	private static readonly string _FILE_NAME = "Config";
	private static readonly string _EXTENSION = ".cfg";
	private static Config _config;
	static SettingsSystem(){
		if(_config == null){
			Load();
		}
	}
	public static void Save(){
		SerializationUtils.SaveSerialized<Config>(
			_FILE_NAME, 
			_config, 
			SerializationUtils.BinaryFormatter(), 
			_DIRECTORY, 
			_EXTENSION
		);
	}
	public static void Load(){
		if(SerializationUtils.LoadSerialized<Config>(
			_FILE_NAME, 
			out Config config, 
			SerializationUtils.BinaryFormatter(), 
			_DIRECTORY, 
			_EXTENSION
		)){
			_config = config;
		}else{
			_config = new Config();
		}
	}
	public static Config GetConfig(){
		return _config;
	}
}
