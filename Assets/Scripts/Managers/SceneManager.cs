using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : Singlton<SceneManager>
{
    [SerializeField] private AssetReference introSceneRef;
    [SerializeField] private AssetReference inGameSceneRef;
    [SerializeField] private AssetReference emptySceneRef;

    [SerializeField] private Image loadingImage = null;

    private void Start()
    {
#if UNITY_EDITOR
#else
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(introSceneRef, LoadSceneMode.Single, true);
#endif
        if (!loadingImage) loadingImage = GameObject.FindGameObjectWithTag("LoadingImage").GetComponent<Image>();
    }

    public void LoadScene(int _index)
    {
        loadingImage.gameObject.SetActive(true);
        switch (_index)
        {
            case 0: 
                StartCoroutine(LoadSceneCoroutine(introSceneRef));
                GameManager.Instance.isInGame = false;
                break;
            case 1:
                StartCoroutine(LoadSceneCoroutine(inGameSceneRef));
                GameManager.Instance.isInGame = true;
                break;
        }
    }

    private IEnumerator LoadSceneCoroutine(AssetReference _sceneRef)
    {
        float time = 0f;
        Color color = loadingImage.color;

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(emptySceneRef, LoadSceneMode.Single, false);
        yield return handle;

        
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            while (time < 1f)
            {
                color.a = Mathf.Lerp(0f, 1f, time);
                loadingImage.color = color;
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            handle.Result.ActivateAsync();
        }
        
        
        handle = Addressables.LoadSceneAsync(_sceneRef, LoadSceneMode.Single, false);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {

            yield return handle.Result.ActivateAsync();
            
            time = 0f;
            while(time < 1f)
            {
                color.a = Mathf.Lerp(1f, 0f, time);
                loadingImage.color = color;
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            loadingImage.gameObject.SetActive(false);
        }
    }
}
