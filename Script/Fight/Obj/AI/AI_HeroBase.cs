﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_HeroBase : AI_Base
{
    protected override void Init()
    {
        base.Init();
        InitRise();

        InitPassiveSkills();
    }

    protected override void AIUpdate()
    {
        base.AIUpdate();

        RiseUpdate();
        //UpdateCriticalAI();
    }

    #region use skill

    protected float _AfterSkillWait = 0;
    protected float _AfterSkillTime = 0;

    protected override bool StartSkill()
    {
        //if (!IsRandomActSkill())
        //    return false;

        if (Time.time - _AfterSkillTime < _AfterSkillWait)
            return false;

        float dis = Vector3.Distance(_SelfMotion.transform.position, _TargetMotion.transform.position);

        for (int i = _AISkills.Count - 1; i >= 0; --i)
        {
            if (!_AISkills[i].IsSkillCD())
                continue;

            //if (!IsCommonCD())
            //    continue;

            if (_AISkills[i].SkillRange < dis)
                continue;

            StartSkill(_AISkills[i]);
            _AfterSkillWait = _AISkills[i].AfterSkillWait;
            return true;

        }

        return false;
    }

    public override void OnStateChange(StateBase orgState, StateBase newState)
    {
        base.OnStateChange(orgState, newState);

        //after skill
        if (orgState is StateSkill && newState is StateIdle)
        {
            _AfterSkillTime = Time.time;
        }
    }

    #endregion

    #region rise

    public bool _IsRiseBoom = false;
    private ImpactBase _RiseBoom;

    private float _RiseTime = 1f;
    private bool _IsRiseEvent = false;

    private void InitRise()
    {
        if (!_IsRiseBoom)
            return;

        var riseBoom = ResourceManager.Instance.GetInstanceGameObject("SkillMotion/CommonImpact/RiseBoomSkill");
        var motionTrans = transform.Find("Motion");
        riseBoom.transform.SetParent(motionTrans);
        riseBoom.transform.localPosition = Vector3.zero;
        riseBoom.transform.localRotation = Quaternion.Euler(Vector3.zero);

        _RiseBoom = riseBoom.GetComponent<ImpactBase>();
        
    }

    private void RiseUpdate()
    {
        if (!_IsRiseBoom)
            return;

        if (_SelfMotion._ActionState == _SelfMotion._StateRise)
        {
            if (!_IsRiseEvent)
            {
                _IsRiseEvent = true;
                _RiseBoom.ActImpact(_SelfMotion, _SelfMotion);
            }
        }
        else
        {
            if (_IsRiseEvent)
            {
                _IsRiseEvent = false;
            }
        }
    }

    #endregion

    #region super armor

    private ImpactBuff _SuperArmorPrefab;
    public ImpactBuff SuperArmorPrefab
    {
        get
        {
            if (_SuperArmorPrefab == null)
            {
                var buffGO = ResourceManager.Instance.GetGameObject("SkillMotion/CommonImpact/SuperArmor");
                _SuperArmorPrefab = buffGO.GetComponent<ImpactBuff>();
            }
            return _SuperArmorPrefab;
        }
    }

    private ImpactBuff _SuperArmorBlockPrefab;
    public ImpactBuff SuperArmorBlockPrefab
    {
        get
        {
            if (_SuperArmorBlockPrefab == null)
            {
                var buffGO = ResourceManager.Instance.GetGameObject("SkillMotion/CommonImpact/SuperArmorBlock");
                _SuperArmorBlockPrefab = buffGO.GetComponent<ImpactBuff>();
            }
            return _SuperArmorBlockPrefab;
        }
    }

    private ImpactBuffSuperArmor _BuffInstance;

    public bool _IsContainsNormalAtk = false;

    protected void InitSkills()
    {
        int startIdx = 0;
        if (!_IsContainsNormalAtk)
        {
            startIdx = 1;
        }
        for (int i = startIdx; i < _AISkills.Count; ++i)
        {
            InitSuperArmorSkill(_AISkills[i].SkillBase);
            InitReadySkillSpeed(_AISkills[i]);
        }
    }

    protected void InitSuperArmorSkill(ObjMotionSkillBase objMotionSkill)
    {
        if (objMotionSkill._NextAnim.Count == 0)
            return;

        float attackConlliderTime = _SelfMotion.AnimationEvent.GetAnimFirstColliderEventTime(objMotionSkill._NextAnim[0], objMotionSkill._SuperArmorColliderID);
        if (attackConlliderTime < 0)
            return;

        _SelfMotion.AnimationEvent.AddEvent(objMotionSkill._NextAnim[0], 0, AttackStart);
        _SelfMotion.AnimationEvent.AddEvent(objMotionSkill._NextAnim[0], attackConlliderTime + 0.05f, AttackCollider);
        _SelfMotion.AnimationEvent.AddEvent(objMotionSkill._NextAnim[0], objMotionSkill._NextAnim[0].length, AttackCollider);
    }

    private void AttackStart()
    {
        _BuffInstance = SuperArmorBlockPrefab.ActBuffInstance(_SelfMotion, _SelfMotion) as ImpactBuffSuperArmor;

    }

    private void AttackCollider()
    {
        if (_BuffInstance != null)
        {
            _SelfMotion.RemoveBuff(_BuffInstance);
        }
    }

    #endregion

    #region 

    #region ready skill speed

    private static float _ReadySkillSpeedTime = 0.8f;
    private float _CurAnimSpeed;
    private float _CurEffectSpeed;
    private Dictionary<ObjMotionSkillBase, float> _SkillColliderTime = new Dictionary<ObjMotionSkillBase, float>();

    public void InitReadySkillSpeed(AI_Skill_Info aiSkillInfo)
    {
        if (aiSkillInfo.ReadyTime == 0)
            return;

        if (_ReadySkillSpeedTime == 0)
            return;

        _SkillColliderTime.Add(aiSkillInfo.SkillBase, aiSkillInfo.ReadyTime);
        _SelfMotion.AnimationEvent.AddEvent(aiSkillInfo.SkillBase._NextAnim[0], 0, AttackStartForSpeed);
        _SelfMotion.AnimationEvent.AddEvent(aiSkillInfo.SkillBase._NextAnim[0], aiSkillInfo.ReadyTime - 0.05f, AttackColliderForSpeed);
        _SelfMotion.AnimationEvent.AddEvent(aiSkillInfo.SkillBase._NextAnim[0], aiSkillInfo.SkillBase._NextAnim[0].length, AttackColliderForSpeed);
    }

    private void AttackStartForSpeed()
    {
        _CurAnimSpeed = -1;
        var animState = _SelfMotion.GetCurAnim();
        if (animState == null)
            return;

        if (!_SkillColliderTime.ContainsKey(_SelfMotion._StateSkill.ActingSkill))
            return;

        var readyTime = _SkillColliderTime[_SelfMotion._StateSkill.ActingSkill];
        if (readyTime > _ReadySkillSpeedTime)
            return;

        var speed = readyTime * animState.speed / _ReadySkillSpeedTime;
        _CurAnimSpeed = animState.speed;
        animState.speed = speed;

        if (_SelfMotion.PlayingEffect != null)
        {
            var effectSpeed = readyTime * _SelfMotion.PlayingEffect.LastPlaySpeed / _ReadySkillSpeedTime;
            _CurEffectSpeed = _SelfMotion.PlayingEffect.LastPlaySpeed;
            _SelfMotion.PlayingEffect.SetEffectSpeed(effectSpeed);
        }
    }

    private void AttackColliderForSpeed()
    {
        if (_CurAnimSpeed < 0)
            return;

        var animState = _SelfMotion.GetCurAnim();
        if (animState == null)
            return;

        if (!_SkillColliderTime.ContainsKey(_SelfMotion._StateSkill.ActingSkill))
            return;

        animState.speed = _CurAnimSpeed;

        if (_SelfMotion.PlayingEffect != null)
        {
            _SelfMotion.PlayingEffect.SetEffectSpeed(_CurEffectSpeed);
        }
    }

    #endregion

    #endregion

    #region critical AI

    public float _CriticalSkillRate = 0;

    //if target start use skill, AI use skill
    protected void UpdateCriticalAI()
    {
        if (_SelfMotion._ActionState != _SelfMotion._StateIdle)
            return;

        if (_TargetMotion.ActingSkill == null)
            return;

        if (_CriticalSkillRate <= 0)
            return;

        var random = Random.Range(0, 1);
        if (_CriticalSkillRate < random)
            return;

        AI_Skill_Info aiSkill = _AISkills[0];
        for (int i = _AISkills.Count - 1; i >= 0; --i)
        {
            if (!_AISkills[i].IsSkillCD())
            {
                aiSkill = _AISkills[i];
                break;
            }
        }
        StartSkill(aiSkill);
    }

    #endregion

    #region Passive skills

    public Transform _PassiveGO;

    public void InitPassiveSkills()
    {
        if (_PassiveGO == null)
            return;

        List<ImpactBase> passiveImpacts = new List<ImpactBase>();
        for (int i = 0; i < _PassiveGO.childCount; ++i)
        {
            var passiveImpact = _PassiveGO.GetChild(i).GetComponents<ImpactBase>();
            passiveImpacts.AddRange(passiveImpact);
        }

        foreach (var buff in passiveImpacts)
        {
            buff.ActImpact(_SelfMotion, _SelfMotion);
        }
    }

    #endregion

}
