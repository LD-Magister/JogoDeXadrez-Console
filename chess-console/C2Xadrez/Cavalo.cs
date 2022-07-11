using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1Tabuleiro;

namespace C2Xadrez
{
    internal class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] aux = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(1, 1);

            //Sobe + Direita
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Direita + Sobe
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Direita + Desce
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Desce + Direita
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }
            //Desce + Esquerda
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }
            //Esquerda + Desce
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Esquerda + Sobe
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Sobe + Esquerda
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            return aux;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
