using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/**
 * 用来解析json文件，并将此文件的数据存储并供给其他脚本调用
 */
public class JsonData
{
    //将解析出来的倒计时提供给其他脚本使用
    public static int Time;
  
    //解析json文件，并存入jsonList中
    public static List<UserJson> JsonToData()
    {
        List<UserJson> jsonList = new List<UserJson>();
        //获取文件中的json字符串放入strJson中，然后通过JsonUtility.FromJson方法解析出数据
        StreamReader streamReader = new StreamReader(Application.dataPath + "/Resources/json/ranklist.json");
        string strJson = streamReader.ReadToEnd();
        //获取玩家信息
        jsonList = JsonUtility.FromJson<JsonHead>(strJson).list;
        //获取倒计时信息
        Time = JsonUtility.FromJson<JsonHead>(strJson).countDown;
        return jsonList;
    }

    /*
     * json文件实体类
     *  countDown:倒计时，单位,秒
     {
         "uid": "3716954261",//玩家id
         "nickName": "Player5278",//玩家昵称
         "avatar": 14,//玩家头像id，全部设置为userHead.png
         "trophy": 6978,//用户奖杯
     } 
     */
    //User实体类
    [Serializable]
    public class UserJson
    {
        //将json中需要用到的字段包装成一个实体类
        public string uid;
        public string nickName;
        public int trophy;
    }

    //json头
    private class JsonHead
    {
        public List<UserJson> list;
        public int countDown;
    }
}