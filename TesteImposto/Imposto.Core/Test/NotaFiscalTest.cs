using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imposto.Core.Domain;

namespace Imposto.Core.Test
{
    [TestClass]
    class NotaFiscalTest
    {
        private NotaFiscal CriarNotaFiscal(string nomeCliente, string estadoOrigem, string estadoDestino,  string nomeProduto, string codigoProduto, double Valor)
        {
            Pedido pedido = new Pedido();
            pedido.NomeCliente = nomeCliente;            
            pedido.EstadoOrigem = estadoOrigem;
            pedido.EstadoDestino = estadoDestino;
            PedidoItem item = new PedidoItem();            
            item.NomeProduto = "Teste";
            item.CodigoProduto = "123456789";
            item.ValorItemPedido = 250.0;
            item.Brinde = false;
            pedido.ItensDoPedido.Add(item);
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);
            return notaFiscal;
        }
        [TestMethod]
        public void ValorIcmsTest()
        {
            NotaFiscal notaFiscal = CriarNotaFiscal("Teste", "SP", "MG", "Tenis XPTO", "12345", 78.0);
            foreach (var item in notaFiscal.ItensDaNotaFiscal)
            {
                Assert.AreEqual(item.BaseIcms * item.AliquotaIcms, item.ValorIcms, 0.001, "Valor ICMS calculado de forma errada.");
            }
        }
        [TestMethod]
        public void ValorIpiTest()
        {
            NotaFiscal notaFiscal = CriarNotaFiscal("Teste", "SP", "MG", "Tenis XPTO", "12345", 78.0);
            foreach (var item in notaFiscal.ItensDaNotaFiscal)
            {
                Assert.AreEqual(item.BaseIpi * item.AliquotaIpi, item.ValorIpi, 0.001, "Valor Ipi calculado de forma errada.");
            }
        }
    }
}
