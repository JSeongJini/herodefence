using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine.Events;

public class UIContext
{
    private static IDictionary<string, UIPath> views = new Dictionary<string, UIPath>();

    public static void Subscribe(string _path, UIPath _view)
    {
        if (views.ContainsKey(_path))
        {
            Debug.LogWarning("UIView path must be Unique\n" + _path + " is alread exist.");
            GameObject.Destroy(_view.gameObject);
            return;
        }
        else
        {
            views.Add(_path, _view);
        }
    }

    public static void Unsubscribe(string _path)
    {
        if (views.ContainsKey(_path))
            views.Remove(_path);
    }

    public static void GetUIByPath(string _path, UnityAction<Component> _OnOpenUI = null)
    {
        UIPath ui = null;
        Component result = null;

        if (views.TryGetValue(_path, out ui))
        {
            result = ui.component;
            if(_OnOpenUI != null) _OnOpenUI(result);
        }
        else
        {
            //Addressable에서 path로 등록된 프리팹이 있는지 찾아보고
            //발견 시 객체화 하여 반환
            Transform parent = GameObject.FindGameObjectWithTag("DialogParent").transform;
            Addressables.InstantiateAsync(_path, parent).Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    result = op.Result.GetComponent<UIPath>().component;

                    if (_OnOpenUI != null) _OnOpenUI(result);
                }
            };
        }
    }

    public static void SetTextComponent(string _path, string _value)
    {
        UIPath ui;

        if (views.TryGetValue(_path, out ui))
        {
            Text textComp = ui.component as Text;
            textComp.text = _value;
        }
        else
        {
            Debug.LogWarning(_path + " is not exist.");
        }
    }

    public static void SetImageComponent(string _path, Sprite _image)
    {
        UIPath ui;

        if (views.TryGetValue(_path, out ui))
        {
            Image imageComp = ui.component as Image;
            imageComp.sprite = _image;
        }
        else
        {
            Debug.LogWarning(_path + " is not exist.");
        }
    }
}
