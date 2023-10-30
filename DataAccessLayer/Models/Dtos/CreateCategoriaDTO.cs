using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos
{
    public class CreateCategoriaDTO
    {

        public string Nombre { get; set; } = "";

        public int[] CategoriasRelacionadas { get; set; } = new int[] {};
    }
}
