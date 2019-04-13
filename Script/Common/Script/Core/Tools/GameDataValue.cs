﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tables;

public class GameDataValue
{
    public static float ConfigIntToFloat(int val)
    {
        var resultVal = new decimal(0.0001) * new decimal(val);
        return (float)resultVal;
    }

    public static float ConfigIntToFloatDex1(int val)
    {
        int dex = Mathf.RoundToInt(val * 0.1f);
        var resultVal = new decimal(0.001) * new decimal(dex);
        return (float)resultVal;
    }

    public static float ConfigIntToPersent(int val)
    {

        var resultVal = new decimal(0.01) * new decimal(val);
        return (float)resultVal;
    }

    public static int ConfigFloatToInt(float val)
    {
        return Mathf.RoundToInt(val * 10000);
    }

    public static int ConfigFloatToPersent(float val)
    {
        float largeVal = val * 100;
        var intVal = Mathf.RoundToInt(largeVal);
        return intVal;
    }

    public static int GetMaxRate()
    {
        return 10000;
    }

    #region fight numaric

    #region level -> baseAttr

    private static int _MaxLv = 50;
    private static float _AttackPerLevel = 136.0f;
    private static float _AttackIncreaseLevel = 1.2f;
    private static float _HPPerLevel = 286.0f;
    private static float _DefencePerLevel = 68.0f;
    private static float _ValuePerLevel = 50;
    private static float _ValuePerAttack = 1;

    private static float _LegHPToTorso = 0.5f;
    private static float _LegDefenceToTorso = 0.5f;

    public static int CalWeaponAttack(int equiplevel)
    {
        var attrRecord = TableReader.EquipBaseAttr.GetEquipBaseAttr(equiplevel);
        return attrRecord.Atk;
        //int power = equiplevel / 5;
        //var attackValue = _AttackPerLevel* Mathf.Pow(_AttackIncreaseLevel, power);
        //return Mathf.CeilToInt(attackValue);
    }

    public static int CalEquipTorsoHP(int equiplevel)
    {
        //int power = equiplevel / 5;
        //var value = _HPPerLevel * Mathf.Pow(_AttackIncreaseLevel, power);
        //return Mathf.CeilToInt(value);
        var value = CalEquipLegsHP(equiplevel) * 0.5f;
        return Mathf.CeilToInt(value);
    }

    public static int CalEquipTorsoDefence(int equiplevel)
    {
        //int power = equiplevel / 5;
        //var value = _DefencePerLevel * Mathf.Pow(_AttackIncreaseLevel, power);
        //return Mathf.CeilToInt(value);
        var attrRecord = TableReader.EquipBaseAttr.GetEquipBaseAttr(equiplevel);
        return attrRecord.Defence;
    }

    public static int CalEquipLegsHP(int equiplevel)
    {
        var attrRecord = TableReader.EquipBaseAttr.GetEquipBaseAttr(equiplevel);
        return attrRecord.HP;
    }

    public static int CalEquipLegsDefence(int equiplevel)
    {
        var value = CalEquipTorsoDefence(equiplevel) * 0.5f;
        return Mathf.CeilToInt(value);
    }

    #endregion

    #region baseAttr -> exAttr atk

    public static int _AtkPerRoleLevel = 0;
    public static int _AtkRoleLevelBase = 0;
    public static int _HPPerRoleLevel = 0;
    public static int _HPRoleLevelBase = 100;

    public static float _AttackPerStrength = 2.5f;
    public static float _DmgEnhancePerStrength = 0f;
    public static float _StrToAtk = 0.4f;

    //public static float _IgnoreAtkPerDex = 0.25f;
    //public static float _CriticalRatePerDex = 0f;
    //public static float _CriticalDmgPerDex = 2f;
    public static float _DefencePerDex = 2.5f;
    public static float _DexToAtk = 0.4f;

    public static float _EleAtkPerInt = 1f;
    public static float _EleEnhancePerInt = 0f;
    public static float _IntToAtk = 0.4f;

    public static float _HPPerVit = 20;
    public static float _FinalDmgRedusePerVit = 0f;
    public static float _VitToAtk = 0.4f;

    public static float _CriticalDmgToAtk = 2f;

    public static float _ElementToAtk = 1f;
    public static float _DmgEnhancePerElementEnhance = 10;
    public static float _EleEnhanceToAtk = 2.8f;
    public static float _EleResistToAtk = 2.8f;

    public static float _IgnoreDefenceToAtk = 0.5f;

    public static float _HpToAtk = 10.0f;
    public static float _DefToAtk = 1.0f;
    public static float _MoveSpeedToAtk = 3f;
    public static float _AtkSpeedToAtk = 2f;
    public static float _CriticalChanceToAtk = 2f;
    public static float _DamageEnhance = 1;

    public static float GetAttrToValue(RoleAttrEnum roleAttr)
    {
        float value = 0.0f;
        switch (roleAttr)
        {
            case RoleAttrEnum.Strength:
                value = _StrToAtk;
                break;
            case RoleAttrEnum.Dexterity:
                value = _DexToAtk;
                break;
            case RoleAttrEnum.Intelligence:
                value = _IntToAtk;
                break;
            case RoleAttrEnum.Vitality:
                value = _VitToAtk;
                break;
            case RoleAttrEnum.Attack:
                value = 1;
                break;
            case RoleAttrEnum.HPMax:
                value = _HpToAtk;
                break;
            case RoleAttrEnum.Defense:
                value = _DefToAtk;
                break;
            case RoleAttrEnum.MoveSpeed:
                value = _MoveSpeedToAtk;
                break;
            case RoleAttrEnum.AttackSpeed:
                value = _AtkSpeedToAtk;
                break;
            case RoleAttrEnum.CriticalHitChance:
                value = _CriticalChanceToAtk;
                break;
            case RoleAttrEnum.PhysicDamageEnhance:
                value = _DamageEnhance;
                break;
            case RoleAttrEnum.CriticalHitDamge:
                value = _CriticalDmgToAtk;
                break;
            case RoleAttrEnum.FireAttackAdd:
                value = _ElementToAtk;
                break;
            case RoleAttrEnum.ColdAttackAdd:
                value = _ElementToAtk;
                break;
            case RoleAttrEnum.LightingAttackAdd:
                value = _ElementToAtk;
                break;
            case RoleAttrEnum.WindAttackAdd:
                value = _ElementToAtk;
                break;
            case RoleAttrEnum.FireEnhance:
                value = _EleEnhanceToAtk;
                break;
            case RoleAttrEnum.ColdEnhance:
                value = _EleEnhanceToAtk;
                break;
            case RoleAttrEnum.LightingEnhance:
                value = _EleEnhanceToAtk;
                break;
            case RoleAttrEnum.WindEnhance:
                value = _EleEnhanceToAtk;
                break;
            case RoleAttrEnum.FireResistan:
                value = _EleResistToAtk;
                break;
            case RoleAttrEnum.ColdResistan:
                value = _EleResistToAtk;
                break;
            case RoleAttrEnum.LightingResistan:
                value = _EleResistToAtk;
                break;
            case RoleAttrEnum.WindResistan:
                value = _EleResistToAtk;
                break;
            case RoleAttrEnum.IgnoreDefenceAttack:
                value = _IgnoreDefenceToAtk;
                break;
            default:
                value = 1;
                break;
        }

        return value;
    }

    public static int GetValueAttr(RoleAttrEnum roleAttr, int value)
    {
        int attrValue = 1;

        if (roleAttr == RoleAttrEnum.AttackSpeed
            || roleAttr == RoleAttrEnum.MoveSpeed
            || roleAttr == RoleAttrEnum.CriticalHitChance
            || roleAttr == RoleAttrEnum.CriticalHitDamge
            || roleAttr == RoleAttrEnum.FireEnhance
            || roleAttr == RoleAttrEnum.ColdEnhance
            || roleAttr == RoleAttrEnum.LightingEnhance
            || roleAttr == RoleAttrEnum.WindEnhance
            || roleAttr == RoleAttrEnum.FireResistan
            || roleAttr == RoleAttrEnum.ColdResistan
            || roleAttr == RoleAttrEnum.LightingResistan
            || roleAttr == RoleAttrEnum.WindResistan
            || roleAttr == RoleAttrEnum.PhysicDamageEnhance
            || roleAttr == RoleAttrEnum.AttackPersent
            || roleAttr == RoleAttrEnum.DefensePersent
            || roleAttr == RoleAttrEnum.HPMaxPersent)
        {
            attrValue = value;
        }
        else
        {
            attrValue = Mathf.CeilToInt(value * GetAttrToValue(roleAttr));
            attrValue = Mathf.Max(attrValue, 1);
        }

        return attrValue;
    }

    public static int GetAttrValue(RoleAttrEnum roleAttr, int value)
    {
        int attrValue = 1;
        var attrToValue = GetAttrToValue(roleAttr);
        if (attrToValue == 0)
        {
            attrValue = 0;
        }
        else
        {
            attrValue = (int)(value / attrToValue);
        }

        return attrValue;
    }
    #endregion

    #region ex -> base

    private static float _ExToBase = 0.2f;
    private static float _LvValueBase = 50;
    private static float _LvValueV = 10;
    private static float _LvValueA = 0.8f;

    public static int CalLvValue(int level, EQUIP_SLOT equipSlot = EQUIP_SLOT.WEAPON)
    {
        if (level == 0)
            return 0;

        var attrRecord = TableReader.EquipBaseAttr.GetEquipBaseAttr(level);
        //int equipValue = attrRecord.Value;
        int equipValue = TableReader.AttrValueLevel.GetBaseValue(level);
        //if (equipSlot == EQUIP_SLOT.AMULET || equipSlot == EQUIP_SLOT.RING)
        //{
        //    equipValue *= 2;

        //}
        return equipValue;
    }

    #endregion

    #region equip

    public static List<float> _ExAttrQualityPersent = new List<float>() { 0.8f, 0.92f, 1.0f, 1.08f, 1.2f };
    public static int _SpAttrStep = 50;
    public static List<int> _ExAttrQualityStrDict = new List<int>() { 92004, 92003, 92002, 92001, 92000 };

    public static int GetExAttrRandomQuality()
    {
        int random = Random.Range(0, _ExAttrQualityPersent.Count);
        return random;
    }

    public static int GetExAttrRandomValue(RoleAttrEnum roleAttr, int attrLevel, int qualityLevel)
    {
        var randomValue = _ExAttrQualityPersent[qualityLevel];
        int attrValue = CalLvValue(attrLevel);
        return Mathf.CeilToInt(attrValue * randomValue);
    }

    public static int GetExAttrRandomValue(EquipExAttrRandom roleAttr, int attrLevel, int qualityLevel)
    {
        if (roleAttr.AttrValueIdx > 0)
        {
            var randomValue = _ExAttrQualityPersent[qualityLevel];
            int attrValue = TableReader.AttrValueLevel.GetSpValue(attrLevel, roleAttr.AttrValueIdx);
            int spAttrStep = (int)((attrValue * randomValue) / _SpAttrStep);
            return Mathf.CeilToInt(spAttrStep * _SpAttrStep);
        }
        else
        {
            return GetExAttrRandomValue(roleAttr.AttrID, attrLevel, qualityLevel);
        }
    }

    public static List<EquipExAttrRandom> GetRandomEquipAttrsType(Tables.EQUIP_SLOT equipSlot, Tables.ITEM_QUALITY quality, EquipItemRecord legencyEquip, int level, int fixNum = 0)
    {
        List<EquipExAttr> exAttrs = new List<EquipExAttr>();

        int exAttrCnt = 0;
        if (quality == ITEM_QUALITY.WHITE)
            return new List<EquipExAttrRandom>();

        List<EquipExAttrRandom> targetRandomList = _WeaponExAttrs;

        switch (equipSlot)
        {
            case Tables.EQUIP_SLOT.WEAPON:
                exAttrCnt = 3;
                if (quality == Tables.ITEM_QUALITY.BLUE)
                {
                    exAttrCnt = Random.Range(1, 3);
                }
                else if (quality == ITEM_QUALITY.PURPER)
                {
                    exAttrCnt = 3;
                }
                else if (quality == ITEM_QUALITY.ORIGIN)
                {
                    if (legencyEquip == null)
                    {
                        exAttrCnt = 4;
                    }
                    else
                    {
                        exAttrCnt = 3;
                    }
                }
                targetRandomList = _WeaponExAttrs;
                break;
            case Tables.EQUIP_SLOT.TORSO:
                exAttrCnt = 3;
                if (quality == Tables.ITEM_QUALITY.BLUE)
                {
                    exAttrCnt = Random.Range(1, 3);
                }
                else if (quality == ITEM_QUALITY.PURPER)
                {
                    exAttrCnt = 3;
                }
                else if (quality == ITEM_QUALITY.ORIGIN)
                {
                    if (legencyEquip == null)
                    {
                        exAttrCnt = 4;
                    }
                    else
                    {
                        exAttrCnt = 3;
                    }
                }
                targetRandomList = _TorsoExAttrs;
                break;
            case Tables.EQUIP_SLOT.LEGS:
                exAttrCnt = 3;
                if (quality == Tables.ITEM_QUALITY.BLUE)
                {
                    exAttrCnt = Random.Range(1, 3);
                }
                else if (quality == ITEM_QUALITY.PURPER)
                {
                    exAttrCnt = 3;
                }
                else if (quality == ITEM_QUALITY.ORIGIN)
                {
                    if (legencyEquip == null)
                    {
                        exAttrCnt = 4;
                    }
                    else
                    {
                        exAttrCnt = 3;
                    }
                }
                targetRandomList = _LegsExAttrs;
                break;
            case Tables.EQUIP_SLOT.AMULET:
                exAttrCnt = 3;
                if (quality == Tables.ITEM_QUALITY.BLUE)
                {
                    exAttrCnt = Random.Range(1, 3);
                }
                else if (quality == ITEM_QUALITY.PURPER)
                {
                    exAttrCnt = Random.Range(3, 5);
                }
                else if (quality == ITEM_QUALITY.ORIGIN)
                {
                    if (legencyEquip == null)
                    {
                        exAttrCnt = 5;
                    }
                    else
                    {
                        exAttrCnt = 4;
                    }
                }
                targetRandomList = _AmuletExAttrs;
                break;
            case Tables.EQUIP_SLOT.RING:
                exAttrCnt = 3;
                if (quality == Tables.ITEM_QUALITY.BLUE)
                {
                    exAttrCnt = Random.Range(1, 3);
                }
                else if (quality == ITEM_QUALITY.PURPER)
                {
                    exAttrCnt = Random.Range(3, 5);
                }
                else if (quality == ITEM_QUALITY.ORIGIN)
                {
                    if (legencyEquip == null)
                    {
                        exAttrCnt = 5;
                    }
                    else
                    {
                        exAttrCnt = 4;
                    }
                }
                targetRandomList = _RingExAttrs;
                break;
        }

        if (fixNum != 0)
        {
            exAttrCnt = fixNum;
        }

        return CalRandomAttrs(targetRandomList, exAttrCnt, level);
    }

    public static List<EquipExAttrRandom> CalRandomAttrs(List<EquipExAttrRandom> staticList, int randomCnt, int level)
    {
        List<EquipExAttrRandom> randomList = new List<EquipExAttrRandom>();
        int totalRandom = 0;
        foreach (var attrRandom in staticList)
        {
            if (attrRandom.AttrValueIdx <= 0 || TableReader.AttrValueLevel.GetSpValue(level, attrRandom.AttrValueIdx) > 0)
            {
                randomList.Add(attrRandom);
                totalRandom += (attrRandom.Random);
            }
        }

        List<EquipExAttrRandom> attrList = new List<EquipExAttrRandom>();
        for (int i = 0; i < randomCnt; ++i)
        {
            int temp = totalRandom;
            int randomVar = Random.Range(0, temp);
            EquipExAttrRandom attr = null;
            foreach (var attrRandom in randomList)
            {
                temp -= attrRandom.Random;
                if (randomVar >= temp)
                {
                    attr = attrRandom;
                    break;
                }
            }
            if (attr == null)
            {
                attr = randomList[randomList.Count - 1];
            }

            attrList.Add(attr);
            if (!attr.CanRepeat)
            {
                totalRandom -= attr.Random;
                randomList.Remove(attr);
            }
        }
        return attrList;
    }

    public static void RefreshEquipExAttrValue(ItemEquip itemEquip)
    {
        List<EquipExAttr> valueAttrs = new List<EquipExAttr>();
        int singleValueMax = Mathf.CeilToInt(itemEquip.EquipValue * _ExToBase);

        foreach (var equipExAttr in itemEquip.EquipExAttrs)
        {
            if (equipExAttr.AttrType != "RoleAttrImpactBaseAttr")
                continue;

            var attrQuality = GameDataValue.GetExAttrRandomQuality();
            equipExAttr.Value = GameDataValue.GetExAttrRandomValue((RoleAttrEnum)equipExAttr.AttrParams[0], itemEquip.EquipLevel, attrQuality);
            equipExAttr.AttrParams[1] = (GameDataValue.GetValueAttr((RoleAttrEnum)equipExAttr.AttrParams[0], equipExAttr.Value));
            equipExAttr.AttrParams[2] = attrQuality;

            equipExAttr.InitAttrQuality();
        }
        
        itemEquip.RefreshEquip();
    }

    public static void RefreshEquipExAttrs(ItemEquip itemEquip)
    {
        EquipItemRecord legendaryRecord = null;
        int exAttrCount = itemEquip.EquipExAttrs.Count;
        if (itemEquip.IsLegandaryEquip())
        {
            legendaryRecord = itemEquip.EquipItemRecord;
            --exAttrCount;
        }
        var exAttrTypes = GameDataValue.GetRandomEquipAttrsType(itemEquip.EquipItemRecord.Slot, itemEquip.EquipQuality, legendaryRecord, itemEquip.EquipLevel, exAttrCount);

        for (int i = 0; i < exAttrTypes.Count; ++i)
        {
            var equipExAttr = new EquipExAttr();
            equipExAttr.AttrType = "RoleAttrImpactBaseAttr";
            var attrQuality = GameDataValue.GetExAttrRandomQuality();
            equipExAttr.Value = GameDataValue.GetExAttrRandomValue(exAttrTypes[i], itemEquip.EquipLevel, attrQuality);
            equipExAttr.AttrParams.Add((int)exAttrTypes[i].AttrID);
            equipExAttr.AttrParams.Add(GameDataValue.GetValueAttr(exAttrTypes[i].AttrID, equipExAttr.Value));
            equipExAttr.AttrParams.Add(attrQuality);
            equipExAttr.InitAttrQuality();
            itemEquip.EquipExAttrs[i] = equipExAttr;
        }

        itemEquip.RefreshEquip();
    }

    public static float GetExAttrPersent(ItemEquip itemEquip, EquipExAttr exAttr)
    {
        if (exAttr.AttrType != "RoleAttrImpactBaseAttr")
            return 1;

        if (ItemEquip.IsAttrSpToEquip(exAttr))
        {
            return ConfigIntToFloat(exAttr.Value);
        }

        int singleValueMax = Mathf.CeilToInt(itemEquip.EquipValue * _ExToBase);
        return (float)exAttr.Value / singleValueMax;
    }

    public class EquipExAttrRandom
    {
        public RoleAttrEnum AttrID;
        public bool CanRepeat;
        public int MinValue;
        public int MaxValue;
        public int Random;
        public int AttrValueIdx = -1;

        public EquipExAttrRandom(RoleAttrEnum attr, bool repeat,int minValue, int maxValue, int randomVal)
        {
            AttrID = attr;
            CanRepeat = repeat;
            MinValue = minValue;
            MaxValue = maxValue;
            Random = randomVal;
        }
    }

    public static List<EquipExAttrRandom> _WeaponExAttrs = new List<EquipExAttrRandom>()
    {
        new EquipExAttrRandom(RoleAttrEnum.Strength, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Dexterity, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Intelligence, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Vitality, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.HPMax, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Attack, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Defense, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.FireAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.ColdAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.LightingAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.WindAttackAdd, true, 1, -1, 100),

        new EquipExAttrRandom(RoleAttrEnum.AttackSpeed, false, 300, 1200, 100)
        {AttrValueIdx=1 },

        new EquipExAttrRandom(RoleAttrEnum.FireEnhance, false, 500, 1500, 100)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.ColdEnhance, false, 500, 1500, 100)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.LightingEnhance, false, 500, 1500, 100)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.WindEnhance, false, 500, 1500, 100)
        {AttrValueIdx=5 },
    };

    public static List<EquipExAttrRandom> _TorsoExAttrs = new List<EquipExAttrRandom>()
    {
        new EquipExAttrRandom(RoleAttrEnum.Strength, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Dexterity, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Intelligence, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Vitality, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.HPMax, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Attack, false, 1, -1, 50),
        new EquipExAttrRandom(RoleAttrEnum.Defense, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.FireAttackAdd, true, 1, -1, 80),
        new EquipExAttrRandom(RoleAttrEnum.ColdAttackAdd, true, 1, -1, 80),
        new EquipExAttrRandom(RoleAttrEnum.LightingAttackAdd, true, 1, -1, 80),
        new EquipExAttrRandom(RoleAttrEnum.WindAttackAdd, true, 1, -1, 80),

        new EquipExAttrRandom(RoleAttrEnum.FireResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },
        new EquipExAttrRandom(RoleAttrEnum.ColdResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },
        new EquipExAttrRandom(RoleAttrEnum.LightingResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },
        new EquipExAttrRandom(RoleAttrEnum.WindResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },

        new EquipExAttrRandom(RoleAttrEnum.FireEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.ColdEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.LightingEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.WindEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },
    };

    public static List<EquipExAttrRandom> _LegsExAttrs = new List<EquipExAttrRandom>()
    {
        new EquipExAttrRandom(RoleAttrEnum.Strength, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Dexterity, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Intelligence, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Vitality, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.HPMax, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Attack, false, 1, -1, 80),
        new EquipExAttrRandom(RoleAttrEnum.Defense, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.FireAttackAdd, true, 1, -1, 80),
        new EquipExAttrRandom(RoleAttrEnum.ColdAttackAdd, true, 1, -1, 80),
        new EquipExAttrRandom(RoleAttrEnum.LightingAttackAdd, true, 1, -1, 80),
        new EquipExAttrRandom(RoleAttrEnum.WindAttackAdd, true, 1, -1, 80),

        new EquipExAttrRandom(RoleAttrEnum.FireResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },
        new EquipExAttrRandom(RoleAttrEnum.ColdResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },
        new EquipExAttrRandom(RoleAttrEnum.LightingResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },
        new EquipExAttrRandom(RoleAttrEnum.WindResistan, true, 1, -1, 100)
        {AttrValueIdx=6 },

        new EquipExAttrRandom(RoleAttrEnum.FireEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.ColdEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.LightingEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },
        new EquipExAttrRandom(RoleAttrEnum.WindEnhance, false, 500, 1500, 50)
        {AttrValueIdx=5 },

        new EquipExAttrRandom(RoleAttrEnum.MoveSpeed, false, 500, 1500, 150)
        {AttrValueIdx=4 },
    };

    public static List<EquipExAttrRandom> _AmuletExAttrs = new List<EquipExAttrRandom>()
    {
        new EquipExAttrRandom(RoleAttrEnum.Strength, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Dexterity, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Intelligence, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Vitality, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.HPMax, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Attack, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Defense, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.FireAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.ColdAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.LightingAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.WindAttackAdd, true, 1, -1, 100),

        new EquipExAttrRandom(RoleAttrEnum.CriticalHitChance, false,300, 1200, 100)
        {AttrValueIdx=2 },
        new EquipExAttrRandom(RoleAttrEnum.CriticalHitDamge, true, 1, -1, 100)
        {AttrValueIdx=3 },
    };

    public static List<EquipExAttrRandom> _RingExAttrs = new List<EquipExAttrRandom>()
    {
        new EquipExAttrRandom(RoleAttrEnum.Strength, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Dexterity, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Intelligence, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Vitality, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.HPMax, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Attack, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Defense, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.FireAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.ColdAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.LightingAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.WindAttackAdd, true, 1, -1, 100),

        new EquipExAttrRandom(RoleAttrEnum.CriticalHitChance, false,300, 1200, 100)
        {AttrValueIdx=2 },
        new EquipExAttrRandom(RoleAttrEnum.CriticalHitDamge, true, 1, -1, 100)
        {AttrValueIdx=3 },
    };

    #endregion

    #region equip set



    #endregion

    #region gem

    public static float _GemAttrV = 10;
    public static float _GemAttrA = 13;

    public static int GetGemValue(int level)
    {
        //int value = Mathf.CeilToInt(level * _GemAttrV + 0.5f * level * level * _GemAttrA);
        int value = CalLvValue(level * 2);
        return value;
    }

    public static EquipExAttr GetGemAttr(RoleAttrEnum attr, int value)
    {
        EquipExAttr exAttr = EquipExAttr.GetBaseExAttr(attr, value);
        return exAttr;
    }

    public static List<EquipExAttr> GetGemSetAttr(Tables.GemSetRecord gemSet, int level)
    {
        List<EquipExAttr> attrList = new List<EquipExAttr>();
        foreach (var attrValue in gemSet.Attrs)
        {
            if (attrValue == null || string.IsNullOrEmpty(attrValue.AttrImpact))
                continue;

            if (attrValue.AttrImpact == "RoleAttrImpactBaseAttr"
                && (RoleAttrEnum)attrValue.AttrParams[0] != RoleAttrEnum.FireEnhance
                && (RoleAttrEnum)attrValue.AttrParams[0] != RoleAttrEnum.ColdEnhance
                && (RoleAttrEnum)attrValue.AttrParams[0] != RoleAttrEnum.LightingEnhance
                && (RoleAttrEnum)attrValue.AttrParams[0] != RoleAttrEnum.WindEnhance
                && (RoleAttrEnum)attrValue.AttrParams[0] != RoleAttrEnum.PhysicDamageEnhance)
            {
                EquipExAttr equipExAttr = new EquipExAttr();
                equipExAttr.AttrType = attrValue.AttrImpact;
                equipExAttr.Value = GetGemValue(level) * (int)attrValue.AttrParams[1];
                equipExAttr.AttrParams.Add((int)attrValue.AttrParams[0]);
                equipExAttr.AttrParams.Add(GameDataValue.GetValueAttr((RoleAttrEnum)attrValue.AttrParams[0], equipExAttr.Value));
                attrList.Add(equipExAttr);
            }
            else if((RoleAttrEnum)attrValue.AttrParams[0] == RoleAttrEnum.FireEnhance
                    || (RoleAttrEnum)attrValue.AttrParams[0] == RoleAttrEnum.ColdEnhance
                    || (RoleAttrEnum)attrValue.AttrParams[0] == RoleAttrEnum.LightingEnhance
                    || (RoleAttrEnum)attrValue.AttrParams[0] == RoleAttrEnum.WindEnhance)
            {
                EquipExAttr equipExAttr = new EquipExAttr();
                equipExAttr.AttrType = attrValue.AttrImpact;
                equipExAttr.Value = GetGemValue(level);
                equipExAttr.AttrParams.Add((int)attrValue.AttrParams[0]);
                equipExAttr.AttrParams.Add(GameDataValue.GetValueAttr((RoleAttrEnum)attrValue.AttrParams[0], TableReader.AttrValueLevel.GetSpValue(level, 9)));
                attrList.Add(equipExAttr);
            }
            else
            {
                int attrLv = TableReader.AttrValueLevel.GetSpValue(level, 10);
                var exAttr = attrValue.GetExAttr(attrLv);
                attrList.Add(exAttr);
            }
        }
        return attrList;
    }

    #endregion

    #region skill

    public static int GetSkillDamageRate(int skillLv, List<int> skillParam)
    {
        var levelRate = skillParam[1];
        var levelVal = levelRate * (skillLv - 1);
        return (int)(levelVal + skillParam[0]);
    }

    #endregion

    #region damage

    public const int _MAX_ROLE_LEVEL = 200;

    public static int GetRoleLv(int level)
    {
        return Mathf.Clamp(level, 1, _MAX_ROLE_LEVEL);
    }

    public static int GetEleDamage(int eleAtk, float damageRate, int eleEnhance, int eleResist)
    {
        float enhanceRate = 1 + ConfigIntToFloat(eleEnhance - eleResist);
        enhanceRate = Mathf.Max(enhanceRate, 0);
        float eleDamage = eleAtk * damageRate * (1 + ConfigIntToFloat(eleEnhance - eleResist));
        int finalDamage = Mathf.CeilToInt(eleDamage);
        return finalDamage;
    }

    public static int GetPhyDamage(int phyAtk, float damageRate, int enhance, int defence, int roleLevel)
    {
        var equipRecord = TableReader.EquipBaseAttr.GetRecord(GetRoleLv(roleLevel).ToString());
        float eleDamage = phyAtk * damageRate * (1 + ConfigIntToFloat(enhance));
        float resistRate = 1;
        if (defence > 0 && roleLevel > 0)
        {
            resistRate = (1 - (float)defence / (defence + equipRecord.DefenceStandar));
        }

        int finalDamage = Mathf.CeilToInt(eleDamage * resistRate);
        return finalDamage;
    }

    public static bool IsCriticleHit(int criticleRate)
    {
        int randomRate = Random.Range(0, 10001);
        return criticleRate > randomRate;
    }

    public static float GetCriticleDamageRate(int criticleDamage)
    {
        return ConfigIntToFloat(criticleDamage);
    }

    #endregion

    #endregion

    #region product & consume

    #region exp
    
    public static int GetLvUpExp(int playerLv, int attrLv)
    {
        int totalLv = playerLv + attrLv;
        RoleExpRecord expRecord = null;
        if (totalLv > 200)
        {
            expRecord = TableReader.RoleExp.GetRecord("200");
        }
        else
        {
            expRecord = TableReader.RoleExp.GetRecord(totalLv.ToString());
        }
        return expRecord.ExpValue;
    }

    public static int GetMonsterExp(MOTION_TYPE motionType, int level, int playerLv, STAGE_TYPE stageType = STAGE_TYPE.NORMAL)
    {
        MonsterAttrRecord monAttrRecord = null;
        if (level > 200)
        {
            monAttrRecord = TableReader.MonsterAttr.GetRecord("200");
        }
        else
        {
            monAttrRecord = TableReader.MonsterAttr.GetRecord(level.ToString());
        }
        int exp= monAttrRecord.Drops[0];
        if (motionType == MOTION_TYPE.Elite || motionType == MOTION_TYPE.ExElite)
        {
            exp = exp * 3;
        }
        else if (motionType == MOTION_TYPE.Hero)
        {
            exp = exp * 10;
        }

        if (stageType == STAGE_TYPE.ACTIVITY)
        {
            exp = 1;
        }
        return exp;
        
    }

    #endregion

    #region equip

    private static List<int> _EquipLevels = new List<int>() { 0, 1, 2, 3,3 };
    private static int _EqiupLevelStep = 5;

    private static List<ITEM_QUALITY> GetDropQualitys(MOTION_TYPE motionType, MonsterBaseRecord monsterRecord, int level, STAGE_TYPE stageType)
    {
        List<ITEM_QUALITY> dropEquipQualitys = new List<ITEM_QUALITY>();
        int dropCnt = 0;
        int dropQuality = 0;
        float exEquipRate = ConfigIntToFloat(RoleData.SelectRole._BaseAttr.GetValue(RoleAttrEnum.ExEquipDrop)) + 1;
        if (stageType == STAGE_TYPE.ACTIVITY)
        {
            
        }
        else if (stageType == STAGE_TYPE.NORMAL)
        {

        }
        else if (stageType == STAGE_TYPE.BOSS)
        {
            switch (motionType)
            {
                case MOTION_TYPE.Normal:
                    dropCnt = GameRandom.GetRandomLevel(95, 5);
                    for (int i = 0; i < dropCnt; ++i)
                    {
                        dropQuality = GameRandom.GetRandomLevel(7000, (int)(3000 * exEquipRate));
                        if (dropQuality > 0)
                        {
                            ++dropQuality;
                        }
                        dropEquipQualitys.Add((ITEM_QUALITY)dropQuality);
                    }

                    break;
                case MOTION_TYPE.Elite:
                    dropCnt = GameRandom.GetRandomLevel(10, 70, 20);
                    for (int i = 0; i < dropCnt; ++i)
                    {
                        dropQuality = GameRandom.GetRandomLevel(3000, (int)(6500 * exEquipRate), (int)(500 * exEquipRate));
                        if (dropQuality > 0)
                        {
                            ++dropQuality;
                        }
                        dropEquipQualitys.Add((ITEM_QUALITY)dropQuality);
                    }

                    break;
                case MOTION_TYPE.Hero:
                    if (level <= 20)
                        dropCnt = GameRandom.GetRandomLevel(0, 10, 50, 30);
                    else if (level <= 40)
                        dropCnt = GameRandom.GetRandomLevel(0, 0, 50, 50, 10);
                    else
                        dropCnt = GameRandom.GetRandomLevel(0, 0, 50, 30, 30);
                    bool isOringe = false;
                    for (int i = 0; i < dropCnt; ++i)
                    {
                        if (level <= 10)
                            dropQuality = GameRandom.GetRandomLevel(0, 7000, (int)(2500 * exEquipRate), (int)(500 * exEquipRate));
                        else if (level <= 30)
                            dropQuality = GameRandom.GetRandomLevel(0, 6000, (int)(2500 * exEquipRate), (int)(1500 * exEquipRate));
                        else
                            dropQuality = GameRandom.GetRandomLevel(0, 4000, (int)(3500 * exEquipRate), (int)(2500 * exEquipRate));
                        if (dropQuality == (int)ITEM_QUALITY.ORIGIN)
                        {
                            if (!isOringe)
                            {
                                isOringe = true;
                            }
                            else
                            {
                                --dropQuality;
                            }
                        }
                        if (dropQuality > 0)
                        {
                            ++dropQuality;
                        }
                        dropEquipQualitys.Add((ITEM_QUALITY)dropQuality);
                    }

                    break;
            }
        }
        return dropEquipQualitys;
    }

    public static int GetEquipLv(EQUIP_SLOT equipSlot, int dropLevel)
    {

        int lvStep = dropLevel / _EqiupLevelStep;
        int slotLevel = lvStep * _EqiupLevelStep + _EquipLevels[(int)equipSlot];
        if (slotLevel > dropLevel)
        {
            slotLevel = slotLevel - _EqiupLevelStep;
        }
        
        return Mathf.Max(slotLevel, 1);
    }

    public static List<ItemEquip> GetMonsterDropEquip(MOTION_TYPE motionType, MonsterBaseRecord monsterRecord, int level, STAGE_TYPE stageType)
    {
        List<ItemEquip> dropEquipList = new List<ItemEquip>();
        var dropEquipQualitys = GetDropQualitys(motionType, monsterRecord, level, stageType);
        if (dropEquipQualitys.Count == 0)
            return dropEquipList;

        for (int i = 0; i < dropEquipQualitys.Count; ++i)
        {
            if (dropEquipQualitys[i] == ITEM_QUALITY.ORIGIN)
            {
                if (monsterRecord.ValidSpDrops.Count == 0)
                    continue;

                int dropIdx = GameRandom.GetRandomLevel(35, 30, 20, 15, 10, 50);
                //int dropIdx = Random.Range(0, monsterRecord.ValidSpDrops.Count);
                var dropItem = monsterRecord.SpDrops[dropIdx];
                if (dropItem == null)
                {
                    var equipSlot = GetRandomItemSlot(dropEquipQualitys[i]);
                    var equipLevel = GetEquipLv(equipSlot, level);
                    var equipValue = CalLvValue(equipLevel, equipSlot);
                    var dropEquip = ItemEquip.CreateEquip(equipLevel, dropEquipQualitys[i], -1, (int)equipSlot);
                    dropEquipList.Add(dropEquip);
                }
                else
                {
                    var dropEquipTab = TableReader.EquipItem.GetRecord(dropItem.Id);
                    var equipLevel = GetEquipLv(dropEquipTab.Slot, level);
                    var equipValue = CalLvValue(equipLevel, dropEquipTab.Slot);
                    var dropEquip = ItemEquip.CreateEquip(equipLevel, dropEquipQualitys[i], int.Parse(dropItem.Id), (int)dropEquipTab.Slot);
                    dropEquipList.Add(dropEquip);
                }
            }
            else
            {
                var equipSlot = GetRandomItemSlot(dropEquipQualitys[i]);
                var equipLevel = GetEquipLv(equipSlot, level);
                var equipValue = CalLvValue(equipLevel, equipSlot);
                var dropEquip = ItemEquip.CreateEquip(equipLevel, dropEquipQualitys[i], -1, (int)equipSlot);
                dropEquipList.Add(dropEquip);
            }
        }

        return dropEquipList;
    }

    public static int GetLegencyLv(int equipLv)
    {
        int impactLevel = Tables.TableReader.AttrValueLevel.GetSpValue(equipLv, 7);
        return impactLevel;
    }

    public static EQUIP_SLOT GetRandomItemSlot(ITEM_QUALITY itemQuality)
    {
        int slotTypeCnt = (int)EQUIP_SLOT.RING + 1;
        if (itemQuality == ITEM_QUALITY.WHITE)
        {
            slotTypeCnt = (int)EQUIP_SLOT.LEGS + 1;
        }
        int randomSlot = UnityEngine.Random.Range(0, slotTypeCnt);
        return (EQUIP_SLOT)randomSlot;
    }

    public static int GetEquipSellGold(ItemEquip itemEquip)
    {
        int gold = Mathf.CeilToInt((itemEquip.EquipLevel * 0.5f + itemEquip.EquipExAttrs.Count) * ((int)itemEquip.EquipQuality + 1));
        return gold;
    }

    public static int GetEquipBuyGold(ItemEquip itemEquip)
    {
        int gold = GetEquipSellGold(itemEquip) * 8;
        return gold;
    }

    public static int GetBuyBackGold(ItemEquip itemEquip)
    {
        int gold = GetEquipSellGold(itemEquip) * 2;
        return gold;
    }


    #endregion

    #region equip material

    public static int _EquipMatDropBase = 100;
    public static int _DropMatLevel = 30;
    public static int _ConsumeOneTime = 6;
    public static int _DestoryEquipMat = 5;
    public static float _ComsumeDiamondFixed = 0.1f;

    public static float _LevelParam = 0.01f;
    public static int _NormalMatBase = 2500;
    public static int _EliteMatBase = 10000;
    public static int _SpecialMatBase = 10000;
    public static int _BossMatBase = 100000;
    //public static int _NormalMatBase = 0;
    //public static int _EliteMatBase = 10000;
    //public static int _SpecialMatBase = 10000;
    //public static int _BossMatBase = 50000;

    public static int GetEquipMatDropCnt(MOTION_TYPE motionType, MonsterBaseRecord monsterRecord, int level)
    {
        int dropCnt = 0;
        if (level < _DropMatLevel)
            return dropCnt;

        float modifyRate = (ConfigIntToFloat(RoleData.SelectRole._BaseAttr.GetValue(RoleAttrEnum.ExMatDrop)) + 1);
        switch (motionType)
        {
            case MOTION_TYPE.Normal:
                dropCnt = GetDropCnt(Mathf.CeilToInt( _NormalMatBase * level * _LevelParam * modifyRate));
                break;
            case MOTION_TYPE.Elite:
                dropCnt = GetDropCnt(Mathf.CeilToInt(_EliteMatBase * level * _LevelParam * modifyRate));
                break;
            case MOTION_TYPE.Hero:
                dropCnt = GetDropCnt(Mathf.CeilToInt(_BossMatBase * level * _LevelParam * modifyRate));
                break;
        }

        return dropCnt;
    }

    private static int GetDropCnt(int rate)
    {
        
        if (rate >= GetMaxRate())
        {
            int baseDrop = Mathf.CeilToInt(rate / GetMaxRate());
            int exRate = (rate - GetMaxRate() * baseDrop) / GetMaxRate();
            if (exRate > 0)
            {
                var exRandom = Random.Range(0, GetMaxRate());
                if (exRandom < exRate)
                    baseDrop = baseDrop + 1;
            }
            return baseDrop;
        }

        var random = Random.Range(0, GetMaxRate());
        if (random < rate)
            return 1;

        return 0;
    }

    public static int GetEquipLvUpConsume(ItemEquip equip)
    {
        return Mathf.CeilToInt(_ConsumeOneTime * equip.EquipLevel * 0.1f);
    }

    public static int GetEquipLvUpConsumeDiamond(ItemEquip equip)
    {
        return Mathf.CeilToInt(GetEquipLvUpConsume(equip) * _ComsumeDiamondFixed);
    }

    public static int GetDestoryGetMatCnt(ItemEquip equip)
    {
        if (equip.EquipLevel < _DropMatLevel)
            return 0;

        int destoryMatCnt = _DestoryEquipMat * equip.EquipLevel;

        if (equip.EquipLevel < 100)
        {
            if (equip.EquipQuality == ITEM_QUALITY.PURPER)
                destoryMatCnt = Mathf.CeilToInt(destoryMatCnt * 0.08f);
            else if (equip.EquipQuality == ITEM_QUALITY.ORIGIN)
                destoryMatCnt = Mathf.CeilToInt(destoryMatCnt * 0.3f);
            else
                destoryMatCnt = Mathf.CeilToInt(destoryMatCnt * 0.03f);
        }
        else
        {
            if (equip.EquipQuality == ITEM_QUALITY.PURPER)
                destoryMatCnt = Mathf.CeilToInt(destoryMatCnt * 0.1f);
            else if (equip.EquipQuality == ITEM_QUALITY.ORIGIN)
                destoryMatCnt = Mathf.CeilToInt(destoryMatCnt * 0.5f);
            else
                destoryMatCnt = Mathf.CeilToInt(destoryMatCnt * 0.04f);
        }

        return destoryMatCnt + equip.EquipRefreshCostMatrial;
    }

    public static int GetEquipRefreshGold(ItemEquip eqiup)
    {
        var value = (int)(GetGoldStageDrop(eqiup.EquipLevel) * 0.5f);
        return value;

    }

    public static int GetEquipRefreshDiamond(ItemEquip eqiup)
    {

        return 5;

    }
    #endregion

    #region gem mat

    public static int GetGemLevelUpCostMoney(int level)
    {
        var stageDrop = GetGoldStageDrop(level * 2);
        return (int)(stageDrop * 0.5f);
    }

    public static int GetGemLevelUpCostMat(int level)
    {
        return TableReader.AttrValueLevel.GetSpValue(level, 15);
    }

    #endregion

    #region gold

    public static float _GoldLevelParam = 10f;
    public static int _NormalGoldBase = 3500;
    public static int _EliteGoldBase = 10000;
    public static int _SpecialGoldBase = 10000;
    public static int[] _BossGoldBase = {0, 0, 3000, 3000, 4000};
    

    public static int GetGoldDropCnt(MOTION_TYPE motionType, int level)
    {
        int dropCnt = 0;
        switch (motionType)
        {
            case MOTION_TYPE.Normal:
                dropCnt = GetGoldDropCnt(_NormalGoldBase);
                break;
            case MOTION_TYPE.Elite:
                dropCnt = GetGoldDropCnt(_EliteGoldBase);
                break;
            case MOTION_TYPE.Hero:
                dropCnt = GetGoldDropCnt(_BossGoldBase) + 1;
                break;
        }

        return dropCnt;
    }

    public static int GetGoldDropNum(MOTION_TYPE motionType, int level)
    {
        var monAttrRecord = TableReader.MonsterAttr.GetRecord(level.ToString());
        if (monAttrRecord == null)
        {
            monAttrRecord = TableReader.MonsterAttr.GetRecord("200");
        }

        float random = Random.Range(0.6f, 1.5f);
        var exGoldDrop = RoleData.SelectRole._BaseAttr.GetValue(RoleAttrEnum.ExGoldDrop);
        float rate = ConfigIntToFloat(exGoldDrop) + 1;
        float dropNum = ConfigIntToFloat(monAttrRecord.Attrs[1]);

        return Mathf.CeilToInt(dropNum * random * rate);
    }

    public static int GetGoldDropCnt(params int[] rates)
    {
        int goldCnt = 0;
        var random = Random.Range(0, GetMaxRate());
        if (rates.Length == 1)
        {
            if (random < rates[0])
                goldCnt = 1;
        }
        else
        {
            goldCnt = GameRandom.GetRandomLevel(rates);
        }

        return goldCnt;
    }

    public static int GetGoldStageDrop(int level)
    {
        var value = CalLvValue(level);
        return value * 10;
    }

    #endregion

    #region skill

    public static int GetSkillLvUpGold(SkillInfoRecord skillRecord, int skillLv)
    {
        if (skillLv == 0)
            return skillRecord.CostStep[1];
        else
            return skillRecord.CostStep[2] * skillLv;
    }

    #endregion

    #region five element

    public static List<EquipExAttrRandom> _FiveElementAttrs = new List<EquipExAttrRandom>()
    {
        new EquipExAttrRandom(RoleAttrEnum.Strength, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Dexterity, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Intelligence, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Vitality, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.HPMax, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.Attack, false, 1, -1, 50),
        new EquipExAttrRandom(RoleAttrEnum.FireAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.ColdAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.LightingAttackAdd, true, 1, -1, 100),
        new EquipExAttrRandom(RoleAttrEnum.WindAttackAdd, true, 1, -1, 100),
        
    };

    public static EquipExAttr GetFiveElementExAttr(int level, FIVE_ELEMENT eleType)
    {
        int attrValue = TableReader.FiveElementLevel.GetElementLevel(level).Value;

        var randomAttrType = CalRandomAttrs(_FiveElementAttrs, 1, level);
        var equipExAttr = new EquipExAttr();
        equipExAttr.AttrType = "RoleAttrImpactBaseAttr";
        var attrQuality = GameDataValue.GetExAttrRandomQuality();
        equipExAttr.Value = GameDataValue.GetExAttrRandomValue(randomAttrType[0].AttrID, level, attrQuality);
        equipExAttr.AttrParams.Add((int)randomAttrType[0].AttrID);
        equipExAttr.AttrParams.Add(GameDataValue.GetValueAttr(randomAttrType[0].AttrID, equipExAttr.Value));
        equipExAttr.AttrParams.Add(attrQuality);
        equipExAttr.InitAttrQuality();

        return equipExAttr;

    }

    public static FiveElementAttrRecord GetFiveElementRandomAttr(List<FiveElementAttrRecord> staticList)
    {
        int totalRandom = 0;
        foreach (var attrRandom in staticList)
        {
            totalRandom += (attrRandom.Random);
        }

        int temp = totalRandom;
        int randomVar = Random.Range(0, temp);
        FiveElementAttrRecord attr = null;
        foreach (var attrRandom in staticList)
        {
            temp -= attrRandom.Random;
            if (randomVar >= temp)
            {
                attr = attrRandom;
                break;
            }
        }
        if (attr == null)
        {
            attr = staticList[staticList.Count - 1];
        }

        return attr;
    }

    //public static ItemFiveElement GetDropElementItem(int level)
    //{

    //}

    #endregion

    #endregion

    #region monster attr

    public static int _BossStageStarLevel;

    public static int GetStageLevel(int difficult, int stageIdx, STAGE_TYPE stageMode)
    {
        int diffLv = (difficult - 2) * 20;
        diffLv = Mathf.Clamp(diffLv, 0, 200);
        int level = 0;
        if (stageMode == STAGE_TYPE.NORMAL)
        {
            level = diffLv + stageIdx;
        }
        else if (stageMode == STAGE_TYPE.BOSS)
        {
            var stageRecord = TableReader.BossStage.GetRecord(stageIdx.ToString());
            level = stageRecord.Level;
        }
        else if (stageMode == STAGE_TYPE.ACTIVITY)
        {
            level = RoleData.SelectRole.TotalLevel;
        }

        return level;
    }

    public static float _MonsterHPParam = 0.012f;
    public static float _MonsterAtkParam = 0.006f;

    public static int GetMonsterHP(MonsterBaseRecord monsterBase, int roleLv, MOTION_TYPE monsterType)
    {
        int hpMax = 0;
        float hpBase = ConfigIntToFloat(monsterBase.BaseAttr[2]);
        MonsterAttrRecord attrRecord = null;
        if (!TableReader.MonsterAttr.ContainsKey(roleLv.ToString()))
        {
            if (roleLv <= 0)
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("1");
            }
            else
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("200");
            }
        }
        else
        {
            attrRecord = TableReader.MonsterAttr.GetRecord(roleLv.ToString());
        }
        hpMax = (int)(attrRecord.Attrs[2] * hpBase);
        //if (roleLv <= 5)
        //{
        //    hpMax = (int)(CalWeaponAttack(roleLv) * hpBase + roleLv * 15 * hpBase);
        //}
        //else if (roleLv <= 10)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 5, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 5) * 20 * hpBase);
        //}
        //else if (roleLv <= 20)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 10, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 10) * 40 * hpBase);
        //}
        //else if (roleLv <= 30)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 20, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 20) * 80 * hpBase);
        //}
        //else if (roleLv <= 40)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 30, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 30) * 80 * hpBase);
        //}
        //else if (roleLv <= 50)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 40, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 40) * 150 * hpBase);
        //}
        //else if (roleLv <= 60)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 50, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 50) * 200 * hpBase);
        //}
        //else if (roleLv <= 70)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 60, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 60) * 280 * hpBase);
        //}
        //else if (roleLv <= 80)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 70, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 70) * 400 * hpBase);
        //}
        //else if (roleLv <= 90)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 80, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 80) * 600 * hpBase);
        //}
        //else if (roleLv <= 100)
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 90, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 90) * 900 * hpBase);
        //}
        //else
        //{
        //    var hpTemp = GetMonsterHP(monsterBase, 100, monsterType);
        //    hpMax = (int)(hpTemp + (roleLv - 100) * 1500 * hpBase);
        //}

        return hpMax;
    }

    public static int GetMonsterAtk(MonsterBaseRecord monsterBase, int roleLv, MOTION_TYPE monsterType)
    {
        int atkValue = 0;
        float atkBase = ConfigIntToFloat(monsterBase.BaseAttr[0]);
        MonsterAttrRecord attrRecord = null;
        if (!TableReader.MonsterAttr.ContainsKey(roleLv.ToString()))
        {
            if (roleLv <= 0)
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("1");
            }
            else
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("200");
            }
        }
        else
        {
            attrRecord = TableReader.MonsterAttr.GetRecord(roleLv.ToString());
        }
        atkValue = (int)(attrRecord.Attrs[0] * atkBase);
        //if (roleLv <= 10)
        //{
        //    atkValue = (int)(CalEquipTorsoHP(roleLv) * 2.0f + _HPRoleLevelBase);
        //}
        //else if (roleLv <= 20)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 10, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 10) * 30);
        //}
        //else if (roleLv <= 30)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 20, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 20) * 60);
        //}
        //else if (roleLv <= 40)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 30, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 30) * 80);
        //}
        //else if (roleLv <= 50)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 40, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 40) * 150);
        //}
        //else if (roleLv <= 60)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 50, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 50) * 200);
        //}
        //else if (roleLv <= 70)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 60, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 60) * 280);
        //}
        //else if (roleLv <= 80)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 70, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 70) * 400);
        //}
        //else if (roleLv <= 90)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 80, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 80) * 600);
        //}
        //else if (roleLv <= 100)
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 90, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 90) * 900);
        //}
        //else
        //{
        //    var hpTemp = GetMonsterAtk(monsterBase, 100, monsterType);
        //    atkValue = (int)(hpTemp + (roleLv - 100) * 1500);
        //}
        return atkValue;
    }

    public static int GetMonsterDef(MonsterBaseRecord monsterBase, int roleLv, MOTION_TYPE monsterType)
    {
        int defValue = 0;
        float defBase = ConfigIntToFloat(monsterBase.BaseAttr[1]);
        MonsterAttrRecord attrRecord = null;
        if (!TableReader.MonsterAttr.ContainsKey(roleLv.ToString()))
        {
            if (roleLv <= 0)
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("1");
            }
            else
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("200");
            }
        }
        else
        {
            attrRecord = TableReader.MonsterAttr.GetRecord(roleLv.ToString());
        }
        defValue = (int)(attrRecord.Attrs[1] * defBase);
        return defValue;
    }

    public static int GetMonsterEleDef(MonsterBaseRecord monsterBase, int roleLv, ElementType elementType)
    {
        int defValue = 0;
        float defBase = ConfigIntToFloat(monsterBase.BaseAttr[2 + (int)elementType]);
        MonsterAttrRecord attrRecord = null;
        if (!TableReader.MonsterAttr.ContainsKey(roleLv.ToString()))
        {
            if (roleLv <= 0)
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("1");
            }
            else
            {
                attrRecord = TableReader.MonsterAttr.GetRecord("200");
            }
        }
        else
        {
            attrRecord = TableReader.MonsterAttr.GetRecord(roleLv.ToString());
        }
        ;
        defValue = (int)(ConfigIntToFloat(attrRecord.Attrs[2 + (int)elementType]) * defBase);
        return defValue;
    }

    #endregion

    #region test 

    public static float GetSkillDamageRate(int skillIdx, bool isEnhance = false)
    {
        var attackRecord = Tables.TableReader.SkillInfo.GetRecord("10001");
        var skillRecord = Tables.TableReader.SkillInfo.GetRecord(TestFight.GetTestSkill(skillIdx).ToString());
        var atkData = SkillData.Instance.GetSkillInfo("10001");
        var skillData = SkillData.Instance.GetSkillInfo(TestFight.GetTestSkill(skillIdx).ToString());
        var atkDamage = GameDataValue.ConfigIntToFloat(GameDataValue.GetSkillDamageRate(atkData.SkillLevel, attackRecord.EffectValue));
        var skillDamage = GameDataValue.ConfigIntToFloat(GameDataValue.GetSkillDamageRate(skillData.SkillLevel, skillRecord.EffectValue));

        if (skillIdx == 1)
        {
            atkDamage *= 0.33f;
        }
        else if (skillIdx == 2)
        {
            atkDamage *= 0.54f;
        }
        else if (skillIdx == 3)
        {
            atkDamage *= 0.75f;
        }

        float damage = 0;

        if (isEnhance)
        {
            int eleLevel = (atkData.SkillLevel / 2) + 1;
            damage = atkDamage + 0.5f + (0.05f * eleLevel) + skillDamage * 1.26f;
        }
        else
        {
            damage = atkDamage + skillDamage;
        }

        return damage;
    }

    #endregion

}
