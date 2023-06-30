using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using Syncfusion.Blazor.Diagram;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DiagramBuilder
{
    public partial class DiagramToolBar
    {
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        #region events

        private async Task DrawShapeChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            Parent.DiagramContent.DrawingObject(args);
            Parent.DiagramContent.UpdateContinousDrawTool();
            await removeSelectedToolbarItem("shape");
        }
        private async Task DrawConnectorChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            Parent.DiagramContent.DrawingObject(args);
            Parent.DiagramContent.UpdateContinousDrawTool();
            diagram.ClearSelection();
            await removeSelectedToolbarItem("connector");
        }

        private void OrderCommandsChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            if (args.Item.Text == "Send To Back")
            {
                diagram.SendToBack();
            }
            else if (args.Item.Text == "Bring To Front")
            {
                diagram.BringToFront();
            }
            else if (args.Item.Text == "Bring Forward")
            {
                diagram.BringForward();
            }
            else if (args.Item.Text == "Send Backward")
            {
                diagram.SendBackward();
            }
        }
        private void DrawZoomChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            double currentZoom = Parent.DiagramContent.CurrentZoom;
            switch (args.Item.Text)
            {
                case "Zoom In":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { Type = "ZoomIn", ZoomFactor = 0.2 });
                    break;
                case "Zoom Out":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { Type = "ZoomOut", ZoomFactor = 0.2 });
                    break;
                case "Zoom to Fit":
                    FitOptions fitoption = new FitOptions()
                    {
                        Mode = FitMode.Both,
                        Region = DiagramRegion.Content,
                    };
                    diagram.FitToPage(fitoption);
                    break;
                case "Zoom to 50%":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { ZoomFactor = ((0.5 / currentZoom) - 1) });
                    break;
                case "Zoom to 100%":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { ZoomFactor = ((1 / currentZoom) - 1) });
                    break;
                case "Zoom to 200%":
                    Parent.DiagramContent.ZoomTo(new DiagramMainContent.ZoomOptions() { ZoomFactor = ((2 / currentZoom) - 1) });
                    break;
            }
            ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
        }
        private async Task ToolbarEditorClickInOrgChart(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            var commandType = args.Item.TooltipText.ToLower(new CultureInfo("en-US"));
            switch (commandType)
            {
                case "undo":
                    diagram.Undo();
                    await EnableToolbarItems(new object() { }, "historychange");
                    break;
                case "redo":
                    diagram.Redo();
                    await EnableToolbarItems(new object() { }, "historychange");
                    break;
                case "pan tool":
                    Parent.DiagramContent.UpdateTool();
                    diagram.ClearSelection();
                    Parent.DiagramPropertyPanel.PanelVisibility();
                    break;
                case "pointer":
                    Parent.DiagramContent.DiagramDrawingObject = null;
                    Parent.DiagramContent.UpdatePointerTool();
                    break;
                case "add child":
                    Parent.OrgChartPropertyPanel.AddNode(diagram.SelectionSettings.Nodes[0].ID);
                    break;
                case "add a child to the same level":
                    Parent.OrgChartPropertyPanel.AddRightChild();
                    break;
                case "move the child parent to the next level":
                    Parent.OrgChartPropertyPanel.changeChildParent();
                    break;
            }
            if (commandType == "pan tool" || commandType == "pointer")
            {
#pragma warning disable CA1307 // Specify StringComparison
                if (args.Item.CssClass.IndexOf("tb-item-selected") == -1)
#pragma warning restore CA1307 // Specify StringComparison
                {
                    if (commandType == "pan tool")
                        PanItemCssClass += " tb-item-selected";
                    if (commandType == "pointer")
                        PointerItemCssClass += " tb-item-selected";
                    await removeSelectedToolbarItem(commandType).ConfigureAwait(true);
                }
            }
            Parent.DiagramPropertyPanel.PanelVisibility();
            Parent.DiagramContent.StateChanged();
        }
        private async Task ToolbarEditorClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            var commandType = args.Item.TooltipText.ToLower(new CultureInfo("en-US"));
            switch (commandType)
            {
                case "undo":
                    diagram.Undo();
                    await EnableToolbarItems(new object() { }, "historychange");
                    break;
                case "redo":
                    diagram.Redo();
                    await EnableToolbarItems(new object() { }, "historychange");
                    break;
                case "pan tool":
                    Parent.DiagramContent.UpdateTool();
                    diagram.ClearSelection();
                    Parent.DiagramPropertyPanel.PanelVisibility();
                    break;
                case "pointer":
                    Parent.DiagramContent.DiagramDrawingObject = null;
                    Parent.DiagramContent.UpdatePointerTool();
                    break;
                case "text tool":
                    Parent.DiagramContent.DiagramDrawingObject = new Node() { Shape = new TextShape() { Type = NodeShapes.Text } };
                    Parent.DiagramContent.DiagramTool = DiagramInteractions.ContinuousDraw;
                    break;
                case "delete":
                    DeleteData();
                    toolbarClassName = "db-toolbar-container db-undo";
                    break;
                case "lock":
                case "unlock":
                    await LockObject().ConfigureAwait(true);
                    StateHasChanged();
                    break;
                case "group":
                    Parent.DiagramContent.GroupObjects();
                    break;
                case "ungroup":
                    Parent.DiagramContent.UngroupObjects();
                    break;
                case "align left":
                case "align right":
                case "align top":
                case "align bottom":
                case "align middle":
                case "align center":
#pragma warning disable CA1307 // Specify StringComparison
                    string alignType = commandType.Replace("align ", "");
#pragma warning restore CA1307 // Specify StringComparison
                    alignType = char.ToUpper(alignType[0], new CultureInfo("en-US")) + alignType.Substring(1);
                    diagram.SetAlign((AlignmentOptions)Enum.Parse(typeof(AlignmentOptions), alignType));
                    break;
                case "distribute objects vertically":
                    diagram.SetDistribute(DistributeOptions.RightToLeft);
                    break;
                case "distribute objects horizontally":
                    diagram.SetDistribute(DistributeOptions.BottomToTop);
                    break;
                case "add child":
                    Parent.MindMapPropertyPanel.AddNode("Left");
                    break;
                case "add sibling":
                    Parent.MindMapPropertyPanel.AddSiblingChild("Bottom");
                    break;
                case "add multiple child":
                    Parent.MindMapPropertyPanel.UpdatePropertyPanel();
                    break;
            }
            if (commandType == "pan tool" || commandType == "pointer" || commandType == "text tool")
            {
#pragma warning disable CA1307 // Specify StringComparison
                if (args.Item.CssClass.IndexOf("tb-item-selected") == -1)
#pragma warning restore CA1307 // Specify StringComparison
                {
                    if (commandType == "pan tool")
                        PanItemCssClass += " tb-item-selected";
                    if (commandType == "pointer")
                        PointerItemCssClass += " tb-item-selected";
                    if (commandType == "text tool")
                        TextItemCssClass += " tb-item-selected";
                    await removeSelectedToolbarItem(commandType).ConfigureAwait(true);
                }
            }
            Parent.DiagramPropertyPanel.PanelVisibility();
            Parent.DiagramContent.StateChanged();
        }
        public void DeleteData()
        {
            bool GroupAction = false;
            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            if ((diagram.SelectionSettings.Nodes.Count > 1 || diagram.SelectionSettings.Connectors.Count > 1 || ((diagram.SelectionSettings.Nodes.Count + diagram.SelectionSettings.Connectors.Count) > 1)))
            {
                GroupAction = true;
            }
            if (GroupAction)
            {
                diagram.StartGroupAction();
            }
            if (diagram.SelectionSettings.Nodes.Count != 0)
            {
                for (var i = diagram.SelectionSettings.Nodes.Count - 1; i >= 0; i--)
                {
                    var item = diagram.SelectionSettings.Nodes[i];

                    diagram.Nodes.Remove(item);
                }
            }
            if (diagram.SelectionSettings.Connectors.Count != 0)
            {
                for (var i = diagram.SelectionSettings.Connectors.Count - 1; i >= 0; i--)
                {
                    var item1 = diagram.SelectionSettings.Connectors[i];

                    diagram.Connectors.Remove(item1);
                }
            }
            if (GroupAction)
                diagram.EndGroupAction();
        }
        private async Task LockObject(bool isPreventPropertyChange = false)
        {
            bool isLock = false;
            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            for (var i = 0; i < diagram.SelectionSettings.Nodes.Count; i++)
            {
                var node = diagram.SelectionSettings.Nodes[i];
                if (node.Constraints.HasFlag(NodeConstraints.Default))
                {
                    if (!isPreventPropertyChange)
                    {
                        node.Constraints = node.Constraints & ~(NodeConstraints.Resize | NodeConstraints.Drag | NodeConstraints.Rotate);
                        node.Constraints = node.Constraints | NodeConstraints.ReadOnly;
                        if (node.Ports.Count > 0)
                        {
                            for (var k = 0; k < node.Ports.Count; k++)
                            {
                                var port = node.Ports[k];
                                port.Constraints = port.Constraints & ~(PortConstraints.Draw);
                            }
                        }
                        isLock = true;
                    }
                }
                else
                {
                    if (!isPreventPropertyChange)
                    {
                        node.Constraints = NodeConstraints.Default;
                        if (node.Ports.Count > 0)
                        {
                            for (var k = 0; k < node.Ports.Count; k++)
                            {
                                var port = node.Ports[k];
                                port.Constraints = port.Constraints | PortConstraints.Draw;
                            }
                        }
                    }
                    else
                        isLock = true;
                }
            }
            for (var j = 0; j < diagram.SelectionSettings.Connectors.Count; j++)
            {
                var connector = diagram.SelectionSettings.Connectors[j];
                if (connector.Constraints.HasFlag(ConnectorConstraints.Default))
                {
                    if (!isPreventPropertyChange)
                    {
                        connector.Constraints = (connector.Constraints & ~(ConnectorConstraints.DragSourceEnd
                | ConnectorConstraints.DragTargetEnd | ConnectorConstraints.DragSegmentThumb)) | ConnectorConstraints.ReadOnly;
                        isLock = true;
                    }
                }
                else
                {
                    if (!isPreventPropertyChange)
                    {
                        connector.Constraints = ConnectorConstraints.Default;
                    }
                    else
                        isLock = true;
                }
            }
            //isLock = (!isPreventPropertyChange)? isLock : !isLock;
            LockToolTip = isLock ? "UnLock" : "Lock";
            LockIcon = isLock ? "e-icons sf-icon-Unlock tb-icons" : "e-icons sf-icon-Lock tb-icons";
        }
        private async Task removeSelectedToolbarItem(string tool)
        {
#pragma warning disable CA1307 // Specify StringComparison

            if (DrawConnectorItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                DrawConnectorItemCssClass = DrawConnectorItemCssClass.Replace(" tb-item-selected", "");
            }
            if (DrawShapeItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                DrawShapeItemCssClass = DrawShapeItemCssClass.Replace(" tb-item-selected", "");
            }
            if (tool != "pan tool" && PanItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                PanItemCssClass = PanItemCssClass.Replace(" tb-item-selected", "");
            }
            if (tool != "pointer" && PointerItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                PointerItemCssClass = PointerItemCssClass.Replace(" tb-item-selected", "");
            }
            if (tool != "text tool" && TextItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                TextItemCssClass = TextItemCssClass.Replace(" tb-item-selected", "");
            }
            await removeSelectedToolbarItems(tool);
            StateHasChanged();
#pragma warning restore CA1307 // Specify StringComparison

        }
        #endregion

        public async Task removeSelectedToolbarItems(string tool)
        {
            string shape = "tb-item-selected";
            if (ConnectorItem.Contains(shape))
            {
                int first = ConnectorItem.IndexOf(shape);
                ConnectorItem = ConnectorItem.Remove(first);
            }
            if (ShapeItem.Contains(shape))
            {
                int second = ShapeItem.IndexOf(shape);
                ShapeItem = ShapeItem.Remove(second);
            }
            if (tool == "shape")
            {
                ShapeItem += " tb-item-selected";
            }
            else if (tool == "connector")
            {
                ConnectorItem += " tb-item-selected";
            }
        }
        public void SingleSelectionToolbarItems()
        {
            bool diagram = Parent.DiagramContent.diagramSelected;
            ShowFill = diagram ? false : !ShowFill ? true : ShowFill;
            ShowStroke = diagram ? false : !ShowStroke ? true : ShowStroke;
            ShowStyleSeparator = diagram ? false : !ShowStyleSeparator ? true : ShowStyleSeparator;
            ShowOrder = diagram ? false : !ShowOrder ? true : ShowOrder;
            ShowOrderSeparator = diagram ? false : !ShowOrderSeparator ? true : ShowOrderSeparator;
            ShowLock = diagram ? false : !ShowLock ? true : ShowLock;
            ShowDelete = diagram ? false : !ShowDelete ? true : ShowDelete;

            ShowGroup = ShowGroup ? false : ShowGroup;
            ShowUnGroup = ShowUnGroup ? false : ShowUnGroup;
            ShowGroupSeparator = ShowGroupSeparator ? false : ShowGroupSeparator;
            ShowAlignLeft = ShowAlignLeft ? false : ShowAlignLeft;
            ShowAlignCenter = ShowAlignCenter ? false : ShowAlignCenter;
            ShowAlignRight = ShowAlignRight ? false : ShowAlignRight;
            ShowAlignTop = ShowAlignTop ? false : ShowAlignTop;
            ShowAlignBottom = ShowAlignBottom ? false : ShowAlignBottom;
            ShowAlignVertical = ShowAlignVertical ? false : ShowAlignVertical;
            ShowAlignHorizontal = ShowAlignHorizontal ? false : ShowAlignHorizontal;
            ShowAlignSeparator = ShowAlignSeparator ? false : ShowAlignSeparator;
            if (Parent.DiagramContent.Diagram.SelectionSettings.Nodes.Count > 0 && Parent.DiagramContent.Diagram.SelectionSettings.Nodes[0] is NodeGroup)
                ShowGroup = diagram ? false : !ShowGroup ? true : ShowGroup;
            if (Parent.DiagramContent.Diagram.SelectionSettings.Nodes.Count > 0 || Parent.DiagramContent.Diagram.SelectionSettings.Connectors.Count > 0)
            {
                _ = LockObject(true).ConfigureAwait(true);
            }
            StateHasChanged();
        }
        public void MutipleSelectionToolbarItems()
        {
            bool diagram = Parent.DiagramContent.diagramSelected;
            SingleSelectionToolbarItems();
            ShowGroup = diagram ? false : !ShowGroup ? true : ShowGroup;
            ShowGroupSeparator = diagram ? false : !ShowGroupSeparator ? true : ShowGroupSeparator;
            ShowAlignLeft = diagram ? false : !ShowAlignLeft ? true : ShowAlignLeft;
            ShowAlignCenter = diagram ? false : !ShowAlignCenter ? true : ShowAlignCenter;
            ShowAlignRight = diagram ? false : !ShowAlignRight ? true : ShowAlignRight;
            ShowAlignTop = diagram ? false : !ShowAlignTop ? true : ShowAlignTop;
            ShowAlignBottom = diagram ? false : !ShowAlignBottom ? true : ShowAlignBottom;
            ShowAlignVertical = diagram ? false : !ShowAlignVertical ? true : ShowAlignVertical;
            ShowAlignHorizontal = diagram ? false : !ShowAlignHorizontal ? true : ShowAlignHorizontal;
            ShowAlignSeparator = diagram ? false : !ShowAlignSeparator ? true : ShowAlignSeparator;
            StateHasChanged();
        }
        public void DiagramSelectionToolbarItems()
        {
            SingleSelectionToolbarItems();
        }
        public void DiagramZoomValueChange()
        {
            ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
            StateHasChanged();
        }

        #region public methods

        public async Task EnableToolbarItems<T>(T obj, string eventname)
        {

            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            ObservableCollection<NodeBase> collection = new ObservableCollection<NodeBase>();
            if (eventname == "selectionchange")
            {
                foreach (NodeBase node in obj as ObservableCollection<IDiagramObject>)
                {
                    Node val = node as Node;
                    collection.Add(node);
                }
                UtilityMethods_enableToolbarItems(collection);
            }

            if (eventname == "historychange")
            {
                RemoveUndo();
                RemoveRedo();
                if (diagram.HistoryManager.CanUndo)
                {
                    this.Parent.DiagramContent.IsUndo = diagram.HistoryManager.CanUndo;
                    this.Parent.DiagramContent.IsRedo = diagram.HistoryManager.CanRedo;
                    toolbarClassName += " db-undo";
                }
                if (diagram.HistoryManager.CanRedo)
                {
                    this.Parent.DiagramContent.IsRedo = diagram.HistoryManager.CanRedo;
                    this.Parent.DiagramContent.IsUndo = diagram.HistoryManager.CanUndo;
                    toolbarClassName += " db-redo";
                }
                StateHasChanged();
            }
        }
        public void RemoveUndo()
        {
            string undo = "undo";
            if (toolbarClassName.Contains(undo))
            {
                int first = toolbarClassName.IndexOf(undo);
                toolbarClassName = toolbarClassName.Remove(first - 4, 8);
            }
        }
        public void RemoveRedo()
        {
            string redo = "redo";
            if (toolbarClassName.Contains(redo))
            {
                int first = toolbarClassName.IndexOf(redo);
                toolbarClassName = toolbarClassName.Remove(first - 4, 8);
            }
        }
        public void UtilityMethods_enableToolbarItems(ObservableCollection<NodeBase> SelectedObjects)
        {
            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            removeClassElement();
            if (this.Parent.DiagramContent.IsUndo)
            {
                toolbarClassName += " db-undo";
            }
            if (this.Parent.DiagramContent.IsRedo)
            {
                toolbarClassName += " db-redo";
            }
            if (SelectedObjects.Count == 1 && !Parent.MindMapPropertyPanel.IsMindMap && !Parent.OrgChartPropertyPanel.IsOrgChart)
            {
                toolbarClassName = toolbarClassName + " db-select";

                if (SelectedObjects[0] is NodeGroup)
                {
                    if ((SelectedObjects[0] as NodeGroup).Children.Length > 2)
                    {
                        toolbarClassName = toolbarClassName + " db-select db-double db-multiple db-node db-group"; ;
                    }
                    else
                    {
                        toolbarClassName = toolbarClassName + " db-select db-double db-node db-group";
                    }
                }
                else
                {
                    if (SelectedObjects[0] is Connector)
                    {
                        toolbarClassName = toolbarClassName + " db-select";
                    }
                    else
                    {
                        toolbarClassName = toolbarClassName + " db-select db-node";
                    }
                }
            }
            else if (SelectedObjects.Count == 2 && !Parent.MindMapPropertyPanel.IsMindMap)
            {
                if (!((SelectedObjects[0] is Node) && (SelectedObjects[1] is Node)))
                {
                    fill = "tb-item-start tb-item-fill";
                }
                GroupIcon = "e-icons sf-icon-Group tb-icons";
                GroupToolTip = "Group";
                toolbarClassName = toolbarClassName + " db-select db-double";
            }
            else if (SelectedObjects.Count > 2 && !Parent.MindMapPropertyPanel.IsMindMap)
            {
                for (int i = 0; i <= SelectedObjects.Count - 1; i++)
                {
                    if (SelectedObjects[i] is Connector)
                    {
                        fill = "tb-item-start tb-item-fill";
                    }
                }
                GroupIcon = "e-icons sf-icon-Group tb-icons";
                GroupToolTip = "Group";
                toolbarClassName = toolbarClassName + " db-select db-double db-multiple";
            }
            else if (SelectedObjects.Count > 0 && Parent.MindMapPropertyPanel.IsMindMap)
            {
                toolbarClassName = toolbarClassName + " db-child-sibling";
                addSiblingCssName = SelectedObjects[0].ID == "rootNode" ? "tb-item-start tb-item-sibling" : "tb-item-start tb-item-child";
            }
            else if (SelectedObjects.Count > 0 && Parent.OrgChartPropertyPanel.IsOrgChart)
            {
                toolbarClassName = toolbarClassName + " db-child-sibling";
                addSiblingCssName = SelectedObjects[0].ID == "rootNode" ? "tb-item-start tb-item-sibling" : "tb-item-start tb-item-child";
            }
            if (SelectedObjects.Count > 1)
                StateHasChanged();
        }
        public async Task HidePropertyContainer()
        {
            int index = Parent.MenuBar.WindowMenuItems.FindIndex(item => item.Text == "Show Properties");
            Parent.MenuBar.WindowMenuItems[index].IconCss = Parent.MenuBar.WindowMenuItems[index].IconCss == "sf-icon-Selection" ? "sf-icon-Remove" : "sf-icon-Selection";
            HideButtonBackground = (Parent.MenuBar.WindowMenuItems[index].IconCss == "sf-icon-Selection") ? "#0078d4" : "rgb(227, 227, 227)";
            HideButtonCss = (Parent.MenuBar.WindowMenuItems[index].IconCss == "sf-icon-Selection") ? "db-toolbar-hide-btn tb-property-open" : "db-toolbar-hide-btn tb-property-close";
            await this.HideElements("hide-properties");
            if (Parent.MindMapPropertyPanel.IsMindMap|| Parent.OrgChartPropertyPanel.IsOrgChart)
            {
                await Task.Delay(800);
                object bounds = await jsRuntime.InvokeAsync<object>("getViewportBounds").ConfigureAwait(true);
                if (bounds != null)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };
                    DiagramSize dataObj = System.Text.Json.JsonSerializer.Deserialize<DiagramSize>(bounds.ToString(), options);
                    if (dataObj != null)
                    {
                        Parent.DiagramContent.Diagram.BeginUpdate();
                        Parent.DiagramContent.Diagram.Width = dataObj.Width + "px";
                        Parent.DiagramContent.Diagram.Height = dataObj.Height + "px";
                        Parent.DiagramContent.Diagram.EndUpdate();
                    }

                }
                await Parent.DiagramContent.Diagram.DoLayout();
            }
            Parent.MenuBar.StateChanged();
        }
        public void removeClassElement()
        {
            string space = " ";
            if (toolbarClassName.Contains(space))
            {
                int first = toolbarClassName.IndexOf(space);
                if (first != 0)
                {
                    toolbarClassName = toolbarClassName.Remove(20);
                }
            }
            fill = "tb-item-start tb-item-fill";
            stroke = "tb-item-end tb-item-stroke";
        }
        private async Task HideToolBar()
        {
#pragma warning disable CA1307 // Specify StringComparison
            if (MenuHideIconCss.Contains("sf-icon-Collapse"))
#pragma warning restore CA1307 // Specify StringComparison
            {
                MenuHideIconCss = "sf-icon-DownArrow tb-icons";
            }
            else
            {
                MenuHideIconCss = "sf-icon-Collapse tb-icons";
            }
            await jsRuntime.InvokeAsync<object>("hideMenubar").ConfigureAwait(true);
        }
        public async Task HideElements(string eventname)
        {
            await jsRuntime.InvokeAsync<object>("UtilityMethods_hideElements", eventname).ConfigureAwait(true);
        }
        #endregion
    }
}
