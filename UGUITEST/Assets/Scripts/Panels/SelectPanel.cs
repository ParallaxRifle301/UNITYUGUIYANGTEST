using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using LitJson;
public class SelectPanel:IPanel
{
    public List<Button> SelectServer;
    public List<GameObject> Server;
    public Text ServerSelectText;
    private int serverCount;//服务器个数
    private GameObject serverSelectparent;//选择服务器下面的
    private GameObject serverparent;//服务器的背景
    private Button currentbtn;
    public ServerPeopleNumData lastLoginData;//上一个登录的信息
    public Button LastLoginBtn;//上一个登录的按钮
    protected override void OnInit()
    {
        base.OnInit();
        serverCount = 18;
        serverSelectparent = GameObject.Find("ServerSelectParent");
        serverparent = GameObject.Find("serverparent");
        GetSelectBtn();
        currentbtn=serverSelectparent.transform.GetChild(0).GetComponent<Button>();
        SetServerCount();
        GetLastLogin();
        ServerSelectText=GameObject.Find("ServerSelectText").GetComponent<Text>();
        ServerSelectText.text = currentbtn.GetComponentInChildren<Text>().text;
        
    }
    private void GetSelectBtn()
    {
        int x = serverCount / 5;
        int y = serverCount % 5;
        int num = serverSelectparent.transform.childCount;
        x++;
        if (x != num)
        {
            if (x > num)
            {
                for (int i = num; i < x; i++)
                {
                    GameObject obj = GameObject.Instantiate(Resources.Load("Artres/Prefabs/ServerSelectBtn"),
                            serverSelectparent.transform)
                        as GameObject;
                    Text txt = obj.GetComponentInChildren<Text>();
                    
                    if (i != x - 1)
                    {
                        txt.text = (i * 5 + 1).ToString() + "~" + ((i + 1) * 5).ToString() + "区";
                    }
                    else
                    {
                        txt.text = (i * 5 + 1).ToString() + "~" + (i * 5 + y).ToString() + "区";
                    }
                    Button btn = obj.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(()=>
                    {
                        currentbtn = btn;
                        SetServerCount();
                        ServerSelectText.text = currentbtn.GetComponentInChildren<Text>().text;
                    });
                }
            }
            else
            {
                for (int i = num; i >= x; i--)
                {
                    Destroy(serverSelectparent.transform.GetChild(i).gameObject);
                }
            }
        }
    }
    private void SetServerCount()
    {
        Text txt=currentbtn.GetComponentInChildren<Text>();
        string[] words=txt.text.Split('~');
        string[] words2 = words[1].Split('区');
        int x1=int.Parse(words[0]);
        int x2=int.Parse(words2[0]);
        int diff = x2 - x1 + 1;
        int num = serverparent.transform.childCount;
        if (num != diff)
        {
            if (diff > num)
            {
                for (int i = num; i < diff; i++)
                {
                    GameObject obj = GameObject.Instantiate(Resources.Load("Artres/Prefabs/Server"),
                            serverparent.transform)
                        as GameObject;
                    Button btn = obj.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(() =>
                    {
                        ServerPeopleNumData data = obj.GetComponent<ServerPeopleNum>().data;
                        Debug.Log(data.txt);
                        UnityTool.Instance.SaveData(Application.persistentDataPath + "/lastlogin.json",data);
                        UIFactory.Instance.GetUIPanel(UIName.LoginSurePanel);
                    });
                }
            }
            else
            {
                for (int i = num-1; i >= diff; i--)
                {
                    Destroy(serverparent.transform.GetChild(i).gameObject);
                }
            }
        }

        for (int i = x1; i <= x2; i++)
        {
            GameObject obj=serverparent.transform.GetChild(i-x1).gameObject;
            // Text txtname = obj.GetComponentInChildren<Text>();
            // txtname.text = "战区：" + i.ToString();
            ServerPeopleNum s=obj.GetComponent<ServerPeopleNum>();
            s.txtinfo= "战区：" + i.ToString();
        }
    }

    private void GetLastLogin()//得到上一次登录的信息
    {
        lastLoginData = UnityTool.Instance.LoadData<ServerPeopleNumData>(Application.persistentDataPath + "/lastlogin.json");
        GameObject obj=GameObject.Find("LastLoginBtn");
        LastLoginBtn = obj.GetComponent<Button>();
        LastLoginBtn.onClick.AddListener(() =>
        {
            ServerPeopleNumData data = obj.GetComponent<ServerPeopleNum>().data;
            Debug.Log(data.txt);
            UnityTool.Instance.SaveData(Application.persistentDataPath + "/lastlogin.json",data);
            UIFactory.Instance.GetUIPanel(UIName.LoginSurePanel);
        });
        ServerPeopleNum s= obj.GetComponent<ServerPeopleNum>();
        s.data=lastLoginData;
        s.txtinfo= lastLoginData.txt;
        s.ChangeImage();
    }
}
