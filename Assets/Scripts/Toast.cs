using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 给生成的toast赋值
 */
public class Toast : MonoBehaviour
{
    [SerializeField] private Text userTxt;
    [SerializeField] private Text rankTxt;
    public void NewToast(string listUserTxt,string listRankTxt)
    {
        userTxt.text= "User: " + listUserTxt;
        rankTxt.text = "Rank: " + listRankTxt;
    }

}
