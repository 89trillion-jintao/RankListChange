using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/**
 * 此类是用来在滑动列表中生成预制件和实现预制件复用的，并调用其他脚本实现对预制件进行渲染填充；
 */
public class ListData : MonoBehaviour
{
    [SerializeField] private RectTransform contentTransform; //获取item生成位置的父节点
    [SerializeField] private RectTransform viewportRect;
    [SerializeField] private ChangeMyItemOnBanner myTrans; //获取存放自己信息banner
    private GameObject loopGo; //处理当前需要复用的item
    private LinkedList<ChangeItemData> goList = new LinkedList<ChangeItemData>(); //用来存放从JsonData中返回的所有玩家数据的list
    private Vector3[] viewPort = new Vector3[4]; //用来存放获取viewport的世界坐标
    private Vector3[] itemFirst = new Vector3[4]; //用来存放获取列表中第一个item的世界坐标
    private Vector3[] itemLast = new Vector3[4]; //用来存放获取列表中最后一个item的世界坐标
    private IEnumerable<JsonData.UserJson> query; //用来存放排序后的jsonList
    private GameObject destroyItem; //把已创建的销毁
    private int destroyCount = 1; //计算销毁次数

    private void Start()
    {
        //获取viewport的世界坐标
        viewportRect.GetWorldCorners(viewPort);
        CreateDataFromJson();
    }

    //根据json数据给创建的预制件填充内容
    public void CreateDataFromJson()
    {
        List<JsonData.UserJson> list = JsonData.JsonToData();
        //根据json数据的数量设置content的宽和长
        contentTransform.sizeDelta = new Vector2(500f, list.Count * 156f + 2f);
        //根据奖杯数给json逆序排序
        query = from items in list orderby items.trophy descending select items;
        //修改自己的数据
        for (int i = 1; i < query.Count() + 1; i++)
        {
            //根据id找到自己的信息并输入到banner上
            if (query.ElementAt(i - 1).uid == "3716954261")
            {
                myTrans.ChangeMyItem(query, i);
            }
        }

        //创建7个预制件并复用用来展示item
        for (int index = 0; index < 7; index++)
        {
            ChangeItemData go = Instantiate(Resources.Load<ChangeItemData>("prefabs/List_normal"), contentTransform);
            go.name = "List_normal" + (index + 1);
            //创建后赋值，渲染item
            go.ChangeListNormal(query.ElementAt(index), index + 1);
            goList.AddLast(go);
        }
    }


    private void Update()
    {
        LoopItem();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    ///
    /// question：ListData为什么要根据内容的text解析index来判断复用？
    /// 
    /// answer：通过text解析出来的index确定复用的item应该填充第几条的内容，防止滑动列表内容填充紊乱
    ///
    /// </summary>
    //实现列表内元素复用
    private void LoopItem()
    {
        //判断当前视图是否隐藏，如果隐藏便重置content
        if (viewportRect.gameObject.activeInHierarchy == false)
        {
            if (destroyCount != -1)
            {
                for (int i = 1; i <= 7; i++)
                {
                    destroyItem = contentTransform.Find("List_normal" + i).gameObject;
                    Destroy(destroyItem);
                }
                goList = new LinkedList<ChangeItemData>();
                CreateDataFromJson();
            }
            destroyCount = -1;
            return;
        }

        //获取列表中第一条和最后一条的rectTransform用来找到元素的坐标
        RectTransform itemFirstRect = goList.First.Value.transform.GetComponent<RectTransform>();
        RectTransform itemLastRect = goList.Last.Value.transform.GetComponent<RectTransform>();
        itemFirstRect.GetWorldCorners(itemFirst); //获取第一条世界坐标
        itemLastRect.GetWorldCorners(itemLast); //获取最后一条世界坐标
        //判断当前item是否移出了viewport的可视范围
        if (itemFirst[0].y > viewPort[1].y)
        {
            //获取复用时临近的item的信息，根据id判定，由此计算出当前这条item是第几名玩家
            int id = int.Parse(goList.Last.Value.rankNormal.text);
            if (id < 42)
            {
                //将第一条移至最后一条进行复用
                itemFirstRect.localPosition =
                    new Vector3(itemLastRect.localPosition.x, itemLastRect.localPosition.y - 156);
                goList.First.Value.ChangeListNormal(query.ElementAt(id), id + 1);
                goList.AddLast(goList.First.Value);
                goList.RemoveFirst();
            }
        }

        //判断当前item是否移出了viewport的可视范围
        if (itemLast[1].y < viewPort[0].y)
        {
            //获取复用时临近的item的信息，根据id判定，由此计算出当前这条item是第几名玩家
            int id = int.Parse(goList.First.Value.rankNormal.text);
            if (id > 1)
            {
                //将最后一条移至第一条进行复用
                itemLastRect.localPosition =
                    new Vector3(itemFirstRect.localPosition.x, itemFirstRect.localPosition.y + 156);
                goList.Last.Value.ChangeListNormal(query.ElementAt(id - 2), id - 1);
                goList.AddFirst(goList.Last.Value);
                goList.RemoveLast();
            }
        }

        destroyCount = 1;
    }
}