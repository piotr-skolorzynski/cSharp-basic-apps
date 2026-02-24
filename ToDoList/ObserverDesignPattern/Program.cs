const int treshold = 30_000;

var emailPriceChangeNotifier = new EmailPriceChangeNotifier(treshold);
var pushPriceChangeNotifier = new PushPriceChangeNotifier(treshold);
var priceReader = new GoldPriceReader();

priceReader.PriceRead += emailPriceChangeNotifier.Update;
priceReader.PriceRead += pushPriceChangeNotifier.Update;

for (int i = 0; i < 3; ++i)
{
    priceReader.ReadCurrentPrice();
}

Console.ReadKey();

// public delegate void PriceRead(decimal price);

public class PriceReadEventArgs : EventArgs
{
    public decimal Price { get; }

    public PriceReadEventArgs(decimal price)
    {
        Price = price;
    }
}

public class GoldPriceReader
{
    public event EventHandler<PriceReadEventArgs>? PriceRead;

    public void ReadCurrentPrice()
    {
        var _currentGoldPrice = new Random().Next(20_000, 50_000);

        OnPriceRead(_currentGoldPrice);
    }

    private void OnPriceRead(decimal price)
    {
        //this means that instance of GoldPriceReader is a sender
        //next is instantiation of PriceReadEventArgs
        PriceRead?.Invoke(this, new PriceReadEventArgs(price));
    }
}

public class EmailPriceChangeNotifier
{
    private readonly decimal _notificationTreshold;

    public EmailPriceChangeNotifier(decimal notificationTreshold)
    {
        _notificationTreshold = notificationTreshold;
    }

    public void Update(object? sender, PriceReadEventArgs eventArgs)
    {
        if (eventArgs.Price > _notificationTreshold)
        {
            //imagine this is acctually sending email
            Console.WriteLine(
                $"Sending an email saying that the gold price exceeded {_notificationTreshold} and is now {eventArgs.Price}\n"
            );
        }
    }
}

public class PushPriceChangeNotifier
{
    private readonly decimal _notificationTreshold;

    public PushPriceChangeNotifier(decimal notificationTreshold)
    {
        _notificationTreshold = notificationTreshold;
    }

    public void Update(object? sender, PriceReadEventArgs eventArgs)
    {
        if (eventArgs.Price > _notificationTreshold)
        {
            //imagine this is acctually sending a push motification
            Console.WriteLine(
                $"Sending a push notification saying that the gold price exceeded {_notificationTreshold} and is now {eventArgs.Price}\n"
            );
        }
    }
}
