using Microsoft.AspNetCore.Mvc;
using ProjectJS.Domain.Entities;
using ProjectJS.Domain.Entities.Fish;

namespace ProjectJS.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AquariumController : ControllerBase
{
    private readonly Aquarium _aquarium;

    public AquariumController(Aquarium aquarium) => _aquarium = aquarium;

    [HttpGet]
    public BaseFish CreateFish(int speed, TypeFish typeFish) => _aquarium.AddFish(speed, typeFish);

    [HttpGet]
    public IEnumerable<BaseFish> GetAll() => _aquarium.Fishes;

    [HttpGet]
    public int DeleteRandomFish()
        => _aquarium.DeleteFishAndGetId(Random.Shared.Next(_aquarium.Fishes.Count() - 1));

    [HttpGet]
    public int DeleteLast()
        => _aquarium.DeleteFishAndGetId(_aquarium.Fishes.Count() - 1);

    [HttpGet]
    public void DeleteAll() => _aquarium.DeleteAllFishes();
}