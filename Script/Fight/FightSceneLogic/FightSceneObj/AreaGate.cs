﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGate : MonoBehaviour
{

    private void Start()
    {
        StartAtNavMesh();
    }

    // Update is called once per frame
    void Update ()
    {
        TeleportUpdate();
    }

    #region teleport

    public Transform _DestPos;
    public bool _IsTransScene = true;
    public static float _TeleDistance = 3;
    public static float _TeleProcessTime = 1;

    protected bool _Teleporting = false;
    protected float _StartingTime = 0;

    protected virtual void TeleportUpdate()
    {
        if (FightManager.Instance == null)
            return;

        var mainChar = FightManager.Instance.MainChatMotion;
        if (mainChar == null)
            return;

        if (mainChar._ActionState == mainChar._StateIdle &&
            Vector3.Distance(transform.position, mainChar.transform.position) < _TeleDistance)
        {
            if (!_Teleporting)
            {
                _Teleporting = true;
                _StartingTime = Time.time;

            }
            UpdateTeleProcesing();
        }
        else
        {
            if (_Teleporting)
            {
                UpdateTeleProcesing();
                _Teleporting = false;
                _StartingTime = 0;

            }
        }

    }

    protected virtual void UpdateTeleProcesing()
    {
        if (_Teleporting)
        {
            var timeDelta = Time.time - _StartingTime;
            FightManager.Instance.MainChatMotion.SkillProcessing = timeDelta / _TeleProcessTime;
            if (FightManager.Instance.MainChatMotion.SkillProcessing >= 1)
            {
                TeleportAct();
                FightManager.Instance.MainChatMotion.SkillProcessing = 0;
                _Teleporting = false;
            }
        }
        else
        {
            FightManager.Instance.MainChatMotion.SkillProcessing = 0;
        }
    }

    protected virtual void TeleportAct()
    {
        if (_DestPos == null)
            return;

        FightManager.Instance.TeleportToNextRegion(_DestPos, _IsTransScene);
    }

    private void StartAtNavMesh()
    {
        UnityEngine.AI.NavMeshHit navMeshHit = new UnityEngine.AI.NavMeshHit();
        if (UnityEngine.AI.NavMesh.SamplePosition(transform.position, out navMeshHit, 10, UnityEngine.AI.NavMesh.AllAreas))
            transform.position = navMeshHit.position;
    }

    #endregion
}
