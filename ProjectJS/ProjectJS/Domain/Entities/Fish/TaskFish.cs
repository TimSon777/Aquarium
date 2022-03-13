namespace ProjectJS.Domain.Entities.Fish;

public class TaskFish : BaseFish
{
    public TaskFish(int speed, Aquarium aquarium) 
        : base(speed, aquarium)
    { }
    
    public override void Swim()
    {
        while (Cts.IsCancellationRequested)
        {
            var task = Task.Run(RecalculateLocation);
            ThreadId = task.Id;
        }
    }
}