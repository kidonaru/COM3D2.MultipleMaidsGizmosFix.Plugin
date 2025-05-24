using System;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;
using COM3D2.MotionTimelineEditor;

namespace COM3D2.MultipleMaidsGizmosFix.Plugin
{
    [
        PluginFilter("COM3D2x64"),
        PluginName(PluginInfo.PluginFullName),
        PluginVersion(PluginInfo.PluginVersion)
    ]
    public class MultipleMaidsGizmosFix : PluginBase
    {
        private static MultipleMaidsWrapper multipleMaids => MultipleMaidsWrapper.instance;

        public MultipleMaidsGizmosFix()
        {
        }

        public void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
        }

        private float _prevFieldOfView = 0f;

        public void Update()
        {
            try
            {
                var mainCamera = GameMain.Instance.MainCamera.camera;
                if (mainCamera == null)
                {
                    return;
                }

                if (mainCamera.fieldOfView == _prevFieldOfView)
                {
                    return;
                }
                _prevFieldOfView = mainCamera.fieldOfView;

                var subCamera = multipleMaids.subcamera;
                if (subCamera == null)
                {
                    return;
                }

                subCamera.fieldOfView = mainCamera.fieldOfView;

                MTEUtils.LogDebug("subCamera.fieldOfView = " + subCamera.fieldOfView);
            }
            catch (Exception e)
            {
                MTEUtils.LogException(e);
            }
        }
    }
}