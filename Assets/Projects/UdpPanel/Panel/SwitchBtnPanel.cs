using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using UnityEngine.UI;
using System;

public class SwitchBtnPanel : BasePanel
{
    public static SwitchBtnPanel Instance;

    public Button button;
    public Button[] ChildButton;

    public Sprite[] ChildSprite;
    public CanvasGroup ChildCanvas;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void InitFind()
    {
        base.InitFind();
        button = FindTool.FindChildComponent<Button>(transform, "Button");
        ChildButton = FindTool.FindChildNode(transform, "ChildButtons").GetComponentsInChildren<Button>();
        ChildCanvas = FindTool.FindChildComponent<CanvasGroup>(transform, "ChildButtons");
    }

    public override void InitEvent()
    {
        base.InitEvent();
        button.onClick.AddListener(() => {
            if(ChildCanvas.alpha == 0)
            {
                ChildCanvas.Open();
                button.GetComponent<Image>().sprite = ChildSprite[0];
            }
            else
            {
                ChildCanvas.Hide();
                button.GetComponent<Image>().sprite = ChildSprite[1];
            }
        });

        if(ChildButton.Length > 0)
        {
            for (int i = 0; i < ChildButton.Length; i++)
            {
                InitChildButton(ChildButton[i], i);
            }
        }
    }

    private void InitChildButton(Button button,int num)
    {
        button.onClick.AddListener(() => {
            WaitPanel.Instance.SendMessages(num);
            SetButtonSprite(num);
            button.GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
            UIState.SwitchPanel((PanelName)Enum.Parse(typeof(PanelName), num.ToString()));
            WaitPanel.Instance.StopCountDown();
            ChildCanvas.Hide();
        });
    }

    private void ResetChildBtn()
    {
        if(ChildButton.Length > 0)
        {
            foreach (var item in ChildButton)
            {
                item.GetComponent<RectTransform>().localScale = Vector3.one;
                item.GetComponent<Image>().sprite = ChildSprite[0];
            }
        }
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Hide()
    {
        base.Hide();
        Reset();
    }

    private void Reset()
    {
        Init();
    }

    public void Init()
    {
        ChildCanvas.Hide();
        ResetChildBtn();
        button.GetComponent<Image>().sprite = ChildSprite[1];
    }

    public void SetButtonSprite(int num)
    {
        ResetChildBtn();
        ChildButton[num].GetComponent<Image>().sprite = ChildSprite[1];
    }
}
