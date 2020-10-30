using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathManagerExtended.Util;
using UnityEngine;

namespace PathManagerExtended.Tool
{
    public class SelectLaneTool : BaseTool
    {
        public override ToolType Type => ToolType.SelectLane;
        public int HoveredLaneIndex { get; private set; } = 0;

        protected override void Reset()
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Tool.SegmentInstance.Segment.GetClosestLanePosition(PathManagerExtendedTool.MouseWorldPosition, NetInfo.LaneType.All, VehicleInfo.VehicleType.All, out _, out uint laneID, out _, out _))
            {
                for (int i = 0; i < Tool.SegmentInstance.Lanes.Count; i++)
                {
                    if (Tool.SegmentInstance.Lanes[i].LaneID == laneID)
                    {
                        HoveredLaneIndex = i;
                        break;
                    }
                }
            }
        }

        public override void RenderOverlay(RenderManager.CameraInfo cameraInfo)
        {
            RenderUtil.RenderLaneOverlay(cameraInfo, Tool.SegmentInstance.Lanes[HoveredLaneIndex], Color.white, true);
        }

        public override void OnSecondaryMouseClicked() => Tool.SetDefaultMode();

        public override void OnPrimaryMouseClicked(Event e)
        {

        }
    }
}
