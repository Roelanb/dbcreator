using CrudUi.Pages.Features.Config.Models;
using CrudUi.Pages.Features.Config.Services;
using CrudUi.Pages.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Syncfusion.Blazor.Grids;

namespace CrudUi.Pages.Features.Config.Pages.Editors;
public partial class TranslationsTypesEditor : ComponentBase
{
    public string? ErrorMessage { get; set; }
    private SfGrid<TranslationsType>? DefaultGrid;

    public List<TranslationsType>? _translationstypes { get; set; }

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

        var translationstypesResult = await TranslationsTypeService.GetTranslationsTypesAsync();

        if (translationstypesResult.IsSuccess)
        {
            _translationstypes = translationstypesResult.Value;
            ErrorMessage = $"ok ";
        }
        else
        {
            ErrorMessage = translationstypesResult.Error;
        }

        EditDisabled = true;
        DeleteDisabled = true;

    }

    public void RowSelectHandler(RowSelectEventArgs<TranslationsType> args)
    {
        DeleteDisabled= false;
        EditDisabled = false;
    }

    private bool _newRecordAdded = false;

    public void RowCreatedHandler(RowCreatedEventArgs<TranslationsType> args)
    {
        _newRecordAdded = true;
    }

    public async Task RowUpdatedHandler(RowUpdatedEventArgs<TranslationsType> args)
    {
        Result<List<TranslationsType>> result;

        if (_newRecordAdded)
        {
            result = await TranslationsTypeService.CreateTranslationsTypeAsync(args.Data);
        }
        else
        {
            result = await TranslationsTypeService.UpdateTranslationsTypeAsync(args.Data);
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

    public async Task RowDeletedHandler(RowDeletedEventArgs<TranslationsType> args)
    {
        Result<List<TranslationsType>> result;

        foreach (var item in args.Datas)
        {
            result = await TranslationsTypeService.DeleteTranslationsTypeAsync(item);

            if (result.IsSuccess)
            {
                ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Success, $"{item.translation_type} deleted");
            }
            else
            {
                ToastService.ShowToast(Microsoft.FluentUI.AspNetCore.Components.ToastIntent.Error, $"{item.translation_type} delete failed: {result.Error}");
            }
        }

        _newRecordAdded = false;
    }
}
