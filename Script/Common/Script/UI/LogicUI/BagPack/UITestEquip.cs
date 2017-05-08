﻿
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using GameLogic;
using UnityEngine.EventSystems;
using System;
using Tables;

using GameBase;

namespace GameUI
{
    public class UITestEquip : UIBase
    {

        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/BagPack/UITestEquip", UILayer.PopUI, hash);
        }

        #endregion

        #region 

        public InputField _InputLevel;
        public InputField _InputQuality;
        public InputField _InputValue;

        #endregion

        #region 

        public override void Show(Hashtable hash)
        {
            base.Show(hash);
        }

        public override void Hide()
        {
            base.Hide();

            UIEquipTooltips.HideAsyn();
        }

        public void OnBtnOk()
        {
            int level = int.Parse(_InputLevel.text);
            ITEM_QUALITY quality = (ITEM_QUALITY)(int.Parse(_InputQuality.text));
            int value = int.Parse(_InputValue.text);
            var equipItem = ItemEquip.CreateEquip(level, quality, value);
            var newEquip = PlayerDataPack.Instance._SelectedRole.AddNewEquip(equipItem);
            UIEquipTooltips.ShowAsyn(newEquip);
            Debug.Log("Input :" + level + "," + quality + "," + value);
        }

        
        #endregion

        

    }
}