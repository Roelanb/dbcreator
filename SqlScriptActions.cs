using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class SqlScriptActions
{


    public void ExecuteScript(string connnectionString, string filename)
    {
        var sql = System.IO.File.ReadAllText(filename);

        // split script on GO command
        IEnumerable<string> commandStrings = Regex.Split(sql, @"^\s*GO\s*$",RegexOptions.Multiline | RegexOptions.IgnoreCase); 


        foreach (string commandString in commandStrings)
        {
            if (commandString.Trim() != "")
            {
                Console.WriteLine(commandString);
                ExecuteNonQuery(connnectionString, commandString);
            }
        }




    }

    private void ExecuteNonQuery(string connnectionString, string commandString)
    {
        try {

        
        using (var connection = new SqlConnection(connnectionString))
        {
            connection.Open();
            connection.Execute(commandString);
        }
        }
        catch(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
  
            Console.WriteLine(commandString);
            Console.WriteLine(ex.Message);

             Console.ResetColor();
        }
    }
}