using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int Owner { get; set; }
        public string Recipient { get; set; }
    }
}