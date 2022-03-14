using Microsoft.AspNetCore.Mvc;
using ProjectJS.Domain.Entities;
using ProjectJS.Domain.Entities.Fish;

namespace ProjectJS.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AquariumController : ControllerBase
{
    private readonly Aquarium _aquarium;

    public AquariumController(Aquarium aquarium)
    {
        _aquarium = aquarium;
    }

    [HttpGet]
    public BaseFish CreateFish(int speed, TypeFish typeFish)
    {
        var fish = BaseFish.CreateFish(typeFish, speed, _aquarium);
        _aquarium.Fishes.Add(fish);
        fish.Swim();
        return fish;
    }
    
    [HttpGet]
    public IEnumerable<BaseFish> GetAll() => _aquarium.Fishes;

    [HttpGet]
    public int DeleteRandomFish()
    {
        if (_aquarium.Fishes.Count == 0) return 0;
        var i = Random.Shared.Next(_aquarium.Fishes.Count - 1);
        var fish = _aquarium[i];
        fish.KillFish();
        _aquarium.Fishes.RemoveAt(i);
        return fish.Id;
    }
    
    [HttpGet]
    public int DeleteLast()
    {
        if (_aquarium.Fishes.Count == 0) return 0;
        var index = _aquarium.Fishes.Count - 1;
        var lastFish = _aquarium.Fishes.Last();
        lastFish.KillFish();
        _aquarium.Fishes.RemoveAt(index);
        return lastFish.Id;
    }

    [HttpGet]
    public void DeleteAll()
    {
        _aquarium.Fishes.ForEach(fish => fish.KillFish());
        _aquarium.Fishes.RemoveAll(_ => true);
    }
}