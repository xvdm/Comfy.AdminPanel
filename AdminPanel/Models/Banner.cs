﻿namespace AdminPanel.Models;

public class Banner
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string PageUrl { get; set; } = null!;
}