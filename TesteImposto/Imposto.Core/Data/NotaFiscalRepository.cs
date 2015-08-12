using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {        
        public int Salvar(int numeroNotaFiscal, int serie, string nomeCliente, string estadoOrigem, string estadoDestino)
        {
            int id = 0;
            var connectionString = ConfigurationManager.ConnectionStrings["Teste"].ConnectionString;
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                SqlCommand command = new SqlCommand("P_NOTA_FISCAL", conexao);               
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pId", id).Direction = ParameterDirection.InputOutput;
                command.Parameters.AddWithValue("@pNumeroNotaFiscal", numeroNotaFiscal);
                command.Parameters.AddWithValue("@pSerie", serie);
                command.Parameters.AddWithValue("@pNomeCliente", nomeCliente);
                command.Parameters.AddWithValue("@pEstadoOrigem", estadoOrigem);
                command.Parameters.AddWithValue("@pEstadoDestino", estadoDestino);
                try
                {
                    command.ExecuteNonQuery();
                    id = (int) command.Parameters["@pId"].Value;
                }
                finally
                {
                    conexao.Close();
                }
            }
            return id;
        }
    }
}
