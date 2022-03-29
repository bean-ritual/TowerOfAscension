using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class LoadSystem{
	private class DummyMonoBehaviour : MonoBehaviour{}
	public enum Scene{
		Game,
	};
	private const string _LOADING_SCENE = "Loading";
	private static Action OnLoadCallback;
	public static void Load(Scene scene, Action GameContent){
		UnityEngine.Debug.Log("Loading Scene :: " + scene.ToString());
		OnLoadCallback = () => {
			GameObject go = new GameObject("Content");
			go.AddComponent<DummyMonoBehaviour>().StartCoroutine(LoadContent(scene, GameContent));
		};
		SceneManager.LoadScene(_LOADING_SCENE);
	}
	public static IEnumerator LoadContent(Scene scene, Action GameContent){
		yield return null;
		GameContent?.Invoke();
		AsyncOperation op = SceneManager.LoadSceneAsync(scene.ToString());
		while(!op.isDone){
			yield return null;
		}
	}
	public static void LoadCallback(){
		if(OnLoadCallback != null){
			OnLoadCallback.Invoke();
			OnLoadCallback = null;
		}
	}
}
