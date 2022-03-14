using ProjectJS.Domain.Entities.Fish;

namespace ProjectJS.Domain.Entities;

public class Aquarium
{
    public const int Height = 500;

    public const int Width = 800;

    public readonly List<BaseFish> Fishes = new();

    public BaseFish this[int index] => Fishes[index];
}
    