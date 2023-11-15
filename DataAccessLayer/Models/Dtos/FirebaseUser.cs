using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos
{
    public class FirebaseUser
    {
        public string Email { get; set; }
        public string Imagen { get; set; }

        public string Uid { get; set; }

        public string Name { get; set; }
    }
}
