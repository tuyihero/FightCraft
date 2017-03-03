﻿using UnityEngine;
using System.Collections;

public class ObjMotionSkillAttack : ObjMotionSkillBase
{
    void Update()
    {
        if (_CanNextInput)
        {
            if (InputManager.Instance.IsKeyDown(_ActInput))
            {
                ContinueAttack();
            }
        }
    }

    #region override

    public override void Init()
    {
        base.Init();

        foreach (var anim in _NextAnim)
        {
            if (anim != null)
            {
                _MotionManager.InitAnimation(anim);
                _MotionManager.AddAnimationEndEvent(anim);
            }
        }
    }
    
    public override void AnimEvent(string function, object param)
    {
        base.AnimEvent(function, param);

        switch (function)
        {
            case AnimationEvent.NEXT_INPUT_START:
                _CanNextInput = true;
                NextInputPress();
                break;
            case AnimationEvent.NEXT_INPUT_END:
                _CanNextInput = false;
                break;
        }
    }

    public override void FinishSkillImmediately()
    {
        base.FinishSkillImmediately();

        _CurStep = -1;
        foreach (var effect in _NextEffect)
        {
            if (effect != null)
            {
                _MotionManager.StopSkillEffect(effect);
            }
        }

        if (InputManager.Instance.IsKeyHold(_ActInput))
        {
            ActSkill();
        }
        Debug.Log("FinishSkillImmediately");
    }

    #endregion

    public AnimationClip[] _NextAnim;
    public EffectController[] _NextEffect;
    public SelectBase[] _Collider;

    private bool _CanNextInput = false;
    public bool CanNextInput
    {
        get
        {
            return _CanNextInput;
        }
    }

    private int _CurStep;
    public int CurStep
    {
        get
        {
            return _CurStep;
        }
    }

    public override bool ActSkill()
    {
        bool isActSkill = base.ActSkill();
        if (!isActSkill)
            return false;

        _CanNextInput = false;
        _CurStep = -1;
        ContinueAttack();

        return true;
    }

    public void NextInputPress()
    {
        if (InputManager.Instance.IsKeyHold(_ActInput))
        {
            ContinueAttack();
        }

    }

    public void ContinueAttack()
    {
        if (_CurStep + 1 < _NextAnim.Length)
        {
            ++_CurStep;
            if (InputManager.Instance._Axis != Vector2.zero)
            {
                _MotionManager.SetRotate(new Vector3(InputManager.Instance._Axis.x, 0, InputManager.Instance._Axis.y));
            }
            _MotionManager.PlayAnimation(_NextAnim[_CurStep]);
            _SkillLastTime = _NextAnim[_CurStep].length;
            if (_NextEffect.Length > _CurStep && _NextEffect[_CurStep] != null)
            {
                _MotionManager.PlaySkillEffect(_NextEffect[_CurStep]);
            }

            if (_CurStep - 1 >= 0 && _NextEffect[_CurStep - 1] != null)
            {
                _MotionManager.StopSkillEffect(_NextEffect[_CurStep - 1]);
            }

            _CanNextInput = false;
        }
    }



}