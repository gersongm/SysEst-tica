using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.SqlServer
{
    //DE IGUAL MODO SERÁ UNA CLASE ABSTRACTA Y AGREGUEMOS LA HERENCIA A LA CLASE CONNECTIONTOSQL
    public abstract class ExecuteCommand : ConnectionToSql
    {
        //DECLARAMOS UN CAMPO PROTEGIDO DE TIPO LISTA GENERICA DE PARAMETROS SQL
        protected List<SqlParameter> parameters;

        //EL PRIMER METODO SERÁ EL ENCARGADO DE EJECUTAR COMANDOS DE NO CONSULTAS, ES DECIR, COMANDOS PARA INSERTAR, EDITAR Y ELIMINAR DATOS
        protected int ExecuteNonQuery(string transactSql)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transactSql;
                    command.CommandType = CommandType.Text;
                    foreach (SqlParameter item in parameters)
                    {
                        _ = command.Parameters.Add(item);
                    }
                    int result = command.ExecuteNonQuery();
                    parameters.Clear();
                    return result;
                }
            }
        }

        //EL SEGUNDO METODO SERÁ EL ENCARGADO DE EJECUTAR COMANDOS DE TIPO CONSULTAS, ES DECIR, PARA LEER LAS FILAS DE UNA TABLA Y MOSTRAR LOS DATOS DE UNA TABLA
        //AMBOS METODOS SON GENERICOS PARA TODAS LAS CLASES
        protected DataTable ExecuteReader(string transactSql)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transactSql;
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    using (DataTable table = new DataTable())
                    {
                        table.Load(reader);
                        reader.Dispose();
                        return table;
                    }
                }
            }
        }
    }
}
