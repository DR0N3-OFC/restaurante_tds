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

            var events = new EventsModel[]
            {
                new EventsModel{
                    Name = "Torneio de Truco",
                    Desc = "Campeonato acadêmico CC",
                    Date = DateTime.Parse("2023-04-01")
                },
            };

            context.AddRange(events);

            var user = new User[]
            {
                new User{
                    Name = "Alpha",
                    Password = "123",
                    Gender = "M",
                    BirthDate = DateTime.Parse("2023-04-01"),
                    IsAdm = false,
                },
            };

            context.AddRange(user);


            user = new User[]
            {
                new User{
                    Name = "admin",
                    Password = "admin",
                    Gender = "M",
                    BirthDate = DateTime.Parse("2023-04-01"),
                    IsAdm = true,
                },
            };

            context.AddRange(user);


            var book = new BooksModel[]
            {
                new BooksModel{
                    Name = "Inteligência Emocional",
                    Author = "Daniel Goleman",
                    Desc = ".",
                    PublicationYear = 2008,
                    Price = 49.99,
                },
            };
            book[0].GetPdfContent("C:/Users/Augusto/OneDrive/Área de Trabalho/Livros/Inteligencia-Emocional-Daniel-Goleman.pdf"); 

            context.AddRange(book);

            book = new BooksModel[]
            {
                new BooksModel{
                    Name = "Origins of the Genius",
                    Author = "Dean Keith Simonton",
                    Desc = "Darwinian Perspectives on how Creativity Works",
                    PublicationYear = 2012,
                    Price = 129.99,
                },
            };

            book[0].GetPdfContent("C:/Users/Augusto/OneDrive/Área de Trabalho/Livros/Cognitive Neuroscience of Memory.pdf");

            context.AddRange(book);

            context.SaveChanges();
        }
    }
}
