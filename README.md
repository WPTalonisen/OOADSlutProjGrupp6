# OOADSlutProjGrupp6

Slutprojekt i OOAD HT26, grupp 6 (William, Max och Noel).

Vi valde Val 7, ordersystem för en matleveranstjänst. Vår tjänst heter Super Duper Eats. Det är ett konsolprogram i C# där man väljer restaurang, lägger till rätter, väljer rabatt, betalar och sen kan leverera eller avboka ordern. Allt är gjort med TDD.

## Köra programmet

Man behöver .NET 10.

```
dotnet run --project OOADSlutprojMEDTDD
```

Testerna körs med:

```
dotnet test
```

Eller öppna `OOADSlutprojMEDTDD.sln` i Rider/Visual Studio.

## Vad programmet kan

Huvudmenyn har fyra val:

1. New Order - välj restaurang och rätter, gå till kassan, välj rabatt och betala (eller avbryt)
2. Show Order History - visar alla ordrar med rätter, delsumma, leveransavgift och totalpris. Avbokade ordrar som var betalda visas som återbetalda
3. Deliver Order - ordern får ett bud och levereras
4. Cancel Order - avbokar en order om det går

Lite regler vi har:

- Varje restaurang har en minsta ordersumma, är ordern under den så nekas den
- Leveransavgiften är 50 kr
- Rabatterna är ingen rabatt, 10% eller 50 kr. 50 kr-rabatten kan inte bli mer än vad maten kostar
- Det är en väntetid på 5-10 sek mellan stegen så det känns lite mer som en riktig beställning
- När en order byter status skrivs det ut, t.ex. "Order #1 is now Preparing"

En order går Received -> Preparing -> Out for delivery -> Delivered. Man kan bara avboka när den är Received eller Preparing, när budet väl har åkt går det inte längre.

## Mappar

- `OOADSlutprojMEDTDD` - själva programmet
  - `Calculator` - PriceCalculator
  - `Delivery` - DeliveryManager, DeliveryPerson, DeliveryStatus
  - `Discount` - rabatterna
  - `Order` - Order, OrderItem, OrderFactory, OrderService och observer-grejerna
  - `OrderStates` - alla statusar
  - `Restaurant` - Restaurant, Dish, RestaurantManager
  - `UI` - Program, OrderMenu, ConsoleInput
- `OOADSlutprojMEDTDD.Tests` - testerna, uppdelade i samma mappar
- `docs/uml` - UML-diagrammen
- `docs/krav.md` - kravlista, user stories, use cases och bortval

## Designmönster

**Strategy** - rabatterna. `IDiscountStrategy` har två implementationer, `PercentageDiscount` och `FixedAmountDiscount`. PriceCalculator bryr sig inte om vilken rabatt det är, så vill man lägga till en ny rabatt gör man bara en ny klass.

**State** - orderns status. Varje status är en egen klass som vet vad nästa status är och om man får avboka. Då slipper vi en massa if-satser i Order.

**Observer** - när ordern ändrar status meddelas alla som prenumererar (`IOrderObserver`). I programmet är det `ConsoleOrderNotifier` som skriver ut i konsolen. I testerna använder vi en `FakeOrderObserver` istället.

**Factory** - `OrderFactory` skapar ordrar och ger dem ett unikt ID.

## SOLID och GRASP

Vi har försökt tänka på:

- SRP: varje klass gör en sak, t.ex. räknar PriceCalculator bara pris och OrderService håller koll på ordrarna
- OCP: nya rabatter och statusar blir nya klasser, man behöver inte ändra den gamla koden
- LSP: alla rabatter och alla statusar går att byta ut mot varandra
- DIP: ConsoleOrderNotifier tar en TextWriter istället för att skriva direkt till Console, så vi kan testa den med en StringWriter
- Information Expert: Order räknar ut sin egen delsumma och Restaurant kollar själv om minsta summan är uppnådd
- Creator: Order skapar sina OrderItems
- Controller: OrderService tar emot ordrarna
- Pure Fabrication: PriceCalculator

## Datastrukturer

- `Dictionary` i RestaurantManager så man kan hämta en restaurang direkt på ID
- Två `Queue` i DeliveryManager, en för lediga bud och en för ordrar som väntar. Först in först ut, så det första lediga budet tar nästa order
- `List` för orderhistoriken

GetOrderById är en linjär sökning (O(n)) och delsumman räknas med en loop över raderna (O(n)).

## TDD

Vi har skrivit testet först, sett det bli rött, skrivit kod tills det blev grönt och sen städat. Vi använder MSTest och försöker ha en sak per test. Några tester kör flera värden med `[DataRow]`. `TestData` har en hjälpmetod som skapar en order så vi slipper skriva samma sak i varje test.

## UML

Diagrammen ligger i `docs/uml`. Vi delade upp det i fem för att det blev för plottrigt i ett:

- [Översikt](docs/uml/uml-1-oversikt.png)
- [Restaurang och order](docs/uml/uml-2-restaurang-order.png)
- [Strategy](docs/uml/uml-3-strategy.png)
- [State och observer](docs/uml/uml-4-state-observer.png)
- [Leverans](docs/uml/uml-5-leverans.png)

## Användning av AI

Vi har använt Claude (en AI-assistent från Anthropic) som hjälp under hela projektet. Vi har använt den till att:

- förklara kod och begrepp, t.ex. designmönster, SOLID och hur MSTest fungerar
- hitta och förklara fel när koden inte kompilerade eller testerna var röda
- ge stommar och kodexempel som vi sen har skrivit in och anpassat själva
- göra UML-diagrammen utifrån vår kod
- hjälpa till att städa upp koden, mappstrukturen och repot
- skriva den här README:n

Alla beslut om hur programmet ska fungera har vi tagit själva, och vi har gått igenom och testat koden så att vi förstår den.
