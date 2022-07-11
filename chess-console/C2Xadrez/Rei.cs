using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1Tabuleiro;

namespace C2Xadrez
{
    internal class Rei : Peca
    {
        public PartidaDeXadrez Partida { get; set; }
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
        }

        private bool TesteRoqueTorre(Posicao pos)
        {
            Peca peca = Tabuleiro.BuscaPeca(pos);
            return peca != null && peca is Torre && peca.CorPeca == CorPeca && peca.QtdeMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] aux = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(1, 1);

            //Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Diagonal Superior Direita            
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Diagonal Inferior Direita
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }
            //Abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }
            //Diagonal Inferior Esquerda
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //Diagonal Superior Esquerda
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                aux[pos.Linha, pos.Coluna] = true;
            }

            //#jogadaespecial Roque
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna);
            if (QtdeMovimentos == 0 && !Partida.Xeque)
            {
                //#jogadaespecial Roque Menor
                Posicao torreH = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteRoqueTorre(torreH))
                {
                    Posicao posCasaG = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    Posicao posCasaF = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.BuscaPeca(posCasaG) == null && Tabuleiro.BuscaPeca(posCasaF) == null)
                    {
                        aux[pos.Linha, pos.Coluna + 2] = true;
                    }
                }

                //#jogadaespecial Roque Maior
                Posicao torreA = new Posicao(Posicao.Linha, Posicao.Coluna -4);
                if (TesteRoqueTorre(torreA))
                {
                    Posicao posCasa2 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    Posicao posCasa3 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posCasa4 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);

                    if (Tabuleiro.BuscaPeca(posCasa2) == null && Tabuleiro.BuscaPeca(posCasa3) == null && Tabuleiro.BuscaPeca(posCasa4) == null)
                    {
                        aux[pos.Linha, pos.Coluna - 2] = true;
                    }
                }
            }

            return aux;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
