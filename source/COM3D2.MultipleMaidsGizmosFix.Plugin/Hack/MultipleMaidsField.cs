using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using COM3D2.MotionTimelineEditor;

namespace COM3D2.MultipleMaidsGizmosFix.Plugin
{
    public class MultipleMaidsField : CustomFieldBase
    {
        public Type MultipleMaidsType;
        public FieldInfo subcamera;

        public override Dictionary<string, string> typeNames { get; } = new Dictionary<string, string>
        {
            { "MultipleMaidsType", "CM3D2.MultipleMaids.Plugin.MultipleMaids" },
        };

        public override bool PrepareLoadFields()
        {
            defaultParentType = MultipleMaidsType;
            return base.PrepareLoadFields();
        }
    }
}