using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Globalization;
using Syncfusion.Blazor.Diagram;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DiagramBuilderMAUI
{
    public partial class DiagramMainContent
    {
        DiagramObjectCollection<NodeBase> flowNodes = new DiagramObjectCollection<NodeBase>();
        DiagramObjectCollection<NodeBase> flowConnectors = new DiagramObjectCollection<NodeBase>();

        int portCount = 0;
        int connectorCount = 0;
        private void CreateConnector(string sourceId, string targetId, string label = default(string), DiagramObjectCollection<ConnectorSegment> segment = null, bool isDashLine = false)
        {
            Connector diagramConnector = new Connector()
            {
                ID = string.Format("connector{0}", ++connectorCount),
                SourceID = sourceId,
                TargetID = targetId,

            };
            if (isDashLine)
            {
                diagramConnector.Style = new ShapeStyle() { StrokeDashArray = "2,2" };
            }
            if (label != default(string))
            {
                var annotation = new PathAnnotation()
                {
                    Content = label,

                    Style = new TextStyle() { Fill = "white" }
                };


                if (sourceId == "node5" && targetId == "node6")
                {
                    annotation.Height = 10;
                    annotation.Width = 15;
                }
                diagramConnector.Annotations = new DiagramObjectCollection<PathAnnotation>() { annotation };
            }


            if (segment != null)
            {
                diagramConnector.Type = ConnectorSegmentType.Orthogonal;
                diagramConnector.Segments = segment;
            }

            flowConnectors.Add(diagramConnector);
        }
        private void CreateNode(string id, double x, double y, NodeFlowShapes shape, string label)
        {
            DiagramObjectCollection<PointPort> defaultsPorts = new DiagramObjectCollection<PointPort>();
            defaultsPorts.Add(CreatePort(0, 0.5));
            defaultsPorts.Add(CreatePort(0.5, 1));
            defaultsPorts.Add(CreatePort(1, 0.5));
            defaultsPorts.Add(CreatePort(0.5, 0));
            Node diagramNode = new Node()
            {
                ID = id,
                OffsetX = x,
                OffsetY = y,
                Width = 145,
                Ports = defaultsPorts,
                Height = 60,
                Style = new ShapeStyle { Fill = "#357BD2", StrokeColor = "White" },

                Shape = new FlowShape() { Type = NodeShapes.Flow, Shape = shape },
                Annotations = new DiagramObjectCollection<ShapeAnnotation>
                {
                    new ShapeAnnotation
                    {
                        Content = label,
                        Style = new TextStyle()
                        {
                          Color="White",

                           Fill = "transparent"
                        }
                    }
                }
            };

            flowNodes.Add(diagramNode);
        }
        private PointPort CreatePort(double x, double y)
        {
            return new PointPort()
            {
                ID = string.Format("port{0}", ++portCount),
                Shape = Syncfusion.Blazor.Diagram.PortShapes.Circle,
                Offset = new DiagramPoint() { X = x, Y = y }
            };
        }
        private void InitDiagramModel()
        {
            CreateNode("node1", 300, 80, NodeFlowShapes.Terminator, "Place order");
            CreateNode("node2", 300, 160, NodeFlowShapes.Process, "Start transaction");
            CreateNode("node3", 300, 240, NodeFlowShapes.Process, "Verification");
            CreateNode("node4", 300, 330, NodeFlowShapes.Decision, "Credit card valid?");
            CreateNode("node5", 300, 430, NodeFlowShapes.Decision, "Funds available?");
            CreateNode("node6", 530, 330, NodeFlowShapes.Process, "Enter payment method");
            CreateNode("node7", 300, 530, NodeFlowShapes.Process, "Complete transaction");
            CreateNode("node8", 110, 530, NodeFlowShapes.Data, "Send e-mail");
            CreateNode("node9", 475, 530, NodeFlowShapes.DirectData, "Customer \n database");
            CreateNode("node10", 300, 630, NodeFlowShapes.Terminator, "Log transaction");
            CreateNode("node11", 480, 630, NodeFlowShapes.Process, "Reconcile the entries");
            DiagramObjectCollection<ConnectorSegment> segment1 = new DiagramObjectCollection<ConnectorSegment>()
            {
                new OrthogonalSegment
                {
                    Type = ConnectorSegmentType.Orthogonal,
                    Direction = Direction.Top,
                    Length=120,
                },
            };
            DiagramObjectCollection<ConnectorSegment> segment2 = new DiagramObjectCollection<ConnectorSegment>()
            {
                 new OrthogonalSegment
                {
                    Type = ConnectorSegmentType.Orthogonal,
                    Length=100,
                    Direction = Direction.Right,

                },
            };
            CreateConnector("node1", "node2");
            CreateConnector("node2", "node3");
            CreateConnector("node3", "node4");
            CreateConnector("node4", "node5");
            CreateConnector("node4", "node6", "No");
            CreateConnector("node5", "node6", "No", segment2);
            CreateConnector("node5", "node7", "Yes");
            CreateConnector("node6", "node2", default(string), segment1);
            CreateConnector("node7", "node8");
            CreateConnector("node7", "node9");
            CreateConnector("node7", "node10");
            CreateConnector("node10", "node11", default(string), null, true);
        }
    }
}
