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
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(2, 4));
                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(7, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(3, 5));
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 7));


                Tela.ImprimirTabuleiro(tab);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}