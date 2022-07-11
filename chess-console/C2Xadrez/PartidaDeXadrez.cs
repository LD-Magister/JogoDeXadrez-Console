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

            //jogadaespecial Roque Menor
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            //jogadaespecial Roque Maior
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
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

            //jogadaespecial Roque Menor
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.ReduzirQtdeMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }

            //jogadaespecial Roque Maior
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.ReduzirQtdeMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }
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
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        public void ValidaPosicaoOrigem(Posicao pos)
        {
            if (Tab.BuscaPeca(pos) == null)
            {
                throw new BoardException("Não há peça na casa selecionada");
            }
            if (JogadorAtual != Tab.BuscaPeca(pos).CorPeca)
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
            if (!Tab.BuscaPeca(origem).PossivelMovimento(destino))
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
                if (peca.CorPeca == cor)
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
                if (peca.CorPeca == cor)
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

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] aux = peca.MovimentosPossiveis();

                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (aux[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapurada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapurada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void AdicionarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void AdicionarPecas()
        {
            AdicionarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            AdicionarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            AdicionarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            AdicionarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            AdicionarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            AdicionarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            AdicionarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            AdicionarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            AdicionarNovaPeca('a', 2, new Peao(Tab, Cor.Branca));
            AdicionarNovaPeca('b', 2, new Peao(Tab, Cor.Branca));
            AdicionarNovaPeca('c', 2, new Peao(Tab, Cor.Branca));
            AdicionarNovaPeca('d', 2, new Peao(Tab, Cor.Branca));
            AdicionarNovaPeca('e', 2, new Peao(Tab, Cor.Branca));
            AdicionarNovaPeca('f', 2, new Peao(Tab, Cor.Branca));
            AdicionarNovaPeca('g', 2, new Peao(Tab, Cor.Branca));
            AdicionarNovaPeca('h', 2, new Peao(Tab, Cor.Branca));

            AdicionarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            AdicionarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            AdicionarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            AdicionarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            AdicionarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            AdicionarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            AdicionarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('a', 7, new Peao(Tab, Cor.Preta));
            AdicionarNovaPeca('b', 7, new Peao(Tab, Cor.Preta));
            AdicionarNovaPeca('c', 7, new Peao(Tab, Cor.Preta));
            AdicionarNovaPeca('d', 7, new Peao(Tab, Cor.Preta));
            AdicionarNovaPeca('e', 7, new Peao(Tab, Cor.Preta));
            AdicionarNovaPeca('f', 7, new Peao(Tab, Cor.Preta));
            AdicionarNovaPeca('g', 7, new Peao(Tab, Cor.Preta));
            AdicionarNovaPeca('h', 7, new Peao(Tab, Cor.Preta));
        }
    }
}

