using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 每点击一下item生成一个toast并在一段时间后自动销毁
 */
public class ToastHandler : MonoBehaviour
{
    private Toast toast;
    private RectTransform ToastParentTransform;
    [SerializeField] private Text listUserTxt;//获取玩家txt的rectTransform
    [SerializeField] private Text listRankTxt;//获取rank text的rectTransform

    private void Start()
    {
        //获取viewport作为父节点
        ToastParentTransform = CLoseAndOpen.getToastParentTransform();
    }

    //生成toast并获取点击item的值放在toast上
    public void GetToast()
    {
        Toast toastGO =Instantiate(Resources.Load<Toast>("prefabs/Toast"), ToastParentTransform); //　附加到父节点（需要显示的UI下）
        toastGO.NewToast(listUserTxt.text,listRankTxt.text);
        Destroy(toastGO.gameObject, 1); // 1秒后 销毁
    }
}