using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
	public class UsuariosEntity
	{
		public int Id { get; set; }
		public string Usuario { get; set; }
		public string Contraseña { get; set; }
		public string Nombres { get; set; }
		public string Email { get; set; }
		public int Privilegios { get; set; }
		public int Sucursal { get; set; }
	}


}
