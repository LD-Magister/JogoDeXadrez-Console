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
        public Peca PodeEnPassant { get; private set; }

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
            PodeEnPassant = null;
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

            //#jogadaespecial Roque Menor
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            //#jogadaespecial Roque Maior
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            //#jogadaespecial En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posPeaoInimigo;
                    if (p.CorPeca == Cor.Branca)
                    {
                        posPeaoInimigo = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posPeaoInimigo = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.RetirarPeca(posPeaoInimigo);
                    Capturadas.Add(pecaCapturada);
                }
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

            //#jogadaespecial Roque Menor
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.ReduzirQtdeMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }

            //#jogadaespecial Roque Maior
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.ReduzirQtdeMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }

            //#jogadaespecial En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == PodeEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posPeao;
                    if (p.CorPeca == Cor.Branca)
                    {
                        posPeao = new Posicao(destino.Linha + 1, origem.Coluna);
                    }
                    else
                    {
                        posPeao = new Posicao(destino.Linha - 1, origem.Coluna);
                    }

                    Tab.ColocarPeca(peao, posPeao);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            Peca pecaMovida = Tab.BuscaPeca(destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new BoardException("Você não pode se colocar em xeque");
            }

            if (pecaMovida is Peao)
            {
                if ((pecaMovida.CorPeca == Cor.Branca && destino.Linha == 0) || (pecaMovida.CorPeca == Cor.Preta && destino.Linha == 7))
                {

                    Peca pecaProm;
                    Console.WriteLine("Selecione uma promoção:");
                    Console.WriteLine("d - Dama");
                    Console.WriteLine("b - Bispo");
                    Console.WriteLine("c - Cavalo");
                    Console.WriteLine("t - Torre");
                    char escolhaPromocao = char.Parse(Console.ReadLine());

                    switch (escolhaPromocao)
                    {
                        case 'd':
                            pecaProm = new Dama(Tab, pecaMovida.CorPeca);
                            break;
                        case 'b':
                            pecaProm = new Bispo(Tab, pecaMovida.CorPeca);
                            break;
                        case 'c':
                            pecaProm = new Cavalo(Tab, pecaMovida.CorPeca);
                            break;
                        case 't':
                            pecaProm = new Torre(Tab, pecaMovida.CorPeca);
                            break;
                        default:
                            Console.WriteLine("Valor selecionado inválido, peça será promovida para Dama");
                            pecaProm = new Dama(Tab, pecaMovida.CorPeca);
                            break;
                    }

                    Tab.RetirarPeca(destino);
                    Pecas.Remove(pecaMovida);
                    Tab.ColocarPeca(pecaProm, destino);
                    Pecas.Add(pecaProm);
                }
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

            //#jogadaespecial En Passant

            if (pecaMovida is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                PodeEnPassant = pecaMovida;
            }
            else
            {
                PodeEnPassant = null;
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
            AdicionarNovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
            AdicionarNovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
            AdicionarNovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
            AdicionarNovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
            AdicionarNovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
            AdicionarNovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
            AdicionarNovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
            AdicionarNovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));

            AdicionarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            AdicionarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            AdicionarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            AdicionarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            AdicionarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            AdicionarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            AdicionarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            AdicionarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
            AdicionarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
            AdicionarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
            AdicionarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
            AdicionarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
            AdicionarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
            AdicionarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
            AdicionarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));
        }
    }
}

