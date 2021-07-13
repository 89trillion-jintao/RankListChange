using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * 实现视图顶端的倒计时
 */
public class CountDown : MonoBehaviour
{
    private int time, seconds, min, hour, day; //将json中的倒计时秒转换成时间阶梯单位

    [SerializeField] private Text countTxt; //获取页面上倒计时文本内容，并实时更换形成倒计时

    // Start is called before the first frame update
    void Start()
    {
        //从json中获取总秒数，并根据计算切为各种单位
        JsonData.JsonToData();
        time = JsonData.Time;
        seconds = time % 60;
        min = time / 60 % 60;
        hour = time / 360 % 24;
        day = time / 3600 / 24;
        //每隔一秒更新一次文本
        InvokeRepeating(nameof(Time_count), 0, 1);
    }

    //实现倒计时，当所有单位的值都减为0时，倒计时停止
    private void Time_count()
    {
        if (seconds == 0)
        {
            if (min == 0)
            {
                if (hour == 0)
                {
                    if (day == 0)
                    {
                        CancelInvoke();
                    }
                    hour = 24;
                    day--;
                }
                min = 60;
                hour--;
            }
            seconds = 60;
            min--;
        }
        seconds--;
        //将倒计时展示在文本上
        countTxt.text =
            "Ending in : " + day + "d " + hour + "h " + min + "m " + seconds + "s";
    }
}