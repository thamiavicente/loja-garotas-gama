using System;
using System.Collections.Generic;
using System.Text;

namespace loja.gama.Entities
{
    public class Produto
    {
        public Produto(string modelo,
                        string tamanho,
                        double preco)
        {
            Id = Guid.NewGuid();

            Modelo = modelo;
            Tamanho = tamanho;
            Preco = preco;
        }

        public Guid Id { get; set; }
        public string Modelo { get; set; }
        public string Tamanho { get; set; }
        public double Preco { get; set; }
        public double PrecoComDesconto { get; set; }

        public virtual void PrecoDesconto()
        {
            var desconto = Preco * 0.15;
            PrecoComDesconto = Preco - desconto;
        }
    }
}
