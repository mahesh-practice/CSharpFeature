using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class RIP
    {
        public void GetEnv(string env, Settings settings)
        {
            if (env == "UAT")
            {
                settings.Environment = "UAT";
            }
            else if (env == "PRE_PROD")
            {
                settings.Environment = "PRP";
            }
            else if (env == "PROD")
            {
                settings.Environment = "PRD";
            }
            else
            {
                settings.Environment = string.Empty;
            }
        }
        public void GetEnv2(string env, Settings settings)
        {
            settings.Environment = env == "UAT" ? "UAT" : env == "PRE_PROD" ? "PRP" : env == "PROD" ? "PRD" : string.Empty;
        }

        public void GetEnv3(string env, Settings settings)
        {
            settings.Environment = env switch
            {
                "UAT" => "UAT",
                "PRE_PROD" => "PRP",
                "PROD" => "PRD",
                _ => string.Empty
            };
        }

        public void GetEnv4(string env, Settings settings)
        {
            settings.Environment = envMap[env];
        }

        Dictionary<string, string> envMap = new()
            {
                { "UAT" , "UAT" },
                {"PRE_PROD" , "PRP" },
                {"PROD" , "PRD" },
                {"" , string.Empty }
            };
    }

    public class Settings
    {
        public string Environment { get; set; }
    }
}
