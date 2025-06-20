
using System.Collections.Generic;
using UnityEngine;

public class TestJsom
{
    public void OnInit()
    {
        Dictionary<string,int> dic=new Dictionary<string, int>();
        dic = UnityTool.Instance.LoadData<Dictionary<string, int>>(Application.persistentDataPath + "Yang.json");
        dic.Add("na",1);
        Debug.Log(dic["na"]);
    }
    
}
