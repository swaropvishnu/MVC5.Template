﻿using Template.Components.Services.AutoMapper;
using Template.Objects;
using System;
using System.Collections;
using System.Data.Entity;

namespace Template.Data.Core
{
    public class Context : DbContext, IContext
    {
        private Hashtable repositories;

        #region DbSets

        #region Account

        private DbSet<Account> Accounts { get; set; }

        #endregion

        #region Administration

        private DbSet<User> Users { get; set; }
        private DbSet<Role> Roles { get; set; }
        private DbSet<Privilege> Privileges { get; set; }
        private DbSet<RolePrivilege> RolePrivileges { get; set; }
        private DbSet<PrivilegeLanguage> PrivilegeLanguages { get; set; }

        #endregion

        #region System

        public DbSet<Language> Languages { get; set; }

        #endregion

        #endregion

        static Context()
        {
            ModelMapping.MapModels();
        }
        public Context()
        {
            repositories = new Hashtable();
        }

        public IRepository<TModel> Repository<TModel>() where TModel : BaseModel
        {
            var modelName = typeof(TModel).Name;
            if (repositories.ContainsKey(modelName))
                return (IRepository<TModel>)repositories[modelName];

            var repositoryType = typeof(Repository<>).MakeGenericType(typeof(TModel));
            var modelRepository = (IRepository<TModel>)Activator.CreateInstance(repositoryType, this);
            repositories.Add(modelName, modelRepository);

            return modelRepository;
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
