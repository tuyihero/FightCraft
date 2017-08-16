﻿using UnityEngine;
using System.Collections;

public class ObjMotionSkillBlock : ObjMotionSkillEmpty
{

    #region override

    public override void Init()
    {
        _MotionManager = gameObject.GetComponentInParent<MotionManager>();

        if (_Anim != null)
        {
            _MotionManager.InitAnimation(_Anim);
        }
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        InitCollider(null);
    }

    #endregion

    public override bool IsCanActSkill()
    {
        return (_MotionManager.MotionPrior == BaseMotionManager.HIT_PRIOR);
    }

    public override bool ActSkill(Hashtable exhash)
    {
        if (_Anim != null)
            PlayAnimation(_Anim);
        if (_Effect != null)
            PlaySkillEffect(_Effect);

        this.enabled = true;
        _ActingColliderIdx = 0;
        if (_StartColliderTime.Length > _ActingColliderIdx)
        {
            StartCoroutine(StartCollider());
        }
        return true;
    }

}