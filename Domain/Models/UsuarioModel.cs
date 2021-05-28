using DataAccess.Dao;
using DataAccess.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models
{
	public class UsuariosModel
	{
		[Key]
		public int Id { get; set; }
		[Required, MaxLength(25)]
		public string Usuario { get; set; }
		[Required, MaxLength(100)]
		public string Contraseña { get; set; }
		[Required, MaxLength(50)]
		public string Nombres { get; set; }
		[Required, MaxLength(100)]
		public string Email { get; set; }
		public int Privilegios {get; set; }
		public string Posición
        {
			get
            {
				return (new PrivilegiosDao().GetAll()).Where(o => o.Id == Privilegios).Select(x => x.Tipo).FirstOrDefault();
			}
        }
		public int Sucursal { get; set; }
		public string Area {
			get
			{
				return (new SucursalesDao()).GetAll().Where(o => o.Id == Sucursal).Select(x => x.Sucursal).FirstOrDefault();
			}
		}

		public EntityState State { private get; set; }
		private readonly UsuariosDao usuariosDao;
		
		private List<UsuariosModel> getList;

		public UsuariosModel()
		{
			usuariosDao = new UsuariosDao();
		}
		public string SaveChanges()
		{
			string message = null;
			try
			{
				string password =Helper.Encrypt(string.Concat(Usuario, Contraseña));
				UsuariosEntity dataModel = new UsuariosEntity
				{
					Id=Id,
					Usuario = Usuario.TrimEnd(),
					Contraseña = password,
					Nombres = Nombres.TrimEnd(),
					Email = Email.TrimEnd(),
					Privilegios = Privilegios,
					Sucursal = Sucursal
				};

				switch (State)
				{
					case EntityState.Added:
						_ = usuariosDao.Add(dataModel);
						message = "Registro guardado correctamente.";
						break;
					case EntityState.Modified:
						_ = usuariosDao.Update(dataModel);
						message = "Registro modificado correctamente.";
						break;
					case EntityState.Deleted:
						_ = usuariosDao.Delete(Id);
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
		public List<UsuariosModel> GetAll()
		{
			IEnumerable<UsuariosEntity> usuariosDataModel = usuariosDao.GetAll();
			getList = new List<UsuariosModel>();

			foreach (UsuariosEntity item in usuariosDataModel)
			{
				getList.Add(new UsuariosModel
				{
					Id=item.Id,
					Usuario = item.Usuario,
					Contraseña = Helper.Decrypt( item.Contraseña).Substring(item.Usuario.TrimEnd().Length),
					Nombres = item.Nombres,
					Email = item.Email,
					Privilegios = item.Privilegios,
					Sucursal = item.Sucursal
				});
			}
			return getList;
		}

		public  UsuariosModel Login(string user,string pass)
        {
			UsuariosModel usuario=null;
			string password = Helper.Encrypt(string.Concat(user, pass));

			usuario = GetAll().Where(x => x.Usuario.Trim() == user || x.Email.Trim() == user && x.Contraseña.Trim() == password).FirstOrDefault();

			

			return usuario;
        }
		public bool securityLogin()
		{
			if (Id >=0)
			{				
				if (Login(Usuario.Trim(),Contraseña.Trim() )!=null)
					return true;
				else
					return false;
			}
			else
				return false;
		}

	}


}


