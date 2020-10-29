using PathManagerExtended.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static ToolBase;

namespace PathManagerExtended.Tool
{
    public class SelectInstanceTool : BaseTool
    {
        public override ToolType Type => ToolType.SelectInstance;
        public override bool ShowPanel => false;

        public ushort HoveredNodeId { get; private set; } = 0;
        public ushort HoveredSegmentId { get; private set; } = 0;
        protected bool IsHover => (HoveredSegmentId != 0 || HoveredNodeId != 0);

        protected bool HoverValid => PathManagerExtendedTool.MouseRayValid && IsHover;


        protected override void Reset()
        {
            HoveredNodeId = 0;
            HoveredSegmentId = 0;
        }

        public override void OnUpdate()
        {
            RaycastInput nodeInput = new RaycastInput(PathManagerExtendedTool.MouseRay, PathManagerExtendedTool.MouseRayLength)
            {
                m_ignoreTerrain = true,
                m_ignoreNodeFlags = NetNode.Flags.None,
                m_ignoreSegmentFlags = NetSegment.Flags.All
            };
            nodeInput.m_netService.m_itemLayers = (ItemClass.Layer.Default | ItemClass.Layer.MetroTunnels);
            nodeInput.m_netService.m_service = ItemClass.Service.Road;

            if (PathManagerExtendedTool.RayCast(nodeInput, out RaycastOutput nodeOutput))
            {
                HoveredNodeId = nodeOutput.m_netNode;
                HoveredSegmentId = 0;
                return;
            }

            HoveredNodeId = 0;

            RaycastInput segmentInput = new RaycastInput(PathManagerExtendedTool.MouseRay, PathManagerExtendedTool.MouseRayLength)
            {
                m_ignoreTerrain = true,
                m_ignoreSegmentFlags = NetSegment.Flags.None,
                m_ignoreNodeFlags = NetNode.Flags.All
            };
            segmentInput.m_netService.m_itemLayers = (ItemClass.Layer.Default | ItemClass.Layer.MetroTunnels);
            segmentInput.m_netService.m_service = ItemClass.Service.Road;

            if (PathManagerExtendedTool.RayCast(segmentInput, out RaycastOutput segmentOutput))
            {
                HoveredSegmentId = segmentOutput.m_netSegment;
                return;
            }

            HoveredSegmentId = 0;
        }

        public override void OnMouseUp(Event e) => OnPrimaryMouseClicked(e);
        public override void OnPrimaryMouseClicked(Event e)
        {
            if (HoverValid)
            {
                if (HoveredNodeId != 0)
                {
                    //Tool.SetNode(HoveredNodeId);
                    //return;
                }
                if (HoveredSegmentId != 0)
                {
                    Tool.SetSegment(HoveredSegmentId);
                    Tool.SetMode(ToolType.SelectLane);
                    return;
                }
            }
        }
        public override void OnSecondaryMouseClicked() => Tool.DisableTool();

        public override void RenderOverlay(RenderManager.CameraInfo cameraInfo)
        {
            if (HoverValid)
            {
                if (HoveredSegmentId != 0)
                {
                    RenderUtil.RenderAutoCutSegmentOverlay(cameraInfo, HoveredSegmentId, Color.white, true);
                }
                if (HoveredNodeId != 0)
                {
                    RenderUtil.DrawNodeCircle(cameraInfo, Color.white, HoveredNodeId, true);
                }
            }
        }
    }
}
