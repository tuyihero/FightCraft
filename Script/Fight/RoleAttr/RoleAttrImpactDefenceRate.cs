﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tables;
public class RoleAttrImpactDefenceRate : RoleAttrImpactBase
{

    public override void InitImpact(string skillInput, List<int> args)
    {
        _SkillInput = skillInput;
        _ValueModify = (float)args[0] * 0.0001f;
    }

    public override List<int> GetSkillImpactVal(SkillInfoItem skillInfo)
    {
        var valList = new List<int>();
        valList.Add(skillInfo.SkillActureLevel * skillInfo.SkillRecord.EffectValue[0]);

        return valList;
    }

    public override void ModifySkillAfterInit(MotionManager roleMotion)
    {
        if (!roleMotion._StateSkill._SkillMotions.ContainsKey(_SkillInput))
            return;

        var skillMotion = roleMotion._StateSkill._SkillMotions[_SkillInput];
        var defenceBuff = (skillMotion as ObjMotionSkillDefence)._BuffDefence;
        defenceBuff._DefenceRate -= (_ValueModify);
        if (defenceBuff._DefenceRate == 0)
        {
            defenceBuff._DefenceHitTime = 0;
        }
    }

    #region 

    public float _ValueModify;
    
    #endregion
}