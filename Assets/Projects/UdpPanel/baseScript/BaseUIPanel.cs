using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using System;
using UnityEngine.UI;

public class BaseUIPanel : BasePanel
{
    public baseChildPanel[] ChildPanels;

    public float Interval = 40f;

    private float _Interval;

    int Count = 0;

    public Dynasty dynasty;

    private List<Texture2D> texture = new List<Texture2D>();

    protected override void Start()
    {
        base.Start();

        Transform trans = FindTool.FindChildNode(transform, "ChildPanels");

        if (Config.Instance)
        {
            Interval = Config.Instance.configData.PanelSwitchTime;
            _Interval = Interval /*+ Config.Instance.configData.AnimaTime*/ + Config.Instance.configData.PanelOpenTime;
        }

        if(PicControl.Instance)
        {
            texture = PicControl.Instance.PicGroup[Convert.ToInt32(dynasty)];
            if (texture.Count > 0 && PoolManager.Instance)
            {
                for (int i = 0; i < texture.Count; i++)
                {
                    GameObject obj = PoolManager.Instance.GetPool(MTFrame.MTPool.PoolType.GenericProp);
                    obj.transform.SetParent(trans);
                    obj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    obj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    obj.GetComponent<RawImage>().texture = texture[i];
                }
            }

            ChildPanels = trans.GetComponentsInChildren<baseChildPanel>();

            if (ChildPanels.Length == 1)
            {
                ChildPanels[0].btnsCanvas[0].Hide();
                ChildPanels[0].btnsCanvas[1].Hide();
            }
            else
            {
                for (int i = 0; i < ChildPanels.Length; i++)
                {
                    if (i == 0)
                    {
                        ChildPanels[0].btnsCanvas[0].Hide();
                    }
                    else if (i == ChildPanels.Length - 1)
                    {
                        ChildPanels[i].btnsCanvas[1].Hide();
                    }

                    if (ChildPanels[i].buttons[0] != null)
                    {
                        ChildPanels[i].buttons[0].onClick.AddListener(() => {
                            StopCoroutine("ChildPanelSwitch");
                            Count = Count - 2;
                            StartCoroutine("ChildPanelSwitch");
                        });
                    }

                    if (ChildPanels[i].buttons[1] != null)
                    {
                        ChildPanels[i].buttons[1].onClick.AddListener(() => {
                            StopCoroutine("ChildPanelSwitch");
                            StartCoroutine("ChildPanelSwitch");
                        });
                    }
                }
            }
        }
        
        Reset();
    }

    //public override void InitFind()
    //{
    //    base.InitFind();
    //    ChildPanels = FindTool.FindChildNode(transform, "ChildPanels").GetComponentsInChildren<baseChildPanel>();
    //}

    //public override void InitEvent()
    //{
    //    base.InitEvent();

    //    if(ChildPanels.Length == 1)
    //    {
    //        ChildPanels[0].btnsCanvas[0].Hide();
    //        ChildPanels[0].btnsCanvas[1].Hide();
    //    }
    //    else
    //    {
    //        for (int i = 0; i < ChildPanels.Length; i++)
    //        {
    //            if (i == 0)
    //            {
    //                ChildPanels[0].btnsCanvas[0].Hide();
    //            }
    //            else if(i == ChildPanels.Length - 1)
    //            {
    //                ChildPanels[i].btnsCanvas[1].Hide();
    //            }

    //            if (ChildPanels[i].buttons[0] != null)
    //            {
    //                ChildPanels[i].buttons[0].onClick.AddListener(() => {
    //                    StopCoroutine("ChildPanelSwitch");
    //                    Count = Count - 2;
    //                    StartCoroutine("ChildPanelSwitch");
    //                });
    //            }

    //            if (ChildPanels[i].buttons[1] != null)
    //            {
    //                ChildPanels[i].buttons[1].onClick.AddListener(() => {
    //                    StopCoroutine("ChildPanelSwitch");
    //                    StartCoroutine("ChildPanelSwitch");
    //                });
    //            }
    //        }
    //    }


    //}

    public override void Open()
    {
        base.Open();
        StartCoroutine(Open2());
    }

    public override void Hide()
    {
        base.Hide();
        StopCoroutine("ChildPanelSwitch");
        foreach (var item in ChildPanels)
        {
            item.Hide();
        }
        Reset();
    }

    private IEnumerator ChildPanelSwitch()
    {
        foreach (var item in ChildPanels)
        {
            item.Hide();
        }
        ChildPanels[Count].Open();
        Count++;
        if(Count > ChildPanels.Length - 1)
        {
            yield return new WaitForSeconds(_Interval - Interval);
            StopCoroutine("ChildPanelSwitch");
            if(WaitPanel.Instance)
            {
                WaitPanel.Instance.StarCountDown();
            }
        }
        else
        {
            yield return new WaitForSeconds(_Interval);
            StartCoroutine("ChildPanelSwitch");
        }
    }

    private void Reset()
    {
        Count = 0;
    }

    private IEnumerator Open2()
    {
        yield return new WaitForEndOfFrame();
        if (ChildPanels.Length > 0)
            StartCoroutine("ChildPanelSwitch");

    }
}
