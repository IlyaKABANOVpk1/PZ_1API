using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PZ_1API.Models;
public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
