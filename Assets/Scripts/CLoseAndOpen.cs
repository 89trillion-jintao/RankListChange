using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * 控制视图底部的按钮控制展示的列表
 */
public class CLoseAndOpen : MonoBehaviour
{
    private static CLoseAndOpen cLoseAndOpen;
    [SerializeField] private GameObject itemList; //获取itemList对象
    [SerializeField] private Text bottonBtnTxt;//按钮文本
    [SerializeField] private RectTransform contentRectTransform;//content
    [SerializeField] private RectTransform toastParentTransform;

    //初始化
    private void Awake() 
    {
        cLoseAndOpen = this;
    }
    //将viewport的rectTransform提供给其他脚本使用
    public static RectTransform getToastParentTransform()
    {
        return cLoseAndOpen.toastParentTransform;
    }

    // public GameObject btn;
    public void OnClick()
    {
        if (itemList.activeInHierarchy)
        {
            //根据当前状态修改button的文本内容
            bottonBtnTxt.text = "OPEN";
            itemList.SetActive(false);
            //当重新打开时，重置content内容
            contentRectTransform.position = new Vector3(0, 0, 0);

        }
        else
        {
            bottonBtnTxt.text = "CLOSE";
            itemList.SetActive(true);
        }
    }
}