using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace ErrorCenter.BaseEntityConfig
{
    public abstract class BaseEntityConfig<T> : EntityTypeConfiguration<T>
        where T : class
    {
        public BaseEntityConfig()
        {
            PrimaryKeyConfig();
            ForeignKeyConfig();
            TableFieldsConfig();
            TableNameConfig();
        }

        protected abstract void PrimaryKeyConfig();
        protected abstract void ForeignKeyConfig();
        protected abstract void TableFieldsConfig();
        protected abstract void TableNameConfig();

    }
}
