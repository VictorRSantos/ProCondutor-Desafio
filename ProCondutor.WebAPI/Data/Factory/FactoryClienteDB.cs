using ProCondutor.WebAPI.ArquivoConfiguracao;
using ProCondutor.WebAPI.Data.Interface;
using ProCondutor.WebAPI.Data.MSSQL;
using System;

namespace ProCondutor.WebAPI.Data.Factory
{
    public static class FactoryClienteDB
    {
        public static IDbCliente GetDBCliente(Configuracao configuracao)
        {
            switch (configuracao.TipoBanco)
            {
                case Enums.TipoBanco.MSSQL:
                    return new MSSQLCliente(configuracao);
                case Enums.TipoBanco.ORACLE:                   
                case Enums.TipoBanco.MYSQL:                    
                default:
                    throw new ArgumentNullException($"Não existe configuração para o banco {configuracao.TipoBanco}");
            }


        }

    }
}
