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
    [SerializeField] private Image rankImage;//rank图标
    [SerializeField] private Image avatar_normal;//段位图标
    [SerializeField] private Image levelHead;//段位头像框
    [SerializeField] private Text cups;//奖杯数
    [SerializeField] private Text userName;//玩家名
    [SerializeField] private Text rankNormal;//排名

    //固定路径
    private const string LevelHeadPath = "Sprites/LevelHead/arenaBadge_";
    private const string Avatar123Path = "Sprites/avatar/avatar_";
    private const string RankPath = "Sprites/Rank123/rank_";

    public void ChangeMyItem(IEnumerable<JsonData.UserJson> query)
    {
        for (int i = 1; i < query.Count() + 1; i++)
        {
            //根据id找到自己的信息并输入到banner上
            if (query.ElementAt(i - 1).uid == "3716954261")
            {
                //在banner上添加自己的信息
                int num = query.ElementAt(i - 1).trophy / 1000 + 1;
                //将自己的信息添加到banner上
                if (i <= 3)
                {
                    rankImage.sprite = Resources.Load<Sprite>(RankPath + i);
                    rankImage.gameObject.SetActive(true);
                    rankImage.SetNativeSize();
                    avatar_normal.sprite = Resources.Load<Sprite>(Avatar123Path + i);
                }
                else
                {
                    avatar_normal.sprite = Resources.Load<Sprite>("avatar/avatar_4");
                }

                rankNormal.text = "" + i;
                userName.text = query.ElementAt(i - 1).nickName;
                cups.text = "" + query.ElementAt(i - 1).trophy;
                levelHead.sprite = Resources.Load<Sprite>(LevelHeadPath + num);
                levelHead.SetNativeSize();
                break;
            }
        }
    }
}