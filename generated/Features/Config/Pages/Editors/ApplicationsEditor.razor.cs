using CrudUi.Pages.Features.Config.Models;
using CrudUi.Pages.Features.Config.Services;
using CrudUi.Pages.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Syncfusion.Blazor.Grids;

namespace CrudUi.Pages.Features.Config.Pages.Editors;
public partial class ApplicationsEditor : ComponentBase
{
    public string? ErrorMessage { get; set; }
    private SfGrid<Application>? DefaultGrid;

    public List<Application>? _applications { get; set; }

    public bool EditDisabled { get; set; }
    public bool DeleteDisabled { get; set; }

 public void AddButton()
{
        this.DefaultGrid.AddRecordAsync();
}
 public void EditButton()
{
        this.DefaultGrid.StartEditAsync();
}
 public void DeleteButton()
{
        this.DefaultGrid.DeleteRecordAsync();
}
 public void RefreshButton()
{

}

    protected override async Task OnInitializedAsync()
    {

        var applicationsResult = await ApplicationService.GetApplicationsAsync();

        if (applicationsResult.IsSuccess)
        {
            _applications = applicationsResult.Value;
            ErrorMessage = $"ok ";
        }
        else
        {
            ErrorMessage = applicationsResult.Error;
        }

        EditDisabled = true;
        DeleteDisabled = true;

    }

    public void RowSelectHandler(RowSelectEventArgs<Application> args)
    {
        DeleteDisabled= false;
        EditDisabled = false;
    }

    private bool _newRecordAdded = false;

    public void RowCreatedHandler(RowCreatedEventArgs<Application> args)
    {
        _newRecordAdded = true;
    }

    public async Task RowUpdatedHandler(RowUpdatedEventArgs<Application> args)
    {
        Result<List<Application>> result;

        if (_newRecordAdded)
        {
            result = await ApplicationService.CreateApplicationAsync(args.Data);
        }
        else
        {
            result = await ApplicationService.UpdateApplicationAsync(args.Data);
        }
        if (result.IsSuccess)
        {
            ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Success, "Record Updated");
        }
        else
        {
            ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Error, result.Error ?? "Unknown errro");
        }

        _newRecordAdded = false;
        return;
    }

    public async Task RowDeletedHandler(RowDeletedEventArgs<Application> args)
    {
        Result<List<Application>> result;

        foreach (var item in args.Datas)
        {
            result = await ApplicationService.DeleteApplicationAsync(item);

            if (result.IsSuccess)
            {
                ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Success, $"{item.Name} deleted");
            }
            else
            {
                ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Error, $"{item.Name} delete failed: {result.Error}");
            }
        }

        _newRecordAdded = false;
    }
}
