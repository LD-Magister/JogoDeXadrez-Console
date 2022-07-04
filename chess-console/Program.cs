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
            PosicaoXadrez pos = new('a', 1);
            Console.WriteLine(pos);

            Posicao pos2 = pos.toPosicao();
            Console.WriteLine(pos2);

            /*try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Rei(tab, 0), new Posicao(2, 4));


                Tela.ImprimirTabuleiro(tab);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }*/
        }
    }
}