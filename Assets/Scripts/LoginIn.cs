using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 通过按钮控制跳转到主视图
 */
public class LoginIn : MonoBehaviour
{
    //获取进入前canvas对象
    [SerializeField] private GameObject firstCanvas;

    //获取进入后的canvas对象
    [SerializeField] private GameObject mainCanvas;

    public void OnClick()
    {
        firstCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}