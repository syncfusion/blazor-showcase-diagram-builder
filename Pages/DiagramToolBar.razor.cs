using Syncfusion.Blazor.Diagrams;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;

namespace DiagramBuilder
{
    public partial class DiagramToolBar
    {
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        #region events
      
        private async Task DrawShapeChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            if (args.Item.Text == "Rectangle")
            {
                Parent.DiagramContent.DiagramDrawingObject = new DiagramNode() { Shape = new DiagramShape() { Type = Shapes.Basic, BasicShape = BasicShapes.Rectangle }, Style = new NodeShapeStyle() { StrokeWidth = 2 } };
            }
            else if (args.Item.Text == "Ellipse")
            {
                Parent.DiagramContent.DiagramDrawingObject = new DiagramNode() { Shape = new DiagramShape() { Type = Shapes.Basic, BasicShape = BasicShapes.Ellipse }, Style = new NodeShapeStyle() { StrokeWidth = 2 } };
            }
            else if (args.Item.Text == "Polygon")
            {
                Parent.DiagramContent.DiagramDrawingObject = new DiagramNode() { Shape = new DiagramShape() { Type = Shapes.Basic, BasicShape = BasicShapes.Polygon }, Style = new NodeShapeStyle() { StrokeWidth = 2 } };
            }
            Parent.DiagramContent.DiagramTool = DiagramTools.ContinuousDraw;
            await diagram.DataBind().ConfigureAwait(true);
            await removeSelectedToolbarItem("shape").ConfigureAwait(true);
            Parent.DiagramContent.StateChanged();
            //document.getElementById("btnDrawShape").classList.add("tb-item-selected");
        }
        private async Task DrawConnectorChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            if (args.Item.Text == "Straight Line")
            {
                Parent.DiagramContent.DiagramDrawingObject = new DiagramConnector() { Type = Segments.Straight, Constraints = ConnectorConstraints.Default, Style = new ConnectorShapeStyle() { StrokeWidth = 2 } };
            }
            else if (args.Item.Text == "Orthogonal Line")
            {
                Parent.DiagramContent.DiagramDrawingObject = new DiagramConnector() { Type = Segments.Orthogonal, Constraints = ConnectorConstraints.Default, Style = new ConnectorShapeStyle() { StrokeWidth = 2 } };
            }
            else if (args.Item.Text == "Bezier")
            {
                Parent.DiagramContent.DiagramDrawingObject = new DiagramConnector() { Type = Segments.Bezier, Constraints = ConnectorConstraints.Default, Style = new ConnectorShapeStyle() { StrokeWidth = 2 } };
            }
            Parent.DiagramContent.DiagramTool = DiagramTools.ContinuousDraw;
            await diagram.ClearSelection().ConfigureAwait(true);
            await removeSelectedToolbarItem("connector").ConfigureAwait(true);
            await diagram.DataBind().ConfigureAwait(true);
            Parent.DiagramContent.StateChanged();
            //document.getElementById("btnDrawConnector').classList.add('tb-item-selected');
        }
        private async Task OrderCommandsChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            if (args.Item.Text == "Send To Back")
            {
                await diagram.SendToBack().ConfigureAwait(true);
            }
            else if (args.Item.Text == "Bring To Front")
            {
                await diagram.BringToFront().ConfigureAwait(true);
            }
            else if (args.Item.Text == "Bring Forward")
            {
                //selectedItem.isModified = true;
                await diagram.MoveForward().ConfigureAwait(true);
            }
            else if (args.Item.Text == "Send Backward")
            {
                //selectedItem.isModified = true;
                await diagram.SendBackward().ConfigureAwait(true);
            }
        }
        private async Task DrawZoomChange(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
        {
            if (ZoomItemDropdownContent != args.Item.Text)
            {
                var diagram = Parent.DiagramContent.Diagram;
                if (args.Item.Text == "Custom")
                {

                }
                else if (args.Item.Text == "Fit To Screen")
                {
                    ZoomItemDropdownContent = "Fit ...";
                    IFitOptions fitoption = new IFitOptions()
                    {
                        Mode = FitModes.Page,
                        Region = DiagramRegions.Content,
                        Margin = new DiagramMargin() { Left = 0, Top = 0, Right = 0, Bottom = 0 }
                    };
                    //Parent.DiagramContent.DigramFitOption = fitoption;
                    await Parent.DiagramContent.Diagram.FitToPage(fitoption).ConfigureAwait(true);
                }
                else
                {
                    var currentZoom = Parent.DiagramContent.CurrentZoom;
                    ZoomOptions zoom = new ZoomOptions();
                    switch (args.Item.Text)
                    {
                        case "400%":
                            zoom.ZoomFactor = (4 / currentZoom) - 1;
                            break;
                        case "300%":
                            zoom.ZoomFactor = (3 / currentZoom) - 1;
                            break;
                        case "200%":
                            zoom.ZoomFactor = (2 / currentZoom) - 1;
                            break;
                        case "150%":
                            zoom.ZoomFactor = (1.5 / currentZoom) - 1;
                            break;
                        case "100%":
                            zoom.ZoomFactor = (1 / currentZoom) - 1;
                            break;
                        case "75%":
                            zoom.ZoomFactor = (0.75 / currentZoom) - 1;
                            break;
                        case "50%":
                            zoom.ZoomFactor = (0.5 / currentZoom) - 1;
                            break;
                        case "25%":
                            zoom.ZoomFactor = (0.25 / currentZoom) - 1;
                            break;
                    }
                    ZoomItemDropdownContent = args.Item.Text;
#pragma warning disable CA1305 // Specify IFormatProvider
                    Parent.DiagramContent.CurrentZoom = double.Parse(args.Item.Text.Remove(args.Item.Text.Length - 1, 1)) / 100;
#pragma warning restore CA1305 // Specify IFormatProvider
                    await diagram.ZoomTo(zoom).ConfigureAwait(true);
                }
            }
            Parent.DiagramContent.StateChanged();
        }
        private async Task ToolbarEditorClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            var diagram = Parent.DiagramContent.Diagram;
            var commandType =  args.Item.TooltipText.ToLower(new CultureInfo("en-US"));
            switch (commandType)
            {
                case "undo":
                    await diagram.Undo().ConfigureAwait(true);
                    break;
                case "redo":
                    await diagram.Redo().ConfigureAwait(true);
                    break;
                case "zoom in(ctrl + +)":
                    await Parent.DiagramContent.DiagramZoomIn().ConfigureAwait(true);
                    ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%";
                    break;
                case "zoom out(ctrl + -)":
                    await Parent.DiagramContent.DiagramZoomOut().ConfigureAwait(true);
                    ZoomItemDropdownContent = FormattableString.Invariant($"{Math.Round(Parent.DiagramContent.CurrentZoom * 100)}") + "%"; 
                    break;
                case "pan tool":
                    Parent.DiagramContent.DiagramTool = DiagramTools.ZoomPan;
                    await diagram.ClearSelection().ConfigureAwait(true);
                    await jsRuntime.InvokeAsync<object>("UtilityMethods_objectTypeChange", "diagram").ConfigureAwait(true);
                    break;
                case "pointer":
                    Parent.DiagramContent.DiagramDrawingObject = new object() { };
                    Parent.DiagramContent.DiagramTool = DiagramTools.SingleSelect | DiagramTools.MultipleSelect;
                    break;
                case "text tool":
                    Parent.DiagramContent.DiagramDrawingObject = new DiagramNode() { Shape = new DiagramShape() { Type = Shapes.Text } };
                    Parent.DiagramContent.DiagramTool = DiagramTools.ContinuousDraw;
                    await diagram.DataBind().ConfigureAwait(true);
                    break;
                case "delete":
                    await DeleteData().ConfigureAwait(true);
                    break;
                case "lock":
                    await LockObject().ConfigureAwait(true);
                    break;
                case "group":
                    await Group().ConfigureAwait(true);
                    break;
                case "ungroup":
                    await Ungroup().ConfigureAwait(true);
                    break;
                case "align left":
                case "align right":
                case "align top":
                case "align bottom":
                case "align middle":
                case "align center":
                    //selectedItem.isModified = true;
#pragma warning disable CA1307 // Specify StringComparison
                    string alignType = commandType.Replace("align ", "");
#pragma warning restore CA1307 // Specify StringComparison
                    alignType = char.ToUpper(alignType[0], new CultureInfo("en-US")) + alignType.Substring(1);
                    await diagram.Align((AlignmentOptions)Enum.Parse(typeof(AlignmentOptions), alignType)).ConfigureAwait(true);
                    break;
                case "distribute objects vertically":
                    await Distribute("RightToLeft").ConfigureAwait(true);
                    break;
                case "distribute objects horizontally":
                    await Distribute("BottomToTop").ConfigureAwait(true);
                    break;
                case "show layers":
                    await ShowLayers().ConfigureAwait(true);
                    break;
                case "themes":
                    await ShowLayers().ConfigureAwait(true);
                    break;
            }
            Parent.DiagramContent.StateChanged();
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
                    await removeSelectedToolbarItem("").ConfigureAwait(true);
                }
            }
        }
        public async Task ShowLayers()
        {
            LayerItemDialog.Parent = this;
            await LayerItemDialog.Show().ConfigureAwait(true);
        }
        private async Task Distribute(string value)
        {
            await Parent.DiagramContent.Diagram.Distribute((DistributeOptions)Enum.Parse(typeof(DistributeOptions), value)).ConfigureAwait(true);
        }
        private async Task Group()
        {
            await Parent.DiagramContent.Diagram.Group().ConfigureAwait(true);
        }

        private async Task Ungroup()
        {
            //selectedItem.isModified = true;
            await Parent.DiagramContent.Diagram.UnGroup().ConfigureAwait(true);
        }
        private async Task DeleteData()
        {
            await Parent.DiagramContent.Diagram.Remove().ConfigureAwait(true);
        }
        private async Task LockObject()
        {
            //selectedItem.isModified = true;
            SfDiagram diagram = Parent.DiagramContent.Diagram;
            for (var i = 0; i < diagram.SelectedItems.Nodes.Count; i++)
            {
                var node = diagram.SelectedItems.Nodes[i];
                if (node.Constraints.HasFlag(NodeConstraints.Drag))
                {
                    node.Constraints = NodeConstraints.PointerEvents | NodeConstraints.Select;
                }
                else
                {
                    node.Constraints = NodeConstraints.Default;
                }
            }
            for (var j = 0; j < diagram.SelectedItems.Connectors.Count; j++)
            {
                var connector = diagram.SelectedItems.Connectors[j];
                if (connector.Constraints.HasFlag(ConnectorConstraints.Drag))
                {
                    connector.Constraints = ConnectorConstraints.PointerEvents | ConnectorConstraints.Select;
                }
                else
                {
                    connector.Constraints = ConnectorConstraints.Default;
                }
            }
            await diagram.DataBind().ConfigureAwait(true);
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
            if (PanItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                PanItemCssClass = PanItemCssClass.Replace(" tb-item-selected", "");
            }
            if (PointerItemCssClass.IndexOf("tb-item-selected") != -1)
            {
                PointerItemCssClass = PointerItemCssClass.Replace(" tb-item-selected", "");
            }
            if (TextItemCssClass.IndexOf("tb-item-selected") != -1)
            {

                TextItemCssClass = TextItemCssClass.Replace(" tb-item-selected", "");
            }
            await jsRuntime.InvokeAsync<object>("removeSelectedToolbarItem", tool).ConfigureAwait(true);
            StateHasChanged();
#pragma warning restore CA1307 // Specify StringComparison

        }
        #endregion

        #region public methods

        public async Task EnableToolbarItems<T>(T obj, string eventname)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
            };

            ObservableCollection<object> collection = new ObservableCollection<object>();
            if (eventname == "selectionchange")
            {
                foreach (DiagramNode node in (obj as DiagramEventObjectCollection).Nodes)
                {
                    collection.Add(node);
                }
                foreach (DiagramConnector connector in (obj as DiagramEventObjectCollection).Connectors)
                {
                    collection.Add(connector);
                }

                object jsonobj = JsonConvert.SerializeObject(collection, jsonSerializerSettings);
                await jsRuntime.InvokeAsync<object>("UtilityMethods_enableToolbarItems", jsonobj).ConfigureAwait(true);
            }
            if(eventname == "historychange")
            {
                object jsonobj = JsonConvert.SerializeObject(obj, jsonSerializerSettings);
                System.Collections.Generic.Dictionary<string, bool> data = await jsRuntime.InvokeAsync<System.Collections.Generic.Dictionary<string, bool>>("DiagramClientSideEvents_historyChange", jsonobj).ConfigureAwait(true);
                this.Parent.DiagramContent.IsUndo = data["undo"];
                this.Parent.DiagramContent.IsRedo = data["redo"];
            }
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

        public void StateChange()
        {
            StateHasChanged();
        }

        #endregion
    }
}
