using System.ComponentModel;

namespace Repro;

public class TimetableItem : INotifyPropertyChanged
{
    TimeSpan _start;

    public string Name { get; set; } = "";

    public TimeSpan Start
    {
        get => _start;
        set
        {
            if (_start == value) return;
            _start = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Start)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
