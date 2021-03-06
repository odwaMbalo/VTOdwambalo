﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServices.Services.Repository
{
    

    public class BaseRepository
    {
        public MongoClient client;
        public IMongoDatabase database;
        public BaseRepository(IConfiguration config)
        {
            var value = config["connectionString:ClientDb"];
            //var connectionstring = config.GetValue<string>("connectionString:ClientDb");
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("ClientDb");
        }
    }
}
