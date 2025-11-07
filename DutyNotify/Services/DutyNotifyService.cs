using System.Text.Json;
using ClassIsland.Core.Controls;
using ClassIsland.Shared.Helpers;
using DutyNotify.Models;
using FluentAvalonia.UI.Controls;

namespace DutyNotify.Services;

public class DutyNotifyService : IDutyNotifyService
{
    private readonly string _pluginConfigFolder;
    public Settings Settings { get; } = new();

    public DutyNotifyService(string pluginConfigFolder)
    {
        _pluginConfigFolder = pluginConfigFolder;
        Settings = ConfigureFileHelper.LoadConfig<Settings>(Path.Combine(_pluginConfigFolder, "Settings.json"));
    }
    
    public void SaveConfig()
    {
        ConfigureFileHelper.SaveConfig(Path.Combine(_pluginConfigFolder, "Settings.json"), Settings);
        new ContentDialog
        {
            Title = "Info",
            Content = "Success",
            CloseButtonText = "Close"
        }.ShowAsync();
    }

    public void AddItem() => Settings.DutyItems.Add(new DutyItem());
    public void DeleteItem(DutyItem item)
    {
        Settings.DutyItems.Remove(item);
    }
}