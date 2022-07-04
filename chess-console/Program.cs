using System;
using C1Tabuleiro;
using C2Xadrez;

namespace chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.ColocarPeca(new Rei(tab, 0), new Posicao(2, 4));


            Tela.ImprimirTabuleiro(tab);
        }
    }
}