using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singlton<GameManager>
{
    [SerializeField] private GameObject dialogCanvas = null;
    public string userName;

    [HideInInspector] public bool isInGame;

    public override void Awake()
    {
        base.Awake();
        userName = PlayerPrefs.GetString("UserName", "");
    }

    private void OnEnable()
    {
        if (!dialogCanvas) dialogCanvas = FindObjectOfType<Canvas>().gameObject;
        DontDestroyOnLoad(dialogCanvas);
    }

    private void Update() { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIContext.GetUIByPath("SettingPanel", (result) => {
                MyUIBase panel = result as MyUIBase;
                panel.Show();

                Time.timeScale = 0f;
            });
        }
    }
}
