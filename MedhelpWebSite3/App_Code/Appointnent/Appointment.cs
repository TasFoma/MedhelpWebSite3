using System;

/// <summary>
/// Сводное описание для Appointment
/// </summary>
[Serializable]
public class Appointment
{
    public string DoctorFullName { get; set; }
    public string DoctorId { get; set; }
    public string AppTime { get; set; }

    public Appointment()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }
}