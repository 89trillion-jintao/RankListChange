# RankList技术文档

## 整体框架

需求部分总共分为

- 文本倒计时

  通过数学将json中提取到的总秒数切割为天，时，分，秒，并每秒执行秒数减一的函数，分秒到0会重置为60并上一级减1，时到0会重置为24并天数减1

- 按钮切换场景

  只有一个场景，通过隐藏和显示来实现跳转界面

- 列表内容从json中获取

  先将json内容读取为整个字符串，然后使用JsonUtility.FromJson解析出json内容并存入已初始化的list中

- 使用滚动列表滚动展示列表信息

  在unity上初始化一个scrollview并通过代码解析出来的json数据写入scrollview，写入之前，先使用IEnumerable存好排序好的列表内容

- 使用滚动列表内元素复用，列表元素数量固定，重复使用已存在的预制件

  预先创建好10个可以复用的预制件，其中9个展示，1个用来缓存和复用，按照预制件的坐标和展示内容的viewport坐标判定如果预制件的坐标超出viewport便将超出的预制件移动到另一端并赋值。 

- 点击item弹出toast展示信息

  先做好toast的预制件，给item增加button组件，添加oncilck事件，点击会弹出toast的弹窗，并在1秒后销毁。

## 脚本

| 脚本名               | 绑定位置                          | 功能                                                         |
| :------------------- | :-------------------------------- | ------------------------------------------------------------ |
| CLoseAndOpen         | bottonPanel/OCbtn                 | 用来关闭和打开整个滚动列表                                   |
| LoginIn              | Before/LoginBtn                   | 用来切换初始界面和主界面                                     |
| JsonData             | 无                                | 用来解析json数据并存入list列表供其他脚本使用                 |
| CountDown            | After/bannerGO/TimeCount          | 实现顶部倒计时功能                                           |
| ListData             | After/AFPanel/ScrollView/Viewport | 用来往滚动列表中添加item并根据不同情况渲染item，同时实现滚动列表元素复用 |
| ToastHandler         | 预制件List_normal                 | 用来点击列表中的预制件便会生成toast                          |
| ChangeItemData       | 无                                | 负责渲染所有生成的item                                       |
| Toast                | 预制件toast                       | 修改生成后的toast信息                                        |
| ChangeMyItemOnBanner | BannerGO                          | 根据自己的信息在banner上赋值和填充信息                       |

## 流程图

```flow
st=>start: getRankList按钮
op=>operation: 跳转场景
op1=>operation: 解析json
op2=>operation: 根据json生成列表item
sub1=>operation: 点击item弹出toast展示信息
sub2=>operation: 点击底部按钮可以打开和关闭scrollview
e=>end: 结束框

st->op->op1->op2->sub1->sub2->e

```



## TODO

- 元素复用功能并未使用插件，灵活度不高，需要学习如何使用插件实现元素复用
- 布局方面适配依旧存在差异，等比例放大缩小的功能并未实现，只是使用锚点控制在比例变化时的位置变化，使其不丢失组件
- 代码较为繁琐，不够精简好看，代码复用率提要更进一步的提高
