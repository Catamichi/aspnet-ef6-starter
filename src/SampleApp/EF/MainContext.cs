using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using SampleApp.Entities;

namespace SampleApp.EF
{
    [DbConfigurationType(typeof(Npgsql.NpgsqlEFConfiguration))]
    public class MainContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        
        public MainContext(String connectionString)
            : base(connectionString)
        {
            Database.Log = Console.Error.WriteLine;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            
            modelBuilder
                .Types()
                .Configure(config => config.ToTable(GetTableName(config.ClrType)));
            
            modelBuilder
                .Properties()
                .Configure(config => config.HasColumnName(GetColumnName(config.ClrPropertyInfo)));
            
            base.OnModelCreating(modelBuilder);
        }
        
        private static String GetTableName(Type type)
        {
            return PascalCaseToLowerUnderscore(type.Name);
        }
        
        private static String GetColumnName(PropertyInfo property)
        {
            String columnName = "";
            
            if (property.ReflectedType.GetCustomAttribute<ComplexTypeAttribute>() != null)
            {
                columnName += PascalCaseToLowerUnderscore(property.DeclaringType.Name) + "_";
            }
            
            columnName += PascalCaseToLowerUnderscore(property.Name);
            
            return columnName;
        }
        
        private static String PascalCaseToLowerUnderscore(String source)
        {
            var result =
                Regex.Replace(
                    source,
                    ".[A-Z]",
                    m => m.Value[0] + "_" + m.Value[1]);
            
            return result.ToLower();
        }
    }
}