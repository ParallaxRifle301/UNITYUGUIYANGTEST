
using System;
using System.Net.Http.Headers;
using Panels;
using UnityEngine;

public enum UIName
{
    MainPanel,
    SelectPanel,
    WarningPanel,
    LoginSurePanel,
}
public class UIFactory
{
    private static UIFactory instance;

    public static UIFactory Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new UIFactory();
            }
            return instance;
        }
    }

    public void GetUIPanel(UIName uiname)
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas.transform.childCount > 0)
        {
            for (int i = canvas.transform.childCount-1; i >=0; i--)
            {
                GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
            }
        }
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Artres/Prefabs/" + uiname.ToString()), canvas.transform);
        
    }
}
