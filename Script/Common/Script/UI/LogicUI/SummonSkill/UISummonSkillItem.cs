﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
using UnityEngine.EventSystems;
using System;
using Tables;

public class UISummonSkillItem : UIItemSelect
{
    public GameObject _InfoPanel;
    public Text _Name;
    public Image _Quality;
    public Image _Icon;
    public Text _Level;
    public GameObject[] _Stars;
    public GameObject _ArraySelect;

    protected SummonMotionData _SummonMotionData;
    public SummonMotionData SummonMotionData
    {
        get
        {
            return _SummonMotionData;
        }
    }

    private bool _IsMaterial = false;

    public override void Show(Hashtable hash)
    {
        base.Show();

        var summonData = (SummonMotionData)hash["InitObj"];
        if (summonData == null)
            return;

        _IsMaterial = false;
        if (hash.ContainsKey("IsMaterial"))
        {
            _IsMaterial = (bool)hash["IsMaterial"];
        }

        ShowSummonData(summonData);
    }

    public override void Refresh()
    {
        base.Refresh();

        ShowSummonData(_SummonMotionData);
    }

    public void ShowSummonData(SummonMotionData summonData)
    {
        //if (_ArraySelect != null)
        //{
        //    _ArraySelect.SetActive(false);
        //}

        _SummonMotionData = summonData;

        if (_SummonMotionData == null)
        {
            _InfoPanel.SetActive(false);
            return;
        }

        _InfoPanel.SetActive(true);
        _Name.text = CommonDefine.GetQualityColorStr(_SummonMotionData.SummonRecord.Quality) + StrDictionary.GetFormatStr(_SummonMotionData.SummonRecord.NameDict) + "</color>";

        if (_IsMaterial)
        {
            _Level.text = _SummonMotionData.ItemStackNum.ToString();
            for (int i = 0; i < _Stars.Length; ++i)
            {
                _Stars[i].gameObject.SetActive(false);
            }
        }
        else
        {
            _Level.text = "Lv." + SummonSkillData.Instance.SummonLevel;
            for (int i = 0; i < _Stars.Length; ++i)
            {
                if (i < _SummonMotionData.StarLevel)
                {
                    _Stars[i].gameObject.SetActive(true);
                }
                else
                {
                    _Stars[i].gameObject.SetActive(false);
                }
            }
        }
    }

    #region 

    public override void OnItemClick()
    {
        base.OnItemClick();
    }

    public void SetArraySelected(bool isSelected)
    {
        _ArraySelect.SetActive(isSelected);
    }

    #endregion
}

