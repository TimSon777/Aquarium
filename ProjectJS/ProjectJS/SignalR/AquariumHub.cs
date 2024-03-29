﻿using Microsoft.AspNetCore.SignalR;
using ProjectJS.Domain.Entities;
using ProjectJS.Domain.Entities.Fish;

namespace ProjectJS.SignalR;

public class AquariumHub : Hub
{
    private readonly Aquarium _aquarium;

    public AquariumHub(Aquarium aquarium) => _aquarium = aquarium;

    public IEnumerable<BaseFish> GetFishes() => _aquarium.Fishes;
}