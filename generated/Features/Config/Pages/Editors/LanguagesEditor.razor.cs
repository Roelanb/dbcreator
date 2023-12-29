using CrudUi.Pages.Features.Config.Models;
using CrudUi.Pages.Features.Config.Services;
using CrudUi.Pages.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Syncfusion.Blazor.Grids;

namespace CrudUi.Pages.Features.Config.Pages.Editors;
public partial class LanguagesEditor : ComponentBase
{
    public string? ErrorMessage { get; set; }
    private SfGrid<Language>? DefaultGrid;

    public List<Language>? _languages { get; set; }

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

        var languagesResult = await LanguageService.GetLanguagesAsync();

        if (languagesResult.IsSuccess)
        {
            _languages = languagesResult.Value;
            ErrorMessage = $"ok ";
        }
        else
        {
            ErrorMessage = languagesResult.Error;
        }

        EditDisabled = true;
        DeleteDisabled = true;

    }

    public void RowSelectHandler(RowSelectEventArgs<Language> args)
    {
        DeleteDisabled= false;
        EditDisabled = false;
    }

    private bool _newRecordAdded = false;

    public void RowCreatedHandler(RowCreatedEventArgs<Language> args)
    {
        _newRecordAdded = true;
    }

    public async Task RowUpdatedHandler(RowUpdatedEventArgs<Language> args)
    {
        Result<List<Language>> result;

        if (_newRecordAdded)
        {
            result = await LanguageService.CreateLanguageAsync(args.Data);
        }
        else
        {
            result = await LanguageService.UpdateLanguageAsync(args.Data);
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

    public async Task RowDeletedHandler(RowDeletedEventArgs<Language> args)
    {
        Result<List<Language>> result;

        foreach (var item in args.Datas)
        {
            result = await LanguageService.DeleteLanguageAsync(item);

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
