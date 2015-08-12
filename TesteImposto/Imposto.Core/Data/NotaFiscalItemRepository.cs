using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Imposto.Core.Data
{
    public class NotaFiscalItemRepository
    {
         public int Salvar(int idNotaFiscal, string cfop, string tipoICMS, double baseICMS, double aliquotaICMS, double valorICMS,
                           double baseIpi, double aliquotaIpi, double valorIpi, string nomeProduto, string codigoProduto)
         {
            int id = 0;
            if (cfop == null) cfop = "";
            var connectionString = ConfigurationManager.ConnectionStrings["Teste"].ConnectionString;
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                SqlCommand command = new SqlCommand("P_NOTA_FISCAL_ITEM", conexao);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pId", id).Direction = ParameterDirection.InputOutput;                
                command.Parameters.AddWithValue("@pIdNotaFiscal", idNotaFiscal);
                command.Parameters.AddWithValue("@pCfop", cfop);
                command.Parameters.AddWithValue("@pTipoIcms", tipoICMS);
                command.Parameters.AddWithValue("@pBaseIcms", baseICMS);
                command.Parameters.AddWithValue("@pAliquotaIcms", aliquotaICMS);
                command.Parameters.AddWithValue("@pValorIcms", valorICMS);
                command.Parameters.AddWithValue("@pBaseIpi", baseIpi);
                command.Parameters.AddWithValue("@pAliquotaIpi", aliquotaIpi);
                command.Parameters.AddWithValue("@pValorIpi", valorIpi);
                command.Parameters.AddWithValue("@pNomeProduto", nomeProduto);
                command.Parameters.AddWithValue("@pCodigoProduto", codigoProduto);
                try
                {
                    command.ExecuteNonQuery();
                    id = (int)command.Parameters["@pId"].Value;
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
