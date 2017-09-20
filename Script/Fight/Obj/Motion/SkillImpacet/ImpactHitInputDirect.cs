﻿using UnityEngine;
using System.Collections;

public class ImpactHitInputDirect : ImpactHit
{
    public float _Time = 0.6f;
    public float _Speed = 10.0f;

    public override void ActImpact(MotionManager senderManager, MotionManager reciverManager)
    {

        Vector3 moveDirect = new Vector3(InputManager.Instance.CameraAxis.x, 0, InputManager.Instance.CameraAxis.y);
        if (moveDirect == Vector3.zero)
        {
            moveDirect = senderManager.transform.forward.normalized;
        }

        Vector3 destMove = moveDirect * _Speed * _Time;

        HitMotion(senderManager, reciverManager, destMove, _Time);

        ProcessDamge(senderManager, reciverManager);

    }

}