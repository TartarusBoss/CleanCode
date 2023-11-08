﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemBank
{
    // La idea es que los modelos no tengan lógica (toda la lógica va en los servicios)
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public double Account { get; set; }
        public bool Rewarded { get; set; }
    }
}
