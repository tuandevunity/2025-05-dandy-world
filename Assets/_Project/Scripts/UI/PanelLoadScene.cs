using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PanelLoadScene : BaseUI
{
    [SerializeField] TMP_Text txtLoading;
    public void OnShow(Coroutine c, Action onDark = null, Action complete = null)
    {
        Show(0);
        StartCoroutine(IeWait(c, onDark, complete));
    }
    private IEnumerator LoadingEffect()
    {
        string baseText = "LOADING";
        int dotCount = 0;
        while(true)
        {
            txtLoading.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator IeWait(Coroutine c, Action onDark = null, Action complete = null)
    {
        StartCoroutine(LoadingEffect());
        Group.alpha = 1;
        var t = Group.DOFade(1, 0.3f);
        yield return c;
        yield return t.WaitForCompletion();
        onDark?.Invoke();
        t = Group.DOFade(1, 0.5f);
        yield return t.WaitForCompletion();
        complete?.Invoke();
        Hide();
    }
}
