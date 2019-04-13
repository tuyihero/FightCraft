﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIGemPack : UIBase
{

    #region static funs

    public static void ShowAsyn()
    {
        Hashtable hash = new Hashtable();
        GameCore.Instance.UIManager.ShowUI("LogicUI/Gem/UIGemPack", UILayer.PopUI, hash);
    }

    public static void RefreshPack()
    {
        var instance = GameCore.Instance.UIManager.GetUIInstance<UIGemPack>("LogicUI/Gem/UIGemPack");
        if (instance == null)
            return;

        if (!instance.isActiveAndEnabled)
            return;

        instance.Refresh();
    }

    public static void RefreshPunchPack()
    {
        var instance = GameCore.Instance.UIManager.GetUIInstance<UIGemPack>("LogicUI/Gem/UIGemPack");
        if (instance == null)
            return;

        if (!instance.isActiveAndEnabled)
            return;

        instance._PunchPanel.RefreshItems();
    }

    public static UIContainerBase GetGemPack()
    {
        var instance = GameCore.Instance.UIManager.GetUIInstance<UIGemPack>("LogicUI/Gem/UIGemPack");
        if (instance == null)
            return null;

        if (!instance.isActiveAndEnabled)
            return null;

        return instance._GemPack;
    }

    public static void SetGemCombine(Tables.GemTableRecord resultRecord)
    {
        var instance = GameCore.Instance.UIManager.GetUIInstance<UIGemPack>("LogicUI/Gem/UIGemPack");
        if (instance == null)
            return;

        if (!instance.isActiveAndEnabled)
            return;

        instance._CombinePanel.AutoFitCombine(resultRecord);
    }

    #endregion

    public void OnTagSelect(int page)
    {
        if (page == 2)
        {
            _GemPackPanel.SetActive(false);
        }
        else
        {
            _GemPackPanel.SetActive(true);
        }
    }

    #region 

    public UIContainerSelect _GemPack;

    //public UITagPanel _TagPanel;
    public UIGemPackPunch _PunchPanel;
    public UIGemPackCombine _CombinePanel;
    public GameObject _GemPackPanel;

    public override void Show(Hashtable hash)
    {
        base.Show(hash);

        _GemPack.InitContentItem(GemData.Instance.PackGemDatas._PackItems, OnPackItemClick, null, OnPackPanelItemClick);
    }

    public void Refresh()
    {
        //_GemPack.RefreshItems();
        _GemPack.InitContentItem(GemData.Instance.PackGemDatas._PackItems, OnPackItemClick, null, OnPackPanelItemClick);
    }

    private void OnPackItemClick(object gemItemObj)
    {
        ItemGem gemItem = gemItemObj as ItemGem;
        if (gemItem == null)
            return;

        //int showingPage = _TagPanel.GetShowingPage();
        //if (showingPage == 0)
        {
            _PunchPanel.ShowGemTooltipsRight(gemItem);
        }
    }

    private void OnPackPanelItemClick(UIItemBase uiItemBase)
    {
        UIGemItem gemItem = uiItemBase as UIGemItem;
        if (gemItem == null)
            return;

        //int showingPage = _TagPanel.GetShowingPage();
        //if (showingPage == 1)
        //{
        //    _CombinePanel.ShowGemTooltipsRight(gemItem);
        //}
    }

    #endregion

    #region 

    public void OnBtnSort()
    {
        GemData.Instance.PacketSort();
        Refresh();
    }

    #endregion


}

