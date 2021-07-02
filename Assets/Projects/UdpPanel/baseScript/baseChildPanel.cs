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

    //public CanvasGroup[] ChildGroups;

    //public Vector2[] LocalVector2;

    //public Vector2[] TargetVector2;

    //图片移动到目标点的时间
    //public float AnimaTime = 2.0f;

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
        //ChildGroups = transform.Find("ChildGroups").GetComponentsInChildren<CanvasGroup>();
        buttons[0] = FindTool.FindChildComponent<Button>(transform, "LeftButton");
        buttons[1] = FindTool.FindChildComponent<Button>(transform, "RightButton");

        btnsCanvas[0] = FindTool.FindChildComponent<CanvasGroup>(transform, "LeftButton");
        btnsCanvas[1] = FindTool.FindChildComponent<CanvasGroup>(transform, "RightButton");
        //int childCount = transform.childCount;
        //if (childCount >= 1)
        //{
        //    images = new RectTransform[childCount];
        //    LocalVector2 = new Vector2[childCount];
        //    for (int i = 0; i < childCount; i++)
        //    {
        //        LocalVector2[i] = images[i].localPosition;
        //    }
        //}
        Reset();
    }

    private void Start()
    {
        if(Config.Instance)
        {
            //AnimaTime = Config.Instance.configData.AnimaTime;
            Times = Config.Instance.configData.PanelOpenTime; 
        }
    }

    public void Open()
    {
        transform.DOKill();
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.DOFade(1, Times).OnComplete(() => {
            //if (TargetVector2.Length > 0)
            //{
            //    for (int i = 0; i < LocalVector2.Length; i++)
            //    {
            //        images[i].DOAnchorPos(TargetVector2[i], AnimaTime).SetEase(Ease.Linear);
            //    }
            //}
            //if (ChildGroups.Length > 0)
            //foreach (var item in ChildGroups)
            //{
            //    item.Open(AnimaTime);
            //}
        });
    }

    public void Hide()
    {
        transform.DOKill();
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.DOFade(0, Times).OnComplete(() => {
            //if (TargetVector2.Length > 0)
            //{
            //    for (int i = 0; i < LocalVector2.Length; i++)
            //    {
            //        images[i].localPosition = LocalVector2[i];
            //    }
            //}
            //if (ChildGroups.Length > 0)
            //foreach (var item in ChildGroups)
            //{
            //    item.Hide();
            //}
        });
    }

    private void Reset()
    {
        CanvasGroup.Hide();
        //if (ChildGroups.Length > 0)
        //foreach (var item in ChildGroups)
        //{
        //    item.Hide();
        //}
    }
}
