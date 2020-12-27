using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventEnum
{
    //机器人可见的时候
    WhenRobotBecameVisible,
    //当机器人跟随主角
    WhenRobotFollowPlayer,
    //当机器人去收集资源的时候
    WhenRobotCollectResource,
    //机器人去收集下一个资源
    RobotCollectNextResource,
    //判断主角是否可控制
    PlayerCanBeControled,
    //当玩家消耗精力值的时候
    WhenPlayerConsumeEnergy,
    //当在激活界面的时候选择的那个按钮
    ClickWhatWhenInActivateChoosePanel,
    //激活面板点击圆圈的时候注册颜色变化的事件
    ClickRegisterColorChange,
    //初次展示任务面板
    FirstShowTaskPanel,
    //材料主界面选择的材料类型
    MaterialsChooseType,
    //收集到的六边形的数量
    SetHexagonNum,
    //收集到的三角形的数量
    SetTriangleNum,
    //显示所有的资源
    ShowOrHideResources,
    //从资源列表中去除收集到的资源
    RemoveResourceFromList,
    //机器人获取所有的坐标
    RobotGetAllResources,
    //房子建好
    HouseIsBuilded,
    //游戏失败
    GameFailure
}
