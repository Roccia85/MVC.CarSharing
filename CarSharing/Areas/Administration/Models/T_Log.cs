using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarSharing.EF.Models
{
    public class T_Log
    {
        [Key]
        public int Id { get; set; }

        public string IdUser { get; set; }

        public DateTime DatOra { get; set; }

        public string Azione { get; set; }
    }
}