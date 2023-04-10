using Aula03.Models;

namespace Aula03.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if(context.Events!.Any())
            {
                return;
            }
            if (context.Users!.Any())
            {
                return;
            }
            if (context.Books!.Any())
            {
                return;
            }


            
            var garcom = new Garcom[]
            {
                new Garcom{
                    Name = "Alpha",
                    Gender = "M",
                    BirthDate = DateTime.Parse("1999-04-20"),
                },
            };

            context.AddRange(garcom);

            garcom = new Garcom[]
            {
                new Garcom{
                    Name = "Afonso Lano",
                    Gender = "M",
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

            var produto1 = new Produto[]
            {
                new Produto{
                    Name = "Arroz",
                    Price = 3.0,

                },
            };

            context.AddRange(produto1);

            var produto2 = new Produto[]
            {
                new Produto{
                    Name = "Peixe",
                    Price = 6.0,

                },
            };

            context.AddRange(produto2);

            var produto3 = new Produto[]
            {
                new Produto{
                    Name = "Feijao",
                    Price = 3.0,

                },
            };

            context.AddRange(produto3);

            produto3 = new Produto[]
            {
                new Produto{
                    Name = "Batata",
                    Price = 4.5,

                },
            };

            context.AddRange(produto3);

            var atendimento = new Atendimento[]
            {
                new Atendimento{
                    Garcom = garcom[0],
                    Mesa = mesa[0],
                },
            };
            atendimento[0].Produtos.Add(produto1[0]);
            atendimento[0].Produtos.Add(produto1[1]);

            context.AddRange(atendimento);


            context.SaveChanges();
        }
    }
}
