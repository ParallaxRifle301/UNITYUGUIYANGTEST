using UnityEngine;
using UnityEngine.UI;
public class LoginSurePanel:IPanel
{
    private Button OkButton;
    private Button againBtn;
    private Text infotxt;
    protected override void OnInit()
    {
        base.OnInit();
        OkButton=GameObject.Find("OkButton").GetComponent<Button>();
        againBtn=GameObject.Find("AgainBtn").GetComponent<Button>();
        infotxt=GameObject.Find("InfoText").GetComponent<Text>();
        ServerPeopleNumData data=UnityTool.Instance.LoadData<ServerPeopleNumData>(Application.persistentDataPath + "/lastlogin.json");
        infotxt.text = data.txt;
        OkButton.onClick.AddListener(() =>
        {
            WarningText.text="成功进入游戏";
            OnShow();
            WarningButton.onClick.RemoveAllListeners();
            WarningButton.onClick.AddListener(() =>
            {
                OnHide();
                UIFactory.Instance.GetUIPanel(UIName.MainPanel);
            });
        });
        againBtn.onClick.AddListener(() =>
        {
            UIFactory.Instance.GetUIPanel(UIName.SelectPanel);
        });
    }
}
