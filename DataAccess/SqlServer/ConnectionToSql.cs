using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess.SqlServer
{
    public abstract class ConnectionToSql
    {
        //CAMPO PRIVADO PARA ALMACENAR LA CADENA DE CONEXION
        //AL DECLARAR UN CAMPO DE FORMA PRIVADA ESTAMOS INDICANDO QUE ESE CAMPO SOLO PODRÁ SER USADA POR LA MISMA CLASE
        private readonly string connectionString;

        //CREAMOS EL CONSTRUCTOR E INICIALIZAMOS EL CAMPO connectionstring EL CUAL ES IGUAL A LA CADENA DE CONEXION DEL APP.CONFIG
        public ConnectionToSql()
        {
            connectionString = @"server=DESKTOP-08MUDHE\BDSQL; database=Est_BD; integrated security = true";
        }

        //CREAMOS UN METODO PROTEGIDO DE TIPO SQLCONNECTION PARA OBTENER LA CONEXION
        protected SqlConnection GetConnection()
        {
            //RETORNAMOS LA CONEXION OBTENIDA A TRAVÉS DE LA INSTANCIA
            return new SqlConnection(connectionString);
        }
        //ESTE METODO ES PROTEGIDO, YA QUE QUEREMOS QUE SOLO SE PUEDA USAR EN LA MISMA CLASE Y LAS CLASES QUE DERIVEN DE ELLA
        //TENIENDO EN CUENTA QUE ESTA CLASE ES ABSTRACTA, SIGNIFICA QUE NO PUEDE SER INSTANCIADA, ES DECIR, NO SE PUEDE ENVOLVER EN OBJETOS PERO SI SE PUEDE HEREDAR
        //Y LAS CLASES HIJAS SERAN LAS ENCARGADAS DE AGREGAR FUNCIONALIDAD A LOS METODOS ABSTRACTOS.
    }
}
