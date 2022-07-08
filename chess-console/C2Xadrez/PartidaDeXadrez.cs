using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C1Tabuleiro;
using Exceptions;

namespace C2Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public HashSet<Peca> Pecas { get; set; }
        public HashSet<Peca> Capturadas { get; set; }
        public bool Xeque { get; set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            AdicionarPecas();
            Xeque = false;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdeMovimentos();            
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.ReduzirQtdeMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new BoardException("Você não pode se colocar em xeque");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque =false;
            }

            Turno++;
            MudaJogador();
        }

        public void ValidaPosicaoOrigem(Posicao pos)
        {
            if (Tab.BuscaPeca(pos) == null)
            {
                throw new BoardException("Não há peça na casa selecionada");
            }
            if (JogadorAtual != Tab.BuscaPeca(pos).Cor)
            {
                throw new BoardException("A peça na casa escolhida não é sua");
            }
            if (!Tab.BuscaPeca(pos).ExisteMovimentoPossivel())
            {
                throw new BoardException("Não existe movimentos possíveis para a peça escolhida");
            }
        }

        public void ValidaPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.BuscaPeca(origem).PodeMoverPara(destino))
            {
                throw new BoardException("Não é possível mover a peça selecionada para essa casa");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in Capturadas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in Pecas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca PecaRei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }                
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = PecaRei(cor);
            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] aux = peca.MovimentosPossiveis();
                if (aux[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public void AdicionarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void AdicionarPecas()
        {
            AdicionarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            AdicionarNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
            AdicionarNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
            AdicionarNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
            AdicionarNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
            AdicionarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));

            AdicionarNovaPeca('c', 7, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('d', 8, new Rei(Tab, Cor.Preta));
        }
    }
}

