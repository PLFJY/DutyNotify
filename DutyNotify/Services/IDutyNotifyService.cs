using DutyNotify.Models;

namespace DutyNotify.Services;

public interface IDutyNotifyService
{
    /// <summary>
    /// 设置对象
    /// </summary>
    public Settings Settings { get; }
    
    /// <summary>
    /// 保存配置
    /// </summary>
    void SaveConfig();

    void AddItem();
    void DeleteItem(DutyItem item);
}