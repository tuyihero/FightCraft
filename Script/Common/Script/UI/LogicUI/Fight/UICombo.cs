﻿
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Tables;

public class UICombo : UIBase
{

    #region static

    public static void ShowAsyn()
    {
        Hashtable hash = new Hashtable();
        GameCore.Instance.UIManager.ShowUI(UIConfig.UICombo, UILayer.BaseUI, hash);

    }
    
    #endregion

    public override void Show(Hashtable hash)
    {
        base.Show(hash);
    }

    private void Update()
    {
        UpdateCombat();
    }

    public UIImgText _CombatText;
    public GameObject _CombatImg;

    public void UpdateCombat()
    {
        if (FightManager.Instance.Combo < 2)
        {
            _CombatImg.SetActive(false);
            _CombatText.text = "";
        }
        else
        {
            _CombatImg.SetActive(true);
            _CombatText.text = FightManager.Instance.Combo.ToString();
        }
    }
    
}

