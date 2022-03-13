using Microsoft.AspNetCore.Mvc;
using ProjectJS.Domain.Entities;
using ProjectJS.Domain.Entities.Fish;

namespace ProjectJS.Controllers;

[ApiController]
public class AquariumController : ControllerBase
{
    private readonly Aquarium _aquarium;

    public AquariumController(Aquarium aquarium)
    {
        _aquarium = aquarium;
    }
    
    [HttpPost]
    public BaseFish CreateFish(int speed, TypeFish typeFish)
    {
        var fish = BaseFish.CreateFish(typeFish, speed, _aquarium);
        _aquarium.Fishes.Add(fish);
        fish.Swim();
        return fish;
    }

    [HttpPost]
    public int DeleteRandomFish()
    {
        if (_aquarium.Fishes.Count == 0) return 0;
        var i = Random.Shared.Next(_aquarium.Fishes.Count - 1);
        _aquarium[i].KillFish();
        _aquarium.Fishes.RemoveAt(i);
        return _aquarium[i].Id;
    }
    
    [HttpPost]
    public void DeleteLast()
    {
        _aquarium.Fishes.Last().KillFish();
        _aquarium.Fishes.RemoveAt(_aquarium.Fishes.Count - 1);
    }

    [HttpPost]
    public void DeleteAll()
    {
        _aquarium.Fishes.ForEach(fish => fish.KillFish());
        _aquarium.Fishes.RemoveAll(_ => true);
    }
}