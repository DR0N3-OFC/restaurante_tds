using Aula03.Models;

namespace Aula03.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if(context.Produto!.Any())
            {
                return;
            }
            if (context.Mesa!.Any())
            {
                return;
            }



            var garcom = new Garcom[]
            {
                new Garcom{
                    Name = "",
                    SecondName = "",
                    Telefone = "",
                    BirthDate = DateTime.Parse("2000-02-01"),
                },
            };

            context.AddRange(garcom);

            garcom = new Garcom[]
            {
                new Garcom{
                    Name = "Alpha",
                    SecondName = "Delta",
                    Telefone = "45898985915",
                    BirthDate = DateTime.Parse("1999-04-20"),
                },
            };

            context.AddRange(garcom);

            garcom = new Garcom[]
            {
                new Garcom{
                    Name = "Afonso",
                    SecondName = "Lano",
                    Telefone = "45898932115",
                    BirthDate = DateTime.Parse("2000-02-01"),
                },
            };

            context.AddRange(garcom);



            var mesa = new Mesa[]
            {
                new Mesa{
                    Status = false
                },
            };
            
            context.AddRange(mesa);

            var categoria1 = new Categoria[]
            {
                new Categoria{
                    Name = "Cereal",

                },
            };

            context.AddRange(categoria1);

            var categoria2 = new Categoria[]
            {
                new Categoria{
                    Name = "Carne",

                },
            };

            context.AddRange(categoria2);

            var categoria3 = new Categoria[]
            {
                new Categoria{
                    Name = "Refrigerante",

                },
            };

            context.AddRange(categoria3);

            var categoria4 = new Categoria[]
            {
                new Categoria{
                    Name = "Suco",

                },
            };

            context.AddRange(categoria4);

            var categoria5 = new Categoria[]
            {
                new Categoria{
                    Name = "Quebra da Matrix",

                },
            };

            context.AddRange(categoria5);

            var produto1 = new Produto[]
            {
                new Produto{
                    Name = "Bife",
                    Desc = "Feijão, Arroz, Farofa e Bife",
                    Price = 12.0,
                    Categoria = categoria2[0]

                },
            };

            context.AddRange(produto1);

            var produto2 = new Produto[]
            {
                new Produto{
                    Name = "Peixe a la carte",
                    Desc = "Peixe e arroz",
                    Price = 6.0,
                    Categoria= categoria2[0]

                },
            };

            context.AddRange(produto2);

            var produto3 = new Produto[]
            {
                new Produto{
                    Name = "Vegetariano",
                    Desc = "Feijão, Arroz e Salada",
                    Price = 3.0,
                    Categoria = categoria1[0]

                },
            };

            context.AddRange(produto3);

            produto3 = new Produto[]
            {
                new Produto{
                    Name = "Batata",
                    Desc = "Arroz e Purê de Batata",
                    Price = 4.5,
                    Categoria = categoria1[0]

                },
            };

            context.AddRange(produto3);


            produto3 = new Produto[]
            {
                new Produto{
                    Name = "Coca Lata",
                    Desc = "Refrigerante Insaldável",
                    Price = 4.5,
                    Categoria = categoria3[0]

                },
            };

            context.AddRange(produto3);

            produto3 = new Produto[]
            {
                new Produto{
                    Name = "Suco de Uva Integral",
                    Desc = "Tier S",
                    Price = 7.5,
                    Categoria = categoria4[0]

                },
            };

            context.AddRange(produto3);

            produto3 = new Produto[]
            {
                new Produto{
                    Name = "Café",
                    Desc = "Tier S",
                    Price = 400.5,
                    Categoria = categoria5[0]

                },
            };

            context.AddRange(produto3);


            var atendimento = new Atendimento[]
            {
                new Atendimento{
                    Garcom = garcom[0],
                    Mesa = mesa[0],
                    InitDate = DateTime.Now,
                    FinishDate = DateTime.Now,
                },
            };
            atendimento[0].Produtos.Add(produto1[0]);
            atendimento[0].Produtos.Add(produto2[0]);

            context.AddRange(atendimento);


            context.SaveChanges();
        }
    }
}
