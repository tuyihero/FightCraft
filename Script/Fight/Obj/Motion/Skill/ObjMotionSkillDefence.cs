﻿using UnityEngine;
using System.Collections;

public class ObjMotionSkillDefence : ObjMotionSkillBase
{
    void FixedUpdate()
    {
        if (_MotionManager.ActingSkill != this)
            return;

        _DefenceTime += Time.fixedDeltaTime;
        if (_DefenceTime < _DefenceMinTime)
        {
            return;
        }

        if (_DefenceMaxTime > 0 && _DefenceTime > _DefenceMaxTime)
        {
            FinishSkill();
            return;
        }

        if (!InputManager.Instance.IsKeyHold(_ActInput))
        {
            FinishSkill();
            return;
        }
    }

    #region override

    public override void Init()
    {
        base.Init();

        _MotionManager._EventController.RegisteEvent(GameBase.EVENT_TYPE.EVENT_MOTION_HIT, HitEvent, 100);
        _MotionManager._EventController.RegisteEvent(GameBase.EVENT_TYPE.EVENT_MOTION_FLY, HitEvent, 100);
    }

    public override void AnimEvent(string function, object param)
    {

    }

    #endregion

    private static float _MoveBackTime = 0.1f;

    public float _DefenceMinTime;
    public float _DefenceMaxTime;
    public float _MoveBackSpeed;

    private float _DefenceTime;

    public override bool ActSkill()
    {
        if (!base.ActSkill())
            return false;

        _DefenceTime = 0;
        return true;
    }

    public void HitEvent(object go, Hashtable eventArgs)
    {
        if (_MotionManager.ActingSkill != this)
            return;

        eventArgs.Add("StopEvent", true);

        Vector3 destMove = -_MotionManager.transform.forward.normalized * _MoveBackSpeed * _MoveBackTime;
        _MotionManager.SetMove(destMove, _MoveBackTime);
    }
}