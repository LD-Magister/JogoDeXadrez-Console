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
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez();
                        partida.ValidaPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.BuscaPeca(origem).MovimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez();
                        partida.ValidaPosicaoDestino(origem, destino);


                        partida.RealizaJogada(origem, destino);
                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }                    
                }
                Console.Clear();
                Tela.ImprimirPartida(partida);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}