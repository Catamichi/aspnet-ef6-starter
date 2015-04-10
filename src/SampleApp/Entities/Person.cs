using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApp.Entities
{
    public class Person
    {
        public Int32 Id { get; set; }
        
        public String Name { get; set; }
    }
}