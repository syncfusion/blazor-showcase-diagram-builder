// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace DiagramBuilder
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using DiagramBuilder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\fork\DB-Source-Fork\Server-side\_Imports.razor"
using DiagramBuilder.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\fork\DB-Source-Fork\Server-side\Pages\DiagramOpenDialog.razor"
using Syncfusion.Blazor.Popups;

#line default
#line hidden
#nullable disable
    public partial class DiagramOpenDialog : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 63 "D:\fork\DB-Source-Fork\Server-side\Pages\DiagramOpenDialog.razor"
       

    internal DiagramMain Parent;

    [Inject]
    protected IJSRuntime jsRuntime { get; set; }

    public bool IsOpenDialogVisible { get; set; } = true;
    string Value;

    protected override void OnInitialized()
    {
        this.Value = "Flow Diagram";
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            this.IsOpenDialogVisible = true;
            StateHasChanged();

        }
    }

    public void StateChanged()
    {
        this.StateChanged();
    }

    public void ShowFlowChartTemplates()
    {
        this.Value = "Flow Diagram";
        StateHasChanged();
    }
    public void ShowMindMapTemplates()
    {
        this.Value = "Mind Map";
        StateHasChanged();
    }
    public void ShowOrgChartTemplates()
    {
        this.Value = "Org Chart";
        StateHasChanged();
    }

    public void BlankFlowDiagram()
    {
        IsOpenDialogVisible = false;
        StateHasChanged();
    }

    public async Task BlankMindMapDiagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/mindmap_images/BlankDiagram.json");
        CurrentDiagramVisibility("mindmap-diagram");
        await Task.Delay(2500);
    }

    public void BlankOrgChartDiagram()
    {
        this.Value = "Org Chart";
        StateHasChanged();
    }

    public async Task CreditProcessDiagram()
    {
        await Parent.DiagramContent.CreditProcessDiagram();
        IsOpenDialogVisible = false;
    }

    public async Task BusinessPlanningDiagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/mindmap_images/BusinessPlanning.json");
        CurrentDiagramVisibility("mindmap-diagram");
        await Task.Delay(2500);
    }

    public void OrgRenderingStyle1Diagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/orgchart_images/OrgTemplateStyle1.json");
    }

    public void BankTellerFlowDiagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/flowchart_Images/BankingTellerProcess.json");
        jsRuntime.InvokeAsync<string>("diagramNameChange", "Banking Teller Process Flow", true);
    }

    public async Task TQMDiagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/mindmap_images/TQM.json");
        CurrentDiagramVisibility("mindmap-diagram");
        await Task.Delay(2500);
    }

    public void OrgRenderingStyle2Diagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/orgchart_images/OrgTemplateStyle2.json");
    }

    public void DeveloperWorkFlowDiagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/flowchart_Images/Developer_Workflow.json");
        jsRuntime.InvokeAsync<string>("diagramNameChange", "Agile's Developer Workflow", true);
    }

    public async Task SoftwareLifeCycleDiagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/mindmap_images/SoftwareDevelopmentLifeCycle.json");
        CurrentDiagramVisibility("mindmap-diagram");
        await Task.Delay(2500);
    }

    public void OrgRenderingStyle3Diagram()
    {
        LoadTemplate(@"./wwwroot/assets/dbstyle/orgchart_images/OrgTemplateStyle3.json");
    }

    public void LoadTemplate(string fileLocation)
    {
        string json = System.IO.File.ReadAllText(fileLocation);
        json = json.Replace(System.Environment.NewLine, string.Empty);
        this.Parent.DiagramContent.LoadNewDiagram(json.ToString());
        IsOpenDialogVisible = false;
        StateHasChanged();
    }

    public void CurrentDiagramVisibility(string DiagramName)
    {
        Parent.PropertyPanelClassName += " " + DiagramName;
        Parent.DiagramBuilderClassName += " hide-palette custom-diagram";
        Parent.DiagramContent.DiagramContainerClassName += " " + DiagramName;
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591