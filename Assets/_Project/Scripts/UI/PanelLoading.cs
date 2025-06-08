using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelLoading : BaseUI
{
    [SerializeField] W_Splash w_Splash;
    [SerializeField] TMP_Text txtPercentLoading;
    [SerializeField] Image imgLoadingProgress;
    bool isCompleted;

    protected override void OnEnable()
    {
        base.OnEnable();
        var uiCanvas = UIManager.Canvas;
        uiCanvas.sortingOrder = 100;
        uiCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);
        //w_Splash.StartLoading();
        Invoke(nameof(Completed), 0);
    }
    public void OnProgressPercent(float percent)
    {
       txtPercentLoading.text = "LOADING " + (percent * 100).ToString("0") + "%...";
       imgLoadingProgress.fillAmount = percent;
    }
    //public void OnComplete()
    //{
    //    Completed();
    //}

    private void Completed()
    {
        var dataGamePlay = DataManager.GamePlayUtility.Profile;
        Debug.Log("data");
        Debug.Log(dataGamePlay);
        if(isCompleted) return;
        isCompleted = true;
        GameController.Instance.LoadingGame(() =>
        {
            OnHide();
            API.Get<ServiceAds>().ShowBanner();
        });
    }
}