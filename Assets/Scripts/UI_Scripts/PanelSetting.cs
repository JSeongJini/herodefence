using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSetting : MyUIBase
{
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider effectSlider;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button escapeButton;

    private void Start()
    {
        if (resumeButton) resumeButton.onClick.AddListener(Resume);
        if (restartButton) restartButton.onClick.AddListener(Restart);
        if (escapeButton) escapeButton.onClick.AddListener(ShowEscapeDialog);
        if (restartButton) backgroundSlider.onValueChanged.AddListener(OnBackgroundVolumeChanged);
        if (effectSlider) effectSlider.onValueChanged.AddListener(OnEffectVolumeChanged);
    }

    public override void Show()
    {
        base.Show();
        restartButton.interactable = GameManager.Instance.isInGame;
    }

    public void OnBackgroundVolumeChanged(float _value)
    {
        SoundManager.Instance.backgroundVolume = _value;
    }

    public void OnEffectVolumeChanged(float _value)
    {
        SoundManager.Instance.effectVolume = _value;
    }

    private void Resume()
    {
        Hide();
        Time.timeScale = 1f;
    }

    private void ShowEscapeDialog()
    {
        UIContext.GetUIByPath("Dialog/EscapeDialog", (result) =>
        {
            MyDialog dialog = result as MyDialog;
            dialog.OnYesEvent.AddListener(GameQuit);
            dialog.Show();
        });
    }

    private void Restart()
    {
        Hide();
        SceneManager.Instance.LoadScene(1);
        System.GC.Collect();
    }

    private void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
