using Imposto.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    
    public class NotaFiscal
    {        
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }
        
        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }        

        public void EmitirNotaFiscal(Pedido pedido)
        {
            this.NumeroNotaFiscal = 99999;
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;

            this.EstadoDestino = pedido.EstadoDestino;
            this.EstadoOrigem = pedido.EstadoOrigem;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                if (this.EstadoDestino == "RJ")
                {
                    notaFiscalItem.Cfop = "6.000";                    
                }
                else if (this.EstadoDestino == "PE")
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if (this.EstadoDestino == "MG")
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if (this.EstadoDestino == "PB")
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if (this.EstadoDestino == "PR")
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if (this.EstadoDestino == "PI")
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if (this.EstadoDestino == "RO")
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if (this.EstadoDestino == "SE")
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if (this.EstadoDestino == "TO")
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if (this.EstadoDestino == "PA")
                {
                    notaFiscalItem.Cfop = "6.010";
                }

                if (this.EstadoDestino == this.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }
                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido*0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }             
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms*notaFiscalItem.AliquotaIcms;

                notaFiscalItem.BaseIpi = itemPedido.ValorItemPedido;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                    notaFiscalItem.AliquotaIpi = 0.0;
                }
                else
                {
                    notaFiscalItem.AliquotaIpi = 0.1;
                }
                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;
                
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                this.ItensDaNotaFiscal.Add(notaFiscalItem);
                
            }
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(NotaFiscal));
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML" + this.NumeroNotaFiscal + "-" + this.Serie + ".xml";
            System.IO.FileStream file = System.IO.File.Create(path);
            x.Serialize(file, this);
            file.Close();

            if (System.IO.File.Exists(path))
            {
                this.Salvar();
            }
        }

        private void Salvar()
        {
            NotaFiscalRepository notaFiscalRepository = new NotaFiscalRepository();
            this.Id = notaFiscalRepository.Salvar(this.NumeroNotaFiscal, this.Serie, this.NomeCliente, this.EstadoOrigem, this.EstadoDestino);
            NotaFiscalItemRepository notaFiscalItemRepository = new NotaFiscalItemRepository();
            foreach (var item in this.ItensDaNotaFiscal)
            {
                item.Id = notaFiscalItemRepository.Salvar(this.Id, item.Cfop, item.TipoIcms, item.BaseIcms, item.AliquotaIcms, item.ValorIcms,
                                                          item.BaseIpi, item.AliquotaIpi, item.ValorIpi, item.NomeProduto, item.CodigoProduto);
            }
        }

    }
}
