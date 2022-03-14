using ProjectJS.Domain.Entities.Fish;

namespace ProjectJS.Domain.Entities;

public class Aquarium
{
    public const int Height = 500;

    public const int Width = 800;

    private readonly List<BaseFish> _fishes = new();
    public IEnumerable<BaseFish> Fishes => _fishes;

    private BaseFish this[int index] => _fishes[index];

    public int DeleteFishAndGetId(int index)
    {
        if (_fishes.Count == 0) return 0;
        var fish = this[index];
        fish.KillFish();
        _fishes.RemoveAt(index);
        return fish.Id;
    }

    public void DeleteAllFishes()
    {
        _fishes.ForEach(fish => fish.KillFish());
        _fishes.RemoveAll(_ => true);
    }

    public BaseFish AddFish(int speed, TypeFish typeFish)
    {
        var fish = BaseFish.CreateFish(typeFish, speed, this);
        _fishes.Add(fish);
        fish.Swim();
        return fish;
    }
}
    