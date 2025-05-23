using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIPC.Models
{
    public class Trip
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string Status { get; set; } // "por aceitar"
        public string HoraSubmit { get; set; }
        public int? UserId { get; set; } // opcional: para associar ao usuário
    }

}
