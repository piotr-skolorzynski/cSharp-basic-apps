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

public delegate void PriceRead(decimal price);

public class GoldPriceReader
{
    public event PriceRead? PriceRead;

    public void ReadCurrentPrice()
    {
        var _currentGoldPrice = new Random().Next(20_000, 50_000);

        OnPriceRead(_currentGoldPrice);
    }

    private void OnPriceRead(decimal price)
    {
        PriceRead?.Invoke(price);
    }
}

public class EmailPriceChangeNotifier
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

public class PushPriceChangeNotifier
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
