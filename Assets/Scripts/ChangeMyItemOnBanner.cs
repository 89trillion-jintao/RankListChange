using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/**
 * 此类是用来渲染和填充banner上玩家自身的信息；
 */
public class ChangeMyItemOnBanner : MonoBehaviour
{
    //自己信息
    [SerializeField] private Image rankImage; //rank图标
    [SerializeField] private Image avatar_normal; //段位图标
    [SerializeField] private Image levelHead; //段位头像框
    [SerializeField] private Text cups; //奖杯数
    [SerializeField] private Text userName; //玩家名
    [SerializeField] private Text rankNormal; //排名

    //固定路径
    private const string LevelHeadPath = "Sprites/LevelHead/arenaBadge_";
    private const string Avatar123Path = "Sprites/avatar/avatar_";
    private const string RankPath = "Sprites/Rank123/rank_";

    public void ChangeMyItem(IEnumerable<JsonData.UserJson> query, int index)
    {
        //在banner上添加自己的信息
        int num = query.ElementAt(index - 1).trophy / 1000 + 1;
        //将自己的信息添加到banner上
        if (index <= 3)
        {
            rankImage.sprite = Resources.Load<Sprite>(RankPath + index);
            rankImage.gameObject.SetActive(true);
            rankImage.SetNativeSize();
            avatar_normal.sprite = Resources.Load<Sprite>(Avatar123Path + index);
        }
        else
        {
            avatar_normal.sprite = Resources.Load<Sprite>("avatar/avatar_4");
        }

        rankNormal.text = "" + index;
        userName.text = query.ElementAt(index - 1).nickName;
        cups.text = "" + query.ElementAt(index - 1).trophy;
        levelHead.sprite = Resources.Load<Sprite>(LevelHeadPath + num);
        levelHead.SetNativeSize();
    }
}