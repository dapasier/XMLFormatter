//using System;
//using System.Data;
//using System.Data.SqlClient;
using System.Data.SqlTypes;
//using Microsoft.SqlServer.Server;
using System.IO;
using System.Text;
using System.Xml;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(Name = "FormatXML", IsDeterministic = true, IsPrecise = true)]
    public static SqlString FormatXML(string unformattedXml)
    {
        //load unformatted xml into a dom
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(unformattedXml.ToString());

        //will hold formatted xml
        StringBuilder sb = new StringBuilder();

        //pumps the formatted xml into the StringBuilder above
        StringWriter sw = new StringWriter(sb);

        //does the formatting
        XmlTextWriter xtw = null;

        try
        {
            //point the xtw at the StringWriter
            xtw = new XmlTextWriter(sw);

            //we want the output formatted
            xtw.Formatting = Formatting.Indented;

            //get the dom to dump its contents into the xtw 
            xd.WriteTo(xtw);
        }
        finally
        {
            //clean up even if error
            if (xtw != null)
                xtw.Close();
        }

        //return the formatted xml
        return (SqlString)sb.ToString();
    }
}
