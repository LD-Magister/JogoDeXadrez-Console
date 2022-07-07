using System;
using C1Tabuleiro;
using C2Xadrez;
using Exceptions;

namespace chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez();

                    bool[,] posicoesPossiveis = partida.Tab.BuscaPeca(origem).MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez();
                   

                    partida.ExecutaMovimento(origem, destino);
                }                
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}