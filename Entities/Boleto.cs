﻿using System;
using System.Collections.Generic;
using System.Text;
using loja.gama.Entities.Interfaces;

namespace loja.gama.Entities
{
    public class Boleto : Pagamento, IPagar
    {
        private const int DiasVencimento = 15;
        private const double Juros = 0.10;

        public Boleto(string cpf,
                        double valor
                        //string descricao
                        )
        {
            Cpf = cpf;
            Valor = valor;
            // Descricao = descricao;
            DataEmissao = DateTime.Now;
        }

        public Guid CodigoBarra { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Descricao { get; set; }

        public void GerarBoleto()
        {
            CodigoBarra = Guid.NewGuid();
            DataVencimento = DataEmissao.AddDays(DiasVencimento);
        }

        public bool EstaPago()
        {
            return Confirmacao;
        }

        public bool EstaVencido()
        {
            return DataVencimento < DateTime.Now;
        }

        public void CalcularJuros()
        {
            var taxa = Valor * Juros;
            Valor += taxa;
        }

        //public void Pagar()
        //{
        //    DataPagamento = DateTime.Now;
        //    Confirmacao = true;
        //}
    }
}

