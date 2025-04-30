using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kuronai.Api.EFCore;
using Kuronai.Api.EFCore.Entities;
using Kuronai.Api.Services;
using Kuronai.Api.Controllers.Models.RecipeTags;
using Kuronai.Api.Exceptions;

namespace Kuronai.Api.Controllers;

public class RecipeTagsController : MealGenBaseController
{
    readonly IRecipeTagService _service;

    public RecipeTagsController(IRecipeTagService recipeTagService)
    {
        _service = recipeTagService;
    }

    // GET: api/RecipeTags
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeTagResponse>>> GetRecipeTags()
    {
        return Ok(await _service.GetRecipeTags());
    }

    // GET: api/RecipeTags/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeTagResponse>> GetRecipeTag(int id)
    {
        var recipeTag = await _service.GetRecipeTag(id);

        if (recipeTag == null)
        {
            return NotFound();
        }

        return Ok(recipeTag);
    }


    // POST: api/RecipeTags
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RecipeTag>> PostRecipeTag(PostRecipeTagRequest recipeTag)
    {
        var response = await _service.UpdateTag(recipeTag);

        return CreatedAtAction("GetRecipeTag", new { id = response.Id }, response);
    }

    // DELETE: api/RecipeTags/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipeTag(int id)
    {
        try
        {
            await _service.DeleteTag(id);
            return NoContent();
        }
        catch (ResourceNotFoundException<RecipeTag>)
        {
            return NotFound();
        }
    }
}
