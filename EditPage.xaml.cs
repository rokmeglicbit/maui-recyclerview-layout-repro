using System.Collections.ObjectModel;

namespace Repro;

public partial class EditPage : ContentPage
{
    // false = sproži napako; true = varna različica (premik po koncu layouta)
    const bool UseWorkaround = true;

    public ObservableCollection<TimetableItem> Items { get; }

    public EditPage()
    {
        InitializeComponent();

        // Namenoma NERAZVRŠČENO, da prvi bind sproži Move()
        Items =
        [
            new() { Name = "Interval A", Start = new TimeSpan(10, 0, 0) },
            new() { Name = "Interval B", Start = new TimeSpan(8, 0, 0) },
            new() { Name = "Interval C", Start = new TimeSpan(9, 0, 0) },
            new() { Name = "Interval D", Start = new TimeSpan(7, 0, 0) },
            new() { Name = "Interval E", Start = new TimeSpan(11, 0, 0) },
        ];
        BindingContext = this;
    }

    // Ta handler se sproži TUDI, ko ga sproži sam binding med OnBindViewHolder
    // (Time se spremeni iz privzetega 00:00 v vrednost elementa) -> Move() sredi layouta.
    void TimePicker_TimeSelected(object? sender, TimeChangedEventArgs e)
    {
        if (sender is not TimePicker { BindingContext: TimetableItem item } || e.NewTime is null)
            return;

        item.Start = e.NewTime.Value;

        if (UseWorkaround)
            Dispatcher.Dispatch(SortItems);   // počakaj, da RecyclerView konča layout
        else
            SortItems();                      // -> IllegalStateException na Androidu
    }

    void SortItems()
    {
        var sorted = Items.OrderBy(x => x.Start).ToList();
        for (int i = 0; i < sorted.Count; i++)
        {
            int current = Items.IndexOf(sorted[i]);
            if (current != i)
                Items.Move(current, i);
        }
    }
}
