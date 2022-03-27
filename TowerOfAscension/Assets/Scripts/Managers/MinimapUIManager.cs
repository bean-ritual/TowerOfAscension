using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MinimapUIManager : MonoBehaviour{
	private static MinimapUIManager _INSTANCE;
	private UIWindowManager _uiWindow;
	private TextureNavigationManager _texNav;
	[SerializeField]private GameObject _prefabUIWindow;
	[SerializeField]private GameObject _prefabMinimap;
	[SerializeField]private CameraManager _minimapCamera;
	[SerializeField]private Canvas _canvas;
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
		}
		_INSTANCE = this;
		BuildUI();
	}
	public void BuildUI(){
		GameObject go = Instantiate(_prefabUIWindow, this.transform);
		_uiWindow = go.GetComponent<UIWindowManager>();
		_uiWindow.Setup(
			"Minimap",
			true,
			_canvas,
			new UIWindowManager.UISizeData(
				false,
				new Vector2(500, 500),
				new Vector2(250, 250),
				new Vector2(100, 100),
				new Vector2(1000, 1000)
			)
		);
		_uiWindow.SetActive(true);
		GameObject go2 =  Instantiate(_prefabMinimap, _uiWindow.GetContent().transform);
		_texNav = go2.GetComponent<TextureNavigationManager>();
		_texNav.Setup(_minimapCamera, _uiWindow);
	}
	public void SetUnit(Unit unit){
		_texNav.SetUnit(unit);
	}
	public static MinimapUIManager GetInstance(){
		return _INSTANCE;
	}
}
