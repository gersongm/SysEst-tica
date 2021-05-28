using DataAccess.Dao;
using DataAccess.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class PrivilegiosModel
	{
		public int Id { get; set; }
		public string Tipo { get; set; }

		public EntityState State { private get; set; }
		private readonly PrivilegiosDao privilegiosDao;
		private List<PrivilegiosModel> getList;

		public PrivilegiosModel()
		{
			privilegiosDao = new PrivilegiosDao();
		}
		public string SaveChanges()
		{
			string message = null;
			try
			{
				PrivilegiosEntity dataModel = new PrivilegiosEntity
				{
					Id = Id,
					Tipo = Tipo
				};

				switch (State)
				{
					case EntityState.Added:
						_ = privilegiosDao.Add(dataModel);
						message = "Registro guardado correctamente.";
						break;
					case EntityState.Modified:
						_ = privilegiosDao.Update(dataModel);
						message = "Registro modificado correctamente.";
						break;
					case EntityState.Deleted:
						_ = privilegiosDao.Delete(Id);
						message = "Registro Eliminado.";
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				message = ex.ToString();
			}
			return message;
		}
		public List<PrivilegiosModel> GetAll()
		{
			IEnumerable<PrivilegiosEntity> privilegiosDataModel = privilegiosDao.GetAll();
			getList = new List<PrivilegiosModel>();

			foreach (PrivilegiosEntity item in privilegiosDataModel)
			{
				getList.Add(new PrivilegiosModel
				{
					Id = item.Id,
					Tipo = item.Tipo
				});
			}
			return getList;
		}

	}


}
