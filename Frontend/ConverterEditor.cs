using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{

    public string GenerateEditorRazorClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating Editor Razor class for {o.TableName}");

        var sb = new StringBuilder();

        sb.AppendLine($"@page \"/config-editor-{o.TableName.ToLower()}\"");
        sb.AppendLine($"@using {o.UiNameSpace}.Features.Config.Services");
        sb.AppendLine($"@using {o.UiNameSpace}.Features.Config.Models");
        sb.AppendLine("@using Microsoft.FluentUI.AspNetCore.Components");
        sb.AppendLine("@using Syncfusion.Blazor");
        sb.AppendLine("@using Syncfusion.Blazor.Buttons");
        sb.AppendLine("@using Syncfusion.Blazor.Grids");
        sb.AppendLine("@using Syncfusion.Blazor.Navigations");
        sb.AppendLine("@using Syncfusion.Blazor.Notifications");

        sb.AppendLine("");
        sb.AppendLine($"@inject I{o.EntityName}Service {o.EntityName}Service");
        sb.AppendLine($"@inject IToastService ToastService");
        sb.AppendLine("");

        sb.AppendLine("<div>");
        sb.AppendLine($"<SfGrid ID=\"Grid\" DataSource=@_{o.TableName.ToLower()} TValue=\"{o.EntityName}\"");
        sb.AppendLine("        @ref=\"DefaultGrid\"");
        sb.AppendLine("        AllowPaging=\"true\" AllowSelection=\"true\" AllowFiltering=\"true\" AllowSorting=\"true\" AllowExcelExport=\"true\"");
        sb.AppendLine("        AllowResizing=\"true\" AllowReordering=\"true\" Height=\"100%\" RowHeight=\"30\">");
        sb.AppendLine("");
        sb.AppendLine("    <GridEvents RowUpdated=\"RowUpdatedHandler\"");
        sb.AppendLine("                RowCreated=\"RowCreatedHandler\"");
        sb.AppendLine("                RowDeleted=\"RowDeletedHandler\"");
        sb.AppendLine("                RowSelected=\"RowSelectHandler\"");
        sb.AppendLine($"                TValue=\"{o.EntityName}\">");
        sb.AppendLine("    </GridEvents>");
        sb.AppendLine("");
        sb.AppendLine("    <GridEditSettings AllowAdding=\"true\" AllowEditing=\"true\" AllowDeleting=\"true\" Mode=\"EditMode.Normal\"></GridEditSettings>");
        sb.AppendLine("    <GridFilterSettings Type=\"Syncfusion.Blazor.Grids.FilterType.Excel\"></GridFilterSettings>");
        sb.AppendLine("    <GridPageSettings PageSize=\"20\" PageSizes=\"true\"></GridPageSettings>");
        sb.AppendLine("");
        sb.AppendLine("    <SfToolbar>");
        sb.AppendLine("        <ToolbarItems>");
        sb.AppendLine("            <ToolbarItem Type=\"ItemType.Input\">");
        sb.AppendLine("                <Template>");
        sb.AppendLine("                    <div class=\"image\">");
        sb.AppendLine("");
        sb.AppendLine("                        <FluentButton OnClick=\"AddButton\">");
        sb.AppendLine("                            <FluentIcon Value=\"@(new Icons.Regular.Size20.Add())\" Color=\"Color.Neutral\" Slot=\"start\" />");
        sb.AppendLine("                            Add");
        sb.AppendLine("                        </FluentButton>");
        sb.AppendLine("                        <FluentButton OnClick=\"EditButton\" Disabled=@EditDisabled>");
        sb.AppendLine("                            <FluentIcon Value=\"@(new Icons.Regular.Size20.Edit())\" Color=\"Color.Neutral\" Slot=\"start\" />");
        sb.AppendLine("                            Edit");
        sb.AppendLine("                        </FluentButton>");
        sb.AppendLine("                        <FluentButton OnClick=\"DeleteButton\" Disabled=@DeleteDisabled>");
        sb.AppendLine("                            <FluentIcon Value=\"@(new Icons.Regular.Size20.Delete())\" Color=\"Color.Neutral\" Slot=\"start\" />");
        sb.AppendLine("                            Delete");
        sb.AppendLine("                        </FluentButton>");
        sb.AppendLine("                        <FluentButton OnClick=\"RefreshButton\">");
        sb.AppendLine("                            <FluentIcon Value=\"@(new Icons.Regular.Size20.ArrowClockwise())\" Color=\"Color.Neutral\" Slot=\"start\" />");
        sb.AppendLine("                            Refresh");
        sb.AppendLine("                        </FluentButton>");
        sb.AppendLine("                        <FluentButton OnClick=\"ExcelExportButton\">");
        sb.AppendLine("                            <FluentIcon Value=\"@(new Icons.Regular.Size20.ArrowClockwise())\" Color=\"Color.Neutral\" Slot=\"start\" />");
        sb.AppendLine("                            Export to Excel");
        sb.AppendLine("                        </FluentButton>");        
        sb.AppendLine("                    </div>");
        sb.AppendLine("                </Template>");
        sb.AppendLine("            </ToolbarItem>");
        sb.AppendLine("        </ToolbarItems>");
        sb.AppendLine("    </SfToolbar>");
        sb.AppendLine("");
        sb.AppendLine("    <GridColumns>");
        sb.AppendLine("        <GridColumn HeaderText=\"\" Width=\"30\" TextAlign=\"TextAlign.Left\">");
        sb.AppendLine("            <GridCommandColumns>");
        sb.AppendLine("                <GridCommandColumn Type=\"CommandButtonType.Edit\" ButtonOption=\"@(new CommandButtonOptions() {IconCss=\"e-icons e-edit\", CssClass=\"e-flat\" })\"></GridCommandColumn>");
        sb.AppendLine("                <GridCommandColumn Type=\"CommandButtonType.Delete\" ButtonOption=\"@(new CommandButtonOptions() {IconCss=\"e-icons e-delete\", CssClass=\"e-flat\" })\"></GridCommandColumn>");
        sb.AppendLine("                <GridCommandColumn Type=\"CommandButtonType.Save\" ButtonOption=\"@(new CommandButtonOptions() {IconCss=\"e-icons e-save\", CssClass=\"e-flat\" })\"></GridCommandColumn>");
        sb.AppendLine("                <GridCommandColumn Type=\"CommandButtonType.Cancel\" ButtonOption=\"@(new CommandButtonOptions() {IconCss=\"e-icons e-cancel-icon\", CssClass=\"e-flat\" })\"></GridCommandColumn>");
        sb.AppendLine("            </GridCommandColumns>");
        sb.AppendLine("        </GridColumn>");
        sb.AppendLine("");

        foreach (var column in o.Columns)
        {
            if (column.DataType == ColumnDataType.Bool)
            {
                sb.AppendLine($"        <GridColumn Field=\"{column.ColumnName}\" HeaderText=\"{column.TableHeader}\" TextAlign=\"TextAlign.Left\" DisplayAsCheckBox=\"true\" Width=\"{column.TableWidth}\" Type=\"ColumnType.Boolean\"></GridColumn>");
            }
            else
            {
                sb.AppendLine($"        <GridColumn Field=\"{column.ColumnName}\" HeaderText=\"{column.TableHeader}\" TextAlign=\"TextAlign.Left\" Width=\"{column.TableWidth}\"></GridColumn>");
            }
        }

        sb.AppendLine("");
        sb.AppendLine("");
        sb.AppendLine("    </GridColumns>");
        sb.AppendLine("</SfGrid>");
        sb.AppendLine("</div>");

        sb.AppendLine("");


        return sb.ToString();
    }


    public string GenerateEditorClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating Editor Razor cs class for {o.TableName}");

        var sb = new StringBuilder();

        sb.AppendLine($"using {o.UiNameSpace}.Features.Config.Models;");
        sb.AppendLine($"using {o.UiNameSpace}.Features.Config.Services;");
        sb.AppendLine($"using {o.UiNameSpace}.Shared;");
        sb.AppendLine($"using Microsoft.AspNetCore.Components;");
        sb.AppendLine($"using Microsoft.FluentUI.AspNetCore.Components;");
        sb.AppendLine($"using Syncfusion.Blazor.Grids;");

        sb.AppendLine("");
        sb.AppendLine($"namespace {o.UiNameSpace}.Features.Config.Pages.Editors;");

        sb.AppendLine($"public partial class {o.TableName}Editor : ComponentBase");
        sb.AppendLine("{");
        sb.AppendLine($"    public string? ErrorMessage {{ get; set; }}");
        sb.AppendLine($"    private SfGrid<{o.EntityName}>? DefaultGrid;");
        sb.AppendLine("");
        sb.AppendLine($"    public List<{o.EntityName}>? _{o.TableName.ToLower()} {{ get; set; }}");
        sb.AppendLine("");
        sb.AppendLine($"    public bool EditDisabled {{ get; set; }}");
        sb.AppendLine($"    public bool DeleteDisabled {{ get; set; }}");

        sb.AppendLine("");

        sb.AppendLine($"    public async Task AddButton()");
        sb.AppendLine("{");
        sb.AppendLine($"        if (DefaultGrid != null) await DefaultGrid.AddRecordAsync();");
        sb.AppendLine("}");

        sb.AppendLine($"    public async Task EditButton()");
        sb.AppendLine("{");
        sb.AppendLine($"        if (DefaultGrid != null) await DefaultGrid.StartEditAsync();");
        sb.AppendLine("}");

        sb.AppendLine($"    public async Task DeleteButton()");
        sb.AppendLine("{");
        sb.AppendLine($"        if (DefaultGrid != null) await DefaultGrid.DeleteRecordAsync();");
        sb.AppendLine("}");

        sb.AppendLine($"    public async Task RefreshButton()");
        sb.AppendLine("{");
        sb.AppendLine("          if (DefaultGrid != null) await Refresh();");
        sb.AppendLine("}");

                sb.AppendLine($"    public async Task ExcelExportButton()");
        sb.AppendLine("{");
        sb.AppendLine("          if (DefaultGrid != null) await DefaultGrid.ExportToExcelAsync();");
        sb.AppendLine("}");


        sb.AppendLine("");
        sb.AppendLine($"    protected override async Task OnInitializedAsync()");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        await Refresh();");
        
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    private async Task Refresh()");
        sb.AppendLine("     {");
        sb.AppendLine($"        var {o.TableName.ToLower()}Result = await {o.EntityName}Service.Get{o.TableName}Async();");
        sb.AppendLine("");
        sb.AppendLine($"        if ({o.TableName.ToLower()}Result.IsSuccess)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _{o.TableName.ToLower()} = {o.TableName.ToLower()}Result.Value;");
        sb.AppendLine($"            ErrorMessage = $\"ok \";");
        sb.AppendLine("        }");
        sb.AppendLine("        else");
        sb.AppendLine("        {");
        sb.AppendLine($"            ErrorMessage = {o.TableName.ToLower()}Result.Error;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine("        EditDisabled = true;");
        sb.AppendLine("        DeleteDisabled = true;");
        sb.AppendLine("");
        sb.AppendLine("        StateHasChanged();");
        sb.AppendLine("    }");
        sb.AppendLine("");

        sb.AppendLine($"    public void RowSelectHandler(RowSelectEventArgs<{o.EntityName}> args)");
        sb.AppendLine("    {");
        sb.AppendLine("        DeleteDisabled= false;");
        sb.AppendLine("        EditDisabled = false;");
        sb.AppendLine("    }");
        sb.AppendLine("");


        sb.AppendLine($"    private bool _newRecordAdded = false;");
        sb.AppendLine("");
        sb.AppendLine($"    public void RowCreatedHandler(RowCreatedEventArgs<{o.EntityName}> args)");
        sb.AppendLine("    {");
        sb.AppendLine("        _newRecordAdded = true;");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    public async Task RowUpdatedHandler(RowUpdatedEventArgs<{o.EntityName}> args)");
        sb.AppendLine("    {");
        sb.AppendLine($"        Result<List<{o.EntityName}>> result;");
        sb.AppendLine("");
        sb.AppendLine("        if (_newRecordAdded)");
        sb.AppendLine("        {");
        sb.AppendLine($"            result = await {o.EntityName}Service.Create{o.EntityName}Async(args.Data);");
        sb.AppendLine("        }");
        sb.AppendLine("        else");
        sb.AppendLine("        {");
        sb.AppendLine($"            result = await {o.EntityName}Service.Update{o.EntityName}Async(args.Data);");
        sb.AppendLine("        }");
        sb.AppendLine("        if (result.IsSuccess)");
        sb.AppendLine("        {");
        sb.AppendLine("            ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Success, \"Record Updated\");");
        sb.AppendLine("        }");
        sb.AppendLine("        else");
        sb.AppendLine("        {");
        sb.AppendLine("            ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Error, result.Error ?? \"Unknown errro\");");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine("        _newRecordAdded = false;");
        sb.AppendLine("        return;");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    public async Task RowDeletedHandler(RowDeletedEventArgs<{o.EntityName}> args)");
        sb.AppendLine("    {");
        sb.AppendLine($"        Result<List<{o.EntityName}>> result;");
        sb.AppendLine("");
        sb.AppendLine("        foreach (var item in args.Datas)");
        sb.AppendLine("        {");
        sb.AppendLine($"            result = await {o.EntityName}Service.Delete{o.EntityName}Async(item);");
        sb.AppendLine("");
        sb.AppendLine("            if (result.IsSuccess)");
        sb.AppendLine("            {");
        sb.AppendLine("                ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Success, $\"{item." + o.PrimaryKey + "} deleted\");");
        sb.AppendLine("            }");
        sb.AppendLine("            else");
        sb.AppendLine("            {");
        sb.AppendLine("                ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Error, $\"{item." + o.PrimaryKey + "} delete failed: {result.Error}\");");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine("        _newRecordAdded = false;");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }
}