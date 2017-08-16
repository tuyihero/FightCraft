﻿using UnityEngine;
using System.Collections;

public class ImpactFlyBack : ImpactFly
{
    public float _Time = 0.6f;
    public float _Speed = 10.0f;

    public override void ActImpact(MotionManager senderManager, MotionManager reciverManager)
    {
        base.ActImpact(senderManager, reciverManager);

        if (reciverManager.BaseMotionManager.IsCanBePush() && _Time > 0)
        {
            Vector3 destMove = -senderManager.transform.forward.normalized * _Speed * _Time;
            reciverManager.SetMove(destMove, _Time / senderManager.RoleAttrManager.AttackSpeed);
        }
    }

}