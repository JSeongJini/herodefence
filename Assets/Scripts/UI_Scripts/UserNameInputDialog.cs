using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Events;

public class UserNameInputDialog : MyUIBase
{
    [SerializeField] private Button okButton;
    [SerializeField] private TMP_InputField input;

    private string userName;
    bool isPosting = false;

    public UnityEvent OnSuccessEvent = new UnityEvent();

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            if (isPosting) return;

            StartCoroutine(PostCoroutine());
        });
    }

    private IEnumerator PostCoroutine()
    {
        isPosting = true;
        
        userName = input.text.Trim();
        if (userName == "")
        {
            isPosting = false;
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("command", "register");
        form.AddField("userName", userName);
        NetworkData data = new NetworkData();


        MyWaitProgress waitProgress = null;
        UIContext.GetUIByPath("WaitProgress", (result) =>
        {
            waitProgress = result as MyWaitProgress;
            waitProgress.Show();
        });

        yield return StartCoroutine(GoogleSheetManager.Instance.Post(form, data));

        waitProgress?.Hide();


        if (data.result == "fail")
        {
            UIContext.GetUIByPath("Dialog/ErrorDialog", (result) =>
            {
                MyDialog dialog = result as MyDialog;
                dialog.SetContent("이미 존재하는 닉네임입니다.");
                dialog.Show();
                dialog.OnYesEvent.AddListener(Show);
            });
        }else if (data.result == "success")
        {
            GameManager.Instance.userName = userName;
            PlayerPrefs.SetString("UserName", userName);

            OnSuccessEvent?.Invoke();
        }
        Hide();

        isPosting = false;
    }
}
