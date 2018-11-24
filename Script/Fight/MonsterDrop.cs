﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tables;

public class DropItemData
{
    public ItemEquip _ItemEquip;
    public ItemBase _ItemBase;
    public int _DropGold;
    public int _DropDiamond;

    public Vector3 _DropPos;
    public Vector3 _MonsterPos;
    public bool _IsAutoPick;
}

public class MonsterDrop
{
    public static string IdentifictionItem = "50000";
    public static string[,] ResetItems = new string[5,3] { 
        { "20000","30000","40000"},
        { "20001","30001","40001"},
        { "20002","30002","40002"},
        { "20003","30003","40003"},
        { "20004","30004","40004"}};

    public static void MonsterDropItems(MotionManager monsterMotion)
    {

        var drops = GetMonsterDrops(monsterMotion.MonsterBase, monsterMotion.RoleAttrManager.MotionType, monsterMotion.RoleAttrManager.Level);
        var randomPoses = GameRandom.GetIndependentRandoms(0, 16, drops.Count);
        int posIdx = 0;
        foreach (var drop in drops)
        {
            var pos = GetDropPos(monsterMotion, randomPoses[posIdx]);
            ++posIdx;
            drop._DropPos = pos;
            drop._MonsterPos = monsterMotion.transform.position;
            var obj = ResourceManager.Instance.GetInstanceGameObject("Drop/DropItem");
            DropItem dropItem = obj.GetComponent<DropItem>();
            dropItem.InitDrop(drop);
        }

        int dropExp = GameDataValue.GetMonsterExp(monsterMotion.RoleAttrManager.MotionType, monsterMotion.RoleAttrManager.Level, RoleData.SelectRole._RoleLevel);
        RoleData.SelectRole.AddExp(dropExp);
    }

    public static int DropGold = 0;
    public static List<DropItemData> GetMonsterDrops(Tables.MonsterBaseRecord monsterRecord, MOTION_TYPE monsterType, int level)
    {
        List<DropItemData> dropList = new List<DropItemData>();

        List<ItemEquip> dropEquips = GameDataValue.GetMonsterDropEquip(monsterType, monsterRecord, level);
        for (int i = 0; i < dropEquips.Count; ++i)
        {
            DropItemData dropItem = new DropItemData();
            dropItem._ItemEquip = dropEquips[i];
            dropList.Add(dropItem);
        }

        int dropMatCnt = GameDataValue.GetEquipMatDropCnt(monsterType, monsterRecord, level);
        if (dropMatCnt > 0)
        {
            DropItemData dropItem = new DropItemData();
            dropItem._ItemBase = new ItemBase(EquipRefresh._RefreshMatDataID, dropMatCnt);
            dropList.Add(dropItem);
        }

        //var gemType = GameDataValue.GetGemMatDropItemID(monsterRecord);
        //var gemCnt = GameDataValue.GetGemMatDropCnt(monsterType, monsterRecord, level);
        //if (gemCnt > 0)
        //{
        //    DropItemData dropItem = new DropItemData();
        //    dropItem._ItemBase = new ItemBase(gemType, gemCnt);
        //    dropList.Add(dropItem);
        //}

        var goldCnt = GameDataValue.GetGoldDropCnt(monsterType, level);
        for (int i = 0; i < goldCnt; ++i)
        {
            var goldNum = GameDataValue.GetGoldDropNum(monsterType, level);
            DropItemData dropItem = new DropItemData();
            dropItem._DropGold = goldNum;
            dropList.Add(dropItem);

            DropGold += goldNum;
        }
        
        return dropList;
    }

    private static List<DropItemData> GetNormalMonsterDrops(MotionManager monsterMotion)
    {
        List<DropItemData> dropList = new List<DropItemData>();

        int dropMoneyRandom = Random.Range(1, 10000);
        if (dropMoneyRandom < 2000)
        {
            DropItemData drop = new DropItemData();
            drop._DropGold = Random.Range(80, 120);
            dropList.Add(drop);
        }

        int dropEquip = Random.Range(1, 10000);
        if (dropEquip < 1000)
        {
            DropItemData drop = new DropItemData();
            int quality = GameRandom.GetRandomLevel(8000, 1900, 99, 1);
            var dropItem = ItemEquip.CreateEquip(monsterMotion.RoleAttrManager.Level, (Tables.ITEM_QUALITY)quality);
            drop._ItemEquip = dropItem;
            dropList.Add(drop);
        }

        return dropList;
    }

    private static List<DropItemData> GetEliteMonsterDrops(MotionManager monsterMotion)
    {
        List<DropItemData> dropList = new List<DropItemData>();

        int dropMoneyCnt = Random.Range(1, 2);
        for (int i = 0; i < dropMoneyCnt; ++i)
        {
            DropItemData drop = new DropItemData();
            drop._DropGold = Random.Range(80, 120);
            dropList.Add(drop);
        }

        {
            DropItemData drop = new DropItemData();
            int quality = GameRandom.GetRandomLevel(8000, 1800, 195, 5);
            var dropItem = ItemEquip.CreateEquip(monsterMotion.RoleAttrManager.Level, (Tables.ITEM_QUALITY)quality);
            drop._ItemEquip = dropItem;
            dropList.Add(drop);
        }
        return dropList;
    }

    private static List<DropItemData> GetBossMonsterDrops(MotionManager monsterMotion)
    {
        List<DropItemData> dropList = new List<DropItemData>();

        int dropMoneyCnt = Random.Range(3, 5);
        for (int i = 0; i < dropMoneyCnt; ++i)
        {
            DropItemData drop = new DropItemData();
            drop._DropGold = Random.Range(80, 120);
            dropList.Add(drop);
        }

        int dropEquipCnt = Random.Range(1, 2);
        for (int i = 0; i < dropEquipCnt; ++i)
        {
            DropItemData drop = new DropItemData();
            int quality = GameRandom.GetRandomLevel(4000, 5500, 450, 50);
            var dropItem = ItemEquip.CreateEquip(monsterMotion.RoleAttrManager.Level, (Tables.ITEM_QUALITY)quality);
            drop._ItemEquip = dropItem;
            dropList.Add(drop);
        }

        int dropIdentiRandom = Random.Range(1, 10000);
        if (dropIdentiRandom < 2000)
        {
            DropItemData drop = new DropItemData();
            drop._ItemBase = ItemBase.CreateItem(IdentifictionItem);
            dropList.Add(drop);
        }

        int levelIdx = 0;
        if (monsterMotion.RoleAttrManager.Level < 40)
        {
            levelIdx = 0;
        }
        else if (monsterMotion.RoleAttrManager.Level < 70)
        {
            levelIdx = 1;
        }
        else if (monsterMotion.RoleAttrManager.Level < 90)
        {
            levelIdx = 2;
        }
        else if (monsterMotion.RoleAttrManager.Level < 100)
        {
            levelIdx = 3;
        }
        else
        {
            levelIdx = 4;
        }

        int dropResetItemCnt = Random.Range(1, 3);
        for (int i = 0; i < dropResetItemCnt; ++i)
        {
            DropItemData drop = new DropItemData();
            drop._ItemBase = ItemBase.CreateItem(ResetItems[levelIdx, Random.Range(0, 3)]);
            dropList.Add(drop);
        }
        return dropList;
    }

    private static Vector3 GetDropPos(MotionManager monsterMotion, int posIdx)
    {
        int rangeParam = posIdx / 8;
        int angleParam = posIdx % 8;

        float range = (rangeParam + 1) * 1;
        float angle = angleParam * 45;

        Vector3 pos = new Vector3(0, monsterMotion.transform.position.y, 0);
        pos.x = monsterMotion.transform.position.x + Mathf.Sin(angle) * range;
        pos.z = monsterMotion.transform.position.z + Mathf.Cos(angle) * range;

        UnityEngine.AI.NavMeshHit navMeshHit;
        if (UnityEngine.AI.NavMesh.SamplePosition(pos, out navMeshHit, range, UnityEngine.AI.NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        return pos;
    }

    public static void PickItem(DropItemData dropItemData)
    {
        if (dropItemData == null)
            return;

        if (dropItemData._DropGold > 0)
        {
            PlayerDataPack.Instance.AddGold(dropItemData._DropGold);
        }
        else if (dropItemData._ItemEquip != null)
        {
            if (!BackBagPack.Instance.AddEquip(dropItemData._ItemEquip))
                return;
        }
        else if (dropItemData._ItemBase != null)
        {
            if (!BackBagPack.Instance.PageItems.AddItem(dropItemData._ItemBase.CommonItemRecord.Id, dropItemData._ItemBase.ItemStackNum))
                return;
        }
        else
        {
            Debug.Log("Drop Empty");
        }
        
    }
}
