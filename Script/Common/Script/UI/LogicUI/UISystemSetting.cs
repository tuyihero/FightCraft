﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UISystemSetting : UIBase
{

    #region static funs

    public static void ShowAsyn(bool isInFight = false)
    {
        Hashtable hash = new Hashtable();
        hash.Add("IsInFight", isInFight);
        GameCore.Instance.UIManager.ShowUI(UIConfig.UISystemSetting, UILayer.BaseUI, hash);
    }

    #endregion


    #region setting

    public Toggle _Shadow;
    public Slider _Volumn;
    public Toggle _AimTarget;
    public GameObject _FightTipsGO;
    public Button _QuitFightBtn;

    public override void Show(Hashtable hash)
    {
        base.Show(hash);

        bool isInFight = (bool)hash["IsInFight"];
        InitSetting(isInFight);
    }

    public void InitSetting(bool isInFight)
    {
        _Shadow.isOn = GlobalValPack.Instance.IsShowShadow;
        _Volumn.value = GlobalValPack.Instance.Volume;
        _AimTarget.isOn = GlobalValPack.Instance.IsRotToAnimTarget;

        if (isInFight)
        {
            _FightTipsGO.SetActive(true);
            _QuitFightBtn.gameObject.SetActive(true);
        }
        else
        {
            _FightTipsGO.SetActive(false);
            _QuitFightBtn.gameObject.SetActive(false);
        }
    }

    public void OnTrigShadow(bool isTrig)
    {
        GlobalValPack.Instance.IsShowShadow = isTrig;
    }

    public void OnSlider()
    {
        GlobalValPack.Instance.Volume = _Volumn.value;
    }

    public void OnTrigAimTarget(bool isTrig)
    {
        GlobalValPack.Instance.IsRotToAnimTarget = isTrig;
    }

    public void OnBtnFightTips()
    {
        UIFightTips.ShowAsyn();
    }

    public void OnBtnStageTips()
    {
        UIStageDiffTips.ShowAsyn();
    }

    public void OnBtnQuitFight()
    {
        UIMessageBox.Show(100000, ()=>
        {
            LogicManager.Instance.ExitFight();
        }, null);
    }

    public void OnBtnQuitGame()
    {
        UIMessageBox.Show(1000006, () =>
        {
            LogicManager.Instance.QuitGame();
            Debug.Log("save data");
        }, null);
    }

    #endregion
}

