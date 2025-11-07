using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DutyNotify.Models;

public class Settings : ObservableObject
{
    public ObservableCollection<DutyItem> DutyItems { get; set; } = [];
    
    public List<int>[] NotifyIndex { get; set; } =
    [
        [],
        [],
        [],
        [],
        [],
        [],
        []
    ];
}