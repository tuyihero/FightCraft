﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tables;



public class PlayerDataPack : DataPackBase
{
    #region 单例

    private static PlayerDataPack _Instance;
    public static PlayerDataPack Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new PlayerDataPack();
            }
            return _Instance;
        }
    }

    private PlayerDataPack()
    {
        _SaveFileName = "PlayerDataPack";
    }

    #endregion

    #region money
    [SaveField(1)]
    private int _Gold = 0;

    [SaveField(2)]
    private int _Diamond = 0;

    public void AddGold(int value)
    {
        _Gold += value;
    }

    public bool DecGold(int value)
    {
        if (_Gold < value)
            return false;

        _Gold -= value;
        return true;
    }

    public void AddDiamond(int value)
    {
        _Diamond += value;
    }

    public bool DecDiamond(int value)
    {
        if (_Diamond < value)
            return false;

        _Diamond -= value;
        return true;
    }
    #endregion


    #region char

    [SaveField(3)]
    public List<RoleData> _RoleList;

    public RoleData _SelectedRole;

    public void InitPlayerData()
    {
        if (_RoleList == null || _RoleList.Count == 0)
        {
            _RoleList = new List<RoleData>();
            for (int i = 0; i < 4; ++i)
            {
                var newRole = new RoleData();
                newRole._SaveFileName = "Role" + i;
                _RoleList.Add(newRole);
            }
        }

        for (int i = 0; i < _RoleList.Count; ++i)
        {
            _RoleList[i].LoadClass(false);
            if (i == (int)PROFESSION.BOY_DEFENCE)
            {
                _RoleList[i].MainBaseName = "MainCharBoyDefence";
                _RoleList[i].MotionFold = "MainCharBoy";
                _RoleList[i].ModelName = "Char_Boy_01_JL_AM";
                _RoleList[i].Profession = PROFESSION.BOY_DEFENCE;
                _RoleList[i].DefaultWeaponModel = "Weapon_HW_01_SM";
            }
            else if (i == 1)
            {
                _RoleList[i].MainBaseName = "MainCharGirlDodge";
                _RoleList[i].MotionFold = "MainCharGirl";
                _RoleList[i].ModelName = "Char_Girl_01_AM";
                _RoleList[i].Profession = PROFESSION.GIRL_DOUGE;
                _RoleList[i].DefaultWeaponModel = "Weapon_S_01_SM";
            }
            else if (i == 2)
            {
                _RoleList[i].MainBaseName = "MainCharBoyDodge";
                _RoleList[i].MotionFold = "MainCharBoy";
                _RoleList[i].ModelName = "Char_Boy_01_AM";
                _RoleList[i].Profession = PROFESSION.BOY_DOUGE;
                _RoleList[i].DefaultWeaponModel = "Weapon_HW_01_SM";
            }
            else if (i == 3)
            {
                _RoleList[i].MainBaseName = "MainCharBoyDodge";
                _RoleList[i].MotionFold = "MainCharGirl";
                _RoleList[i].ModelName = "Char_Girl_02_AM";
                _RoleList[i].Profession = PROFESSION.GIRL_DEFENCE;
                _RoleList[i].DefaultWeaponModel = "Weapon_S_01_SM";
            }
        }
    }

    public void SelectRole(int roleIdx)
    {
        Debug.Log("_RoleList.Count:" + _RoleList.Count);
        if (roleIdx >= 0 && roleIdx < _RoleList.Count)
        {
            _SelectedRole = _RoleList[roleIdx];
        }

        _SelectedRole.LoadClass(true);
        _SelectedRole.InitRoleData();
        _SelectedRole.CalculateAttr();
    }

    #endregion

}

