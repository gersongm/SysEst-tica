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
	public class SucursalesDao : ExecuteCommand
	{
		private readonly string select;
		private readonly string insert;
		private readonly string update;
		private readonly string delete;

		public SucursalesDao()
		{
			select = "select* from Sucursales";
			insert = " insert into Sucursales values (@id,@sucursal,@ubicacion,@telefono)";
			update = "update Sucursales set =  id= @id, sucursal= @sucursal, ubicacion= @ubicacion, telefono= @telefono where Id=@Id";
			delete = "delete from Sucursales where Id=@Id";
		}
		public int Add(SucursalesEntity entity)
		{
			parameters = new List<SqlParameter>
		{
		  new SqlParameter("@sucursal", entity.Sucursal),
		  new SqlParameter("@ubicacion", entity.Ubicacion),
		  new SqlParameter("@telefono", entity.Teléfono)
	   };
			return ExecuteNonQuery(insert);
		}


		public int Update(SucursalesEntity entity)
		{
			parameters = new List<SqlParameter>
		{
		  new SqlParameter("@id", entity.Id),
		  new SqlParameter("@sucursal", entity.Sucursal),
		  new SqlParameter("@ubicacion", entity.Ubicacion),
		  new SqlParameter("@telefono", entity.Teléfono)
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

		public IEnumerable<SucursalesEntity> GetAll()
		{
			DataTable tableresult = ExecuteReader(select);
			List<SucursalesEntity> getList = new List<SucursalesEntity>();
			foreach (DataRow item in tableresult.Rows)
			{
				getList.Add(new SucursalesEntity
				{
					Id = Convert.ToInt32(item[0]),
					Sucursal = Convert.ToString(item[1]),
					Ubicacion = Convert.ToString(item[2]),
					Teléfono = Convert.ToString(item[3])
				});
			}

			return getList;
		}

	}


}
