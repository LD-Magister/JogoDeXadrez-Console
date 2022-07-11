using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1Tabuleiro
{
    abstract internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Tabuleiro Tabuleiro { get; set; }
        public Cor CorPeca { get; private set; }
        public int QtdeMovimentos { get; set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            CorPeca = cor;
            Tabuleiro = tabuleiro;
            QtdeMovimentos = 0;
        }

        public void IncrementarQtdeMovimentos()
        {
            QtdeMovimentos++;
        }

        public void ReduzirQtdeMovimentos()
        {
            QtdeMovimentos--;
        }

        protected bool PodeMover(Posicao pos)
        {
            Peca p = Tabuleiro.BuscaPeca(pos);
            return p == null || p.CorPeca != CorPeca;
        }

        public bool ExisteMovimentoPossivel()
        {
            bool[,] aux = MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (aux[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossivelMovimento(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
