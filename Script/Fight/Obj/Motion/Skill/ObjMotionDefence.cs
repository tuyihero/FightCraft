﻿using UnityEngine;
using System.Collections;

public class ObjMotionDefence : ObjMotionSkillBase
{
    

    #region unity



    #endregion

    public EffectController _DefendEffect;

    public void PlayMotionHit(object go, Hashtable eventArgs)
    {

        if ((EVENT_TYPE)eventArgs["EVENT_TYPE"] == EVENT_TYPE.EVENT_MOTION_HIT)
        {
            eventArgs.Add("StopEvent", true);
        }

        if (_DefendEffect != null)
        {
            _DefendEffect.PlayEffect();
        }
    }

}
