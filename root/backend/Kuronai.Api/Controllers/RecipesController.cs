using Kuronai.Api.Controllers.Models.Recipes;
using Kuronai.Api.EFCore;
using Kuronai.Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kuronai.Api.Controllers;

public class RecipesController : MealGenBaseController
{
    private readonly MealGenDbContext _context;

    public RecipesController(MealGenDbContext context)
    {
        _context = context;
    }

    // GET: api/Recipes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeResponse>>> GetRecipes()
    {
        var recipes = await _context.Recipes.ToListAsync();
        return Ok(recipes.Select(RecipeMapper.ToResponse));
    }

    // GET: api/Recipes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeResponse>> GetRecipe(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return RecipeMapper.ToResponse(recipe);
    }

    // PUT: api/Recipes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRecipe(int id, PutRecipeRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        var recipe = RecipeMapper.ToRecipe(request);

        _context.Entry(recipe).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeExists(id))
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

    // POST: api/Recipes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RecipeResponse>> PostRecipe(PostRecipeRequest request)
    {
        var recipe = RecipeMapper.ToRecipe(request);
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRecipe", new { id = recipe.Id }, RecipeMapper.ToResponse(recipe));
    }

    // DELETE: api/Recipes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RecipeExists(int id)
    {
        return _context.Recipes.Any(e => e.Id == id);
    }
}
