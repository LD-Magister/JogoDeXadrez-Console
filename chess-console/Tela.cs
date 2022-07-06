using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1Tabuleiro;

namespace chess
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(8-i + " ");
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.BuscaPeca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimePeca(tab.BuscaPeca(i, j));                        
                    }                    
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("  A B C D E F G H");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ImprimePeca(Peca peca)
        {
            if (peca.Cor == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(peca + " ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(peca + " ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
