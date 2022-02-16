using Syncfusion.Blazor.Diagrams;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using Syncfusion.Blazor.Diagrams.Wasm;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace WebApplicationDiagramBuilder
{
    public partial class DiagramOpenDialog
    {
        DiagramObjectCollection<Node> flowNodes = new DiagramObjectCollection<Node>();
        DiagramObjectCollection<Connector> flowConnectors = new DiagramObjectCollection<Connector>();
        
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

                    Style = new TextShapeStyle() { Fill = "white" }
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
                diagramConnector.Type = Segments.Orthogonal;
                diagramConnector.Segments = segment;
            }

            flowConnectors.Add(diagramConnector);
        }
        private void CreateNode(string id, double x, double y, FlowShapes shape, string label)
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

                Shape = new FlowShape() { Type = Shapes.Flow, Shape = shape },
                Annotations = new DiagramObjectCollection<ShapeAnnotation>
            {
                    new ShapeAnnotation
                    {
                        Content = label,
                        Style = new TextShapeStyle()
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
                Shape = PortShapes.Circle,
                Offset = new Point() { X = x, Y = y }
            };
        }
        private void InitDiagramModel()
        {
            CreateNode("node1", 300, 80, FlowShapes.Terminator, "Place order");
            CreateNode("node2", 300, 160, FlowShapes.Process, "Start transaction");
            CreateNode("node3", 300, 240, FlowShapes.Process, "Verification");
            CreateNode("node4", 300, 330, FlowShapes.Decision, "Credit card valid?");
            CreateNode("node5", 300, 430, FlowShapes.Decision, "Funds available?");
            CreateNode("node6", 530, 330, FlowShapes.Process, "Enter payment method");
            CreateNode("node7", 300, 530, FlowShapes.Process, "Complete transaction");
            CreateNode("node8", 110, 530, FlowShapes.Data, "Send e-mail");
            CreateNode("node9", 475, 530, FlowShapes.DirectData, "Customer \n database");
            CreateNode("node10", 300, 630, FlowShapes.Terminator, "Log transaction");
            CreateNode("node11", 480, 630, FlowShapes.Process, "Reconcile the entries");
            DiagramObjectCollection<ConnectorSegment> segment1 = new DiagramObjectCollection<ConnectorSegment>()
        {
            new OrthogonalSegment
            {
                Type=Segments.Orthogonal,
                Direction = Direction.Top,
                Length=120,
            },
        };
            DiagramObjectCollection<ConnectorSegment> segment2 = new DiagramObjectCollection<ConnectorSegment>()
        {
             new OrthogonalSegment
            {
                Type=Segments.Orthogonal,
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
            //CreateConnector("node10", "node11", default(string));
        }

    }
}
