using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 朝代
/// </summary>
public enum Dynasty
{
    南朝 = 0,
    宋元气象 = 1,
    抗战记略 = 2,
    明清印记 = 3,
    晋代 = 4,
    民国人物 = 5,
    秦汉春秋 = 6,
    隋唐 = 7,
}

public class PicControl : MonoBehaviour
{
    public static PicControl Instance;

    public string[] Path;

    public List<List<Texture2D>> PicGroup = new List<List<Texture2D>>();

    private List<Texture2D> ds;
    private List<string> vs;

    public RawImage rawImage;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < Path.Length; i++)
        {
            vs = new List<string>();
            ds = new List<Texture2D>();

            vs = FileHandle.Instance.GetImagePath(Application.streamingAssetsPath + "/打包/" + Path[i]);
            for (int j = 0; j < vs.Count; j++)
            {
                ds.Add(FileHandle.Instance.LoadByIO(vs[j]));
            }

            PicGroup.Add(ds);
        }
    }

    private void Start()
    {
        rawImage.texture = PicGroup[0][0];
    }
}
