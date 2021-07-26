using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MTFrame;

[ExecuteInEditMode]
public class baseChildPanel : MonoBehaviour
{
    public CanvasGroup CanvasGroup;

    //CanvasGroup打开动画的时间
    public float Times = 2.0f;

    /// <summary>
    /// buttons[0] == 左边按钮 buttons[1] == 右边按钮
    /// </summary>
    public Button[] buttons = new Button[2];
    public CanvasGroup[] btnsCanvas = new CanvasGroup[2];


    private void Awake()
    {
        CanvasGroup = transform.GetComponent<CanvasGroup>();
        buttons[0] = FindTool.FindChildComponent<Button>(transform, "LeftButton");
        buttons[1] = FindTool.FindChildComponent<Button>(transform, "RightButton");

        btnsCanvas[0] = FindTool.FindChildComponent<CanvasGroup>(transform, "LeftButton");
        btnsCanvas[1] = FindTool.FindChildComponent<CanvasGroup>(transform, "RightButton");

        Reset();
    }

    private void Start()
    {
        if(Config.Instance)
        {
            Times = Config.Instance.configData.PanelOpenTime; 
        }
    }

    public void Open()
    {
        transform.DOKill();
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.DOFade(1, Times);
    }

    public void Hide()
    {
        transform.DOKill();
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.DOFade(0, Times);
    }

    private void Reset()
    {
        CanvasGroup.Hide();
    }
}
