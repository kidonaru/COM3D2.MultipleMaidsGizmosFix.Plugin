using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using COM3D2.MotionTimelineEditor;
using UnityEngine;

namespace COM3D2.MultipleMaidsGizmosFix.Plugin
{
    public class MultipleMaidsWrapper
    {
        private MultipleMaidsField field = new MultipleMaidsField();
        private object multipleMaids = null;

        public Camera subcamera
        {
            get => (Camera)field.subcamera.GetValue(multipleMaids);
        }

        private static MultipleMaidsWrapper _instance = null;
        public static MultipleMaidsWrapper instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MultipleMaidsWrapper();
                    _instance.Init();
                }
                return _instance;
            }
        }

        public bool initialized { get; private set; } = false;

        public bool Init()
        {
            var assemblyPath = Path.GetFullPath(MTEUtils.CombinePaths(
                "Sybaris", "UnityInjector", "COM3D2.MultipleMaids.Plugin.dll"));
            if (!File.Exists(assemblyPath))
            {
                MTEUtils.LogWarning("COM3D2.MultipleMaids.Plugin.dll" + " not found");
                return false;
            }

            var assembly = Assembly.LoadFile(assemblyPath);

            if (!field.Init(assembly))
            {
                return false;
            }

            {
                GameObject gameObject = GameObject.Find("UnityInjector");
                multipleMaids = gameObject.GetComponent(field.MultipleMaidsType);
                MTEUtils.AssertNull(multipleMaids != null, "multipleMaids is null");
            }

            initialized = true;

            return true;
        }

        public bool IsValid()
        {
            return initialized;
        }
    }
}