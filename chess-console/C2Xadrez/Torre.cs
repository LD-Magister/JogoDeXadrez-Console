using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1Tabuleiro;

namespace C2Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] aux = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            //Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.BuscaPeca(pos) != null && Tabuleiro.BuscaPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha -= 1;
            }

            //Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.BuscaPeca(pos) != null && Tabuleiro.BuscaPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna += 1;
            }

            //Abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.BuscaPeca(pos) != null && Tabuleiro.BuscaPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha += 1;
            }

            //Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.BuscaPeca(pos) != null && Tabuleiro.BuscaPeca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna -= 1;
            }
            return aux;
        }

        public override string ToString()
        {
            return "T";
        }

    }
}
