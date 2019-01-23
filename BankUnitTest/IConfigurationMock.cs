using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUnitTest
{
    class IConfigurationMock : IConfiguration
    {
        public string this[string key] {
            get
            {
                switch(key)
                {
                    case "ClientDb":
                        return "mongodb://localhost:27017";
                }

                throw new Exception($"Could not find setting for {key}");
            }
        }

        string IConfiguration.this[string key]
        {
            get
            {

                return "mongodb://localhost:27017";
            }
            set
            {
                throw new NotImplementedException();

            }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
