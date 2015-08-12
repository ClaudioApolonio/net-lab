using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Pedido
    {
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public string NomeCliente { get; set; }

        public List<PedidoItem> ItensDoPedido { get; set; }

        public Pedido()
        {
            ItensDoPedido = new List<PedidoItem>();
        }

        public bool EstadoOrigemValido()
        {            
            return (this.EstadoOrigem == "SP") || (this.EstadoOrigem == "MG");            
        }

        public bool EstadoDestinoValido()
        {
            return (this.EstadoDestino == "RJ") || (this.EstadoDestino == "PE") || (this.EstadoDestino == "MG") || (this.EstadoDestino == "PB")
                   || (this.EstadoDestino == "PR") || (this.EstadoDestino == "PI") || (this.EstadoDestino == "RO") || (this.EstadoDestino == "SE")
                   || (this.EstadoDestino == "TO") || (this.EstadoDestino == "PA");            
        }
    }
}
