using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MyDialog : MyUIBase
{
    [SerializeField] protected Text text;
    [SerializeField] protected Button okButton;
    [SerializeField] protected Button cancelButton;

    public UnityEvent OnYesEvent = new UnityEvent();
    public UnityEvent OnNoEvent = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();
        if(okButton) okButton.onClick.AddListener(HandleYes);
        if(cancelButton) cancelButton.onClick.AddListener(HandleNo);
    }

    protected virtual void HandleYes()
    {
        OnYesEvent?.Invoke();
        Hide();
    }

    protected virtual void HandleNo()
    {
        OnNoEvent?.Invoke();
        Hide();
    }

    public override void Hide()
    {
        base.Hide();
        OnYesEvent.RemoveAllListeners();
        OnNoEvent.RemoveAllListeners();
    }

    public void SetContent(string _content)
    {
        text.text = _content;
    }
}
