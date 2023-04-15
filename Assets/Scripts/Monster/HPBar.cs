using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [HideInInspector] public Transform target = null;

    private string prefabKey = "UI/P_HP_Bar";
    private Image slide = null;
    private readonly Color green = Color.green;
    private readonly Color yellow = Color.yellow;
    private readonly Color orange = new Color(1f, 0.5f, 0f, 1f);
    private readonly Color red = Color.red;

    #region Cache
    private Camera cam = null;
    private RectTransform rectTr = null;
    private Canvas hpBarCanvas = null;
    private Vector3 offset;
    #endregion

    private void Awake()
    {
        if (!cam) cam = Camera.main;
        if (!hpBarCanvas) hpBarCanvas = GameObject.Find("HP_Bar_Canvas").GetComponent<Canvas>();
        
        float width = Screen.width * 0.08f;
        float height = width * 0.2f;

        Addressables.LoadAssetAsync<GameObject>(prefabKey).Completed += (op) =>
        {
            if (op.Status != AsyncOperationStatus.Succeeded)
                return;

            var go = Instantiate(op.Result);
            rectTr = go.GetComponent<RectTransform>();
            rectTr.sizeDelta = new Vector2(width, height);
            slide = go.transform.Find("Image_Front").GetComponent<Image>();
            slide.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            go.transform.SetParent(hpBarCanvas.transform);
        };

        offset = (Screen.height * 0.035f * Vector3.up);
    }


    private void Update()
    {
        if (!target || !rectTr) return;

        Vector3 worldToScreen = cam.WorldToScreenPoint(target.position);
        rectTr.position = worldToScreen + offset;
    }

    public void SetHPBar(float _curHp, float _maxHp)
    {
        if (_maxHp > 0f)
        {
            float ratio = _curHp / _maxHp;
            slide.fillAmount = ratio;

            if (ratio >= 0.75f)
                slide.color = green;
            else if (ratio >= 0.5f)
                slide.color = yellow;
            else if (ratio >= 0.25f)
                slide.color = orange;
            else
                slide.color = red;
        }
    }

    public void OnEnable()
    {
        if(rectTr)
            rectTr.gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        if(rectTr)
            rectTr.gameObject.SetActive(false);
    }
}
