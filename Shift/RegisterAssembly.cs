﻿using Autofac;
using Shift.DataLayer;
using Shift.Entities;

using System;
using System.Collections.Generic;

namespace Shift
{
    public static class RegisterAssembly
    {
        public static void RegisterTypes(ContainerBuilder builder, string storageMode, string dbConnectionString, string encryptionKey, string dbAuthKey)
        {
            var parameters = Helpers.GenerateNamedParameters(new Dictionary<string, object> { { "connectionString", dbConnectionString }, { "encryptionKey", encryptionKey} });
            switch (storageMode.ToLower())
            {
                case StorageMode.MSSql:
                    builder.RegisterType<JobDALSql>().As<IJobDAL>().UsingConstructor(typeof(string), typeof(string)).WithParameters(parameters);
                    break;
                case StorageMode.Redis:
                    builder.RegisterType<JobDALRedis>().As<IJobDAL>().UsingConstructor(typeof(string), typeof(string)).WithParameters(parameters);
                    break;
                case StorageMode.MongoDB:
                    builder.RegisterType<JobDALMongo>().As<IJobDAL>().UsingConstructor(typeof(string), typeof(string)).WithParameters(parameters);
                    break;
                case StorageMode.DocumentDB:
                    parameters = Helpers.GenerateNamedParameters(new Dictionary<string, object> { { "connectionString", dbConnectionString }, { "encryptionKey", encryptionKey }, { "authKey", dbAuthKey } });
                    builder.RegisterType<JobDALDocumentDB>().As<IJobDAL>().UsingConstructor(typeof(string), typeof(string), typeof(string)).WithParameters(parameters);
                    break;
                default:
                    throw new ArgumentNullException("The storage mode configuration must not be empty or null.");
                    break;
            }
        }

    }
}
