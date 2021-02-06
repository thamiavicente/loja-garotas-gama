using loja.gama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace loja.gama
{
    class Program
    {
        private static List<Boleto> listaBoletos;
        private static List<Dinheiro> listaAVista;
        static void Main(string[] args)
        {
            listaBoletos = new List<Boleto>();
            listaAVista = new List<Dinheiro>();

            while (true)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("============== Loja Garotas Gama =============");

                Console.WriteLine("Selecione uma opção");
                Console.WriteLine("1-Compra | 2-Pagamento | 3-Relatório");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Comprar();
                        break;
                    case 2:
                        Pagamento();
                        break;
                    case 3:
                        Relatorio();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Comprar()
        {
            var geladeira = new Geladeira("Vivace LG", "300L", 4999.99);
            geladeira.PrecoDesconto();

            var televisao = new Televisao("QLED Samsung", "55 polegadas", 4599.99);
            televisao.PrecoDesconto();

            Console.WriteLine("Qual produto deseja comprar?");
            Console.WriteLine("1-Geladeira | 2-Televisão");

            var opcaoProduto = int.Parse(Console.ReadLine());
            double valorCompra;

            if (opcaoProduto == 1)
            {
                Console.WriteLine("===== Geladeira =====");
                Console.WriteLine($"== Modelo: {geladeira.Modelo}");
                Console.WriteLine($"== Tamanho: {geladeira.Tamanho}");
                Console.WriteLine($"== Preço: {geladeira.Preco}");
                Console.WriteLine($"== SÓ HOJE POR APENAS: {geladeira.PrecoComDesconto}");
                valorCompra = geladeira.PrecoComDesconto;
            }
            else
            {
                Console.WriteLine("===== Televisao =====");
                Console.WriteLine($"== Modelo: {televisao.Modelo}");
                Console.WriteLine($"== Tamanho: {televisao.Tamanho}");
                Console.WriteLine($"== Preço: {televisao.Preco}");
                Console.WriteLine($"== SÓ HOJE POR APENAS: {televisao.PrecoComDesconto}");
                valorCompra = televisao.PrecoComDesconto;
            }

            var valor = valorCompra;

            Console.WriteLine("Digite o CPF do cliente:");
            var cpf = Console.ReadLine();

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Qual a forma de pagamento:");
            Console.WriteLine("1-Boleto | 2-Dinheiro");

            var opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                var boleto = new Boleto(cpf, valor);
                boleto.GerarBoleto();

                Console.WriteLine($"Boleto gerado com sucesso:\n===== número {boleto.CodigoBarra}\n===== data de vencimento para o dia {boleto.DataVencimento}\n===== valor {boleto.Valor}");

                listaBoletos.Add(boleto);
            }
            else
            {
                Console.WriteLine($"========= Á VISTA: { valor } =========");

                var dinheiro = new Dinheiro(valor);
                dinheiro.Pagar();

                Console.WriteLine($"Pagamento feito com sucesso:\n===== número {dinheiro.Id}\n===== valor {dinheiro.Valor}");

                listaAVista.Add(dinheiro);
            }
        }

        public static void Pagamento()
        {

            Console.WriteLine("Digite o código de barras:");
            var numero = Guid.Parse(Console.ReadLine());

            var boleto = listaBoletos
                            .Where(item => item.CodigoBarra == numero)
                            .FirstOrDefault();

            if (boleto is null)
            {
                Console.WriteLine($"Boleto de código {numero} não encontrado!");
                return;
            }

            if (boleto.EstaPago())
            {
                Console.WriteLine($"Boleto pago no dia {boleto.DataPagamento}");
                return;
            }

            if (boleto.EstaVencido())
            {
                boleto.CalcularJuros();
                Console.WriteLine($"Boleto está vencido, terá acrescimo de 10% === R$ {boleto.Valor}");
            }

            boleto.Pagar();
            Console.WriteLine($"Boleto de código {numero} foi pago com sucesso.");
        }

        public static void Relatorio()
        {
            Console.WriteLine("Qual opção de relatório:");
            Console.WriteLine("1-Boleto | 2-Dinheiro");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    RelatorioBoleto();
                    break;
                case 2:
                    RelatorioAVista();
                    break;
                default:
                    break;
            }
        }

        public static void RelatorioBoleto()
        {
            Console.WriteLine("1-Pagos | 2-À pagar | 3-Vencidos");
            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    BoletosPagos();
                    break;
                case 2:
                    BoletosAPagar();
                    break;
                case 3:
                    BoletosVencidos();
                    break;
                default:
                    break;
            }
        }

        public static void BoletosPagos()
        {
            Console.WriteLine("========== Boletos pagos ==========");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ------------------------------------");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== Boletos pagos ========== \n");
        }

        public static void BoletosAPagar()
        {
            Console.WriteLine("========== Boletos a pagar ==========");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao == false
                                    && item.DataVencimento > DateTime.Now)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ------------------------------------");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== Boletos a pagar ========== \n");
        }

        public static void BoletosVencidos()
        {
            Console.WriteLine("========== Boletos vencidos ==========");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao == false
                                    && item.DataVencimento < DateTime.Now)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ------------------------------------");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== Boletos vencidos ========== \n");
        }

        public static void RelatorioAVista()
        {
            Console.WriteLine("========== Pagamentos à vista ==========");

            var boletos = listaAVista
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ------------------------------------");
                Console.WriteLine($"Pagamento: {item.Id}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }

            Console.WriteLine("========== Pagamentos à vista ========== \n");
        }

    }
}

