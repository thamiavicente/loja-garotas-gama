﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Girls.Gama2
{
    public class Boleto
    {
        private const int DiasVencimento = 15;
        private const decimal Juros = 0.10M;

        public Boleto(string cpf,
                       decimal valor,
                       string descricao)
        {
            Cpf = cpf;
            Valor = valor;
            Descricao = descricao;

            DataEmissao = DateTime.Now;
            Confirmacao = false;
        }

        public Guid CodigoBarra { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Confirmacao { get; set; }
        public string Cpf { get; set; }
        public string Descricao { get; set; }

        public void GerarBoleto()
        {
            CodigoBarra = Guid.NewGuid();
            DataVencimento = DataEmissao.AddDays(DiasVencimento);
        }

        public void CalcularJuros()
        {
            var taxa = Valor * Juros;
            Valor += taxa;
        }

        public void Pagar()
        {
            DataPagamento = DateTime.Now;
            Confirmacao = true;
        }
    }
}
