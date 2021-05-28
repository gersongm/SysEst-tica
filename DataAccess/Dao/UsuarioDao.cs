using DataAccess.Entities;
using DataAccess.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
	public class UsuariosDao : ExecuteCommand
	{
		private readonly string select;
		private readonly string insert;
		private readonly string update;
		private readonly string delete;

		public UsuariosDao()
		{
			select = "select* from Usuarios";
			insert = " insert into Usuarios values (@usuario,@contraseña,@nombres,@email,@privilegios,@sucursal)";
			update = "update Usuarios set usuario= @usuario, contraseña= @contraseña, nombres= @nombres, email= @email, privilegios= @privilegios, sucursal= @sucursal where Id=@Id";
			delete = "delete from Usuarios where Id=@Id";
		}
		public int Add(UsuariosEntity entity)
		{
			parameters = new List<SqlParameter>
		{
		  new SqlParameter("@usuario", entity.Usuario),
		  new SqlParameter("@contraseña", entity.Contraseña),
		  new SqlParameter("@nombres", entity.Nombres),
		  new SqlParameter("@email", entity.Email),
		  new SqlParameter("@privilegios", entity.Privilegios),
		  new SqlParameter("@sucursal", entity.Sucursal)
	   };
			return ExecuteNonQuery(insert);
		}


		public int Update(UsuariosEntity entity)
		{
			parameters = new List<SqlParameter>
		{
		  new SqlParameter("@id", entity.Id),
		  new SqlParameter("@usuario", entity.Usuario),
		  new SqlParameter("@contraseña", entity.Contraseña),
		  new SqlParameter("@nombres", entity.Nombres),
		  new SqlParameter("@email", entity.Email),
		  new SqlParameter("@privilegios", entity.Privilegios),
		  new SqlParameter("@sucursal", entity.Sucursal)
	   };
			return ExecuteNonQuery(update);
		}

		public int Delete(int id)
		{
			parameters = new List<SqlParameter>
		{
		  new SqlParameter("@id",id)
	   };
			return ExecuteNonQuery(delete);
		}

		public IEnumerable<UsuariosEntity> GetAll()
		{
			DataTable tableresult = ExecuteReader(select);
			List<UsuariosEntity> getList = new List<UsuariosEntity>();
			foreach (DataRow item in tableresult.Rows)
			{
				getList.Add(new UsuariosEntity
				{
					Id = Convert.ToInt32(item[0]),
					Usuario = Convert.ToString(item[1]),
					Contraseña = Convert.ToString(item[2]),
					Nombres = Convert.ToString(item[3]),
					Email = Convert.ToString(item[4]),
					Privilegios = Convert.ToInt32(item[5]),
					Sucursal = Convert.ToInt32(item[6])
				});
			}

			return getList;
		}

	}


}
