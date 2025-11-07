using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using DynamicData;

namespace DutyNotify.Converters;

public static class IntDayWeekConverters
{
    public static FuncValueConverter<Control?, string> IntToDayWeekConverter { get; } = new(c =>
    {
        if(c == null) return string.Empty;
        var itemsControl = ItemsControl.ItemsControlFromItemContainer(c);
        if (itemsControl == null) return "N/A"; // 安全回退
        
        var index = itemsControl.IndexFromContainer(c);
        
        // 关键修复：验证索引有效性
        if (index < 0 || index >= itemsControl.Items.Count || index > 6) 
            return $"Item {index + 1}"; // 超出星期范围时显示序号
        
        return ((DayOfWeek)index).ToString("G"); // 安全转换
    });
}