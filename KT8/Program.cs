public class Timer
{

    public event EventHandler Tick;

    System.Threading.Timer timer;

    public Timer()
    {
        
        timer = new System.Threading.Timer(OnTick, null, 0, 1000);
    }

    public void OnTick(object state)
    {
        
        Tick?.Invoke(this, EventArgs.Empty);
    }

    
    public void Stop()
    {
        timer?.Change(Timeout.Infinite, Timeout.Infinite);
        timer?.Dispose();
    }
}


public class Clock
{
    public Clock(Timer timer)
    {
        
        timer.Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        Console.WriteLine($"Текущее время: {DateTime.Now:HH:mm:ss}");
    }
}


public class Counter
{
    private int _count = 0;

    public Counter(Timer timer)
    {
        
        timer.Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        _count++;
        Console.WriteLine($"Счетчик: {_count}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Timer timer = new Timer();

        Clock clock = new Clock(timer);
        Counter counter = new Counter(timer);

        Console.WriteLine("Нажмите Enter для завершения...");
        Console.WriteLine();

        Console.ReadLine();
        timer.Stop();
    }
}