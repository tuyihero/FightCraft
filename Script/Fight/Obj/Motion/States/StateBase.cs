﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    // state priors
    public const int IDLE_PRIOR = 0;
    public const int MOVE_PRIOR = 10;
    public const int HIT_PRIOR = 1000;
    public const int FLY_PRIOR = 1001;
    public const int CATCH_PRIOR = 1002;
    public const int RISE_PRIOR = 999;
    public const int LIE_PRIOR = 998;
    public const int DIE_PRIOR = 2000;

    // state opts
    public enum MotionOpt
    {
        Input_Direct,
        Move_Target,
        Stop_Move,
        Input_Skill,
        Act_Skill,
        Stop_Skill,
        Pause_State,
        Resume_State,
        Anim_Event,
        Catch,
        Stop_Catch,
        Hit,
        Fly,
    }

    #region 

    protected int _StatePrior;
    protected MotionManager _MotionManager;
    protected AnimationClip _Animation;

    protected virtual string GetAnimName()
    {
        return "";
    }

    public virtual void InitState(MotionManager motionManager)
    {
        _MotionManager = motionManager;

        string animName = GetAnimName();
        if (!string.IsNullOrEmpty(animName))
        {
            string animPath = "Animation/" + _MotionManager._MotionAnimPath + "/" + animName;
            _Animation = ResourceManager.Instance.GetAnimationClip(animPath);
        }

        if (_Animation != null)
        {
            _MotionManager.InitAnimation(_Animation);
        }
    }

    public virtual bool CanStartState(params object[] args)
    {
        return true;
    }

    public virtual void StartState(params object[] args)
    {
        if (_Animation != null)
        {
            _MotionManager.PlayAnimation(_Animation);
        }
    }

    public virtual void StateOpt(StateBase.MotionOpt opt, object[] args)
    {

    }

    public virtual void FinishState()
    {

    }

    public virtual void StateUpdate()
    { }

    #endregion

    protected Vector2 GetVector2(object arg)
    {
        if (arg is Vector2)
        {
            return (Vector2)arg;
        }
        return Vector2.zero;
    }

    protected Vector3 GetVector3(object arg)
    {
        if (arg is Vector3)
        {
            return (Vector3)arg;
        }
        return Vector3.zero;
    }

    protected int GetInt(object arg)
    {
        return (int)arg;
    }
}
