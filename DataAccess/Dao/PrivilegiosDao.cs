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
	public class PrivilegiosDao : ExecuteCommand
	{
		private readonly string select;
		private readonly string insert;
		private readonly string update;
		private readonly string delete;

		public PrivilegiosDao()
		{
			select = "select* from Privilegios";
			insert = " insert into Privilegios values (@id,@tipo)";
			update = "update Privilegios set =  id= @id, tipo= @tipo where Id=@Id";
			delete = "delete from Privilegios where Id=@Id";
		}
		public int Add(PrivilegiosEntity entity)
		{
			parameters = new List<SqlParameter>
		{
		  new SqlParameter("@tipo", entity.Tipo)
	   };
			return ExecuteNonQuery(insert);
		}


		public int Update(PrivilegiosEntity entity)
		{
			parameters = new List<SqlParameter>
		{
		  new SqlParameter("@id", entity.Id),
		  new SqlParameter("@tipo", entity.Tipo)
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

		public IEnumerable<PrivilegiosEntity> GetAll()
		{
			DataTable tableresult = ExecuteReader(select);
			List<PrivilegiosEntity> getList = new List<PrivilegiosEntity>();
			foreach (DataRow item in tableresult.Rows)
			{
				getList.Add(new PrivilegiosEntity
				{
					Id = Convert.ToInt32(item[0]),
					Tipo = Convert.ToString(item[1])
				});
			}

			return getList;
		}

	}


}
