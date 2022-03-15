using System.Drawing;

namespace ProjectJS.Domain.Entities.Fish;

public abstract class BaseFish
{
    protected const int Delay = 15;
    public readonly int Speed;
    public Direction Direction { get; set; } = Direction.Right;
    public Point CurrentLocation { get; set; }

    public TypeFish TypeFish { get; }

    public int ThreadId { get; set; }

    public readonly CancellationTokenSource Cts;

    private static int _autoIncrement;
    private static int AutoIncrement => ++_autoIncrement;

    public int Id { get; }

    public void KillFish()
    {
        Cts.Cancel();
    }

    public abstract void Swim();

    protected BaseFish(int speed, TypeFish typeFish)
    {
        Speed = speed;
        TypeFish = typeFish;
        Id = AutoIncrement;
        CurrentLocation = new Point(0, Random.Shared.Next(Aquarium.Height));
        Cts = new CancellationTokenSource();
    }

    public static BaseFish CreateFish(TypeFish typeFish, int speed, Aquarium aquarium)
        => typeFish switch
        {
            TypeFish.TaskFish => new TaskFish(speed, aquarium),
            TypeFish.ThreadFish => new ThreadFish(speed, aquarium),
            _ => throw new ArgumentOutOfRangeException(nameof(typeFish), typeFish, null)
        };

    protected void RecalculateLocation()
    {
        var k = Direction == Direction.Left ? -1 : 1;
        var x = CurrentLocation.X + k * Speed;
        var newX = x >= Aquarium.Width
            ? Aquarium.Width
            : x < 0 ? 0 : x;

        Direction = newX switch
        {
            Aquarium.Width => Direction.Left,
            0 => Direction.Right,
            _ => Direction
        };

        CurrentLocation = CurrentLocation with
        {
            X = newX
        };
    }
}