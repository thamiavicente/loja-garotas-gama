using System;
using System.Collections.Generic;
using System.Text;

namespace loja.gama.Entities
{
    public class Televisao : Produto
    {
        public Televisao(string modelo,
                         string tamanho,
                         double preco)

            : base(modelo, tamanho, preco)
        {
        }
    }
}
