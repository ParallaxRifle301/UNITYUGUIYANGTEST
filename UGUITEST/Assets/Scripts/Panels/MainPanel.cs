using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text;
using UnityEditor.SceneManagement;
[System.Serializable]
public class Users
{
    public string name;
    public string password;
    public Users(string name, string password)
    {
        this.name = name;
        this.password = password;
    }

    public Users()
    {
        
    }
    public string GetPassword()
    {
        return password;
    }
}
public class MainPanel:IPanel
{
    public InputField nameInput;
    public InputField passwordInput;
    public Button createUserBtn;
    public Button LoginBtn;
    public bool isremb;
    public bool islogin;
    public Toggle remtgl;
    public Toggle logintgl;
    public Dictionary<string, Users> users;
    public override void OnShow()
    {
        base.OnShow();
    }

    protected override void OnInit()
    {
        base.OnInit();
        users = new Dictionary<string, Users>();
        if (!File.Exists(Application.persistentDataPath + "/usersdata.json"))
        {
            
        }
        else
        {
            string jsonstr = File.ReadAllText(Application.persistentDataPath + "/usersdata.json"); 
            users = UnityTool.Instance.LoadData<Dictionary<string, Users>>(Application.persistentDataPath + "/usersdata.json");//JsonMapper.ToObject<Dictionary<string,Users>>(jsonstr);
            if(users == null)
            {
                users = new Dictionary<string, Users>();
            }
        }
        nameInput=GameObject.Find("用户名").GetComponentInChildren<InputField>();
        passwordInput=GameObject.Find("密码").GetComponentInChildren<InputField>();
        createUserBtn=GameObject.Find("注册").GetComponentInChildren<Button>();
        LoginBtn=GameObject.Find("登入").GetComponentInChildren<Button>();
        remtgl=GameObject.Find("记住密码").GetComponentInChildren<Toggle>();
        logintgl=GameObject.Find("自动登录").GetComponentInChildren<Toggle>();
        isremb = PlayerPrefs.GetInt("isremb",0) == 1;
        islogin = PlayerPrefs.GetInt("islogin",0) == 1;
        nameInput.text=PlayerPrefs.GetString("name");
        passwordInput.text=PlayerPrefs.GetString("password");
        createUserBtn.onClick.AddListener(CreateUser);
        LoginBtn.onClick.AddListener(Login);
        remtgl.isOn=isremb;
        logintgl.isOn=islogin;
    }

    private void CreateUser()
    {
        string name1=nameInput.text;
        string password1=passwordInput.text;
        Debug.Log(name1 + password1);
        if (!OnJudge())
        {
            Users user=new Users(name1, password1);
            users.Add(name1, user);
            string jsonstr = JsonMapper.ToJson(users);
            File.WriteAllText(Application.persistentDataPath + "/usersdata.json", jsonstr);
            WarningText.text = "注册成功";
            OnShow();
            PlayerPrefs.SetString("name",name1);
        }
        else
        {
            WarningText.text = "注册失败，用户名已存在";
            OnShow();
        }
    }
    private void Login()
    {
        if (OnJudge())
        {
            if (remtgl.isOn)
            {
                PlayerPrefs.SetInt("isremb",1) ;
                PlayerPrefs.SetInt("islogin",1);
                PlayerPrefs.SetString("name",nameInput.text);
                PlayerPrefs.SetString("password",passwordInput.text);
            }
            UIFactory.Instance.GetUIPanel(UIName.SelectPanel);
        }
    }

    protected override bool OnJudge()
    {
        string name1=nameInput.text;
        string password1 = passwordInput.text;
        if (users!=null&&users.ContainsKey(name1))
        {
            return users[name1].GetPassword() == password1;
        }
        return false;
    }
    public override void OnHide()
    {
        base.OnHide();
    }
}
