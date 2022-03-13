using Microsoft.AspNetCore.SignalR;
using ProjectJS.Domain.Entities;

namespace ProjectJS.Services;

public class AquariumHub : Hub
{
    private readonly Aquarium _aquarium;

    public AquariumHub(Aquarium aquarium)
    {
        _aquarium = aquarium;
    }

    public async Task SendAsync()
    {
        await Clients.All.SendAsync("", _aquarium.Fishes);
    }
}