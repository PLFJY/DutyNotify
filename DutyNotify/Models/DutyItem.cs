using CommunityToolkit.Mvvm.ComponentModel;

namespace DutyNotify.Models;

public partial class DutyItem : ObservableObject
{
    /// <summary>
    /// 值日项目
    /// </summary>
    [ObservableProperty]
    private string _name = "项目名称";

    /// <summary>
    /// 值日的学生列表
    /// </summary>
    public DutyStudents[] OnDutyStudents { get; set; } =
    [
        new(),
        new(),
        new(),
        new(),
        new(),
        new(),
        new()
    ];

    public class DutyStudents
    {
        public string Value { get; set; } = string.Empty;
    }
}