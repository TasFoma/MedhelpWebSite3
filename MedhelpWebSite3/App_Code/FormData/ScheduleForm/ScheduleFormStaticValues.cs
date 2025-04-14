using System.Collections.Generic;

/// <summary>
/// Сводное описание для ScheduleFormStaticValues
/// </summary>
public static class ScheduleFormStaticValues
{
    public static Dictionary<string, int> DaysOfWeek = new Dictionary<string, int>()
    {
        ["Monday"] = 1,
        ["Tuesday"] = 2,
        ["Wednesday"] = 3,
        ["Thursday"] = 4,
        ["Friday"] = 5,
        ["Saturday"] = 6,
        ["Sunday"] = 7
    };

    public static Dictionary<int, string> Months = new Dictionary<int, string>()
    {
        [1] = "Январь",
        [2] = "Февраль",
        [3] = "Март",
        [4] = "Апрель",
        [5] = "Май",
        [6] = "Июнь",
        [7] = "Июль",
        [8] = "Август",
        [9] = "Сентябрь",
        [10] = "Октябрь",
        [11] = "Ноябрь",
        [12] = "Декабрь"
    };

    public static Dictionary<int, string> Buttons = new Dictionary<int, string>()
    {
        [1] = "MonButton",
        [2] = "TueButton",
        [3] = "WedButton",
        [4] = "ThuButton",
        [5] = "FriButton",
        [6] = "SatButton",
        [7] = "SunButton"
    };
}