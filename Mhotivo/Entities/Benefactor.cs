﻿using System.Collections.Generic;

namespace Mhotivo.Models
{
    public class Benefactor : People
    {
        public int Capacity { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}