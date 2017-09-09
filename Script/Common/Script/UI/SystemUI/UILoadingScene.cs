﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;

public class UILoadingScene : UIBase
{
    #region static funs

    public static void ShowAsyn(string sceneName)
    {
        Hashtable hash = new Hashtable();
        hash.Add("SceneName", sceneName);
        GameCore.Instance.UIManager.ShowUI("SystemUI/UILoadingScene", UILayer.TopUI, hash);
    }

    #endregion

    #region 

    public RawImage _BG;
    public Slider _LoadProcess;

    private AsyncOperation _LoadSceneOperation;
    private string _LoadSceneName;

    private bool _IsFinishLoading;

    private const float MAX_PROCESS_TIME = 5.0f;
    private float _ProcessStartTime;
    #endregion

    #region 

    public override void Show(Hashtable hash)
    {
        base.Show(hash);

        _LoadSceneName = (string)hash["SceneName"];

        _LoadSceneOperation = SceneManager.LoadSceneAsync(_LoadSceneName);

        ShowBG();
        _IsFinishLoading = false;
        _ProcessStartTime = Time.time;
    }

    public void FixedUpdate()
    {
        if (_IsFinishLoading)
            return;

        transform.SetSiblingIndex(10000);

        float processValue = (Time.time - _ProcessStartTime) / MAX_PROCESS_TIME;
        if (processValue < 0.85)
        {
            processValue = Mathf.Max(_LoadSceneOperation.progress, processValue);
        }
        else
        {
            processValue = _LoadSceneOperation.progress;
        }
        _LoadProcess.value = processValue;
        if (_LoadSceneOperation == null || _LoadSceneOperation.isDone)
        {
            _IsFinishLoading = true;
            if (_LoadSceneName == GameDefine.GAMELOGIC_SCENE_NAME)
            {
                LogicManager.Instance.StartLogic();
                //base.Destory(0.2f);
            }
            else
            {
                LogicManager.Instance.EnterFightFinish();
            }
            base.Destory();
        }
    }


    #endregion

    #region 

    public void ShowBG()
    {
        if (_LoadSceneName == GameDefine.GAMELOGIC_SCENE_NAME)
        {
            _BG.texture = ResourceManager.Instance.GetTexture("Loading");
        }
        else
        {
            _BG.texture = ResourceManager.Instance.GetTexture("LoadFight");
        }
    }

    #endregion
}

