using System;
using System.Collections.Generic;
using loja.gama.Entities.Interfaces;
using System.Text;

namespace loja.gama.Entities
{
    public class Dinheiro : Pagamento, IPagar
    {
        private const double Desconto = 0.05;
        public Dinheiro(double valor)
        {
            Valor = valor;
        }

        public void Pagar()
        {
            var valorDesconto = Valor * Desconto;
            Valor = Valor - valorDesconto;

            DataPagamento = DateTime.Now;
            Confirmacao = true;
        }

        //public override void Pagar()
        //{
        //    var valorDesconto = Valor * Desconto;
        //    Valor = Valor - valorDesconto;

        //    base.Pagar();
        //}

    }
}
