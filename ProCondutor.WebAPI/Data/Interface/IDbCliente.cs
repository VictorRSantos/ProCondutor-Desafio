using ProCondutor.WebAPI.Models;
using System.Collections.Generic;

namespace ProCondutor.WebAPI.Data.Interface
{
    public interface IDbCliente
    {
        List<Cliente> Clientes();        
        bool Insert(Cliente cliente);
        bool Update(Cliente cliente);
        bool Delete(int Id);


    }
}
