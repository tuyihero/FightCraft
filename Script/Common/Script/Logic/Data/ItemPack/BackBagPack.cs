﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackBagPack : DataPackBase
{
    #region 单例

    private static BackBagPack _Instance;
    public static BackBagPack Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new BackBagPack();
            }
            return _Instance;
        }
    }

    private BackBagPack()
    {
        GuiTextDebug.debug("BackBagPack init");
        _SaveFileName = "BackBagPack";
    }

    #endregion

    public const int _BAG_PAGE_SLOT_CNT = 50;

    private ItemPackBase<ItemEquip> _PageEquips;
    public ItemPackBase<ItemEquip> PageEquips
    {
        get
        {
            return _PageEquips;
        }
    }

    private ItemPackBase<ItemBase> _PageItems;
    public ItemPackBase<ItemBase> PageItems
    {
        get
        {
            return _PageItems;
        }
    }

    public void InitBackPack()
    {
        bool needSave = false;

        _PageEquips = new ItemPackBase<ItemEquip>();
        _PageEquips._SaveFileName = "BackPackEquipPage";
        _PageEquips._PackSize = _BAG_PAGE_SLOT_CNT;
        _PageEquips.LoadClass(true);

        _PageItems = new ItemPackBase<ItemBase>();
        _PageItems._SaveFileName = "BackPackItemPage";
        _PageItems._PackSize = _BAG_PAGE_SLOT_CNT;
        _PageItems.LoadClass(true);

        if (_PageEquips._PackItems == null || _PageEquips._PackItems.Count != _BAG_PAGE_SLOT_CNT)
        {
            if (_PageEquips._PackItems == null)
            {
                _PageEquips._PackItems = new List<ItemEquip>();
                _PageEquips._PackSize = _BAG_PAGE_SLOT_CNT;
            }
            int equipSlotCnt = _BAG_PAGE_SLOT_CNT;
            int startIdx = _PageEquips._PackItems.Count;
            for (int i = startIdx; i < equipSlotCnt; ++i)
            {
                ItemEquip newItemEquip = new ItemEquip("-1");
                _PageEquips._PackItems.Add(newItemEquip);
            }
            _PageEquips.SaveClass(true);
        }
        else
        {
            for (int i = 0; i < _BAG_PAGE_SLOT_CNT; ++i)
            {
                if (_PageEquips._PackItems[i].IsVolid())
                {
                    _PageEquips._PackItems[i].CalculateSet();
                    _PageEquips._PackItems[i].CalculateCombatValue();
                }
            }
        }

        if (PageItems._PackItems == null || PageItems._PackItems.Count != _BAG_PAGE_SLOT_CNT)
        {
            if (PageItems._PackItems == null)
            {
                _PageItems._PackItems = new List<ItemBase>();
                _PageItems._PackSize = _BAG_PAGE_SLOT_CNT;
            }
            int equipSlotCnt = _BAG_PAGE_SLOT_CNT;
            int startIdx = PageItems._PackItems.Count;
            for (int i = 0; i < equipSlotCnt; ++i)
            {
                ItemBase newItemEquip = new ItemBase("-1");
                _PageItems._PackItems.Add(newItemEquip);
            }
            PageItems.SaveClass(true);
        }
    }

    public bool AddEquip(ItemEquip equip)
    {
        var equipSlot = _PageEquips.AddItem(equip);
        if (equipSlot == null)
        {
            UIMessageTip.ShowMessageTip(10002);
            return false;
        }

        Hashtable hash = new Hashtable();
        hash.Add("EquipInfo", equipSlot);
        GameCore.Instance.EventController.PushEvent(EVENT_TYPE.EVENT_LOGIC_EQUIP_GET, this, hash);

        return true;
    }

    public void SortEquip()
    {
        _PageEquips._PackItems.Sort((equipA, equipB) =>
        {
            if (equipA.EquipQuality > equipB.EquipQuality)
                return -1;
            else if (equipA.EquipQuality < equipB.EquipQuality)
                return 1;
            else
            {
                if (equipA.EquipLevel > equipB.EquipLevel)
                    return -1;
                else if (equipA.EquipLevel < equipB.EquipLevel)
                    return 1;
                else
                    return 0;
            }
        });
        _PageEquips.SaveClass(true);
    }

    public void SortItem()
    {
        PageItems.SortStack();
        PageItems.SortEmpty();
    }
}

