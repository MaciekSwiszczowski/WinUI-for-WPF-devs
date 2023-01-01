using System;

namespace Binding;

internal class TestViewModel : ViewModelBase
{
    public DateTime Date
    {
        get => _date;
        set => SetField(ref _date, value);
    }

    public int ClickCount
    {
        get => _clickCount;
        set => SetField(ref _clickCount, value);
    }

    public string OnDateChanged()
    {
        return Date.Hour.ToString();
    }

    public void OnHourModified(string value)
    {
        if (int.TryParse(value, out var hour) && hour is >= 0 and < 24)
        {
            Date = new DateTime(Date.Year, Date.Month, Date.Day, hour, Date.Minute, Date.Second, Date.Millisecond);
        }
    }

    public void IncreaseCountVersion1(object sender, RoutedEventArgs e)
    {
        ClickCount++;
    }

    public void IncreaseCountVersion2()
    {
        ClickCount++;
    }

    private int _clickCount;
    private DateTime _date = DateTime.Now;
}