using ProCondutor.Domain.Models;
using System.Collections.Generic;

namespace ProCondutor.Web.Models
{
    public class ClienteViewModel
    {
        public Cliente Cliente { get; set; }

        public List<Cliente> Clientes { get; set; }
    }
}
