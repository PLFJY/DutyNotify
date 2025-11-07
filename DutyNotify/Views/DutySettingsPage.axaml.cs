using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Controls;
using DutyNotify.ViewModels;

namespace DutyNotify.Views;

[SettingsPageInfo("top.plfjy.DutyNotifySettingsPage","值日提醒设置")]
public partial class DutySettingsPage : SettingsPageBase
{
    public DutySettingsPage(DutySettingsPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}