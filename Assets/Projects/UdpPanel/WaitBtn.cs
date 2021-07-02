using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MTFrame;

[ExecuteInEditMode]
public class WaitBtn : BaseButton
{
    public Image image;
    public RectTransform rect;
    public Sprite[] sprites;
    public LoadSprite loadSprite;
   

    protected override void Awake()
    {
        base.Awake();
        image = transform.GetChild(0).GetComponent<Image>();
        rect = transform.GetChild(0).GetComponent<RectTransform>();
        loadSprite = transform.GetChild(0).GetComponent<LoadSprite>();
    }

    public override void TriggerDown()
    {
        base.TriggerDown();
        loadSprite.IsPlaying = false;
        if (sprites.Length > 0)
        image.sprite = sprites[1];
        rect.sizeDelta = Vector3.one * 90;
    }

    public override void TriggerUp()
    {
        base.TriggerUp();
        //if (sprites.Length > 0)
        //    image.sprite = sprites[0];
        loadSprite.IsPlaying = true;
        rect.sizeDelta = Vector3.one * 140;
    }
}
