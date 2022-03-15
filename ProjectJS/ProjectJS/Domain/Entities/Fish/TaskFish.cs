namespace ProjectJS.Domain.Entities.Fish;

public class TaskFish : BaseFish
{
    public TaskFish(int speed, Aquarium aquarium)
        : base(speed, TypeFish.TaskFish)
    { }

    public override void Swim()
    {
        Task.Run(async () =>
        {
            while (!Cts.IsCancellationRequested)
            {
                ThreadId = Environment.CurrentManagedThreadId;
                await Task.Delay(Delay);
                RecalculateLocation();
            }
        });
    }
}