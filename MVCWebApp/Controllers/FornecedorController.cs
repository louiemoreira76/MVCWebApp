using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Web;
using MVCWebApp.Models;
using MVCWebApp.DataBase;
using MVCWebApp.Models.Entities;
using MVCWebApp.DTOs;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVCWebApp.DTO;

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

                // if (!ModelState.IsValid)
                //{
                //    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                //    logger.LogWarning("Erros do ModelState: " + string.Join(", ", errors));
               //     return View("Add", fornecedorDto); // Retorna à view com os dados preenchidos
               // }

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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var fornecedores = await dbContext.Fornecedores.ToListAsync();

            // Converta a lista de Fornecedor para FornecedorDTO
            var fornecedoresDTO = fornecedores.Select(f => new FornecedorDTO
            {
                Id = f.Id,
                Cep = f.Cep,
                Cnpj = f.Cnpj,
                Endereco = f.Endereco,
                Name = f.Name,
                Segmento = f.Segmento, // Ou a conversão correta, dependendo de como você armazenou o Segmento
                ImageUrl = f.Image
            }).ToList();

            return View(fornecedoresDTO);
        }


        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    logger.LogError("ID do fornecedor inválido.");
                    logger.LogError($"ID:{id}");
                    return RedirectToAction("List");
                }

                var fornecedor = await dbContext.Fornecedores.FindAsync(id);

                if (fornecedor is null)
                {
                    logger.LogWarning($"Fornecedor com ID {id} não encontrado.");
                    return RedirectToAction("List");
                }

                dbContext.Fornecedores.Remove(fornecedor);
                await dbContext.SaveChangesAsync();

                logger.LogInformation($"Fornecedor removido com sucesso. ID: {fornecedor.Id}");

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao excluir fornecedor: {ex.Message}");
                return RedirectToAction("List");
            }
        }

        [HttpGet("fornecedor/edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedor = await dbContext.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                logger.LogError($"Fornecedor não encontrado com ID {id}");
                return NotFound();
            }

            var fornecedorDto = new FornecedorDTO
            {
                Id = fornecedor.Id,
                Name = fornecedor.Name,
                Cnpj = fornecedor.Cnpj,
                Segmento = fornecedor.Segmento,
                Cep = fornecedor.Cep,
                Endereco = fornecedor.Endereco,
                ImageUrl = fornecedor.Image
            };

            return View(fornecedorDto);
        }

        [HttpPost("fornecedor/edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, FornecedorDTO fornecedorDto, IFormFile file)
        {
            var fornecedor = await dbContext.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                logger.LogError($"Fornecedor não encontrado com ID {id}");
                return NotFound();
            }

            // Atualize os campos do fornecedor
            fornecedor.Name = fornecedorDto.Name;
            fornecedor.Cnpj = fornecedorDto.Cnpj;
            fornecedor.Segmento = fornecedorDto.Segmento;
            fornecedor.Cep = fornecedorDto.Cep;
            fornecedor.Endereco = fornecedorDto.Endereco;

            // Lógica para atualização de imagem
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                var filePath = Path.Combine(uploads, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                fornecedor.Image = Path.Combine("Images", file.FileName); // Atualize a propriedade da imagem
            }

            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Fornecedor editado com sucesso com ID {id}");
            return RedirectToAction("List");
        }




    }
}
