using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Task_Solution.Persistence;
public static class Configuration
{
    static public string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/PYP_Task_Solution.WebApi"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("Default");
        }
    }

    static public Dictionary<string, string> EmailConfiguration
    {
        get
        {
            ConfigurationManager c = new();
            c.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/PYP_Task_Solution.WebApi"));
            c.AddJsonFile("appsettings.json");

            var config = new Dictionary<string, string>()
            {
             {"ApiKey", $"{c.GetSection("SendGrid:ApiKey").Value}"},
             {"From",$"{c.GetSection("SendGrid:From").Value}"},
            };
            return config;
        }
    }
}

