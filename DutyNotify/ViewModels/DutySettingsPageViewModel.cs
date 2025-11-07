using System.Windows.Input;
using ClassIsland.Core.Abstractions.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DutyNotify.Models;
using DutyNotify.Services;
using FluentAvalonia.UI.Controls;

namespace DutyNotify.ViewModels;

public partial class DutySettingsPageViewModel : ObservableObject
{
    private readonly IDutyNotifyService _dutyNotifyService;
    public Settings Settings { get; }

    public NotifyIndexViewModel[] NotifyIndexViewModels { get; } = new NotifyIndexViewModel[7];

    public DutySettingsPageViewModel(IDutyNotifyService dutyNotifyService)
    {
        _dutyNotifyService = dutyNotifyService;
        Settings = _dutyNotifyService.Settings;
        for (var i = 0; i < 7; i++)
        {
            NotifyIndexViewModels[i] = new NotifyIndexViewModel(Settings, i, Save);
        }
        OnPropertyChanged(string.Empty);
    }

    [RelayCommand]
    private void Save()
    {
        foreach (var viewModel in NotifyIndexViewModels)
        {
            viewModel.Save();
        }
        _dutyNotifyService.SaveConfig();
    }

    [RelayCommand]
    private void Add() => _dutyNotifyService.AddItem();

    [RelayCommand]
    private async Task DeleteAsync(DutyItem item)
    {
        var dialog = new ContentDialog
        {
            Content = "Confirm To Delete?",
            Title = "Warning",
            PrimaryButtonText = "Confirm",
            CloseButtonText = "Close"
        };

        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary) return;

        _dutyNotifyService.DeleteItem(item);
    }

    public partial class NotifyIndexViewModel(Settings settings, int dayOfWeek, Action? saveAction) : ObservableObject
    {
        public string DayOfWeek { get; } = ((DayOfWeek)dayOfWeek).ToString();

        [ObservableProperty]
        private string _indexAssembly = string.Join(", ", settings.NotifyIndex[dayOfWeek]);
        
        public void Save()
        {
            if (string.IsNullOrEmpty(IndexAssembly)) return;
            var indexList = IndexAssembly.Split(',');
            settings.NotifyIndex[dayOfWeek].Clear();
            foreach (var item in indexList)
            {
                if (!int.TryParse(item, out var index))
                {
                    var dialog = new ContentDialog
                    {
                        Content = "输入不合法，请使用英文逗号分隔",
                        Title = "Warning",
                        CloseButtonText = "Close"
                    }.ShowAsync();
                    continue;
                }
                settings.NotifyIndex[dayOfWeek].Add(index);
            }
        }


    }

}