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
            ShapeItem = "e-control e-dropdown-btn e-lib e-btn e-icon-btn tb-item-selected";
            await removeSelectedToolbarItem("shape");
        }
        private async Task DrawConnectorChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            Parent.DiagramContent.DrawingObject(args);
            Parent.DiagramContent.UpdateContinousDrawTool();
            ConnectorItem = "e-control e-dropdown-btn e-lib e-btn e-icon-btn tb-item-selected";
            diagram.ClearSelection();
            await removeSelectedToolbarItem("connector");
        }
        private async Task DrawZoomChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            if (ZoomItemDropdownContent != args.Item.Text)
            {
                var diagram = Parent.DiagramContent.Diagram;
                diagram.BeginUpdate();
                if (args.Item.Text == "Custom")
                {

                }
                else if (args.Item.Text == "Fit To Screen")
                {
                    ZoomItemDropdownContent = "Fit ...";
                }
                else
                {
                    var currentZoom = Parent.DiagramContent.CurrentZoom;
                    ZoomItemDropdownContent = args.Item.Text;
#pragma warning disable CA1305 // Specify IFormatProvider
                    Parent.DiagramContent.CurrentZoom = double.Parse(args.Item.Text.Remove(args.Item.Text.Length - 1, 1)) / 100;
#pragma warning restore CA1305 // Specify IFormatProvider
                }
                diagram.EndUpdate();
            }
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
                case "zoom in(ctrl + +)":
                    Parent.DiagramContent.DiagramZoomIn();
                    if (Parent.DiagramContent.CurrentZoom <= 30)
                    {
                        ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
                    }
                    break;
                case "zoom out(ctrl + -)":
                    Parent.DiagramContent.DiagramZoomOut();
                    if (Parent.DiagramContent.CurrentZoom >= 0.25)
                    {
                        ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
                    }
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
                case "delete":
                    diagram.BeginUpdate();
                    DeleteData();
                    toolbarClassName = "db-toolbar-container db-undo";
                    diagram.EndUpdate();
                    break;
                case "lock":
                    await LockObject().ConfigureAwait(true);
                    break;
                case "group":
                    Group();
                    break;
                case "ungroup":
                    Ungroup();
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
                    await removeSelectedToolbarItem(commandType).ConfigureAwait(true);
                }
            }
        }
        private void Group()
        {
            Parent.DiagramContent.Diagram.Group();
        }

        private void Ungroup()
        {
            Parent.DiagramContent.Diagram.Ungroup();
            Parent.DiagramContent.Diagram.ClearSelection();
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
        private async Task LockObject()
        {
            SfDiagramComponent diagram = Parent.DiagramContent.Diagram;
            for (var i = 0; i < diagram.SelectionSettings.Nodes.Count; i++)
            {
                var node = diagram.SelectionSettings.Nodes[i];
                if (node.Constraints.HasFlag(NodeConstraints.Default))
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
                }
                else
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
            }
            for (var j = 0; j < diagram.SelectionSettings.Connectors.Count; j++)
            {
                var connector = diagram.SelectionSettings.Connectors[j];
                if (connector.Constraints.HasFlag(ConnectorConstraints.Default))
                {
                    connector.Constraints = (connector.Constraints & ~(ConnectorConstraints.DragSourceEnd
                | ConnectorConstraints.DragTargetEnd | ConnectorConstraints.DragSegmentThumb)) | ConnectorConstraints.ReadOnly;
                }
                else
                {
                    connector.Constraints = ConnectorConstraints.Default;
                }
            }
        }
        private async Task removeSelectedToolbarItem(string tool)
        {
#pragma warning disable CA1307 // Specify StringComparison

            if ((ConnectorItem.Contains("tb-item-selected")) && tool != "connector")
            {
                ConnectorItem = "e-control e-dropdown-btn e-lib e-btn e-icon-btn";
            }
            if ((PanItemCssClass.Contains("tb-item-selected")) && tool != "pan tool")
            {
                PanItemCssClass = PanItemCssClass.Replace(" tb-item-selected", "");
            }
            if ((PointerItemCssClass.Contains("tb-item-selected")) && tool != "pointer")
            {
                PointerItemCssClass = PointerItemCssClass.Replace(" tb-item-selected", "");
            }
            if ((ShapeItem.Contains("tb-item-selected")) && tool != "shape")
            {
                ShapeItem = "e-control e-dropdown-btn e-lib e-btn e-icon-btn";
            }
            StateHasChanged();
#pragma warning restore CA1307 // Specify StringComparison

        }
        #endregion

        #region public methods

        public void DiagramZoomValueChange()
        {
            if (Parent.DiagramContent.CurrentZoom >= 0.25 && Parent.DiagramContent.CurrentZoom <= 30)
            {
                ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
            }
            StateHasChanged();
        }

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
            if (SelectedObjects.Count == 1)
            {
                toolbarClassName = toolbarClassName + " db-select";

                if (SelectedObjects[0] is NodeGroup)
                {
                    if ((SelectedObjects[0] as NodeGroup).Children.Length > 2)
                    {
                        toolbarClassName = toolbarClassName + " db-select db-double db-multiple db-node db-group";
                        fill = fill + "-multiple-nodes";
                        stroke = stroke + "-multiple-objects";
                    }
                    else
                    {
                        toolbarClassName = toolbarClassName + " db-select db-double db-node db-group";
                        fill = fill + "-multiple-nodes";
                        stroke = stroke + "-multiple-objects";
                    }
                }
                else
                {
                    if (SelectedObjects[0] is Connector)
                    {
                        stroke = stroke + "-multiple-objects";
                        toolbarClassName = toolbarClassName + " db-select";
                    }
                    else
                    {
                        toolbarClassName = toolbarClassName + " db-select db-node";
                    }
                }
            }
            else if (SelectedObjects.Count == 2)
            {
                if ((SelectedObjects[0] is Node) && (SelectedObjects[1] is Node))
                {
                    fill = fill + "-multiple-nodes";
                }
                else
                {
                    fill = "tb-item-start tb-item-fill";
                    stroke = stroke + "-multiple-objects";
                }
                toolbarClassName = toolbarClassName + " db-select db-double";
            }
            else if (SelectedObjects.Count > 2)
            {
                for (int i = 0; i <= SelectedObjects.Count - 1; i++)
                {
                    if ((SelectedObjects[i] is Node))
                    {
                        fill = fill + "-multiple-nodes";
                    }
                    if (SelectedObjects[i] is Connector)
                    {
                        fill = "tb-item-start tb-item-fill";
                        stroke = stroke + "-multiple-objects";
                    }
                }

                toolbarClassName = toolbarClassName + " db-select db-double db-multiple";

            }
            StateHasChanged();
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
                MenuHideIconCss = "sf-icon-DownArrow2 tb-icons";
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
