﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAttrImpactPassiveBlockOne : RoleAttrImpactPassive
{

    public override void ModifySkillAfterInit(MotionManager roleMotion)
    {
        if (!roleMotion._StateSkill._SkillMotions.ContainsKey(_SkillInput))
            return;

        var buffGO = ResourceManager.Instance.GetInstanceGameObject("Bullet\\Passive\\" + _ImpactName);
        var buffs = buffGO.GetComponents<ImpactBuff>();
        foreach (var buff in buffs)
        {
            buff.ActImpact(roleMotion, roleMotion);
        }
    }


    #region 

    
    #endregion
}