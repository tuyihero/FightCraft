﻿
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.EventSystems;
using System;
using Tables;

public class UIGemTooltips : UIItemTooltips
{

    #region static funs

    public static void ShowAsyn(ItemGem itemBase, params ToolTipFunc[] funcs)
    {
        Hashtable hash = new Hashtable();
        hash.Add("ItemGem", itemBase);
        hash.Add("ToolTipFun", funcs);
        GameCore.Instance.UIManager.ShowUI("LogicUI/Gem/UIGemTooltips", UILayer.MessageUI, hash);
    }

    public new static void HideAsyn()
    {
        UIManager.Instance.HideUI("LogicUI/Gem/UIGemTooltips");
    }

    public static void ShowAsynInType(ItemGem itemBase, TooltipType toolTipType, params ToolTipFunc[] funcs)
    {
        Hashtable hash = new Hashtable();
        hash.Add("ItemGem", itemBase);
        hash.Add("ToolTipFun", funcs);
        hash.Add("TooltipType", toolTipType);
        GameCore.Instance.UIManager.ShowUI("LogicUI/Gem/UIGemTooltips", UILayer.MessageUI, hash);
    }

    #endregion

    #region 

    public UIGemInfo _GemInfo;

    public UICurrencyItem _CostMat;

    #endregion

    #region 

    private ItemGem _ItemGem;

    public override void Show(Hashtable hash)
    {
        base.Show(hash);
        _ItemGem = hash["ItemGem"] as ItemGem;
        _ShowItem = _ItemGem;
        _GemInfo.ShowTips(_ItemGem);

        _CostMat.ShowCostCurrency(_ItemGem.ItemDataID, GemData.Instance.GetLevelCostMat(_ItemGem));

        _HideAfterBtn = false;
    }


    #endregion
    

}

