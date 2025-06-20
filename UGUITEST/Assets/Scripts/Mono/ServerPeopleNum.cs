using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerPeopleNumData
{
    public int room;
    public bool isnew;
    public string txt;
}
public class ServerPeopleNum:MonoBehaviour
{
    public ServerPeopleNumData data=new ServerPeopleNumData();

    public string txtinfo
    {
        get
        {
            return this.GetComponentInChildren<Text>().text;
        }
        set
        {
            this.GetComponentInChildren<Text>().text=value;
            data.txt=value;
        }
    }
    public int room
    {
        get { return data.room; }
        set { data.room = value; }
    }
    public bool isnew
    {
        get { return data.isnew; }
        set { data.isnew = value; }
    }
    public Image img1;
    public Image img2;
    private int x = 10;
    private void Awake()
    {
        OnInit();
        ChangeImage();
    }
    private void OnInit()
    {
        img1=this.transform.GetChild(1).GetComponent<Image>();
        img2=this.transform.GetChild(2).GetComponent<Image>();
    }
    private void Update()
    {
        
    }
    public void ChangeImage()
    {
        if (!isnew&&img1!=null)
        {
            img1.gameObject.SetActive(false);
        }

        if (room >= x)
        {
            img2.sprite = Resources.Load<Sprite>("Artres/Sprites/crowd");
        }
        else
        {
            img2.sprite = Resources.Load<Sprite>("Artres/Sprites/nocrowd");
        }
    }
}
