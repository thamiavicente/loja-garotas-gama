using System;
using System.Collections.Generic;
using System.Text;

namespace loja.gama.Entities
{
    public class Geladeira : Produto
    {
        public Geladeira(string modelo,
                         string tamanho,
                         double preco)

            : base(modelo, tamanho, preco)
        {
        }

        public override void PrecoDesconto()
        {
            base.PrecoDesconto();
            PrecoComDesconto = Preco - 100;
        }

    }
}
