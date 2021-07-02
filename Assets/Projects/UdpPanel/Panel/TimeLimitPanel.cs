using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTFrame;
using System;

public class TimeLimitPanel : BasePanel
{
    string data1 = "2021/7/10 00:00:00";
    string Code = "";

    protected override void Awake()
    {
        base.Awake();
        Hide();
    }

    protected override void Start()
    {
        base.Start();
        if(Config.Instance)
        {
            Code = Config.Instance.configData.Code;
        }

        CompanyDate(System.DateTime.Now.ToString(), data1);
    }

    public void CompanyDate(string dateStr1, string dateStr2)
    {
        if(  !Code.Contains("aabb"))
        {
            //将日期字符串转换为日期对象
            DateTime t1 = Convert.ToDateTime(dateStr1);
            DateTime t2 = Convert.ToDateTime(dateStr2);
            //通过DateTIme.Compare()进行比较（）
            int num = DateTime.Compare(t1, t2);
            //t1> t2
            if (num > 0)
            {
                Open();
            }
        }
    }
}
