﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmClientVS.Domain.IRepository;

namespace SwarmClientVS.Domain.DataModel
{
    public class ProjectData : IData
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string GetCleanName()
        {
            return Name.Replace(".sln", "");
        }
    }
}
