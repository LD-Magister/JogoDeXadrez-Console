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
        public Cor Cor { get; set; }        
        public int QtdeMovimentos { get; set; }        

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;            
            Tabuleiro = tabuleiro;
            QtdeMovimentos = 0;
        }

        public void IncrementarQtdeMovimentos()
        {
            QtdeMovimentos++;
        }

        protected bool PodeMover(Posicao pos)
        {
            Peca p = Tabuleiro.BuscaPeca(pos);
            return p == null || p.Cor != Cor;
        }

        public abstract bool[,] MovimentosPossiveis();        
    }
}
