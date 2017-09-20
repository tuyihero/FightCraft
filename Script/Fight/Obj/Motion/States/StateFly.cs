﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFly : StateBase
{

    protected override string GetAnimName()
    {
        return "Act_Fly_01";
    }

    public override void InitState(MotionManager motionManager)
    {
        base.InitState(motionManager);

        _MotionManager.AddAnimationEndEvent(_Animation);
        _FlyBody = _MotionManager.AnimationEvent.gameObject;
    }

    public override bool CanStartState(params object[] args)
    {
        return IsBuffCanBeHit((MotionManager)args[2], (ImpactHit)args[3]);
    }

    public override void StartState(params object[] args)
    {
        //base.StartState(args);
        MotionFly((float)args[0], (int)args[1], (MotionManager)args[2]);
        SetHitMove((Vector3)args[4], (float)args[5]);
    }

    public override void StateOpt(MotionOpt opt, params object[] args)
    {
        switch (opt)
        {
            case MotionOpt.Pause_State:
                _MotionManager.PauseAnimation(_Animation, (float)args[0]);
                break;
            case MotionOpt.Act_Skill:
                _MotionManager.TryEnterState(_MotionManager._StateSkill, args);
                break;
            case MotionOpt.Input_Skill:
                _MotionManager.TryEnterState(_MotionManager._StateSkill, args);
                break;
            case MotionOpt.Hit:
                //_MotionManager.TryEnterState(_MotionManager._StateHit, args);
                MotionFlyStay((float)args[0], (int)args[1], (MotionManager)args[2]);
                SetHitMove((Vector3)args[4], (float)args[5]);
                break;
            case MotionOpt.Fly:
                MotionFly((float)args[0], (int)args[1], (MotionManager)args[2]);
                SetHitMove((Vector3)args[4], (float)args[5]);
                break;
            case MotionOpt.Catch:
                _MotionManager.TryEnterState(_MotionManager._StateCatch, args);
                break;
            case MotionOpt.Anim_Event:
                DispatchFlyEvent(args[0] as string, args[1]);
                break;
            default:
                break;
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        FlyUpdate();
    }

    #region 

    Hashtable _BuffArg = new Hashtable();
    public bool IsBuffCanBeHit(MotionManager impactSender, ImpactHit impactHit)
    {
        _BuffArg.Clear();
        _BuffArg.Add(ImpactBuff.BuffModifyType.IsCanHit, true);
        _MotionManager.ForeachBuffModify(ImpactBuff.BuffModifyType.IsCanHit, _BuffArg, impactSender, impactHit);
        return (bool)_BuffArg[ImpactBuff.BuffModifyType.IsCanHit];
    }

    private const float _UpSpeed = 15;
    private const float _DownSpeed = 10;

    private GameObject _FlyBody;
    private float _FlyHeight = 0;
    private float _StayTime = 0;

    private bool IsFlyEnd = false;

    private void DispatchFlyEvent(string funcName, object param)
    {
        switch (funcName)
        {
            case AnimEventManager.ANIMATION_END:
                FlyEnd();
                break;
        }
    }

    public void SetHitMove(Vector3 moveDirect, float moveTime)
    {
        if (moveTime <= 0)
            return;

        _MotionManager.SetMove(moveDirect, moveTime);
    }

    private void FlyEnd()
    {
        IsFlyEnd = true;
    }

    public void MotionFly(float flyHeight, int effectID, MotionManager impactSender)
    {
        Debug.Log("MotionFly");
        _MotionManager.PlayHitEffect(impactSender, effectID);
        _MotionManager.SetLookAt(impactSender.transform.position);
        _FlyHeight = flyHeight;

        IsFlyEnd = false;
        _MotionManager.SetCorpsePrior();

        _MotionManager.RePlayAnimation(_Animation, 1);
    }

    public void MotionFlyStay(float time, int effectID, MotionManager impactSender)
    {
        Debug.Log("MotionFlyStay");
        _MotionManager.PlayHitEffect(impactSender, effectID);

        _MotionManager.RePlayAnimation(_Animation, 1);

        _StayTime = 0.15f;
    }

    public void FlyUpdate()
    {
        if (_StayTime > 0)
        {
            _StayTime -= Time.fixedDeltaTime;
        }
        else if (_FlyHeight > 0)
        {
            _FlyBody.transform.localPosition += _UpSpeed * Time.fixedDeltaTime * Vector3.up;

            if (_FlyBody.transform.localPosition.y > _FlyHeight)
            {
                _FlyBody.transform.localPosition = new Vector3(0, _FlyHeight, 0);
                _FlyHeight = 0;
            }
        }
        else if (_FlyBody.transform.localPosition.y > 0)
        {
            _FlyBody.transform.localPosition -= _DownSpeed * Time.fixedDeltaTime * Vector3.up;
            if (_FlyBody.transform.localPosition.y < 0)
            {
                _FlyBody.transform.localPosition = Vector3.zero;
            }

        }
        else if (IsFlyEnd)
        {
            if (_MotionManager.IsMotionDie)
            {
                _MotionManager.TryEnterState(_MotionManager._StateDie);
            }
            else
            {
                _MotionManager.TryEnterState(_MotionManager._StateLie);
            }
        }
    }

    #endregion
}