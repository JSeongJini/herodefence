using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This script loads the initial Scene, to allow to start the game from any gameplay Scene
/// </summary>
public class EditorInitialisationLoader : MonoBehaviour
{
#if UNITY_EDITOR
	[Tooltip("Scene Build Index must be same with InitialisationScene index")]
	public int sceneBuildIndex = 0;

	private static bool needInitial = true;

	private void Awake()
	{
		if (needInitial)
		{
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
			Invoke("UnloadScene", 3f);
			needInitial = false;
		}
	}

	private void UnloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneBuildIndex);
		Destroy(gameObject);
	}
#endif
}
