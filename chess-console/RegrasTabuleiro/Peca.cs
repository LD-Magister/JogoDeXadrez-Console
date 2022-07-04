using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasTabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public Tabuleiro Tabuleiro { get; set; }
        public int QtdeMovimentos { get; set; }        

        public Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;            
            Tabuleiro = tabuleiro;
            QtdeMovimentos = 0;
        }
    }
}
