using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Text;

public class ShopPage : MyUIBase
{
    [SerializeField] private FloatingMessage floatingMessage;
    [SerializeField] private AudioClip buySFX = null;
    [SerializeField] private Transform stoneListTr = null;
    [SerializeField] private AssetReference[] stonePrefabs = null;

    [Header("--Other Component References--")]
    [SerializeField] private CoinManager coinManager = null;

    private int[] costs = { 500, 500, 500, 500, 500, 500, 500, 500, 500, 400 };
    private string failMessage = "코인이 부족합니다.";
    

    protected override void Awake()
    {
        base.Awake();

        if (!stoneListTr) stoneListTr = FindObjectOfType<StoneList>().transform;
        if (!coinManager) coinManager = FindObjectOfType<CoinManager>();
    }

    public void PurchaseStone(int _index)
    {
        int index = _index;
        if (_index == 9) index = Random.Range(0, 9);

        if (coinManager.UseCoin(costs[_index]))
        {
            Addressables.InstantiateAsync(stonePrefabs[index], stoneListTr);
            SoundManager.Instance.PlayClip(buySFX);
            floatingMessage.ShowFloatingMessage(GetPurchaseMessage(index), 1f);
        }
        else
        {
            floatingMessage.ShowFloatingMessage(failMessage, 1f);
        }
    }

    private string GetPurchaseMessage(int _index)
    {
        StringBuilder sb = new StringBuilder();

        int i = 0;
        foreach(EWeaponStat stat in typeof(EWeaponStat).GetEnumValues())
        {
            if(i++ == _index)
            {
                sb.Append(Weapon.GetWeaponStatToUIString(stat));
                break;
            }
        }

        sb.Append(" 강화석을 구매하였습니다.");

        return sb.ToString();
    }
}
