using Dapper;
using ProCondutor.WebAPI.ArquivoConfiguracao;
using ProCondutor.WebAPI.Data.Interface;
using ProCondutor.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProCondutor.WebAPI.Data.MSSQL
{
    public class MSSQLCliente : IDbCliente
    {
        private readonly string _conexao;
        public MSSQLCliente(Configuracao configuracao)
        {
            _conexao = configuracao.ConexaoBancoSQLServer;
        }

        public List<Cliente> Clientes()
        {
            try
            {

                var sql = "SELECT Id, Nome, DataNascimento, Observacao, Ativo FROM Clientes";

                using (var con = new SqlConnection(_conexao))
                {

                    return con.Query<Cliente>(sql).ToList();

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new List<Cliente>();
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var sql = "DELETE FROM Clientes WHERE Id = @Id";

                using (var con = new SqlConnection(_conexao))
                {

                    con.Execute(sql, new { Id});

                    return true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public bool Insert(Cliente cliente)
        {
            try
            {
                using (var con = new SqlConnection(_conexao))
                {
                    con.Execute("InsertCliente",new {Nome = cliente.Nome, DataNascimento = cliente.DataNascimento, Observacao = cliente.Observacao, Ativo = cliente.Ativo }, commandType: CommandType.StoredProcedure);

                    return true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public bool Update(Cliente cliente)
        {
            try
            {
                var ativo = cliente.Ativo is true ? 1 : 0;

                var sql = $@" SET LANGUAGE 'Portuguese'
                             UPDATE Clientes
                             SET
                                Nome = '{cliente.Nome}'
                               ,DataNascimento = '{cliente.DataNascimento}'
                               ,Observacao = '{cliente.Observacao}'
                               ,Ativo = {ativo}
                            WHERE
                                Id = {cliente.Id}";

                using (var con = new SqlConnection(_conexao))
                {

                    con.Execute(sql);

                    return true;

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
