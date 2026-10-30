# Super Duper Eats – ordersystem för matleverans

Slutprojekt i **Objektorienterad design och problemlösning (OOAD), HT26 – Grupp 6**
Val 7: *Matleveranstjänstens ordersystem*

Ett konsolprogram i C# där en kund väljer restaurang, lägger rätter i en order, väljer rabatt, betalar och följer ordern hela vägen till leverans. Projektet är byggt med **TDD** (testdriven utveckling) och använder flera designmönster från kursen.

**Grupp 6:** William, Max, Noel och Pär

---

## Kom igång

**Krav:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
# Kör programmet
dotnet run --project OOADSlutprojMEDTDD

# Kör alla tester
dotnet test
```

I Rider eller Visual Studio: öppna `OOADSlutprojMEDTDD.sln`, kör projektet `OOADSlutprojMEDTDD` och kör testerna i `OOADSlutprojMEDTDD.Tests`.

---

## Funktioner

| Menyval | Vad det gör |
|---|---|
| **1. New Order** | Välj restaurang → välj rätter och antal → kassa → välj rabatt → betala eller avbryt |
| **2. Show Order History** | Visar alla ordrar med rätter, delsumma, leveransavgift och total / återbetalning |
| **3. Deliver Order** | Skickar ordern med ett ledigt bud och levererar den |
| **4. Cancel Order** | Avbokar en order – om dess nuvarande status tillåter det |

- Restaurangerna har en **minsta ordersumma** – ordrar under den nekas.
- Leveransavgiften är **50 kr**.
- Rabatter: ingen, **10 %** eller **50 kr** (en fast rabatt kan aldrig bli större än delsumman).
- Varje steg simulerar en väntetid på 5–10 sekunder.
- Statusändringar skrivs ut automatiskt, t.ex. `Order #1 is now Preparing`.

### En orders livscykel

```
Received ──► Preparing ──► Out for delivery ──► Delivered
    │            │
    └────────────┴──► Cancelled
```

En order kan bara avbokas medan den är **Received** eller **Preparing**. När den är ute för leverans går den inte att avboka.

---

## Projektstruktur

```
OOADSlutprojMEDTDD/            Programmet
├── Calculator/                PriceCalculator
├── Delivery/                  DeliveryManager, DeliveryPerson, DeliveryStatus
├── Discount/                  IDiscountStrategy, PercentageDiscount, FixedAmountDiscount
├── Order/                     Order, OrderItem, OrderFactory, OrderService, IOrderObserver, ConsoleOrderNotifier
├── OrderStates/               IOrderState + Received, Preparing, OutForDelivery, Delivered, Cancelled
├── Restaurant/                Restaurant, Dish, RestaurantManager
└── UI/                        Program, OrderMenu, ConsoleInput

OOADSlutprojMEDTDD.Tests/      Enhetstester (MSTest), samma mappindelning
docs/uml/                      UML-klassdiagram
```

---

## Designmönster

| Mönster | Var | Varför |
|---|---|---|
| **Strategy** | `IDiscountStrategy` → `PercentageDiscount`, `FixedAmountDiscount` | Nya rabatttyper kan läggas till utan att ändra `PriceCalculator` |
| **State** | `IOrderState` → fem statusklasser | Varje status bestämmer själv vilken nästa status är och om avbokning är tillåten – inga långa `if`/`switch`-kedjor |
| **Observer** | `IOrderObserver` → `ConsoleOrderNotifier` | `Order` meddelar prenumeranter vid statusändring utan att veta vem de är |
| **Simple Factory** | `OrderFactory` | Skapar ordrar och delar ut unika ID:n på ett ställe |

## SOLID och GRASP

- **SRP** – varje klass har ett ansvar: `PriceCalculator` räknar pris, `OrderService` håller orderhistorik, `OrderMenu` hanterar menyn.
- **OCP** – nya rabatter och statusar läggs till som nya klasser, befintlig kod ändras inte.
- **LSP** – alla `IDiscountStrategy` och `IOrderState` kan användas utbytbart.
- **DIP** – `ConsoleOrderNotifier` tar emot en `TextWriter` i stället för att skriva direkt till `Console`, så den kan testas med `StringWriter`.
- **Information Expert** – `Order` räknar ut sin egen `Subtotal`, `Restaurant` avgör själv `MeetsMinimumOrder`.
- **Creator** – `Order` skapar sina `OrderItem`.
- **Controller** – `OrderService` tar emot och validerar ordrar.
- **Pure Fabrication** – `PriceCalculator` finns inte i verkligheten men samlar prislogiken på ett ställe.

## Datastrukturer och algoritmer

| Struktur | Var | Varför |
|---|---|---|
| `Dictionary<int, Restaurant>` | `RestaurantManager` | Snabb uppslagning på ID – O(1) |
| `Queue<DeliveryPerson>` / `Queue<Order>` | `DeliveryManager` | Rättvis FIFO-tilldelning – först ledigt bud får nästa order, ordrar utan bud väntar i kö |
| `List<Order>` | `OrderService` | Orderhistorik i den ordning ordrarna lades |

- **Summering** av orderns rader – O(n)
- **Linjär sökning** i `GetOrderById` – O(n)

---

## Testdriven utveckling

All logik är utvecklad enligt **röd → grön → refaktorera**: först ett test som misslyckas, sedan minsta möjliga kod för att få det grönt, sedan städning.

- Ramverk: **MSTest 4**
- Varje test testar en sak (Arrange – Act – Assert)
- `[DataRow]` för att testa flera värden med samma test
- `FakeOrderObserver` är en test double som registrerar notiser, så Observer-mönstret kan testas utan konsolen
- `TestData` samlar gemensam testdata

---

## UML

Klassdiagrammen finns i [`docs/uml/`](docs/uml/):

| Diagram | Innehåll |
|---|---|
| [Översikt](docs/uml/uml-1-oversikt.png) | Hela systemet på hög nivå |
| [Restaurang & Order](docs/uml/uml-2-restaurang-order.png) | Restaurant, Dish, Order, OrderItem |
| [Strategy](docs/uml/uml-3-strategy.png) | Rabatter och PriceCalculator |
| [State & Observer](docs/uml/uml-4-state-observer.png) | Orderstatus och notiser |
| [Leverans](docs/uml/uml-5-leverans.png) | DeliveryManager och bud |
