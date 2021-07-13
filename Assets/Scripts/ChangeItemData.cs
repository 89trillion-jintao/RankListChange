using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/**
 * 此类使用来渲染和填充所有显示在当前视图中的item；
 */
public class ChangeItemData : MonoBehaviour
{
    [SerializeField] private Text userName;//用户名
    [SerializeField] private Text cups;//奖杯数
    [SerializeField] public Text rankNormal;//排名
    [SerializeField] private Image levelHead;//排名头像框
    [SerializeField] private Image rankImage;//前三名头像
    [SerializeField] private Image avatarNormal;//段位图标
    [SerializeField] private Image listNormal;//前三名背景图

    //固定路径
    private const string LevelHeadPath = "Sprites/LevelHead/arenaBadge_";
    private const string RankListNormalPath = "Sprites/rankList/rank list_";
    private const string Avatar123Path = "Sprites/avatar/avatar_";
    private const string RankPath = "Sprites/Rank123/rank_";

    // ReSharper disable Unity.PerformanceAnalysis
    public void ChangeListNormal(JsonData.UserJson value, int ranking)
    {
        //计算当前的item的段位图标
        int num = value.trophy / 1000 + 1;
        //用户名
        userName.text = value.nickName;
        //排名
        rankNormal.text = "" + ranking;
        //奖杯数
        cups.text = "" + value.trophy;
        //段位
        levelHead.sprite = Resources.Load<Sprite>(LevelHeadPath + num);
        levelHead.SetNativeSize();
        //将前三名rank图标隐藏
        rankImage.gameObject.SetActive(false);
        //设置背景
        listNormal.sprite = Resources.Load<Sprite>("Sprites/rankList/rank list_normal");
        //设置段位图标
        avatarNormal.sprite = Resources.Load<Sprite>("Sprites/avatar/avatar_4");
        //前三名的特殊化处理
        if (ranking <= 3)
        {
            //将item背景设置成前三名独有的背景
            listNormal.sprite = Resources.Load<Sprite>(RankListNormalPath + ranking);
            //前三名独有的排名图标
            rankImage.sprite = Resources.Load<Sprite>(RankPath + ranking);
            rankImage.SetNativeSize();
            //前三名独有的头像框
            avatarNormal.sprite = Resources.Load<Sprite>(Avatar123Path + ranking);
            //将隐藏的排名图标显示出来
            GameObject openRankIma = rankImage.gameObject;
            openRankIma.SetActive(true);
        }
    }
}