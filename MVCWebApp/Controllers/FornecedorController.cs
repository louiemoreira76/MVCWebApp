using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Web;
using MVCWebApp.Models;
using MVCWebApp.DataBase;
using MVCWebApp.Models.Entities;
using MVCWebApp.DTOs;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace MVCWebApp.Web.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly DbContextH dbContext;
        private readonly ILogger<FornecedorController> logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FornecedorController(DbContextH dbContext, ILogger<FornecedorController> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(FornecedorDTO fornecedorDto, IFormFile file)
        {
            logger.LogInformation("Método Submit chamado");

            try
            {
                logger.LogInformation("Entrando no bloco try");

                if (file == null || file.Length == 0)
                {
                    logger.LogWarning("Arquivo não enviado ou vazio.");
                    ModelState.AddModelError("file", "O campo de imagem é obrigatório.");
                    return View("Add", fornecedorDto);
                }

                logger.LogInformation("Arquivo verificado");

                if (!file.ContentType.StartsWith("image/"))
                {
                    logger.LogWarning("Arquivo não é uma imagem.");
                    ModelState.AddModelError("file", "O arquivo deve ser uma imagem.");
                    return View("Add", fornecedorDto);
                }

                logger.LogInformation("Tipo de arquivo verificado");

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Erros do ModelState: " + string.Join(", ", errors));
                    return View("Add", fornecedorDto);
                }

                logger.LogInformation("ModelState verificado");

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", file.FileName);
                logger.LogInformation($"Caminho do arquivo: {filePath}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                logger.LogInformation("Arquivo copiado");

                // Criação do objeto Fornecedor
                var fornecedor = new Fornecedor
                {
                    Id = Guid.NewGuid(),
                    Name = fornecedorDto.Name,
                    Cnpj = fornecedorDto.Cnpj,
                    Segmento = fornecedorDto.Segmento,
                    Cep = fornecedorDto.Cep,
                    Endereco = fornecedorDto.Endereco,
                    Image = filePath,
                };

                logger.LogInformation("Fornecedor criado\n");
                logger.LogInformation($"Fornecedor: {fornecedor.Name}, Cnpj: {fornecedor.Cnpj}, Segmento: {fornecedor.Segmento}, Cep: {fornecedor.Cep}, Endereco: {fornecedor.Endereco}, Image: {fornecedor.Image}");

                // Garante que o fornecedor está sendo rastreado pelo DbContext
                dbContext.Fornecedores.Attach(fornecedor);

                var result = await dbContext.SaveChangesAsync();
                logger.LogInformation($"Registros afetados: {result}");

                // Adiciona o fornecedor ao contexto e salva
                await dbContext.Fornecedores.AddAsync(fornecedor);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Fornecedor adicionado ao banco de dados");
                logger.LogInformation($"Registros afetados: {result}");

                return RedirectToAction("Add", "Fornecedor");
            }
            catch (Exception ex)
            {
                logger.LogError("Erro ao adicionar fornecedor: " + ex.Message);
                return RedirectToAction("Add", "Fornecedor");
            }
        }


    }
}
