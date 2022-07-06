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

                    bool[,] marcacao = partida.Tab.BuscaPeca(origem).MovimentosPossiveis();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez();
                    marcacao = null;

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