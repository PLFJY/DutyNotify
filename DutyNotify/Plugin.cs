using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using DutyNotify.Services;
using DutyNotify.ViewModels;
using DutyNotify.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutyNotify;

[PluginEntrance]
public class Plugin : PluginBase
{
    public override void Initialize(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<IDutyNotifyService, DutyNotifyService>();
        services.AddNotificationProvider<DutyNotificationProvider>();
        services.AddSingleton<DutySettingsPageViewModel>();
        services.AddSettingsPage<DutySettingsPage>();
    }
}