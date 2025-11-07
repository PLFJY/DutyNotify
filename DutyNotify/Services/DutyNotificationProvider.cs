using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Abstractions.Services.NotificationProviders;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Models.Notification;
using DutyNotify.Models;

namespace DutyNotify.Services;

[NotificationProviderInfo("6A1C3ED7-C2AF-4D4B-9EFE-9DFC0A1844D6",
    "值日提醒", "\ue902", "提供值日提醒的提醒提供方")]
public class DutyNotificationProvider : NotificationProviderBase
{
    private readonly ILessonsService _lessonsService;
    private readonly IExactTimeService _timeService;

    private readonly Settings _settings;

    public DutyNotificationProvider(IDutyNotifyService dutyNotifyService,
        ILessonsService lessonsService, IExactTimeService timeService)
    {
        _lessonsService = lessonsService;
        _timeService = timeService;
        _settings = dutyNotifyService.Settings;
        _lessonsService.OnBreakingTime += LessonsServiceOnBreakingTime;
        _lessonsService.OnAfterSchool += LessonsServiceOnOnAfterSchool;
    }

    private void LessonsServiceOnOnAfterSchool(object? sender, EventArgs e)
    {
        var day = (int)_timeService.GetCurrentLocalDateTime().DayOfWeek;
        NotifyDuty(day);
    }

    private void LessonsServiceOnBreakingTime(object? sender, EventArgs e)
    {
        var day = (int)_timeService.GetCurrentLocalDateTime().DayOfWeek;
        if (!_settings.NotifyIndex[day].Contains(_lessonsService.CurrentSelectedIndex)) return;
        NotifyDuty(day);
    }

    private void NotifyDuty(int day)
    {
        var notifyContent = "今天值日生: " + string.Join(", ", 
            _settings.DutyItems
                .Where(x => !string.IsNullOrEmpty(x.OnDutyStudents[day].Value))
                .Select(x=> $"{x.Name} {x.OnDutyStudents[day].Value}")
        ) + "记得做值日";

        ShowNotification(new NotificationRequest
        {
            MaskContent = NotificationContent.CreateTwoIconsMask("值日提醒"),
            OverlayContent = NotificationContent.CreateRollingTextContent(notifyContent, TimeSpan.FromSeconds(15))
        });
    }
}