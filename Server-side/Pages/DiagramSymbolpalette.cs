using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Diagram;
using Syncfusion.Blazor.Diagram.SymbolPalette;

namespace DiagramBuilder
{
    public partial class DiagramSymbolpalette
    {
        private void InitializePalettes()
        {
            FlowShapeList = new DiagramObjectCollection<NodeBase>();
            CreatePaletteNode(NodeFlowShapes.Terminator, "Terminator");
            CreatePaletteNode(NodeFlowShapes.Process, "Process");
            CreatePaletteNode(NodeFlowShapes.Decision, "Decision");
            CreatePaletteNode(NodeFlowShapes.Document, "Document");
            CreatePaletteNode(NodeFlowShapes.PreDefinedProcess, "PredefinedProcess");
            CreatePaletteNode(NodeFlowShapes.PaperTap, "PunchedTape");
            CreatePaletteNode(NodeFlowShapes.DirectData, "DirectData");
            CreatePaletteNode(NodeFlowShapes.SequentialData, "SequentialData");
            CreatePaletteNode(NodeFlowShapes.Sort, "Sort");
            CreatePaletteNode(NodeFlowShapes.MultiDocument, "Multidocument");
            CreatePaletteNode(NodeFlowShapes.Collate, "Collate");
            CreatePaletteNode(NodeFlowShapes.SummingJunction, "SummingJunction");
            CreatePaletteNode(NodeFlowShapes.Or, "Or");
            CreatePaletteNode(NodeFlowShapes.InternalStorage, "InternalStorage");
            CreatePaletteNode(NodeFlowShapes.Extract, "Extract");
            FlowShapePalette = new Palette() { Symbols = FlowShapeList, Title = "Flow Shapes", ID = "Flow Shapes", IconCss = "e-ddb-icons e-flow" };

            BasicShapeList = new DiagramObjectCollection<NodeBase>();
            CreateBasicPaletteNode(NodeBasicShapes.Rectangle, "Rectangle");
            CreateBasicPaletteNode(NodeBasicShapes.Ellipse, "Ellipse");
            CreateBasicPaletteNode(NodeBasicShapes.Hexagon, "Hexagon");
            CreateBasicPaletteNode(NodeBasicShapes.Parallelogram, "Parallelogram");
            CreateBasicPaletteNode(NodeBasicShapes.Triangle, "Triangle");
            CreateBasicPaletteNode(NodeBasicShapes.Plus, "Plus");
            CreateBasicPaletteNode(NodeBasicShapes.Star, "Star");
            CreateBasicPaletteNode(NodeBasicShapes.Pentagon, "Pentagon");
            CreateBasicPaletteNode(NodeBasicShapes.Heptagon, "Heptagon");
            CreateBasicPaletteNode(NodeBasicShapes.Octagon, "Octagon");
            CreateBasicPaletteNode(NodeBasicShapes.Trapezoid, "Trapezoid");
            CreateBasicPaletteNode(NodeBasicShapes.Decagon, "Decagon");
            CreateBasicPaletteNode(NodeBasicShapes.RightTriangle, "RightTriangle");
            CreateBasicPaletteNode(NodeBasicShapes.Cylinder, "Cylinder");
            CreateBasicPaletteNode(NodeBasicShapes.Diamond, "Diamond");
            BasicShapePalette = new Palette() { Symbols = BasicShapeList, Title = "Basic Shapes", ID = "Basic Shapes", IconCss = "e-ddb-icons e-flow" };

            ConnectorList = new DiagramObjectCollection<NodeBase>();
            CreatePaletteConnector("Link1", ConnectorSegmentType.Orthogonal, DecoratorShape.Arrow);
            CreatePaletteConnector("Link2", ConnectorSegmentType.Orthogonal, DecoratorShape.None);
            CreatePaletteConnector("Link3", ConnectorSegmentType.Straight, DecoratorShape.Arrow);
            CreatePaletteConnector("Link4", ConnectorSegmentType.Straight, DecoratorShape.None);
            CreatePaletteConnector("Link5", ConnectorSegmentType.Bezier, DecoratorShape.Arrow);
            ConnectorPalette = new Palette() { Symbols = ConnectorList, Title = "Connectors", ID = "Connector Shapes", IconCss = "e-ddb-icons e-connector" };
        }
        private void InitializeHTMLPalettes()
        {
            HTMLShapeList = new DiagramObjectCollection<NodeBase>();
            CreateHTMLShape(NodeShapes.HTML, "node1checknode");
            CreateHTMLShape(NodeShapes.HTML, "node2checknode");
            HtmlShapePalette = new Palette() { Symbols = HTMLShapeList, Title = "HTML Shapes", ID = "Html Shapes", IsExpanded = false, IconCss = "e-ddb-icons e-flow" };
        }
        private void InitializeNativePalettes()
        {
            NativeShapeList = new DiagramObjectCollection<NodeBase>();
            CreateNativeShape(NodeShapes.SVG, "Nativenode1");
            CreateNativeShape(NodeShapes.SVG, "Nativenode2");
            NativeShapePalette = new Palette() { Symbols = NativeShapeList, Title = "Native Shapes", ID = "Native Shapes", IsExpanded = false, IconCss = "e-ddb-icons e-flow" };
        }

        private void CreateHTMLShape(NodeShapes HTML, string id)
        {
            Node diagramNode = new Node()
            {
                ID = id,
                Shape = new Shape() { Type = NodeShapes.HTML },
            };
            HTMLShapeList.Add(diagramNode);
        }

        private void CreateNativeShape(NodeShapes Native, string id)
        {
            Node diagramNode = new Node()
            {
                ID = id,
                Shape = new Shape() { Type = NodeShapes.SVG },
            };
            NativeShapeList.Add(diagramNode);
        }
        private void CreatePaletteConnector(string id, ConnectorSegmentType type, DecoratorShape decoratorShape)
        {
            Connector diagramConnector = new Connector()
            {
                ID = id,
                Type = type,
                SourcePoint = new Syncfusion.Blazor.Diagram.DiagramPoint() { X = 0, Y = 0 },
                TargetPoint = new Syncfusion.Blazor.Diagram.DiagramPoint() { X = 60, Y = 60 },
                Style = new ShapeStyle() { StrokeWidth = 2 },
                TargetDecorator = new DecoratorSettings() { Shape = decoratorShape }
            };

            ConnectorList.Add(diagramConnector);
        }
        private void CreatePaletteNode(NodeFlowShapes flowShape, string id)
        {
            Node diagramNode = new Node()
            {
                ID = id,
                Shape = new FlowShape() { Type = NodeShapes.Flow, Shape = flowShape },
                Style = new ShapeStyle() { StrokeWidth = 2 }
            };
            FlowShapeList.Add(diagramNode);
        }
        private void CreateBasicPaletteNode(NodeBasicShapes basicShape, string id)
        {
            Node diagramNode = new Node()
            {
                ID = id,
                Shape = new BasicShape() { Type = NodeShapes.Basic, Shape = basicShape },
                Style = new ShapeStyle() { StrokeWidth = 2 }
            };
            BasicShapeList.Add(diagramNode);
        }
        public void InitializeNetworkShapes()
        {

            NetworkShapesList = new DiagramObjectCollection<NodeBase>();

            Node NetworkShapes0 = new Node()
            {
                ID = "uKVMSwitch1",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,32.8157654 C1.49248678,32.8157654 1,32.3232787 1,31.7157654 L1,16.1 C1,15.4924868 1.49248678,15 2.1,15 L47.9,15 C48.5075132,15 49,15.4924868 49,16.1 L49,31.7157654 C49,32.3232787 48.5075132,32.8157654 47.9,32.8157654 L2.1,32.8157654 Z"
                },
                Width = 48,
                Height = 17.815765380859375,
                OffsetX = 25,
                OffsetY = 23.907882690429688,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes0);
            Node NetworkShapes1 = new Node()
            {
                ID = "uKVMSwitch2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M47,30.8157654 L3,30.8157654 L3,17 L47,17 L47,30.8157654 Z M9.1875,26.0625 C9.1875,25.269 8.5435,24.625 7.75,24.625 C6.95554167,24.625 6.3125,25.269 6.3125,26.0625 C6.3125,26.856 6.95554167,27.5 7.75,27.5 C8.5435,27.5 9.1875,26.856 9.1875,26.0625 Z M19.25,26.0625 C19.25,25.269 18.606,24.625 17.8125,24.625 C17.0180417,24.625 16.375,25.269 16.375,26.0625 C16.375,26.856 17.0180417,27.5 17.8125,27.5 C18.606,27.5 19.25,26.856 19.25,26.0625 Z M13.5,26.0625 C13.5,25.269 12.856,24.625 12.0625,24.625 C11.2680417,24.625 10.625,25.269 10.625,26.0625 C10.625,26.856 11.2680417,27.5 12.0625,27.5 C12.856,27.5 13.5,26.856 13.5,26.0625 Z M23.5625,26.0625 C23.5625,25.269 22.9185,24.625 22.125,24.625 C21.3305417,24.625 20.6875,25.269 20.6875,26.0625 C20.6875,26.856 21.3305417,27.5 22.125,27.5 C22.9185,27.5 23.5625,26.856 23.5625,26.0625 Z M29.3125,26.0625 C29.3125,25.269 28.6685,24.625 27.875,24.625 C27.0805417,24.625 26.4375,25.269 26.4375,26.0625 C26.4375,26.856 27.0805417,27.5 27.875,27.5 C28.6685,27.5 29.3125,26.856 29.3125,26.0625 Z M33.625,26.0625 C33.625,25.269 32.981,24.625 32.1875,24.625 C31.3930417,24.625 30.75,25.269 30.75,26.0625 C30.75,26.856 31.3930417,27.5 32.1875,27.5 C32.981,27.5 33.625,26.856 33.625,26.0625 Z M39.375,26.0625 C39.375,25.269 38.731,24.625 37.9375,24.625 C37.1430417,24.625 36.5,25.269 36.5,26.0625 C36.5,26.856 37.1430417,27.5 37.9375,27.5 C38.731,27.5 39.375,26.856 39.375,26.0625 Z M43.6875,26.0625 C43.6875,25.269 43.0435,24.625 42.25,24.625 C41.4555417,24.625 40.8125,25.269 40.8125,26.0625 C40.8125,26.856 41.4555417,27.5 42.25,27.5 C43.0435,27.5 43.6875,26.856 43.6875,26.0625 Z"
                },
                Width = 44,
                Height = 13.815765380859375,
                OffsetX = 25,
                OffsetY = 23.907882690429688,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes1);
            Node NetworkShapes2 = new Node()
            {
                ID = "uKVMSwitch3",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M9.1875,26.0625 C9.1875,26.856 8.5435,27.5 7.75,27.5 C6.95554167,27.5 6.3125,26.856 6.3125,26.0625 C6.3125,25.269 6.95554167,24.625 7.75,24.625 C8.5435,24.625 9.1875,25.269 9.1875,26.0625 Z M19.25,26.0625 C19.25,26.856 18.606,27.5 17.8125,27.5 C17.0180417,27.5 16.375,26.856 16.375,26.0625 C16.375,25.269 17.0180417,24.625 17.8125,24.625 C18.606,24.625 19.25,25.269 19.25,26.0625 Z M29.3125,26.0625 C29.3125,26.856 28.6685,27.5 27.875,27.5 C27.0805417,27.5 26.4375,26.856 26.4375,26.0625 C26.4375,25.269 27.0805417,24.625 27.875,24.625 C28.6685,24.625 29.3125,25.269 29.3125,26.0625 Z M39.375,26.0625 C39.375,26.856 38.731,27.5 37.9375,27.5 C37.1430417,27.5 36.5,26.856 36.5,26.0625 C36.5,25.269 37.1430417,24.625 37.9375,24.625 C38.731,24.625 39.375,25.269 39.375,26.0625 Z"
                },
                Width = 33.0625,
                Height = 2.875,
                OffsetX = 22.84375,
                OffsetY = 26.0625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes2);
            Node NetworkShapes3 = new Node()
            {
                ID = "uKVMSwitch4",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M6.3125,20.3125 L13.5,20.3125 L13.5,23.1875 L6.3125,23.1875 L6.3125,20.3125 Z M16.375,20.3125 L23.5625,20.3125 L23.5625,23.1875 L16.375,23.1875 L16.375,20.3125 Z M26.4375,20.3125 L33.625,20.3125 L33.625,23.1875 L26.4375,23.1875 L26.4375,20.3125 Z M36.5,20.3125 L43.6875,20.3125 L43.6875,23.1875 L36.5,23.1875 L36.5,20.3125 Z"
                },
                Width = 37.375,
                Height = 2.875,
                OffsetX = 25,
                OffsetY = 21.75,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes3);
            NodeGroup NetworkShapes4 = new NodeGroup()
            {
                ID = "Switch",
                Children = new string[] {
               "uKVMSwitch1",
               "uKVMSwitch2",
               "uKVMSwitch3",
               "uKVMSwitch4"
            }
            };
            NetworkShapesList.Add(NetworkShapes4);
            Node NetworkShapes5 = new Node()
            {
                ID = "uSpacer1",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,33.3259514 C1.49248678,33.3259514 1,32.8334646 1,32.2259514 L1,16.1 C1,15.4924868 1.49248678,15 2.1,15 L47.9,15 C48.5075132,15 49,15.4924868 49,16.1 L49,32.2259514 C49,32.8334646 48.5075132,33.3259514 47.9,33.3259514 L2.1,33.3259514 Z"
                },
                Width = 48,
                Height = 18.325950622558594,
                OffsetX = 25,
                OffsetY = 24.162975311279297,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes5);
            Node NetworkShapes6 = new Node()
            {
                ID = "uSpacer2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3 31.3259514 47 31.3259514 47 17 3 17"
                },
                Width = 44,
                Height = 14.325950622558594,
                OffsetX = 25,
                OffsetY = 24.162975311279297,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes6);
            Node NetworkShapes7 = new Node()
            {
                ID = "uSpacer3",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M6.4516129,27.8709677 L6.4516129,20.4516129 L7.93548387,20.4516129 L7.93548387,27.8709677 L6.4516129,27.8709677 Z M24.2580645,27.8709677 L24.2580645,20.4516129 L25.7419355,20.4516129 L25.7419355,27.8709677 L24.2580645,27.8709677 Z M12.3870968,27.8709677 L12.3870968,20.4516129 L13.8709677,20.4516129 L13.8709677,27.8709677 L12.3870968,27.8709677 Z M30.1935484,27.8709677 L30.1935484,20.4516129 L31.6774194,20.4516129 L31.6774194,27.8709677 L30.1935484,27.8709677 Z M18.3225806,27.8709677 L18.3225806,20.4516129 L19.8064516,20.4516129 L19.8064516,27.8709677 L18.3225806,27.8709677 Z M36.1290323,27.8709677 L36.1290323,20.4516129 L37.6129032,20.4516129 L37.6129032,27.8709677 L36.1290323,27.8709677 Z M9.41935484,27.8709677 L9.41935484,20.4516129 L10.9032258,20.4516129 L10.9032258,27.8709677 L9.41935484,27.8709677 Z M27.2258065,27.8709677 L27.2258065,20.4516129 L28.7096774,20.4516129 L28.7096774,27.8709677 L27.2258065,27.8709677 Z M15.3548387,27.8709677 L15.3548387,20.4516129 L16.8387097,20.4516129 L16.8387097,27.8709677 L15.3548387,27.8709677 Z M33.1612903,27.8709677 L33.1612903,20.4516129 L34.6451613,20.4516129 L34.6451613,27.8709677 L33.1612903,27.8709677 Z M42.0645161,27.8709677 L42.0645161,20.4516129 L43.5483871,20.4516129 L43.5483871,27.8709677 L42.0645161,27.8709677 Z M21.2903226,27.8709677 L21.2903226,20.4516129 L22.7741935,20.4516129 L22.7741935,27.8709677 L21.2903226,27.8709677 Z M39.0967742,27.8709677 L39.0967742,20.4516129 L40.5806452,20.4516129 L40.5806452,27.8709677 L39.0967742,27.8709677 Z"
                },
                Width = 37.096771240234375,
                Height = 7.419355392456055,
                OffsetX = 24.999998569488525,
                OffsetY = 24.161290168762207,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes7);
            NodeGroup NetworkShapes8 = new NodeGroup()
            {
                ID = "Spacer",
                Children = new string[] {
               "uSpacer1",
               "uSpacer2",
               "uSpacer3"
            }
            };
            NetworkShapesList.Add(NetworkShapes8);
            Node NetworkShapes9 = new Node()
            {
                ID = "uTray1",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M1,32.7098224 L1,15.1 C1,14.4924868 1.49248678,14 2.1,14 L47.9,14 C48.5075132,14 49,14.4924868 49,15.1 L49,32.7098224 C49,33.3173356 48.5075132,33.8098224 47.9,33.8098224 L2.1,33.8098224 C1.49248678,33.8098224 1,33.3173356 1,32.7098224 Z"
                },
                Width = 48,
                Height = 19.80982208251953,
                OffsetX = 25,
                OffsetY = 23.904911041259766,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes9);
            Node NetworkShapes10 = new Node()
            {
                ID = "uTray2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3,31.8098224 L3,16 L47,16 L47,31.8098224 L3,31.8098224 Z M25.7419355,29.8387097 C29.0200127,29.8387097 31.6774194,27.181303 31.6774194,23.9032258 C31.6774194,20.6251486 29.0200127,17.9677419 25.7419355,17.9677419 C22.4638583,17.9677419 19.8064516,20.6251486 19.8064516,23.9032258 C19.8064516,27.181303 22.4638583,29.8387097 25.7419355,29.8387097 Z"
                },
                Width = 44,
                Height = 15.809822082519531,
                OffsetX = 25,
                OffsetY = 23.904911041259766,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes10);
            Node NetworkShapes11 = new Node()
            {
                ID = "uTray3",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Width = 2.9677419662475586,
                Height = 5.935483932495117,
                OffsetX = 25.74193525314331,
                OffsetY = 23.903225898742676,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes11);
            NodeGroup NetworkShapes12 = new NodeGroup()
            {
                ID = "Tray",
                Children = new string[] {
               "uTray1",
               "uTray2",
               "uTray3"
            }
            };
            NetworkShapesList.Add(NetworkShapes12);
            Node NetworkShapes13 = new Node()
            {
                ID = "uLCDCopy1",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,30.6875 C1.49248678,30.6875 1,30.1950132 1,29.5875 L1,12.5342346 C1,11.9267213 1.49248678,11.4342346 2.1,11.4342346 L47.9,11.4342346 C48.5075132,11.4342346 49,11.9267213 49,12.5342346 L49,29.5875 C49,30.1950132 48.5075132,30.6875 47.9,30.6875 L2.1,30.6875 Z"
                },
                Width = 48,
                Height = 19.253265380859375,
                OffsetX = 25,
                OffsetY = 21.060867309570312,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes13);
            Node NetworkShapes14 = new Node()
            {
                ID = "uLCDCopy2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3 28.6875 47 28.6875 47 13.4342346 3 13.4342346"
                },
                Width = 44,
                Height = 15.253265380859375,
                OffsetX = 25,
                OffsetY = 21.060867309570312,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes14);
            Node NetworkShapes15 = new Node()
            {
                ID = "uLCDCopy3",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M6.3125,16.75 L9.1875,16.75 L9.1875,19.625 L6.3125,19.625 L6.3125,16.75 Z M6.3125,22.5 L9.1875,22.5 L9.1875,25.375 L6.3125,25.375 L6.3125,22.5 Z M12.0625,16.75 L14.9375,16.75 L14.9375,19.625 L12.0625,19.625 L12.0625,16.75 Z M12.0625,22.5 L14.9375,22.5 L14.9375,25.375 L12.0625,25.375 L12.0625,22.5 Z M17.8125,16.75 L20.6875,16.75 L20.6875,19.625 L17.8125,19.625 L17.8125,16.75 Z M17.8125,22.5 L20.6875,22.5 L20.6875,25.375 L17.8125,25.375 L17.8125,22.5 Z M23.5625,16.75 L26.4375,16.75 L26.4375,19.625 L23.5625,19.625 L23.5625,16.75 Z M23.5625,22.5 L26.4375,22.5 L26.4375,25.375 L23.5625,25.375 L23.5625,22.5 Z M29.3125,16.75 L32.1875,16.75 L32.1875,19.625 L29.3125,19.625 L29.3125,16.75 Z M29.3125,22.5 L32.1875,22.5 L32.1875,25.375 L29.3125,25.375 L29.3125,22.5 Z M35.0625,16.75 L37.9375,16.75 L37.9375,19.625 L35.0625,19.625 L35.0625,16.75 Z M35.0625,22.5 L37.9375,22.5 L37.9375,25.375 L35.0625,25.375 L35.0625,22.5 Z M40.8125,16.75 L43.6875,16.75 L43.6875,19.625 L40.8125,19.625 L40.8125,16.75 Z M40.8125,22.5 L43.6875,22.5 L43.6875,25.375 L40.8125,25.375 L40.8125,22.5 Z"
                },
                Width = 37.375,
                Height = 8.625,
                OffsetX = 25,
                OffsetY = 21.0625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes15);
            NodeGroup NetworkShapes16 = new NodeGroup()
            {
                ID = "LCD-Copy",
                Children = new string[] {
               "uLCDCopy1",
               "uLCDCopy2",
               "uLCDCopy3"
            }
            };
            NetworkShapesList.Add(NetworkShapes16);
            Node NetworkShapes17 = new Node()
            {
                ID = "uLCD1",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,45.3298502 C1.49248678,45.3298502 1,44.8373634 1,44.2298502 L1,7.6298502 C1,7.02233697 1.49248678,6.5298502 2.1,6.5298502 L47.9,6.5298502 C48.5075132,6.5298502 49,7.02233697 49,7.6298502 L49,44.2298502 C49,44.8373634 48.5075132,45.3298502 47.9,45.3298502 L2.1,45.3298502 Z"
                },
                Width = 48,
                Height = 38.79999923706055,
                OffsetX = 25,
                OffsetY = 25.92984962463379,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes17);
            Node NetworkShapes18 = new Node()
            {
                ID = "uLCD2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3 43.3298502 47 43.3298502 47 8.5298502 3 8.5298502"
                },
                Width = 44,
                Height = 34.79999923706055,
                OffsetX = 25,
                OffsetY = 25.92984962463379,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes18);
            Node NetworkShapes19 = new Node()
            {
                ID = "uLCD3",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M40.3333333,39.7298502 C40.3333333,38.8834502 41.0187333,38.1965169 41.8666667,38.1965169 C42.7146,38.1965169 43.4,38.8834502 43.4,39.7298502 C43.4,40.5762502 42.7146,41.2631835 41.8666667,41.2631835 C41.0187333,41.2631835 40.3333333,40.5762502 40.3333333,39.7298502 Z M35.7333333,39.7298502 C35.7333333,38.8834502 36.4187333,38.1965169 37.2666667,38.1965169 C38.1146,38.1965169 38.8,38.8834502 38.8,39.7298502 C38.8,40.5762502 38.1146,41.2631835 37.2666667,41.2631835 C36.4187333,41.2631835 35.7333333,40.5762502 35.7333333,39.7298502 Z M7.6,36.6631835 C7.04771525,36.6631835 6.6,36.2154683 6.6,35.6631835 L6.6,13.1298502 C6.6,12.5775654 7.04771525,12.1298502 7.6,12.1298502 L42.4,12.1298502 C42.9522847,12.1298502 43.4,12.5775654 43.4,13.1298502 L43.4,35.6631835 C43.4,36.2154683 42.9522847,36.6631835 42.4,36.6631835 L7.6,36.6631835 Z"
                },
                Width = 36.80000305175781,
                Height = 29.133333206176758,
                OffsetX = 25.000001430511475,
                OffsetY = 26.69651699066162,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes19);
            NodeGroup NetworkShapes20 = new NodeGroup()
            {
                ID = "LCD",
                Children = new string[] {
               "uLCD1",
               "uLCD2",
               "uLCD3"
            }
            };
            NetworkShapesList.Add(NetworkShapes20);
            
            Node NetworkShapes42 = new Node()
            {
                ID = "Fax1",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M21.3046875,9.21428571 L21.3046875,2.2953125 C21.3046875,1.96394165 21.5733167,1.6953125 21.9046875,1.6953125 L41.2649554,1.6953125 C41.5963262,1.6953125 41.8649554,1.96394165 41.8649554,2.2953125 L41.8649554,9.21428571 L46.6142857,9.21428571 C47.2217989,9.21428571 47.7142857,9.70677249 47.7142857,10.3142857 L47.7142857,47.9 C47.7142857,48.5075132 47.2217989,49 46.6142857,49 L4.1,49 C3.49248678,49 3,48.5075132 3,47.9 L3,10.3142857 C3,9.70677249 3.49248678,9.21428571 4.1,9.21428571 L21.3046875,9.21428571 Z"
                },
                Width = 44.71428680419922,
                Height = 47.3046875,
                OffsetX = 25.35714340209961,
                OffsetY = 25.34765625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes42);
            Node NetworkShapes43 = new Node()
            {
                ID = "Fax2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M41.864603,16.2857143 C41.8648373,16.2788235 41.8649554,16.2719032 41.8649554,16.2649554 L41.8649554,10.5864662 L46.4285714,10.5864662 L46.4285714,47.6278195 L4.28571429,47.6278195 L4.28571429,10.5864662 L21.3046875,10.5864662 L21.3046875,16.2649554 C21.3046875,16.2719032 21.3048056,16.2788235 21.3050399,16.2857143 L20.4285714,16.2857143 C20.1524291,16.2857143 19.9285714,16.5095719 19.9285714,16.7857143 C19.9285714,17.0618567 20.1524291,17.2857143 20.4285714,17.2857143 L42.9685719,17.2857143 C43.2447142,17.2857143 43.4685719,17.0618567 43.4685719,16.7857143 C43.4685719,16.5095719 43.2447142,16.2857143 42.9685719,16.2857143 L41.864603,16.2857143 Z M10.0714286,40.9285714 C10.0714286,41.4808562 10.5191438,41.9285714 11.0714286,41.9285714 L16.6428571,41.9285714 C17.1951419,41.9285714 17.6428571,41.4808562 17.6428571,40.9285714 L17.6428571,17.2857143 C17.6428571,16.7334295 17.1951419,16.2857143 16.6428571,16.2857143 L11.0714286,16.2857143 C10.5191438,16.2857143 10.0714286,16.7334295 10.0714286,17.2857143 L10.0714286,40.9285714 Z M42.1534729,24.1534729 L42.1534729,20.2249015 C42.1534729,19.6726167 41.7057577,19.2249015 41.1534729,19.2249015 L22.7750985,19.2249015 C22.2228138,19.2249015 21.7750985,19.6726167 21.7750985,20.2249015 L21.7750985,24.1534729 C21.7750985,24.7057577 22.2228138,25.1534729 22.7750985,25.1534729 L41.1534729,25.1534729 C41.7057577,25.1534729 42.1534729,24.7057577 42.1534729,24.1534729 Z M23.7142857,30.4285714 L25.3571429,30.4285714 C25.6332852,30.4285714 25.8571429,30.2047138 25.8571429,29.9285714 C25.8571429,29.6524291 25.6332852,29.4285714 25.3571429,29.4285714 L23.7142857,29.4285714 C23.4381433,29.4285714 23.2142857,29.6524291 23.2142857,29.9285714 C23.2142857,30.2047138 23.4381433,30.4285714 23.7142857,30.4285714 Z M28.6428571,30.4285714 L30.2857143,30.4285714 C30.5618567,30.4285714 30.7857143,30.2047138 30.7857143,29.9285714 C30.7857143,29.6524291 30.5618567,29.4285714 30.2857143,29.4285714 L28.6428571,29.4285714 C28.3667148,29.4285714 28.1428571,29.6524291 28.1428571,29.9285714 C28.1428571,30.2047138 28.3667148,30.4285714 28.6428571,30.4285714 Z M33.5714286,30.4285714 L35.2142857,30.4285714 C35.4904281,30.4285714 35.7142857,30.2047138 35.7142857,29.9285714 C35.7142857,29.6524291 35.4904281,29.4285714 35.2142857,29.4285714 L33.5714286,29.4285714 C33.2952862,29.4285714 33.0714286,29.6524291 33.0714286,29.9285714 C33.0714286,30.2047138 33.2952862,30.4285714 33.5714286,30.4285714 Z M38.5,30.4285714 L40.1428571,30.4285714 C40.4189995,30.4285714 40.6428571,30.2047138 40.6428571,29.9285714 C40.6428571,29.6524291 40.4189995,29.4285714 40.1428571,29.4285714 L38.5,29.4285714 C38.2238576,29.4285714 38,29.6524291 38,29.9285714 C38,30.2047138 38.2238576,30.4285714 38.5,30.4285714 Z M23.7142857,33.7142857 L25.3571429,33.7142857 C25.6332852,33.7142857 25.8571429,33.4904281 25.8571429,33.2142857 C25.8571429,32.9381433 25.6332852,32.7142857 25.3571429,32.7142857 L23.7142857,32.7142857 C23.4381433,32.7142857 23.2142857,32.9381433 23.2142857,33.2142857 C23.2142857,33.4904281 23.4381433,33.7142857 23.7142857,33.7142857 Z M28.6428571,33.7142857 L30.2857143,33.7142857 C30.5618567,33.7142857 30.7857143,33.4904281 30.7857143,33.2142857 C30.7857143,32.9381433 30.5618567,32.7142857 30.2857143,32.7142857 L28.6428571,32.7142857 C28.3667148,32.7142857 28.1428571,32.9381433 28.1428571,33.2142857 C28.1428571,33.4904281 28.3667148,33.7142857 28.6428571,33.7142857 Z M33.5714286,33.7142857 L35.2142857,33.7142857 C35.4904281,33.7142857 35.7142857,33.4904281 35.7142857,33.2142857 C35.7142857,32.9381433 35.4904281,32.7142857 35.2142857,32.7142857 L33.5714286,32.7142857 C33.2952862,32.7142857 33.0714286,32.9381433 33.0714286,33.2142857 C33.0714286,33.4904281 33.2952862,33.7142857 33.5714286,33.7142857 Z M38.5,33.7142857 L40.1428571,33.7142857 C40.4189995,33.7142857 40.6428571,33.4904281 40.6428571,33.2142857 C40.6428571,32.9381433 40.4189995,32.7142857 40.1428571,32.7142857 L38.5,32.7142857 C38.2238576,32.7142857 38,32.9381433 38,33.2142857 C38,33.4904281 38.2238576,33.7142857 38.5,33.7142857 Z M23.7142857,37 L25.3571429,37 C25.6332852,37 25.8571429,36.7761424 25.8571429,36.5 C25.8571429,36.2238576 25.6332852,36 25.3571429,36 L23.7142857,36 C23.4381433,36 23.2142857,36.2238576 23.2142857,36.5 C23.2142857,36.7761424 23.4381433,37 23.7142857,37 Z M28.6428571,37 L30.2857143,37 C30.5618567,37 30.7857143,36.7761424 30.7857143,36.5 C30.7857143,36.2238576 30.5618567,36 30.2857143,36 L28.6428571,36 C28.3667148,36 28.1428571,36.2238576 28.1428571,36.5 C28.1428571,36.7761424 28.3667148,37 28.6428571,37 Z M33.5714286,37 L35.2142857,37 C35.4904281,37 35.7142857,36.7761424 35.7142857,36.5 C35.7142857,36.2238576 35.4904281,36 35.2142857,36 L33.5714286,36 C33.2952862,36 33.0714286,36.2238576 33.0714286,36.5 C33.0714286,36.7761424 33.2952862,37 33.5714286,37 Z M38.5,37 L40.1428571,37 C40.4189995,37 40.6428571,36.7761424 40.6428571,36.5 C40.6428571,36.2238576 40.4189995,36 40.1428571,36 L38.5,36 C38.2238576,36 38,36.2238576 38,36.5 C38,36.7761424 38.2238576,37 38.5,37 Z M23.7142857,40.2857143 L25.3571429,40.2857143 C25.6332852,40.2857143 25.8571429,40.0618567 25.8571429,39.7857143 C25.8571429,39.5095719 25.6332852,39.2857143 25.3571429,39.2857143 L23.7142857,39.2857143 C23.4381433,39.2857143 23.2142857,39.5095719 23.2142857,39.7857143 C23.2142857,40.0618567 23.4381433,40.2857143 23.7142857,40.2857143 Z M28.6428571,40.2857143 L30.2857143,40.2857143 C30.5618567,40.2857143 30.7857143,40.0618567 30.7857143,39.7857143 C30.7857143,39.5095719 30.5618567,39.2857143 30.2857143,39.2857143 L28.6428571,39.2857143 C28.3667148,39.2857143 28.1428571,39.5095719 28.1428571,39.7857143 C28.1428571,40.0618567 28.3667148,40.2857143 28.6428571,40.2857143 Z M33.5714286,40.2857143 L35.2142857,40.2857143 C35.4904281,40.2857143 35.7142857,40.0618567 35.7142857,39.7857143 C35.7142857,39.5095719 35.4904281,39.2857143 35.2142857,39.2857143 L33.5714286,39.2857143 C33.2952862,39.2857143 33.0714286,39.5095719 33.0714286,39.7857143 C33.0714286,40.0618567 33.2952862,40.2857143 33.5714286,40.2857143 Z M38.5,40.2857143 L40.1428571,40.2857143 C40.4189995,40.2857143 40.6428571,40.0618567 40.6428571,39.7857143 C40.6428571,39.5095719 40.4189995,39.2857143 40.1428571,39.2857143 L38.5,39.2857143 C38.2238576,39.2857143 38,39.5095719 38,39.7857143 C38,40.0618567 38.2238576,40.2857143 38.5,40.2857143 Z M41.1534729,24.1534729 L22.7750985,24.1534729 L22.7750985,20.2249015 L41.1534729,20.2249015 L41.1534729,24.1534729 Z M16.6428571,40.9285714 L11.0714286,40.9285714 L11.0714286,17.2857143 L16.6428571,17.2857143 L16.6428571,40.9285714 Z"
                },
                Width = 42.14285659790039,
                Height = 37.041351318359375,
                OffsetX = 25.357142448425293,
                OffsetY = 29.107141494750977,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes43);
            Node NetworkShapes44 = new Node()
            {
                ID = "Fax3",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M38.1696429 18.5602679 38.1696429 0 25 2.22044605e-16 25 18.5602679"
                },
                Width = 13.16964340209961,
                Height = 18.56026840209961,
                OffsetX = 31.584821701049805,
                OffsetY = 9.280134201049805,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes44);
            NodeGroup NetworkShapes45 = new NodeGroup()
            {
                ID = "Fax",
                Children = new string[] {
               "Fax1",
               "Fax2",
               "Fax3"
            }
            };
            NetworkShapesList.Add(NetworkShapes45);
            NetworkShapes = new Palette() { ID = "NetworkShapes", IsExpanded = false, Symbols = NetworkShapesList, Title = "Network Shapes" };
        }
        public void InitializeFloorShapes()
        {

            FloorPlaneShapesList = new DiagramObjectCollection<NodeBase>();
            Node FloorPlaneShapes1200 = new Node()
            {
                ID = "WashBasin_Mirror2siLuxk",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M0,32H160a0,0,0,0,1,0,0v79a10,10,0,0,1-10,10H10A10,10,0,0,1,0,111V32A0,0,0,0,1,0,32Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#270805",
                    StrokeWidth = 0
                },
                Width = 160,
                Height = 89,
                OffsetX = 80,
                OffsetY = 76.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1200);
            Node FloorPlaneShapes1201 = new Node()
            {
                ID = "WashBasin_Mirror3Q9PxVP",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd"
                },
                Width = 160,
                Height = 7.819999694824219,
                OffsetX = 80,
                OffsetY = 35.90999984741211,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1201);
            Node FloorPlaneShapes1202 = new Node()
            {
                ID = "WashBasin_Mirror4a4GtBc",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "#c2c3c4"
                },
                Width = 160,
                Height = 7.819999694824219,
                OffsetX = 80,
                OffsetY = 43.72999954223633,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1202);
            Node FloorPlaneShapes1203 = new Node()
            {
                ID = "WashBasin_Mirror5Ec581y",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse,
                    CornerRadius = 44.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd"
                },
                Width = 89,
                Height = 48,
                OffsetX = 81.5,
                OffsetY = 85,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1203);
            Node FloorPlaneShapes1204 = new Node()
            {
                ID = "WashBasin_Mirror6xYjsZx",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse,
                    CornerRadius = 40.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#7f7f7f"
                },
                Width = 81,
                Height = 42,
                OffsetX = 81.5,
                OffsetY = 85,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1204);
            Node FloorPlaneShapes1205 = new Node()
            {
                ID = "WashBasin_Mirror7H0IdfR",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fcfcfc"
                },
                Width = 6.420000076293945,
                Height = 6.420000076293945,
                OffsetX = 81.59999942779541,
                OffsetY = 77.32000064849854,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1205);
            Node FloorPlaneShapes1206 = new Node()
            {
                ID = "WashBasin_Mirror8JGH6zw",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 1
                },
                Style = new ShapeStyle()
                {
                    Fill = "#2080ce"
                },
                Width = 7.4599995613098145,
                Height = 10.529999732971191,
                OffsetX = 146.5499918460846,
                OffsetY = 86.38500261306763,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1206);
            Node FloorPlaneShapes1207 = new Node()
            {
                ID = "WashBasin_Mirror9K7WbYf",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M81.09,53.8h0a2.18,2.18,0,0,1,2.17,2.29l-.63,11.68a1.55,1.55,0,0,1-1.54,1.46h0a1.55,1.55,0,0,1-1.54-1.46l-.63-11.68A2.17,2.17,0,0,1,81.09,53.8Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 4.346092224121094,
                Height = 15.430004119873047,
                OffsetX = 81.08972549438477,
                OffsetY = 61.51500129699707,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1207);
            Node FloorPlaneShapes1208 = new Node()
            {
                ID = "WashBasin_Mirror10UE5NjR",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M87.65,56.31h0a2.18,2.18,0,0,1,2.29-2.17l11.68.63a1.54,1.54,0,0,1,1.45,1.54h0a1.54,1.54,0,0,1-1.45,1.54l-11.68.63A2.17,2.17,0,0,1,87.65,56.31Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 15.420013427734375,
                Height = 4.346099853515625,
                OffsetX = 95.36000061035156,
                OffsetY = 56.310272216796875,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1208);
            Node FloorPlaneShapes1209 = new Node()
            {
                ID = "WashBasin_Mirror116kqZx9",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M74.91,56.31h0a2.18,2.18,0,0,0-2.29-2.17l-11.68.63a1.54,1.54,0,0,0-1.45,1.54h0a1.54,1.54,0,0,0,1.45,1.54l11.68.63A2.17,2.17,0,0,0,74.91,56.31Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 15.420013427734375,
                Height = 4.346099853515625,
                OffsetX = 67.20000457763672,
                OffsetY = 56.310272216796875,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1209);
            Node FloorPlaneShapes1210 = new Node()
            {
                ID = "WashBasin_Mirror125Y2eYg",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M133.54,47.64h18.55a0,0,0,0,1,0,0V49.7a1,1,0,0,1-1,1H134.54a1,1,0,0,1-1-1V47.64A0,0,0,0,1,133.54,47.64Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 18.550003051757812,
                Height = 3.0600013732910156,
                OffsetX = 142.81499481201172,
                OffsetY = 49.170000076293945,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1210);
            Node FloorPlaneShapes1211 = new Node()
            {
                ID = "WashBasin_Mirror13dEOJiO",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M10.2,47.64H28.75a0,0,0,0,1,0,0V49.7a1,1,0,0,1-1,1H11.2a1,1,0,0,1-1-1V47.64A0,0,0,0,1,10.2,47.64Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 18.549999237060547,
                Height = 3.0600013732910156,
                OffsetX = 19.47499942779541,
                OffsetY = 49.170000076293945,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1211);
            Node FloorPlaneShapes1212 = new Node()
            {
                ID = "WashBasin_Mirror14YGyxGd",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M143.82,91.65a1,1,0,0,1-1-1V82.12a1,1,0,0,1,1-1h5.45a1,1,0,0,1,1,1Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 7.4499969482421875,
                Height = 10.529998779296875,
                OffsetX = 146.54500579833984,
                OffsetY = 86.38500213623047,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1212);
            NodeGroup FloorPlaneShapes1213 = new NodeGroup()
            {
                ID = "WashBasin_Mirror",
                Children = new string[] {
               "WashBasin_Mirror2siLuxk",
               "WashBasin_Mirror3Q9PxVP",
               "WashBasin_Mirror4a4GtBc",
               "WashBasin_Mirror5Ec581y",
               "WashBasin_Mirror6xYjsZx",
               "WashBasin_Mirror7H0IdfR",
               "WashBasin_Mirror8JGH6zw",
               "WashBasin_Mirror9K7WbYf",
               "WashBasin_Mirror10UE5NjR",
               "WashBasin_Mirror116kqZx9",
               "WashBasin_Mirror125Y2eYg",
               "WashBasin_Mirror13dEOJiO",
               "WashBasin_Mirror14YGyxGd"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1213);
            Node FloorPlaneShapes1194 = new Node()
            {
                ID = "Washbasin22mwWtpz",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M42,118.5V44.6A5.6,5.6,0,0,1,47.6,39h75.53a5.58,5.58,0,0,1,5.6,5.73c-.42,19-7.58,78.46-81,79.37A5.61,5.61,0,0,1,42,118.5Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd",
                    StrokeWidth = 0
                },
                Width = 86.73202514648438,
                Height = 85.10133361816406,
                OffsetX = 85.36600875854492,
                OffsetY = 81.55062103271484,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1194);
            Node FloorPlaneShapes1195 = new Node()
            {
                ID = "Washbasin23RiEEC4",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M48,109.94V49.57A4.58,4.58,0,0,1,52.57,45h61.7a4.57,4.57,0,0,1,4.57,4.68c-.34,15.48-6.19,64.13-66.26,64.83A4.55,4.55,0,0,1,48,109.94Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#7f7f7f",
                    StrokeWidth = 0
                },
                Width = 70.84136962890625,
                Height = 69.51010131835938,
                OffsetX = 83.4206428527832,
                OffsetY = 79.75504684448242,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1195);
            Node FloorPlaneShapes1196 = new Node()
            {
                ID = "Washbasin24R1ibaH",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M48,103V50a5,5,0,0,1,5-5h52S105,89.12,48,103Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 57,
                Height = 58,
                OffsetX = 76.5,
                OffsetY = 74,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1196);
            Node FloorPlaneShapes1197 = new Node()
            {
                ID = "Washbasin258VaJeD",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff"
                },
                Width = 7,
                Height = 7,
                OffsetX = 65,
                OffsetY = 61,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1197);
            Node FloorPlaneShapes1198 = new Node()
            {
                ID = "Washbasin26XjKRF6",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84.34,41.29h0a2.31,2.31,0,0,1,2.3,2.43L86,56.07a1.63,1.63,0,0,1-1.63,1.54h0a1.62,1.62,0,0,1-1.62-1.54l-.67-12.35A2.3,2.3,0,0,1,84.34,41.29Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 4.5668182373046875,
                Height = 16.320003509521484,
                OffsetX = 84.3597183227539,
                OffsetY = 49.450002670288086,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1198);
            NodeGroup FloorPlaneShapes1199 = new NodeGroup()
            {
                ID = "Washbasin2",
                Children = new string[] {
               "Washbasin22mwWtpz",
               "Washbasin23RiEEC4",
               "Washbasin24R1ibaH",
               "Washbasin258VaJeD",
               "Washbasin26XjKRF6"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1199);
            Node FloorPlaneShapes1188 = new Node()
            {
                ID = "Washbasin12HCS1Cb",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84.13,112.33h0A50.88,50.88,0,0,1,33.32,64.12,12.87,12.87,0,0,1,34,59.2V51a3.29,3.29,0,0,1,3.29-3.29h92.9a4,4,0,0,1,4,4V59.2a13.06,13.06,0,0,1,.71,4.92A50.89,50.89,0,0,1,84.13,112.33Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd",
                    StrokeWidth = 0
                },
                Width = 101.6159439086914,
                Height = 64.62001037597656,
                OffsetX = 84.1071891784668,
                OffsetY = 80.02000427246094,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1188);
            Node FloorPlaneShapes1189 = new Node()
            {
                ID = "Washbasin13fWI7lW",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84.13,106.33h0C62.9,106.33,45.4,93,43,75.87a13.27,13.27,0,0,1,.91-7l.8-1.91c1.14-2.72,4.23-4.55,7.69-4.55h63.51c3.46,0,6.55,1.83,7.69,4.55l.79,1.91a13.18,13.18,0,0,1,.92,7C122.86,93,105.36,106.33,84.13,106.33Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#7f7f7f",
                    StrokeWidth = 0
                },
                Width = 82.56951904296875,
                Height = 43.92000198364258,
                OffsetX = 84.15504455566406,
                OffsetY = 84.3700008392334,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1189);
            Node FloorPlaneShapes1190 = new Node()
            {
                ID = "Washbasin14bwxPiq",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M55,96.21s28.25-41,58,0C113,96.21,85.16,118.24,55,96.21Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 58,
                Height = 28.013328552246094,
                OffsetX = 84,
                OffsetY = 91.99444961547852,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1190);
            Node FloorPlaneShapes1191 = new Node()
            {
                ID = "Washbasin156XUxTC",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff"
                },
                Width = 9,
                Height = 9,
                OffsetX = 84.5,
                OffsetY = 84.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1191);
            Node FloorPlaneShapes1192 = new Node()
            {
                ID = "Washbasin16SjrokS",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M83.38,50.32h0a3.7,3.7,0,0,1,3.7,3.9L86,74.12a2.62,2.62,0,0,1-2.62,2.48h0a2.63,2.63,0,0,1-2.62-2.48l-1.07-19.9A3.69,3.69,0,0,1,83.38,50.32Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 7.401397705078125,
                Height = 26.280014038085938,
                OffsetX = 83.38471221923828,
                OffsetY = 63.459999084472656,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1192);
            NodeGroup FloorPlaneShapes1193 = new NodeGroup()
            {
                ID = "Washbasin1",
                Children = new string[] {
               "Washbasin12HCS1Cb",
               "Washbasin13fWI7lW",
               "Washbasin14bwxPiq",
               "Washbasin156XUxTC",
               "Washbasin16SjrokS"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1193);
            Node FloorPlaneShapes1183 = new Node()
            {
                ID = "Washbasin2xLatbz",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 43.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#707070"
                },
                Width = 87,
                Height = 87,
                OffsetX = 82.5,
                OffsetY = 80.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1183);
            Node FloorPlaneShapes1184 = new Node()
            {
                ID = "Washbasin30qnQfG",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84,50H81A34,34,0,0,0,47,84h0a34,34,0,0,0,34,34h3a34,34,0,0,0,34-34h0A34,34,0,0,0,84,50ZM82,86a6,6,0,1,1,6-6A6,6,0,0,1,82,86Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#d1d2d3",
                    StrokeWidth = 0
                },
                Width = 71,
                Height = 68,
                OffsetX = 82.5,
                OffsetY = 84,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1184);
            Node FloorPlaneShapes1185 = new Node()
            {
                ID = "Washbasin4cmVZFl",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M57.21,107.79a33.77,33.77,0,0,1,0-47.75h0C70.45,46.85,93.83,45.83,107,59Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 59.677005767822266,
                Height = 58.17007827758789,
                OffsetX = 77.16149711608887,
                OffsetY = 78.7049617767334,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1185);
            Node FloorPlaneShapes1186 = new Node()
            {
                ID = "Washbasin5WpMfbs",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M82.3,38.32h0A3.69,3.69,0,0,1,86,42.22l-1.07,19.9A2.63,2.63,0,0,1,82.3,64.6h0a2.62,2.62,0,0,1-2.62-2.48L78.6,42.22A3.71,3.71,0,0,1,82.3,38.32Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 7.410850524902344,
                Height = 26.280040740966797,
                OffsetX = 82.30055618286133,
                OffsetY = 51.45998573303223,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1186);
            NodeGroup FloorPlaneShapes1187 = new NodeGroup()
            {
                ID = "Washbasin",
                Children = new string[] {
               "Washbasin2xLatbz",
               "Washbasin30qnQfG",
               "Washbasin4cmVZFl",
               "Washbasin5WpMfbs"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1187);
            Node FloorPlaneShapes1175 = new Node()
            {
                ID = "VerticalWall2DppOoX",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "#464747"
                },
                Width = 113,
                Height = 14,
                OffsetX = 81,
                OffsetY = 80.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1175);
            NodeGroup FloorPlaneShapes1176 = new NodeGroup()
            {
                ID = "Vertical Wall",
                Children = new string[] {
               "VerticalWall2DppOoX"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1176);
            Node FloorPlaneShapes1177 = new Node()
            {
                ID = "Wardrobe2DkdyWy",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "url(#linear-gradient)"
                },
                Width = 77,
                Height = 4,
                OffsetX = 38.5,
                OffsetY = 100,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1177);
            Node FloorPlaneShapes1178 = new Node()
            {
                ID = "Wardrobe3I9qfIw",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M160 101.29 83.92 111.29 82.92 107.54 160 97.54 160 101.29"
                },
                Style = new ShapeStyle()
                {
                    Fill = "url(#linear-gradient-2)"
                },
                Width = 77.08000183105469,
                Height = 13.75,
                OffsetX = 121.45999908447266,
                OffsetY = 104.41500091552734,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1178);
            Node FloorPlaneShapes1179 = new Node()
            {
                ID = "Wardrobe41tIQyC",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M4,54H156a4,4,0,0,1,4,4V98a0,0,0,0,1,0,0H0a0,0,0,0,1,0,0V58A4,4,0,0,1,4,54Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#522c0a",
                    StrokeWidth = 0
                },
                Width = 160,
                Height = 44,
                OffsetX = 80,
                OffsetY = 76,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1179);
            Node FloorPlaneShapes1180 = new Node()
            {
                ID = "Wardrobe5Kkgznd",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 0.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#522c0a"
                },
                Width = 3,
                Height = 1,
                OffsetX = 49.5,
                OffsetY = 102.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1180);
            Node FloorPlaneShapes1181 = new Node()
            {
                ID = "Wardrobe6o9EXmW",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 0.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#522c0a"
                },
                Width = 3,
                Height = 1,
                OffsetX = 115.17000579833984,
                OffsetY = 107.69000244140625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1181);
            NodeGroup FloorPlaneShapes1182 = new NodeGroup()
            {
                ID = "Wardrobe",
                Children = new string[] {
               "Wardrobe2DkdyWy",
               "Wardrobe3I9qfIw",
               "Wardrobe41tIQyC",
               "Wardrobe5Kkgznd",
               "Wardrobe6o9EXmW"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1182);
            FloorPlaneShapes = new Palette() { ID = "FloorPlaneShapes", IsExpanded = false, Symbols = FloorPlaneShapesList, Title = "Floor Plane Shapes" };
        }
        internal async Task UpdatePalettes(List<DiagramMoreShapes.ListViewDataFields> SelectedItems)
        {
            DiagramObjectCollection<Palette> AddPalettes = new DiagramObjectCollection<Palette>();
            List<string> RemovePalette = new List<string>();
            foreach (DiagramMoreShapes.ListViewDataFields data in SelectedItems)
            {
                string paletteName = data.Text; bool isChecked = data.Checked;
                switch (paletteName)
                {
                    case "Flow":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(FlowShapePalette))
                                AddPalettes.Add(FlowShapePalette);
                            PaletteInstance.AddPalettes(AddPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(FlowShapePalette.ID);
                        break;
                    case "Basic":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(BasicShapePalette))
                                AddPalettes.Add(BasicShapePalette);
                            PaletteInstance.AddPalettes(AddPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(BasicShapePalette.ID);
                        break;
                    case "BPMN":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(BpmnShapePalette))
                                AddPalettes.Add(BpmnShapePalette);
                            PaletteInstance.AddPalettes(AddPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(BpmnShapePalette.ID);
                        break;
                    case "Connectors":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(ConnectorPalette))
                                AddPalettes.Add(ConnectorPalette);
                            PaletteInstance.AddPalettes(AddPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(ConnectorPalette.ID);
                        break;
                    case "HTML":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(HtmlShapePalette))
                                AddPalettes.Add(HtmlShapePalette);
                            PaletteInstance.AddPalettes(AddPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(HtmlShapePalette.ID);
                        break;
                    case "Native":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(NativeShapePalette))
                                AddPalettes.Add(NativeShapePalette);
                            PaletteInstance.AddPalettes(AddPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(NativeShapePalette.ID);
                        break;
                    case "Network":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(NetworkShapes))
                                AddPalettes.Add(NetworkShapes);
                            PaletteInstance.AddPalettes(AddPalettes);

                        }
                        else
                            PaletteInstance.RemovePalettes(NetworkShapes.ID);
                        break;
                    case "Floorplan":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(FloorPlaneShapes))
                                AddPalettes.Add(FloorPlaneShapes);
                            PaletteInstance.AddPalettes(AddPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(FloorPlaneShapes.ID);
                        break;
                }
            }
            PaletteInstance.AddPalettes(AddPalettes);
            StateHasChanged();
        }
    }
}
