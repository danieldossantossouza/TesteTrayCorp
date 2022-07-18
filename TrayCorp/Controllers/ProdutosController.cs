using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrayCorp.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrayCorp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		private readonly AppDbContext _context;
		public ProdutosController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/<ProdutosController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Produto>>>ObterProdutos()
		{
			return await _context.Produtos.ToListAsync();
		}

		// GET api/<ProdutosController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Produto>>ObterProdutoId(int id)
		{
			var produto = await _context.Produtos.FindAsync(id);
			if (produto == null)
			{
				return NotFound();
			}
			return produto;

		}

	

		// POST api/<ProdutosController>
		[HttpPost]
		public async Task<ActionResult<Produto>>CriarProduto(Produto produto)
		{
			if (produto.Preco < 0)
			{
				return BadRequest("O valor do Produto não pode ser negativo");
			}
			_context.Produtos.Add(produto);
			await _context.SaveChangesAsync();
			return (produto);
		}

		// PUT api/<ProdutosController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> AtualizarProduto(int id, Produto produto)
		{
			if (produto.Id != id)
			{
				return BadRequest();
			}
			else if (produto.Preco < 0)
			{
				return BadRequest("O valor do Produto não pode ser negativo");
			}
			
			_context.Entry(produto).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch(DbUpdateConcurrencyException)
			{
				if (!ProdutoExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();
		}

		

		// DELETE api/<ProdutosController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletarProduto(int id)
		{
			var produto = await _context.Produtos.FindAsync(id);
			if (produto == null)
			{
				return NotFound();
			}
			_context.Produtos.Remove(produto);
			await _context.SaveChangesAsync();
			return NoContent();
		}


		private bool ProdutoExists(int id)
		{
			return _context.Produtos.Any(p => p.Id == id);
		}
	}
}
