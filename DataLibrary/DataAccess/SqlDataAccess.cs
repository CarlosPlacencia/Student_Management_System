using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        // Establish SQL Connection
        public static string GetConnectionString(string connectionName = "RegistrationBD")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        // LoadStudentData()
        public static List<T> LoadStudentData<T>(string sql)
        {
            using(IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static List<T> LoadStudentInfo<T>(string sql, object parameters)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, parameters).ToList();
            }
        }

        // SaveStudentData()
        public static int SaveStudentData<T>(string sql, T data)
        {
            using(IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        // GetAvailableCourses()
        public static List<T> GetAvailableCourses<T>(string sql)
        {
            
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }


        // Save Students Course
        // SaveStudentData()
        public static int SaveStudentTakes<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data, commandType: CommandType.StoredProcedure);
            }
        }

        public static List<T> GetStudentsCourses<T>(string sql, object parameters)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql, parameters).ToList();
            }
        }
    }

}
