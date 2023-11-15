﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Dtos
{
    public class CompraOKDTO
    {

        public bool Ok { get; set; } = false;

        public string Mensaje { get; set; } = "";

        public string compraId {  get; set; } = string.Empty;
    }
}
