using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2Xadrez;
using Exceptions;

namespace C1Tabuleiro
{
    internal class Tabuleiro
    {
        //Propriedades
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        //Construtores
        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        //Métodos
        public Peca BuscaPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca BuscaPeca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return BuscaPeca(pos) != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos)) {
                throw new BoardException("Já existe uma peça nessa posição");
            }
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (BuscaPeca(pos) == null)
            {
                return null;
            }
            else
            {
                Peca aux = BuscaPeca(pos);
                aux.Posicao = null;
                Pecas[pos.Linha, pos.Coluna] = null;
                return aux;
            }

        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new BoardException("Posição inválida");
            }
        }
    }
}
