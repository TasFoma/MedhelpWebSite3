using System;
using System.Web.UI;

public partial class SessionEnded : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var error = Server.UrlDecode(Request.QueryString["errorString"]);
            /*if (!string.IsNullOrEmpty(error))
                errorStrLabel.Text = error;*/
        }
        catch { }
    }
}