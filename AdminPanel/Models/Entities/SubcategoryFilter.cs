﻿namespace AdminPanel.Models.Entities;

public sealed class SubcategoryFilter
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string FilterQuery { get; set; } = null!;

    public int SubcategoryId { get; set; }
    public Subcategory Subcategory { get; set; } = null!;
}