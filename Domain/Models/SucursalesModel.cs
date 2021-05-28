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
	public class SucursalesModel
	{
		public int Id { get; set; }
		public string Sucursal { get; set; }
		public string Ubicacion { get; set; }
		public string Teléfono { get; set; }

		public EntityState State { private get; set; }
		private readonly SucursalesDao sucursalesDao;
		private List<SucursalesModel> getList;

		public SucursalesModel()
		{
			sucursalesDao = new SucursalesDao();
		}
		public string SaveChanges()
		{
			string message = null;
			try
			{
				SucursalesEntity dataModel = new SucursalesEntity
				{
					Id = Id,
					Sucursal = Sucursal,
					Ubicacion = Ubicacion,
					Teléfono = Teléfono
				};

				switch (State)
				{
					case EntityState.Added:
						_ = sucursalesDao.Add(dataModel);
						message = "Registro guardado correctamente.";
						break;
					case EntityState.Modified:
						_ = sucursalesDao.Update(dataModel);
						message = "Registro modificado correctamente.";
						break;
					case EntityState.Deleted:
						_ = sucursalesDao.Delete(Id);
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
		public List<SucursalesModel> GetAll()
		{
			IEnumerable<SucursalesEntity> sucursalesDataModel = sucursalesDao.GetAll();
			getList = new List<SucursalesModel>();

			foreach (SucursalesEntity item in sucursalesDataModel)
			{
				getList.Add(new SucursalesModel
				{
					Id = item.Id,
					Sucursal = item.Sucursal,
					Ubicacion = item.Ubicacion,
					Teléfono = item.Teléfono
				});
			}
			return getList;
		}

	}


}
