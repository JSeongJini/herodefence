using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public abstract class PanelSelectBase<A, B> : MyUIBase where A : ScriptableObject where B : DataTable<A>
{
    [Header("Own Copmponent References")]
    [SerializeField] protected Button[] buttons = null;
    [SerializeField] protected Button rerollButton = null;
    [SerializeField] protected AudioClip rerollSFX = null;
    [SerializeField] protected ScalingOpenClose scaleUI = null;

    [Space]
    [Header("Others Copmponent References")]
    [SerializeField] protected GameSessionManager gameSessionManager = null;
    [SerializeField] protected B table = null;
    [SerializeField] protected Hero hero = null;
    [SerializeField] protected AdmobAdsController adController = null;

    protected A[] randomDatas = new A[3];
    protected RectTransform[] buttonRTs = null;
    protected StringBuilder sb = null;


    #region Unity MonoBehaviour Funtions
    protected override void Awake()
    {
        base.Awake();
        if (!gameSessionManager) gameSessionManager = FindObjectOfType<GameSessionManager>();
        if (!hero) hero = FindObjectOfType<Hero>();
        if (!table) table = FindObjectOfType<B>();
        if (!rerollButton) rerollButton = transform.Find("Reroll_Button").GetComponent<Button>();
        if (!adController) adController = AdmobAdsController.Instance;
        if (!scaleUI) scaleUI = GetComponent<ScalingOpenClose>();
    }

    protected virtual void Start()
    {
        buttonRTs = new RectTransform[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => { Select(temp); });
            buttonRTs[i] = buttons[i].GetComponent<RectTransform>();
        }
    }

    public virtual void OnEnable()
    {
        GameEventBus.Subscribe(EGameState.ROUNDEND, SetNewData);
    }

    public virtual void OnDisable()
    {
        GameEventBus.Unsubscribe(EGameState.ROUNDEND, SetNewData);
    }
    #endregion

    public virtual void SetNewData()
    {
        table.GetThreeRandomData(randomDatas);
        SetDataIntoComponents();

        rerollButton.gameObject.SetActive(true);
    }

    public void Reroll()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(RerollCorotine());
        rerollButton.gameObject.SetActive(false);
    }

    private IEnumerator RerollCorotine()
    {
        table.GetThreeRandomData(randomDatas);
        SetDataIntoComponents();
        float time = 0f;
        SoundManager.Instance.PlayClip(rerollSFX);
        while (time < 0.5f)
        {
            foreach (RectTransform rect in buttonRTs)
            {
                rect.rotation = Quaternion.Euler(0f, Mathf.Lerp(0f, 360f, time / 0.5f), 0f);
            }
            time += Time.unscaledTime;
            yield return null;
        }
        foreach (RectTransform rect in buttonRTs)
        {
            rect.rotation = Quaternion.identity;
        }
    }

    public override void Show()
    {
        base.Show();
        StartCoroutine(scaleUI.ScaleUpCoroutine());
        adController.OnEarendReward.AddListener(Reroll);
    }

    public override void Hide()
    {
        StartCoroutine(CloseCoroutine());
        adController.OnEarendReward.RemoveListener(Reroll);
    }

    private IEnumerator CloseCoroutine()
    {
        yield return scaleUI.StartCoroutine(scaleUI.ScaleDownCoroutine());
        base.Hide();

#if UNITY_EDITOR
        rectTransform.localScale = Vector3.one;
#endif
    }

    protected abstract void Select(int _index);
    protected abstract void SetDataIntoComponents();
}
