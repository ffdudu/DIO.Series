using System;

namespace DIO.Series
{
    class Program
    {
    	static SerieRepositorio repositorio = new SerieRepositorio();
        
        static void Main(string[] args)
        {
			
			string opcaoUsuario = ObterOpcaoUsuario().ToUpper();

			while (opcaoUsuario != "X")
			{
				try
                {
					switch (opcaoUsuario)
					{
						case "1":
							ListarSeries();
							break;
						case "2":
							InserirSerie();
							break;
						case "3":
							AtualizarSerie();
							break;
						case "4":
							ExcluirSerie();
							break;
						case "5":
							VisualizarSerie();
							break;
						case "C":
							Console.Clear();
							break;
	                }
				}	
                catch
                {
                    continue;
                }

               	opcaoUsuario = ObterOpcaoUsuario().ToUpper();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");

		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{

			string entrada = null;
			bool status = false;
			int limite = repositorio.ProximoId();
			int indiceSerie = 0;
			int entradaGenero = 0;
			string entradaTitulo;
			int entradaAno = 0;
			string entradaDescricao;

			while (status == false)
			{
				Console.Write("Informe o id da série (ou tecle X para Sair): ");
				entrada = Console.ReadLine().ToUpper();
				if (entrada == "X")
					status = true;
				try
				{
					if (int.Parse(entrada) >= 0 && int.Parse(entrada) <= limite - 1)
						status = true;
				}
				catch	
				{
					continue;
				}
			}
			
			if (entrada == "X")
				return;
			else 
				indiceSerie = int.Parse(entrada);

			int i = 0;
			foreach (int genero in Enum.GetValues(typeof(Genero)))
			{
				i++;
				Console.WriteLine("{0, 2} - {1}", i, Enum.GetName(typeof(Genero), genero));
			}
			
			status = false;
			while (status == false)
			{
				Console.Write("Informe o gênero entre as opções acima (ou tecle X para Sair): ");
				entrada = Console.ReadLine().ToUpper();
				if (entrada == "X")
					status = true;
				try
				{
					if (int.Parse(entrada) >= 1 && int.Parse(entrada) <= i)
						status = true;
				}
				catch	
				{
					continue;
				}
			}
			if (entrada == "X")
				return;
			else 
				entradaGenero = int.Parse(entrada);

			Console.Write("Informe o Título da Série: ");
			entradaTitulo = Console.ReadLine();

			status = false;
			while (status == false)
			{
				Console.Write("Informe o Ano de Início da Série (ou tecle X para Sair): ");
				entrada = Console.ReadLine().ToUpper();
				if (entrada == "X")
					status = true;
				try
				{
					if (int.Parse(entrada) > 0)
						status = true;
				}
				catch	
				{
					continue;
				}
			}
			if (entrada == "X")
				return;
			else 
				entradaAno = int.Parse(entrada);

			Console.Write("Digite a Descrição da Série: ");
			entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);

			Console.WriteLine("\nSérie alterada com sucesso!\nPressione [Enter] para continuar.");
			Console.ReadLine();

		}

		private static void ExcluirSerie()
		{

			string entrada = null;
			bool status = false;
			int limite = repositorio.ProximoId();
			int indiceSerie = 0;

			while (status == false)
			{
				Console.Write("Informe o id da série (ou tecle X para Sair): ");
				entrada = Console.ReadLine().ToUpper();
				if (entrada == "X")
					status = true;
				try
				{
					if (int.Parse(entrada) >= 0 && int.Parse(entrada) <= limite - 1)
						status = true;
				}
				catch	
				{
					continue;
				}
			}

			if (entrada == "X")
				return;
			else 
				indiceSerie = int.Parse(entrada);

			repositorio.Exclui(indiceSerie);
		}

		private static void ListarSeries()
		{
			Console.WriteLine("\n{0, 65}",  "< Listar séries >");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			int somaAcao = 0;
			int somaAventura = 0;
			int somaComedia = 0;
			int somaDocumentario = 0;
			int somaDrama = 0;
			int somaEspionagem = 0;
			int somaFaroeste = 0;
			int somaFantasia = 0;
			int somaFiccao_Cientifica = 0;
			int somaMusical = 0;
			int somaRomance = 0;
			int somaSuspense = 0;
			int somaTerror = 0;
			int somaExcluidos = 0;

			Console.WriteLine ("\n ID{0, 16} {1, 46} {2, 23} {3, 15}", "Título", "Gênero", "Descrição", "Ano\n");
			
			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("{0, 3}   {1, -49} {2, -20} {3, -24} {4, -4} {5, 12}", serie.retornaId(), serie.retornaTitulo(), serie.retornaGenero(), serie.retornaDescricao(), serie.retornaAno(), (excluido ? "*Excluído*" : ""));

				if(serie.retornaGenero() == "Acao")
					somaAcao++;
				if(serie.retornaGenero() == "Aventura")
					somaAventura++;
				if(serie.retornaGenero() == "Comedia")
					somaComedia++;
				if(serie.retornaGenero() == "Documentario")
					somaDocumentario++;
				if(serie.retornaGenero() == "Drama")
					somaDrama++;
				if(serie.retornaGenero() == "Espionagem")
					somaEspionagem++;
				if(serie.retornaGenero() == "Faroeste")
					somaFaroeste++;
				if(serie.retornaGenero() == "Fantasia")
					somaFantasia++;
				if(serie.retornaGenero() == "Ficcao_Cientifica")
					somaFiccao_Cientifica++;
				if(serie.retornaGenero() == "Musical")
					somaMusical++;
				if(serie.retornaGenero() == "Romance")
					somaRomance++;
				if(serie.retornaGenero() == "Suspense")
					somaSuspense++;
				if(serie.retornaGenero() == "Terror")
					somaTerror++;
				if(excluido == true)
					somaExcluidos++;

			}

			Console.WriteLine("\n   < RELATÓRIO ANALÍTICO >\n");
			Console.WriteLine("Filmes de Acao = " + somaAcao);
			Console.WriteLine("Filmes de Aventura = " + somaAventura);
			Console.WriteLine("Filmes de Comedia = " + somaComedia);
			Console.WriteLine("Filmes de Documentario = " + somaDocumentario);
			Console.WriteLine("Filmes de Drama = " + somaDrama);
			Console.WriteLine("Filmes de Espionagem = " + somaEspionagem);
			Console.WriteLine("Filmes de Faroeste = " + somaFaroeste);
			Console.WriteLine("Filmes de Fantasia = " + somaFantasia);
			Console.WriteLine("Filmes de Ficcao_Cientifica = " + somaFiccao_Cientifica);
			Console.WriteLine("Filmes de Musical = " + somaMusical);
			Console.WriteLine("Filmes de Romance = " + somaRomance);
			Console.WriteLine("Filmes de Suspense = " + somaSuspense);
			Console.WriteLine("Filmes de Terror = " + somaTerror);
			Console.WriteLine("  "+somaExcluidos + " excluídos.");

		}

        private static void InserirSerie()
		{

			string opcao = null;
            while (opcao != "1" || opcao != "2" || opcao != "X")
            {
                Console.WriteLine("\nOpções de preenchimento de nova(s) serie(s):\n  1 - Para preenchimento automático (atualiza com algumas séries previamente cadastradas a fim de viabilizar os testes)\n  2 - Para preenchimento manual\n  X - Para Sair\n");
                opcao = Console.ReadLine().ToUpper();
                switch (opcao)
                {
                    case "1":

                        PreencherSeries();
                        return;
                    
                    case "2":

						string entrada = "";
						int entradaGenero = 0;
						string entradaTitulo;
						int entradaAno = 0;
						string entradaDescricao;

						Console.WriteLine("\n  < Inserir nova série >\n");

						// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
						// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
						int i = 0;
						foreach (int genero in Enum.GetValues(typeof(Genero)))
						{
							i++;
							Console.WriteLine("{0, 2} - {1}", i, Enum.GetName(typeof(Genero), genero));
						}
						bool status = false;
						while (status == false)
						{
							Console.Write("Informe o gênero entre as opções acima (ou tecle X para Sair): ");
							entrada = Console.ReadLine().ToUpper();
							if (entrada == "X")
								status = true;
							try
							{
								if (int.Parse(entrada) >= 1 && int.Parse(entrada) <= i)
									status = true;
							}
							catch	
							{
								continue;
							}
						}
						if (entrada == "X")
							return;
						else 
							entradaGenero = int.Parse(entrada);

						Console.Write("Informe o Título da Série: ");
						entradaTitulo = Console.ReadLine();

						status = false;
						while (status == false)
						{
							Console.Write("Informe o Ano de Início da Série (ou tecle X para Sair): ");
							entrada = Console.ReadLine().ToUpper();
							if (entrada == "X")
								status = true;
							try
							{
								if (int.Parse(entrada) > 0)
									status = true;
							}
							catch	
							{
								continue;
							}
						}
						if (entrada == "X")
							return;
						else 
							entradaAno = int.Parse(entrada);

						Console.Write("Digite a Descrição da Série: ");
						entradaDescricao = Console.ReadLine();

						Serie novaSerie = new Serie(id: repositorio.ProximoId(),
													genero: (Genero)entradaGenero,
													titulo: entradaTitulo,
													ano: entradaAno,
													descricao: entradaDescricao);

						repositorio.Insere(novaSerie);
						Console.WriteLine("\nSérie cadastrada com sucesso!\nPressione [Enter] para continuar.");
						Console.ReadLine();
						return;
					
					case "X":
                        return;

            	}
			}
		}

        private static void PreencherSeries()
        {

            int[] generos = new int [] {1, 2, 8, 9, 10, 12, 1, 5, 6, 11, 13, 1, 2, 3, 5, 7, 10, 11, 13, 1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 1, 2, 3, 7, 8, 9, 10, 12, 13, 1, 3, 4, 7, 9, 10, 2, 4, 8, 10, 11, 12, 2, 3, 9, 12, 13, 2, 5, 7, 10, 11, 13, 1, 3, 4, 5, 13, 3, 8, 10, 4, 8, 12, 8, 4, 5, 8, 13, 3, 5, 9, 5, 6, 7, 1, 4, 12, 12, 11, 1, 6, 3, 6, 11, 4, 11, 10, 11, 7, 12, 9, 10, 6, 2, 9, 6, 1, 8, 12, 6, 8, 9, 2, 4, 7, 7, 5, 13, 2, 6, 3};
            string[] titulos = new string [] {"FANTASIA", "A DAMA E O VAGABUNDO", "TOY STORY", "O CORCUNDA DE NOTRE DAME", "HÉRCULES", "UMA VIDA DE INSETO", "TOY STORY 2", "MONSTROS S.A", "SHREK ", "PROCURANDO NEMO ", "NEM QUE A VACA TUSSA", "OS INCRÍVEIS", "SHREK 2", "MADAGASCAR", "CARROS", "MATE E A LUZ FANTASMA ", "A FAMÍLIA DO FUTURO", "BEE MOVIE A HISTÓRIA DE UMA ABELHA", "RATATUI", "SHREK 3", "TRANSFORMERS", "BOLT SUPERCÃO", "KUNG FU PANDA", "MADAGASCAR 2 A GRANDE ESCAPADA", "WALL-E", "MONSTROS VS ALIENÍGENAS", "UP ALTAS AVENTURAS", "COMO TREINAR O SEU DRAGÃO ", "TOY STORY 3 ", "CARROS 2", "KUNG FU PANDA 2", "O GATO DE BOTAS", "DETONA RALPH", "MADAGASCAR 3 OS PROCURADOS", "A UNIVERSIDADE MONSTROS", "CENTRAL DA FESTA", "OS CROODS", "TOY STORY DE TERROR", "TURBO", "AS AVENTURAS DE PEABODY E SHERMAN", "COMO TREINAR O SEU DRAGÃO 2", "TOY STORY ESQUECIDOS PELO TEMPO", "CADA UM NA SUA CASA", "DIVERTIDA MENTE", "KUNG FU PANDA 3", "O BOM DINOSSAURO", "PROCURANDO DORY", "TROLLS", "AS AVENTURAS DO CAPITÃO CUECA", "CARROS 3", "O PODEROSO CHEFINHO", "O TOURO FERDINANDO", "VIVA A VIDA É UMA FESTA", "OS INCRÍVEIS 2", "WIFI RALPH QUEBRANDO A INTERNET", "COMO TREINAR O SEU DRAGÃO 3", "TOY STORY 4", "DOIS IRMÃOS UMA JORNADA FANTÁSTICA", "SOUL", "TROLLS 2", "O PODEROSO CHEFINHO 2 NEGÓCIOS DA FAMÍLIA", "OS CROODS 2", "STAR WARS UMA NOVA ESPERANÇA", "STAR WARS O IMPÉRIO CONTRA ATACA", "STAR WARS O RETORNO DE JEDI", "STAR WARS EPISODE 1 A AMEAÇA FANTASMA", "GLADIADOR", "STAR WARS EPISODE 2 ATAQUE DOS CLONES", "PIRATAS DO CARIBE A MALDIÇÃO DO PÉROLA NEGRA", "STAR WARS EPISODE 3 A VINGANÇA DOS SITH ", "PIRATAS DO CARIBE O BAÚ DA MORTE", "PIRATAS DO CARIBE NO FIM DO MUNDO", "TRANSFORMERS A VINGANÇA DOS DERROTADOS", "PIRATAS DO CARIBE NAVEGANDO EM ÁGUAS MISTERIOSAS", "TRANSFORMERS O LADO OCULTO DA LUA", "TRANSFORMERS A ERA DA EXTINÇÃO", "STAR WARS O DESPERTAR DA FORÇA", "MEU AMIGO DRAGÃO", "O BOM GIGANTE AMIGO", "ROGUE ONE UMA HISTÓRIA STAR WARS", "PIRATAS DO CARIBE A VINGANÇA DE SALAZAR", "STAR WARS OS ÚLTIMOS JEDI", "TRANSFORMERS O ÚLTIMO CAVALEIRO", "ZOMBIES", "STAR WARS A ASCENSÃO SKYWALKER", "ZOMBIES 2", "MEU MALVADO FAVORITO", "MEU MALVADO FAVORITO 2", "MEU MALVADO FAVORITO 3", "PETS A VIDA SECRETA DOS BICHOS", "PETS A VIDA SECRETA DOS BICHOS 2", "JURASSIC PARK", "JURASSIC PARK 2 O MUNDO PERDIDO", "JURASSIC PARK 3", "JURASSIC WORLD O MUNDO DOS DINOSSAUROS", "JURASSIC WORLD REINO AMEAÇADO", "O LIVRO DA SELVA ENTRE DOIS MUNDOS", "VELOZES E FURIOSOS", "VELOZES E FURIOSOS 2", "VELOZES E FURIOSOS 3 DESAFIO EM TÓQUIO", "VELOZES E FURIOSOS 4", "VELOZES E FURIOSOS 5", "VELOZES E FURIOSOS 6", "IRMÃO URSO", "IRMÃO URSO 2", "VELOZES E FURIOSOS 7", "VELOZES E FURIOSOS 8", "MINIONS ", "SONIC BOOM", "MINIONS 2 A ORIGEM DE GRU", "SONIC O FILME", "SONIC 2 O FILME", "PICA PAU O FILME", "BABE O PORQUINHO ATRAPALHADO", "POLICIAL EM APUROS 2", "OPERAÇÃO BIG HERO", "HAN SOLO UMA HISTÓRIA STAR WARS", "COISAS DE PÁSSAROS", "VELOZES E FURIOSOS HOBBS E SHAW", "VELOZES E FURIOSOS 9"};
            string[] descricoes = new string [] {"Disney (Desenho)", "Disney (Desenho)", "Pixar (Desenho)", "Disney (Desenho)", "Disney (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Disney (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Disney (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Disney (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Disney (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Pixar  (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Pixar  (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Disney (Desenho)", "Dreamworks (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Pixar (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Dreamworks (Desenho)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Universal (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Dreamworks (Filme)", "Disney (Filme)", "Dreamworks (Filme)", "Dreamworks (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Dreamworks (Filme)", "Disney (Filme)", "Disney (Filme)", "Disney (Filme)", "Universal (Desenho)", "Universal (Desenho)", "Universal (Desenho)", "Universal (Desenho)", "Universal (Desenho)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Disney (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Disney (Desenho)", "Disney (Desenho)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Desenho)", "Universal (Desenho)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Universal (Filme)", "Disney (Desenho)", "Disney (Filme)", "Pixar (Desenho)", "Universal (Filme)", "Universal (Filme)"};
            int[] anos = new int[] {1940, 1955, 1995, 1996, 1997, 1998, 1999, 2001, 2001, 2003, 2004, 2004, 2004, 2005, 2006, 2006, 2007, 2007, 2007, 2007, 2007, 2008, 2008, 2008, 2008, 2009, 2009, 2010, 2010, 2011, 2011, 2011, 2012, 2012, 2013, 2013, 2013, 2013, 2013, 2014, 2014, 2014, 2015, 2015, 2016, 2016, 2016, 2016, 2017, 2017, 2017, 2017, 2017, 2018, 2018, 2019, 2019, 2020, 2020, 2020, 2021, 2021, 1977, 1980, 1983, 1999, 2000, 2002, 2003, 2005, 2006, 2007, 2009, 2011, 2011, 2014, 2015, 2016, 2016, 2016, 2017, 2017, 2017, 2018, 2019, 2020, 2010, 2013, 2017, 2016, 2019, 1993, 1997, 2001, 2015, 2018, 2018, 2001, 2003, 2006, 2009, 2011, 2013, 2003, 2006, 2015, 2017, 2015, 2014, 2020, 2020, 2021, 2017, 1995, 2016, 2014, 2017, 2000, 2019, 2021};

            for ( int i = 0; i <= titulos.Length - 1; i++ )
            {
				Serie novaSerie = new Serie(id: repositorio.ProximoId(),
											genero: (Genero)generos[i],
											titulo: titulos[i],
											ano: anos[i],
											descricao: descricoes[i]);

				repositorio.Insere(novaSerie);
            }
            Console.WriteLine("\nSéries cadastradas com sucesso!\nPressione [Enter] para continuar.");
			Console.ReadLine();

        }

		private static string ObterOpcaoUsuario()
		{

			Console.WriteLine("\nDIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");
			Console.WriteLine("1 - Listar séries");
			Console.WriteLine("2 - Inserir nova série");
			Console.WriteLine("3 - Atualizar série");
			Console.WriteLine("4 - Excluir série");
			Console.WriteLine("5 - Visualizar série");
			Console.WriteLine("C - Limpar Tela");
			Console.WriteLine("X - Sair\n");
			return Console.ReadLine();

		}
    }
}
