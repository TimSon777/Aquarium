using Microsoft.AspNetCore.SignalR;
using ProjectJS.Domain.Entities;
using ProjectJS.Domain.Entities.Fish;

namespace ProjectJS.Services;

public class AquariumHub : Hub
{
    private readonly Aquarium _aquarium;

    public AquariumHub(Aquarium aquarium)
    {
        _aquarium = aquarium;
    }

    public IEnumerable<BaseFish> SendFishes() 
        => _aquarium.Fishes;
}