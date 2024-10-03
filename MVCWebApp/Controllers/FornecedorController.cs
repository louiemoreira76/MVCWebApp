using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Web;
using MVCWebApp.Models;
using MVCWebApp.DataBase;
using MVCWebApp.Models.Entities;
using MVCWebApp.DTOs;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MVCWebApp.Web.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly DbContextH dbContext;
        private readonly ILogger<FornecedorController> logger;
        public FornecedorController(DbContextH dbContext, ILogger<FornecedorController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger; 
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(FornecedorDTO fornecedorDto, ViewModelFornecedor viewModel)
        {
            logger.LogInformation("Método Submit chamado");

            try
            {
                logger.LogWarning($"Subscribed: {fornecedorDto.Subscribed}");
                Debug.WriteLine("Método Submit chamado");

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    logger.LogWarning("Erros do ModelState: " + string.Join(", ", errors));
                    return RedirectToAction("Add", "Fornecedor");
                }

                if (!ModelState.IsValid)
                {
                    logger.LogWarning("ModelState inválido");
                    return RedirectToAction("Add", "Fornecedor");
                }
                // Aqui você pode usar o fornecedorDto para acessar os dados do formulário


                var fornecedor = new Fornecedor
                {
                    Name = fornecedorDto.Name,
                    Cnpj = fornecedorDto.Cnpj,
                    Segmento = fornecedorDto.Segmento,
                    Cep = fornecedorDto.Cep,
                    Endereco = fornecedorDto.Endereco,
                    Image = fornecedorDto.Image,
                };

                logger.LogWarning($"Adicionando fornecedor: {fornecedor.Name}, CNPJ: {fornecedor.Cnpj}");

                await dbContext.Fornecedores.AddAsync(fornecedor);
                await dbContext.SaveChangesAsync();

                logger.LogWarning("Fornecedor adicionado com sucesso!");

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
