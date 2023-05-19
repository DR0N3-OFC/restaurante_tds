using Aula03.Models;

namespace Aula03.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if(context.Products!.Any())
            {
                return;
            }
            if (context.Tables!.Any())
            {
                return;
            }



            var garcom = new Waiter[]
            {
                new Waiter{
                    Name = ".",
                    LastName = ".",
                    Cellphone = ".",
                    BirthDate = DateTime.Parse("2000-02-01")
                },
            };

            context.AddRange(garcom);

            garcom = new Waiter[]
            {
                new Waiter{
                    Name = "Alpha",
                    LastName = "Delta",
                    Cellphone = "(45) 99898-5915",
                    BirthDate = DateTime.Parse("1999-04-20")
                },
            };

            context.AddRange(garcom);

            garcom = new Waiter[]
            {
                new Waiter{
                    Name = "Afonso",
                    LastName = "Lano",
                    Cellphone = "(45) 99893-2115",
                    BirthDate = DateTime.Parse("2000-02-01")
                },
            };

            context.AddRange(garcom);



            var mesa = new Table[]
            {
                new Table{
                    Name = "00",
                    Status = false
                },
            };

            context.AddRange(mesa);

            mesa = new Table[]
            {
                new Table{
                    Name = "01",
                    Status = false
                },
            };
            
            context.AddRange(mesa);

            var categoria1 = new Category[]
            {
                new Category{
                    Name = "Cereal"

                },
            };

            context.AddRange(categoria1);

            var categoria2 = new Category[]
            {
                new Category{
                    Name = "Carne"
                },
            };

            context.AddRange(categoria2);

            var categoria3 = new Category[]
            {
                new Category{
                    Name = "Refrigerante"

                },
            };

            context.AddRange(categoria3);

            var categoria4 = new Category[]
            {
                new Category{
                    Name = "Suco"

                },
            };

            context.AddRange(categoria4);

            var categoria5 = new Category[]
            {
                new Category{
                    Name = "Quebra da Matrix"

                },
            };

            context.AddRange(categoria5);

            var produto1 = new Product[]
            {
                new Product{
                    Name = "Bife",
                    Description = "Feijão, Arroz, Farofa e Bife",
                    Price = 12.0,
                    Category = categoria2[0]

                },
            };

            context.AddRange(produto1);

            var produto2 = new Product[]
            {
                new Product{
                    Name = "Peixe a la carte",
                    Description = "Peixe e arroz",
                    Price = 6.0,
                    Category= categoria2[0]

                },
            };

            context.AddRange(produto2);

            var produto3 = new Product[]
            {
                new Product{
                    Name = "Vegetariano",
                    Description = "Feijão, Arroz e Salada",
                    Price = 3.0,
                    Category = categoria1[0]

                },
            };

            context.AddRange(produto3);

            produto3 = new Product[]
            {
                new Product{
                    Name = "Batata",
                    Description = "Arroz e Purê de Batata",
                    Price = 4.5,
                    Category = categoria1[0]

                },
            };

            context.AddRange(produto3);


            produto3 = new Product[]
            {
                new Product{
                    Name = "Coca Lata",
                    Description = "Refrigerante Insaldável",
                    Price = 4.5,
                    Category = categoria3[0]

                },
            };

            context.AddRange(produto3);

            produto3 = new Product[]
            {
                new Product{
                    Name = "Suco de Uva Integral",
                    Description = "Tier S",
                    Price = 7.5,
                    Category = categoria4[0]

                },
            };

            context.AddRange(produto3);

            produto3 = new Product[]
            {
                new Product{
                    Name = "Café",
                    Description = "Tier S",
                    Price = 400.5,
                    Category = categoria5[0]

                },
            };

            context.AddRange(produto3);


            var service_product1 = new ServiceProduct[]
            {
                new ServiceProduct{
                    Product = produto1[0],
                    Amount = 1
                }
            };

            context.AddRange(service_product1);

            var service_product2 = new ServiceProduct[]
            {
                new ServiceProduct{
                    Product = produto2[0],
                    Amount = 2
                }
            };

            context.AddRange(service_product2);

            var atendimento = new Service[]
            {
                new Service{
                    Waiter = garcom[0],
                    Table = mesa[0],
                    InitDate = DateTime.Now,
                    FinishDate = DateTime.Now,
                },
            };
            atendimento[0].ServiceProducts!.Add(service_product1[0]);
            atendimento[0].ServiceProducts!.Add(service_product2[0]);

            context.AddRange(atendimento);


            context.SaveChanges();
        }
    }
}
