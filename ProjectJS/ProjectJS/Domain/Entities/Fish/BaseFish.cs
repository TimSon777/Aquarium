using System.Drawing;

namespace ProjectJS.Domain.Entities.Fish;

public abstract class BaseFish
{
    public readonly int Speed;
    public readonly Aquarium Aquarium;
    public Direction Direction { get; set; } = Direction.Right;
    public Point CurrentLocation { get; set; }

    public int ThreadId { get; set; }

    public readonly CancellationTokenSource Cts;

    private static int _autoIncrement;
    public static int AutoIncrement => ++_autoIncrement;

    public readonly int Id;
    public void KillFish()
    {
        Cts.Cancel();
    }

    public abstract void Swim();
    protected BaseFish(int speed, Aquarium aquarium)
    {
        Speed = speed;
        Aquarium = aquarium;
        Id = AutoIncrement;
        CurrentLocation = new Point(0, new Random().Next(Aquarium.Height));
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
        int newX;

        if (Direction == Direction.Right)
        {
            var x = CurrentLocation.X + Speed;
            newX = x >= Aquarium.Width ? Aquarium.Width : x;
            if (newX == Aquarium.Width) Direction = Direction.Left;
        }
        else
        {
            var x = CurrentLocation.X - Speed;
            newX = x < 0 ? 0 : x;
            if (newX == 0) Direction = Direction.Right;
        }

        CurrentLocation = CurrentLocation with
        {
            X = newX
        };
    }
}