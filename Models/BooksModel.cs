using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Aula03.Models
{
    public class BooksModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BookID { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Autor é obrigatório")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string? Desc { get; set; }

        [Required(ErrorMessage = "Ano é obrigatório")]
        public int? PublicationYear { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        public double? Price { get; set; }
        public byte[]? Content { get; set; }


        public void GetPdfContent(string pdfPath)
        {
            byte[] pdfBytes;

            using (var stream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
            {
                pdfBytes = new byte[stream.Length];
                stream.Read(pdfBytes, 0, pdfBytes.Length);
            }
            Content = pdfBytes;
        }

        public void ReadPdfContent()
        {
            if (Content == null) return;

            string tempFilePath = Path.GetFullPath(Path.GetTempFileName());


            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                fileStream.Write(Content, 0, Content.Length);
            }
            string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            Process.Start(chromePath, tempFilePath);
        }
    }
}
