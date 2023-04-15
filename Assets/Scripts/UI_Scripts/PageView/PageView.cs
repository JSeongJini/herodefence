using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageView : MonoBehaviour
{
    [SerializeField] private MyUIBase[] pages;
    [SerializeField] private int mainPageIndex = 0;
    [SerializeField] private Button[] buttons;

    private int currentPageIndex;

    private void Start()
    {
        if(pages.Length != buttons.Length)
        {
            Debug.LogError("페이지 수와 버튼 수가 일치하지 않습니다.");
            return;
        }

        if (pages.Length > mainPageIndex)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (i != mainPageIndex) pages[i].Hide();
                else pages[i].Show();
            }
            currentPageIndex = mainPageIndex;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int tmp = i;
            buttons[i].onClick.AddListener(() => { SwapPage(tmp); });
        }
    }

    public void SwapPage(int _index)
    {
        if (currentPageIndex == _index || _index < 0 || _index >= pages.Length) return;

        pages[currentPageIndex].Hide();

        pages[_index].Show();

        currentPageIndex = _index;
    }
}
