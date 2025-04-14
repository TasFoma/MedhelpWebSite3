using System;
using System.Web;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var serverError = Server.GetLastError() as HttpException;

            var error = Server.UrlDecode(Request.QueryString["errorString"]);
            /*if(!string.IsNullOrEmpty(error))                
                errorStrLabel.Text = error;*/
        }
        catch { }
    }
}