using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DAL.Services
{
    public class DAL
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlCommand cmd = new SqlCommand();       

        //READ    regresa lista de sp
        public T GetReaderFromSpModel<T>(string spName, params SqlParameter[] parameters)
        {
            SqlConnection sqlConn = new SqlConnection(connString);
            T modelo = default(T);

            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            cmd.Connection = sqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = spName;
            cmd.Parameters.AddRange(parameters);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                modelo = DataReaderMapToModel<T>(reader);
            }
            return modelo;
        }
        //
        public T DataReaderMapToModel<T>(IDataReader dr)
        {

            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
            }
            return obj;
        }
        //INSERT, UPDATE, DELETE
        public bool CUD(string spName, params SqlParameter[] parameters)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connString);

                if (sqlConn.State == System.Data.ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                cmd.Connection = sqlConn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //
        public List<T> GetReaderFromStringToList<T>(string str)
        {
            SqlConnection sqlConn = new SqlConnection(connString);
            //lista generica
            List<T> lista = new List<T>();

            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            cmd.Connection = sqlConn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = str;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                lista = DataReaderMapToList<T>(reader);
            }

            return lista;
        }
        //
        public List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            //lista generica
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!Object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        //sp
        public List<T> GetReaderFromSpToList<T>(string spName, params SqlParameter[] parameters)
        {
            //parametros
            //SqlParameter[] parameters =
            //{
            //    new SqlParameter("@id",id)
            //};

            SqlConnection sqlConn = new SqlConnection(connString);
            List<T> lista = new List<T>();
            if (sqlConn.State == System.Data.ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            cmd.Connection = sqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = spName;
            cmd.Parameters.AddRange(parameters);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                lista = DataReaderMapToList<T>(reader);
            }

            return lista;

        }


    }
}