﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathManagerExtended.Util;

namespace PathManagerExtended.UI.Editors
{
    public class TemplateEditor : BaseEditor<LaneItem, LaneData, LaneIcons>
    {
        public override string Name => "Template Editor";
        public override string SelectionMessage => "No Templates";

        public override void Render(RenderManager.CameraInfo cameraInfo)
        {

        }
    }
}
