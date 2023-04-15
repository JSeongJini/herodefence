using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] private AssetReference inGameSceneRef;

    [Space]
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject tap_Text;
    [SerializeField] private RectTransform shinningEffect;

    private float start = -920f;
    private float end = 920f;
    private float time = 0f;

    bool isLoading = false;

    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            if (isLoading) return;

            isLoading = true;
            if (GameManager.Instance.userName != "")
            {
                LoadInGameScene();
            }
            else
            {
                UIContext.GetUIByPath("Dialog/UserNameGetDialog", (result) =>
                {
                    UserNameInputDialog dialog = result as UserNameInputDialog;
                    dialog.OnSuccessEvent.AddListener(LoadInGameScene);
                    dialog.Show();
                });
            }
        });
    }

    private void Update()
    {
        shinningEffect.anchoredPosition = new Vector3(Mathf.Lerp(start, end, time/1f), 0f, 0f);
        time += Time.deltaTime;
        if(time >= 1f)
        {
            time = 0f;
        }
    }

    public void LoadInGameScene()
    {
        titleText.SetActive(false);
        tap_Text.SetActive(false);

        SceneManager.Instance.LoadScene(1);
    }
}
