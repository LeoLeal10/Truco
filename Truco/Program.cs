using System;

namespace Truco
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Truco 10.0

            int reiniciar = 0;
            do
            {

                #region Sorteio de primeiras cartas

                int pontosPCPartida = 0, pontosJCPartida = 0, truco = 0;

                Console.Title = "SuperTruco";
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine("----Olá!! Seja Bem-Vindo ao SUPER TRUCO----");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("As regras são:\n-Melhor de cinco partidas;\n-Caso um truco for aceito, o vencedor da partida ganha o jogo;\n-Para pedir truco digite 4;\n-Você só pode pedir truco 1 vez;");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();

                for (int u = 1; u < 10; u++)
                {
                    string[] naipeCarta = new string[40], numCarta = new string[40], baralhoJogador = new string[3], cartasJogadas = new string[40], baralhoComputador = new string[3];
                    string[] cor = { "Espadas", "Ouros", "Paus", "Copas" };
                    string[] num = { "4", "5", "6", "7", "Q", "J", "K", "A", "2", "3" };
                    int[] opcao = new int[4];
                    int[] forcaPC = new int[3], forcaJG = new int[3];
                    string cartaJogo = "";
                    string cartaMesa = "";
                    string opcao2 = "";
                    int pontosPC, pontosJG, desempateAmarro, amarroEscudo, analise;
                    char manilha;
                    int i = 0, cont = 0, z = 0;
                    Random numero = new Random();

                    if (pontosPCPartida == 3 || pontosJCPartida == 3) break;
                    Console.Clear();
                    Console.WriteLine("---- Partida número {0} ------------\n---- Pontos do Computador = {1} ----\n---- Seus Pontos = {2} -------------", u, pontosPCPartida, pontosJCPartida);
                    pontosPC = 0;
                    pontosJG = 0;
                    desempateAmarro = 0;
                    amarroEscudo = 0;
                    analise = 0;
                    void sorteiomesa()
                    {
                        cartaJogo = num[numero.Next(0, 10)] + "-" + cor[numero.Next(0, 4)];
                        foreach (string x in cartasJogadas)
                        {
                            if (x == cartaJogo) sorteiomesa();
                        }
                    }
                    void sorteio()
                    {
                        numCarta[i] = num[numero.Next(0, 10)];
                        naipeCarta[i] = cor[numero.Next(0, 4)];
                        cartaJogo = numCarta[i] + "-" + naipeCarta[i];
                        foreach (string x in cartasJogadas)
                        {
                            if (x == cartaJogo)
                            {
                                sorteio();
                            }
                        }
                    }
                    Console.WriteLine("\nSuas cartas são: ");
                    for (i = 1; i < 4; i++)
                    {
                        sorteio();
                        cartasJogadas[(i - 1)] = cartaJogo;
                        baralhoJogador[(i - 1)] = i + "°:" + cartaJogo;
                        Console.WriteLine("{0}", baralhoJogador[(i - 1)]);
                    }
                    for (i = 4; i < 7; i++)
                    {
                        sorteio();
                        cartasJogadas[(i - 1)] = cartaJogo;
                        baralhoComputador[(i - 4)] = (i - 3) + "º:" + cartaJogo;
                    }
                    sorteiomesa();
                    cartasJogadas[i + 7] = cartaJogo;
                    cartaMesa = cartaJogo;
                    Console.WriteLine("\nCarta da mesa: {0}", cartaMesa);

                    manilha = cartaMesa[0];
                    for (int x = 0; x < num.Length - 1; x++)
                    {
                        if (num[x][0] == manilha)
                        {
                            manilha = char.Parse(num[x + 1]);
                            break;
                        }
                        if (manilha == '3')
                        {
                            manilha = '4';
                            break;
                        }
                    }
                    Console.WriteLine("As manilhas são: {0}", manilha);
                    #endregion

                    #region Força das cartas

                    for (int k = 0; k < 3; k++)
                    {
                        if (baralhoJogador[k][3] == manilha)
                        {
                            if (baralhoJogador[k].Substring(5) == "Ouros") forcaJG[k] = 99;
                            if (baralhoJogador[k].Substring(5) == "Espadas") forcaJG[k] = 100;
                            if (baralhoJogador[k].Substring(5) == "Copas") forcaJG[k] = 101;
                            if (baralhoJogador[k].Substring(5) == "Paus") forcaJG[k] = 102;

                        }

                        if (baralhoComputador[k][3] == manilha)
                        {
                            if (baralhoComputador[k].Substring(5) == "Ouros") forcaPC[k] = 99;
                            if (baralhoComputador[k].Substring(5) == "Espadas") forcaPC[k] = 100;
                            if (baralhoComputador[k].Substring(5) == "Copas") forcaPC[k] = 101;
                            if (baralhoComputador[k].Substring(5) == "Paus") forcaPC[k] = 102;
                        }

                        for (int j = 0; j < num.Length; j++)
                        {
                            if (baralhoJogador[k][3] != manilha)
                            {
                                if (baralhoJogador[k][3] == num[j][0])
                                {
                                    forcaJG[k] = j;
                                }
                            }
                            if (baralhoComputador[k][3] != manilha)
                            {
                                if (baralhoComputador[k][3] == num[j][0])
                                {
                                    forcaPC[k] = j;
                                }
                            }
                        }
                    }


                    #endregion

                    #region Escolha das cartas
                    for (int m = 0; m < 3; m++)
                    {
                        if (pontosJG == 2 || pontosPC == 2) break;
                        void escolha()
                        {   
                            int o;
                            do
                            {
                                o = 0;
                                Console.WriteLine("\n---------------------------------");
                                Console.Write("Qual carta você deseja usar: ");
                                opcao2 = Console.ReadLine();

                                if (opcao2 != "3" && opcao2 != "2" && opcao2 != "1" && opcao2 != "4")
                                {
                                    Console.Write("\nOpção inválida");
                                    o = 1;
                                }
                                if (opcao2 == "3" || opcao2 == "2" || opcao2 == "1" || opcao2 == "4")
                                {
                                    opcao[cont] = int.Parse(opcao2);

                                    if (opcao[cont] < 4 && opcao[cont] > 0)
                                    {

                                        if (baralhoJogador[opcao[cont] - 1][baralhoJogador[opcao[cont] - 1].Length - 1] == '.')
                                        {
                                            Console.Write("\nEssa Carta Já foi jogada Anteriormente: ");
                                            o = 1;
                                        }
                                    }
                                    else if (opcao[cont] == 4)
                                    {
                                        if (truco == 1 || truco == 2)
                                        {
                                            Console.WriteLine("\n-----------Você já pediu truco neste jogo!!-----------\n");
                                            o = 1;
                                        }
                                        else if (truco == 0)
                                        {
                                            if (pontosJG >= 2 && pontosPC != 2) break;
                                            if (pontosPC >= 2 && pontosJG != 2) break;
                                            Console.WriteLine("-------Você pediu truco!-------");

                                            for (int y = 0; y < 3; y++)
                                            {
                                                if (forcaPC[0] > forcaJG[y] || forcaPC[1] > forcaJG[y] || forcaPC[2] > forcaJG[y])
                                                {
                                                    analise++;
                                                }
                                                if (analise == 3)
                                                {
                                                    Console.WriteLine("\nO Computador aceitou o truco, a rodada agora vale 3 pontos.");
                                                    truco = 1;
                                                    o = 1;
                                                }
                                            }
                                            if (analise < 3)
                                            {
                                                analise = 0;
                                                if (forcaPC[0] >= 99 && forcaPC[1] >= 9 || forcaPC[1] >= 99 && forcaPC[2] >= 9 || forcaPC[0] >= 99 && forcaPC[2] >= 9 || forcaPC[0] >= 9 && forcaPC[1] >= 99 || forcaPC[1] >= 9 && forcaPC[2] >= 99 || forcaPC[0] >= 9 && forcaPC[2] >= 99)
                                                {
                                                    Console.WriteLine("O Computador aceitou o truco, a rodada agora vale 3 pontos.");
                                                    truco = 1;
                                                    o = 1;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("O Computador não aceitou o truco, você venceu a rodada.");
                                                    truco = 2;
                                                    pontosJG = 2;
                                                }
                                            }
                                        }
                                    }
                                }
                            } while (o == 1);

                            for (int y = 1; y < 4; y++)
                            {

                                if (opcao[cont] == y)
                                {
                                    baralhoJogador[y - 1] += " - Já usou.";
                                    Console.Clear();
                                    Console.WriteLine("Suas cartas são: ");
                                    Console.WriteLine("{0}", baralhoJogador[0]);
                                    Console.WriteLine("{0}", baralhoJogador[1]);
                                    Console.WriteLine("{0}", baralhoJogador[2]);
                                    Console.WriteLine("\nCarta da mesa: {0}", cartaMesa);
                                    Console.WriteLine("A manilha é: {0}", manilha);
                                    Console.WriteLine("\nCarta do jogador: {0}", baralhoJogador[y - 1].Replace(" - Já usou.", ""));

                                }

                            }
                        }
                        try
                        {
                            escolha();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Erro: {0}", e.Message);
                            Console.WriteLine("Digite um valor numérico para escolher a carta.");
                            Console.WriteLine("O programa será fechado");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }

                        if (z == 0)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (pontosJG >= 2 && pontosPC != 2) break;
                                if (pontosPC >= 2 && pontosJG != 2) break;

                                if (k == 3)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (baralhoComputador[j][baralhoComputador[j].Length - 1] != '.')
                                        {
                                            if (baralhoComputador[j][3] == baralhoJogador[opcao[cont] - 1][3] && baralhoJogador[opcao[cont] - 1][3] != manilha && baralhoComputador[j][3] != manilha)
                                            {
                                                Console.WriteLine("Carta do PC: {0}", baralhoComputador[j]);
                                                Console.WriteLine("-------------------------");
                                                Console.WriteLine("Amarrou.");
                                                baralhoComputador[j] += " - Já usou.";
                                                pontosJG++;
                                                pontosPC++;
                                                amarroEscudo = 5;
                                                z = 0;
                                                break;
                                            }
                                        }
                                    }

                                    if (amarroEscudo == 0)
                                    {
                                        for (int j = 0; j < 3; j++)
                                        {
                                            if (baralhoComputador[j][baralhoComputador[j].Length - 1] != '.')
                                            {
                                                Console.WriteLine("Carta do PC: {0}", baralhoComputador[j]);
                                                Console.WriteLine("-----------------------------");
                                                Console.WriteLine("Você venceu a rodada.");
                                                baralhoComputador[j] += " - Já usou.";
                                                pontosJG++;
                                                z = 0;
                                                break;
                                            }
                                        }
                                    }
                                    amarroEscudo = 0;
                                    break;
                                }
                                else if (baralhoComputador[k][baralhoComputador[k].Length - 1] != '.')
                                {

                                    if (baralhoJogador[opcao[cont] - 1][3] == manilha)
                                    {
                                        foreach (int x in forcaPC)
                                        {
                                            if (x > forcaJG[opcao[cont] - 1])
                                            {
                                                if (forcaPC[k] == x)
                                                {
                                                    Console.WriteLine("Carta do PC: {0}", baralhoComputador[k]);
                                                    Console.WriteLine("---------------------------------");
                                                    Console.WriteLine("PC vence a rodada.");
                                                    baralhoComputador[k] += " - Já usou.";
                                                    pontosPC++;
                                                    z = 1;
                                                    break;
                                                }
                                            }
                                        }
                                        if (baralhoComputador[k][baralhoComputador[k].Length - 1] == '.')
                                        {
                                            break;
                                        }
                                        foreach (int x in forcaPC)
                                        {
                                            if (x < 99)
                                            {
                                                if (forcaPC[k] == x)
                                                {
                                                    Console.WriteLine("Carta do PC: {0}", baralhoComputador[k]);
                                                    Console.WriteLine("---------------------------------");
                                                    Console.WriteLine("Você venceu a rodada.");
                                                    baralhoComputador[k] += " - Já usou.";
                                                    pontosJG++;
                                                    z = 0;
                                                    break;
                                                }

                                            }

                                        }
                                        if (baralhoComputador[k][baralhoComputador[k].Length - 1] == '.')
                                        {
                                            break;
                                        }
                                    }
                                    {
                                        if (forcaPC[k] > forcaJG[opcao[cont] - 1])
                                        {
                                            Console.WriteLine("Carta do PC: {0}", baralhoComputador[k]);
                                            Console.WriteLine("---------------------------------");
                                            Console.WriteLine("PC vence a rodada.");
                                            baralhoComputador[k] += " - Já usou.";
                                            pontosPC++;
                                            z = 1;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (pontosJG == 1 && pontosPC == 0) desempateAmarro = 1;
                        if (pontosJG == 0 && pontosPC == 1) desempateAmarro = 2;

                        if (z == 1)
                        {
                            for (int k = 0; k < 3; k++)
                            {

                                if (pontosJG >= 2 && pontosPC != 2) break;
                                if (pontosPC >= 2 && pontosJG != 2) break;

                                if (baralhoComputador[k][baralhoComputador[k].Length - 1] != '.')
                                {
                                    Console.WriteLine("---------------------------------");
                                    Console.WriteLine("Carta do PC: {0}", baralhoComputador[k]);

                                    escolha();
                                    if (pontosJG >= 2 && pontosPC != 2) break;
                                    if (pontosPC >= 2 && pontosJG != 2) break;
                                    Console.WriteLine("Carta do PC: {0}", baralhoComputador[k]);
                                    baralhoComputador[k] += " - Já usou.";


                                    if (forcaPC[k] > forcaJG[opcao[cont] - 1])
                                    {
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine("PC vence a rodada.");
                                        pontosPC++;
                                        z = 0;
                                        break;
                                    }
                                    else if (baralhoComputador[k][3] == baralhoJogador[opcao[cont] - 1][3] && baralhoJogador[opcao[cont] - 1][3] != manilha && baralhoComputador[k][3] != manilha)
                                    {
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine("Amarrou.");
                                        pontosJG++;
                                        pontosPC++;
                                        z = 0;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("---------------------------------");
                                        Console.WriteLine("Você venceu a rodada.");
                                        pontosJG++;
                                        z = 0;
                                        break;
                                    }

                                }
                            }
                        }

                    }
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("--------Ver o resultado!!!-------");
                    Console.WriteLine("---------------------------------");
                    Console.ReadKey();
                    Console.Clear();
                    if (truco == 2)
                    {
                        Console.WriteLine("---------------------------------Você venceu!---------------------------------");
                        pontosJCPartida++;

                    }
                    if (truco == 1)
                    {
                        Console.Clear();
                        if (pontosJG > pontosPC)
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("--------------------------------Você venceu o JOGO!-------------------------------\n " +
                                              "----------------------------------------------------------------------------------\n" +
                                              "-------------------------------------Parabéns.------------------------------------");
                        }
                        if (pontosPC > pontosJG)
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------");
                            Console.WriteLine("---------------------------------PC venceu o Jogo---------------------------------");
                            Console.WriteLine("----------------------------------------------------------------------------------");
                        }
                        truco = 0;
                        break;
                    }
                    else if (truco == 0)
                    {
                        if (pontosPC == 2 && pontosJG == 2)
                        {
                            if (desempateAmarro == 1)
                            {
                                Console.WriteLine("Você venceu!!!!!!\nApesar de ter amarrado, você venceu a primeira rodada Iehhhh");
                                pontosJCPartida++;
                            }
                            if (desempateAmarro == 2)
                            {
                                Console.WriteLine("PC venceu\nApesar de ter amarrado, o PC venceu a primeira rodada :(");
                                pontosPCPartida++;
                            }

                        }
                        else if (pontosPC == 3 && pontosJG == 3)
                        {
                            Console.WriteLine("Quem ganhou ou quem perdeu, nem quem ganhou ou perdeu, " +
                                              "vai ganhar ou perder, porque todo mundo perdeu.\nDilma do Truco");
                        }
                        else if (pontosPC > pontosJG)
                        {
                            Console.WriteLine("---------------------------------------------------------------------------");
                            Console.WriteLine("---------------------------------PC venceu---------------------------------");
                            Console.WriteLine("---------------------------------------------------------------------------");
                            pontosPCPartida++;

                        }
                        else if (pontosJG > pontosPC)
                        {
                            Console.WriteLine("------------------------------------------------------------------------------");
                            Console.WriteLine("---------------------------------Você venceu!---------------------------------");
                            Console.WriteLine("------------------------------------------------------------------------------");
                            pontosJCPartida++;
                        }
                    }
                    Console.WriteLine("\n\nAperte uma tecla para continuar");
                    Console.ReadKey();                
                }
                #endregion

                #region Pontos da Partida
                if (pontosPCPartida == 3)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    Console.WriteLine("---------------------------------PC venceu o Jogo---------------------------------");
                    Console.WriteLine("----------------------------------------------------------------------------------");
                }
                if (pontosJCPartida == 3)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    Console.WriteLine("--------------------------------Você venceu o JOGO!-------------------------------\n " +
                                      "----------------------------------------------------------------------------------\n" +
                                      "-------------------------------------Parabéns.------------------------------------");
                }
                int b = 0;

                Console.WriteLine("\n\nAperte uma tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                do
                {
                    Console.WriteLine("Você gostaria de jogar de novo?" +
                                      "\nSe sim, digite 1.\nSe não, digite 2.");
                    string q = Console.ReadLine();
                    if (q == "1")
                    {
                        reiniciar = 1;
                        b = 0;
                    }
                    else if (q == "2")
                    {
                        Console.WriteLine("Obrigado por Jogar!\nAcho que esse trabalho vale 10!");
                        reiniciar = 0;
                        b = 0;
                    }
                    else if (q != "1")
                    {
                        Console.WriteLine("Opção inválida");
                        b = 1;
                    }

                } while (b == 1);
                b = 0;

                #endregion

            } while (reiniciar == 1);

            #endregion
        }
    }

}