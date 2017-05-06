﻿using UnityEngine;
using System.Collections;
using GameLogic;

public class DropItem : MonoBehaviour
{
    private const float _DropTime = 0.3f;
    private const float _DropHeightSpeed = 5.0f;

    public string _DropName;

    private Vector3 _DropSpeed;
    private float _MoveTime;

    private Collider _Collider;
    public Collider Collider
    {
        get
        {
            if (_Collider == null)
            {
                _Collider = gameObject.GetComponent<Collider>();
            }
            return _Collider;
        }
    }

    void FixedUpdate()
    {
        if(_MoveTime < _DropTime)
            UpdateDropPos();
    }

    public void InitDrop(DropItemData dropData)
    {
        if (dropData._DropGold > 0)
        {
            InitGoldModel(dropData._DropGold);
            _DropName = dropData._DropGold.ToString();
            Collider.enabled = true;
        }
        else if (dropData._ItemEquip != null)
        {
            InitItemModel(dropData._ItemEquip);
            _DropName = dropData._ItemEquip.GetEquipNameWithColor();
            Collider.enabled = false;
        }
        else if (dropData._ItemBase != null)
        {
            InitItemModel(dropData._ItemBase);
            _DropName = dropData._ItemBase.CommonItemRecord.Name;
            Collider.enabled = false;
        }
        else
        {
            Debug.Log("Drop Empty");
        }

        transform.position = dropData._MonsterPos;
        _DropSpeed = (dropData._DropPos - dropData._MonsterPos) / _DropTime;
        _MoveTime = 0;

        GameUI.UIDropNamePanel.ShowDropItem(this);
    }

    private void InitGoldModel(int gold)
    {
        var obj = GameBase.ResourceManager.Instance.GetInstanceGameObject("Drop/Drop_055/Drop_055");
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;

        int randomAngle = Random.Range(0, 360);
        obj.transform.localRotation = Quaternion.Euler(0, randomAngle, 0);
    }

    private void InitItemModel(ItemBase itembase)
    {
        var obj = GameBase.ResourceManager.Instance.GetInstanceGameObject(itembase.CommonItemRecord.DropItem);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;

        int randomAngle = Random.Range(0, 360);
        obj.transform.localRotation = Quaternion.Euler(0, randomAngle, 0);
    }

    private void UpdateDropPos()
    {
        _MoveTime += Time.fixedDeltaTime;
        transform.position += _DropSpeed * Time.fixedDeltaTime;
        if (_MoveTime < _DropTime * 0.5f)
        {
            transform.position += new Vector3(0, _DropHeightSpeed * Time.fixedDeltaTime, 0);
        }
        else if(_MoveTime < _DropTime)
        {
            transform.position -= new Vector3(0, _DropHeightSpeed * Time.fixedDeltaTime, 0);
        }
    }

    #region 

    void OnTriggerEnter(Collider other)
    {
        MonsterDrop.PickItem(this);
    }

    #endregion
}
