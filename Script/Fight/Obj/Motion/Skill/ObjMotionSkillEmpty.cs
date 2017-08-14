﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjMotionSkillEmpty : ObjMotionSkillBase
{

    public float[] _StartColliderTime;

    protected int _ActingColliderIdx = 0;

    public override bool ActSkill(Hashtable exHash = null)
    {
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

    protected IEnumerator StartCollider()
    {
        yield return new WaitForSeconds(_StartColliderTime[_ActingColliderIdx]);
        ColliderStart(_ActingColliderIdx);
        ++_ActingColliderIdx;
        if (_StartColliderTime.Length > _ActingColliderIdx)
        {
            StartCoroutine(StartCollider());
        }
        else
        {
            FinishSkill();
        }
    }
}
