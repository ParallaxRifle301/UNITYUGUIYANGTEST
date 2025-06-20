using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class IPanel : MonoBehaviour
{
    public Text WarningText{get;protected set;}
    public Image WarningPanel{get;protected set;}
    public Button WarningButton{get;protected set;}
    public virtual void OnShow()
    {
        WarningPanel.gameObject.SetActive(true);
    }
    public virtual void OnHide()
    {
        WarningPanel.gameObject.SetActive(false);
    }
    void Start()
    {
        OnInit();
    }
    protected virtual void OnInit()
    {
        WarningPanel=GameObject.Find("WarningCanvas").transform.GetChild(0).GetComponent<Image>();
        OnShow();
        WarningText = GameObject.Find("WarningText").GetComponentInChildren<Text>();
        WarningButton = WarningPanel.GetComponentInChildren<Button>();
        WarningButton.onClick.AddListener(OnHide);
        OnHide();
    }

    protected virtual bool OnJudge()
    {
        return true;
    }

    // protected virtual void TextShow()
    // {
    //     WarningText.gameObject.SetActive(true);
    //     Coroutine c1 = StartCoroutine(enumerator());
    // }
    // IEnumerator enumerator()
    // {
    //     yield return new WaitForSeconds(3f);
    //     WarningText.gameObject.SetActive(false);
    //     StopCoroutine(enumerator());
    // }
    // Update is called once per frame
    void Update()
    {
        
    }
}
