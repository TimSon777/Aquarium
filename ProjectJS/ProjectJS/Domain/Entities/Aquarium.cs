﻿using ProjectJS.Domain.Entities.Fish;
using ProjectJS.Infrastructure.Exceptions;

namespace ProjectJS.Domain.Entities;

public class Aquarium
{
    public int Height { get; private set; } = 500;

    public int Width { get; private set; } = 800;

    public readonly List<BaseFish> Fishes = new();

    public void ChangeSize(int height, int width)
    {
        height.ThrowWhenNotPositive(nameof(height));
        width.ThrowWhenNotPositive(nameof(width));
        Height = height;
        Width = width;
    }

    public BaseFish this[int index] => Fishes[index];
}
    