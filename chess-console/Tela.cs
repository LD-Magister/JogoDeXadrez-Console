using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1Tabuleiro;
using C2Xadrez;

namespace chess
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(8 - i + " ");
                //Console.ForegroundColor = ConsoleColor.Gray;
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimePeca(tab.BuscaPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  A B C D E F G H");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(8 - i + " ");               
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    ImprimePeca(tab.BuscaPeca(i, j));
                    
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  A B C D E F G H");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor= ConsoleColor.Black;
        }

        public static void ImprimePeca(Peca peca)
        {
            if (peca == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(peca + " ");
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(peca + " ");
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }                
            }
        }

        public static Posicao LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha).toPosicao();
        }       
    }
}
