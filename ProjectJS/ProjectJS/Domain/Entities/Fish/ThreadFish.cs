namespace ProjectJS.Domain.Entities.Fish;

public class ThreadFish : BaseFish
{
    private readonly Thread _thread;

    public ThreadFish(int speed, Aquarium aquarium) 
        : base(speed, aquarium, TypeFish.ThreadFish)
    {
        _thread = new Thread(Move);
        ThreadId = _thread.ManagedThreadId;
    }

    public override void Swim() => _thread.Start();

    private void Move()
    {
        while (!Cts.IsCancellationRequested)
        {
            Thread.Sleep(Delay);
            RecalculateLocation();
        }
    }
}