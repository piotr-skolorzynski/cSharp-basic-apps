const int treshold = 30_000;

var emailPriceChangeNotifier = new EmailPriceChangeNotifier(treshold);
var pushPriceChangeNotifier = new PushPriceChangeNotifier(treshold);
var priceReader = new GoldPriceReader();
priceReader.AttachObserver(emailPriceChangeNotifier);
priceReader.AttachObserver(pushPriceChangeNotifier);

for (int i = 0; i < 3; ++i)
{
    priceReader.ReadCurrentPrice();
}

Console.ReadKey();

public class GoldPriceReader : IObservable<decimal>
{
    private int _currentGoldPrice;
    private readonly List<IObserver<decimal>> _observers = [];

    public void ReadCurrentPrice()
    {
        _currentGoldPrice = new Random().Next(20_000, 50_000);
        NotifyObservers();
    }

    public void AttachObserver(IObserver<decimal> observer)
    {
        _observers.Add(observer);
    }

    public void DetachObser(IObserver<decimal> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_currentGoldPrice);
        }
    }
}

public class EmailPriceChangeNotifier : IObserver<decimal>
{
    private readonly decimal _notificationTreshold;

    public EmailPriceChangeNotifier(decimal notificationTreshold)
    {
        _notificationTreshold = notificationTreshold;
    }

    public void Update(decimal price)
    {
        if (price > _notificationTreshold)
        {
            //imagine this is acctually sending email
            Console.WriteLine(
                $"Sending an email saying that the gold price exceeded {_notificationTreshold} and is now {price}\n"
            );
        }
    }
}

public class PushPriceChangeNotifier : IObserver<decimal>
{
    private readonly decimal _notificationTreshold;

    public PushPriceChangeNotifier(decimal notificationTreshold)
    {
        _notificationTreshold = notificationTreshold;
    }

    public void Update(decimal price)
    {
        if (price > _notificationTreshold)
        {
            //imagine this is acctually sending a push motification
            Console.WriteLine(
                $"Sending a push notification saying that the gold price exceeded {_notificationTreshold} and is now {price}\n"
            );
        }
    }
}

public interface IObserver<TData>
{
    void Update(TData data);
}

public interface IObservable<TData>
{
    void AttachObserver(IObserver<TData> observer);
    void DetachObser(IObserver<TData> observer);
    void NotifyObservers();
}
