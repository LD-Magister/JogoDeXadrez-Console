using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1Tabuleiro;

namespace C2Xadrez
{
    internal class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca peca = Tabuleiro.BuscaPeca(pos);
            return peca != null && peca.CorPeca != CorPeca;
        }

        private bool CasaLivre(Posicao pos)
        {
            return Tabuleiro.BuscaPeca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] aux = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(1, 1);

            if (CorPeca == Cor.Branca)
            {
                //Andar 1 Casa
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && CasaLivre(pos))
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }

                //Andar 2 Casas
                pos.DefinirValores(Posicao.Linha -2 , Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && CasaLivre(pos) && QtdeMovimentos == 0)
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }

                //Capturar Direita
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }

                //Capturar Esquerda
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }
            }
            else
            {
                //Andar 1 Casa
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && CasaLivre(pos))
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }

                //Andar 2 Casas
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && CasaLivre(pos) && QtdeMovimentos == 0)
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }

                //Capturar Direita
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }

                //Capturar Esquerda
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    aux[pos.Linha, pos.Coluna] = true;
                }
            }

            return aux;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
