using System;

/// <summary>
/// Сводное описание для SpecialException
/// </summary>
public class SpecialException : Exception
{
    public SpecialException()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }

    public SpecialException(string message)
        : base(message)
    {

    }
}

public class MissingBusinessIDException : SpecialException
{
    public MissingBusinessIDException() { }

    public MissingBusinessIDException(string message)
        : base(message)
    {

    }
}

public class SessionEndedException : SpecialException
{
    public SessionEndedException()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }

    public SessionEndedException(string message)
        : base(message)
    {

    }
}