namespace ProjectJS.Domain.Entities.Fish;

public class ThreadFish : BaseFish
{
    private readonly Thread _thread;

    public ThreadFish(int speed, Aquarium aquarium) 
        : base(speed, aquarium)
    {
        _thread = new Thread(A);
        ThreadId = _thread.ManagedThreadId;
    }

    public override void Swim()
    {
        _thread.Start();
    }

    private void A()
    {
        while (Cts.IsCancellationRequested)
        {
            RecalculateLocation();
        }
    }
}