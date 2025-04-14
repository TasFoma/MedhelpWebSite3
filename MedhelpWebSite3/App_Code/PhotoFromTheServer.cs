using System;
using System.IO;

/// <summary>
/// Сводное описание для PhotoFromTheServer
/// </summary>
public static class PhotoFromTheServer
{
    public static string PhotoFromServerOrDefault(string strBase64)
    {
        byte[] photoarray;
        if (string.IsNullOrEmpty(strBase64)) //Убрал специально 24.04.2023 чтобы не висла запись
        {
            photoarray = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~/Image/dummy.png"));
            strBase64 = Convert.ToBase64String(photoarray);
        }
        return strBase64;
    }
}