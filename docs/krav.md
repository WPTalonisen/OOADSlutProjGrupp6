# Krav, user stories och use cases

Här har vi samlat kraven för Super Duper Eats, hur vi har löst dem och vad vi valt bort. Utgångspunkten är kravspecifikationen för Val 7.

## Kravlista

### Funktionella krav

| # | Krav | Status | Hur vi löst det |
|---|---|---|---|
| F1 | Skapa en ny beställning från en vald restaurang | Klart | `OrderFactory.CreateOrder` skapar ordern, menyval 1 i `OrderMenu` |
| F2 | Lägga till maträtter med antal | Klart | `Order.AddItem(dish, quantity)` skapar en `OrderItem` |
| F3 | Kolla att beställningen når restaurangens minsta summa | Klart | `Restaurant.MeetsMinimumOrder`, anropas från `OrderService.PlaceOrder` |
| F4 | Räkna ut totalpris med rabatt och leveransavgift | Klart | `PriceCalculator.CalculateTotal`: delsumma - rabatt + leveransavgift |
| F5 | Uppdatera status genom hela flödet | Klart | State-mönstret, `Order.AdvanceStatus()` |
| F6 | Tilldela ordern till ett ledigt bud | Klart | `DeliveryManager.AssignOrder` tar första lediga budet ur en kö |
| F7 | Avboka en beställning | Klart | `Order.Cancel()`, går bara när ordern är Received eller Preparing |
| F8 | Se beställningshistorik | Klart | `OrderService.OrderHistory`, menyval 2 |
| F9 | Nya rabatttyper utan att ändra befintlig kod | Klart | Strategy-mönstret, `IDiscountStrategy` |
| F10 | Validera statusövergångar | Klart | Varje statusklass kastar `InvalidOperationException` vid otillåten övergång, t.ex. Delivered -> Preparing |

### Icke-funktionella krav

| Krav | Hur vi uppfyller det |
|---|---|
| Användbarhet | Numrerade menyer, delsumman visas medan man väljer rätter och man får ett tydligt meddelande om något inte går |
| Robusthet | `ConsoleInput.ReadInt` frågar igen tills man skriver ett tal. Felaktig restaurang, rätt, antal och ordernummer fångas. Otillåtna statusbyten fångas med try/catch så programmet inte kraschar. Rabatterna kastar fel vid ogiltiga värden (procent utanför 0-100, belopp 0 eller mindre) |
| Prestanda | `Dictionary` för restauranger ger O(1)-uppslag, `Queue` för bud ger O(1) när ett bud ska tilldelas. `GetOrderById` är O(n), vilket räcker för tusentals ordrar |
| Underhållbarhet | En klass per fil, mappar per ansvarsområde och varje klass har ett ansvar (SRP) |
| Utbyggbarhet | Ny rabatt = ny klass som implementerar `IDiscountStrategy`. Ny restaurang eller nytt bud läggs till via `RestaurantManager.AddRestaurant` och `DeliveryManager.AddDeliveryPerson` utan att någon klass ändras |
| Testbarhet | Enhetstester med MSTest för alla centrala klasser. `PriceCalculator` testas helt utan konsolen, och `ConsoleOrderNotifier` tar en `TextWriter` så den kan testas med `StringWriter` |
| Läsbarhet | Klasser, metoder och variabler har engelska namn och följer C#-konventioner (PascalCase, `_` för privata fält) |

## Domänobjekt

| Objekt i kravspecen | Vår klass | Det som finns | Det som skiljer sig |
|---|---|---|---|
| Order | `Order` | ID, restaurang, rader, status, delsumma, leveransavgift, totalpris, betald eller inte | Ingen kundinformation, budet sparas på `DeliveryPerson` istället (se bortval) |
| OrderRad | `OrderItem` | Maträtt, antal, pris per enhet, delpris | - |
| Restaurang | `Restaurant` | ID, namn, beskrivning, meny, minsta beställningssumma | Ingen leveranstid, enda regeln är minsta summan |
| Maträtt | `Dish` | ID, namn, pris, beskrivning | Ingen referens tillbaka till restaurangen (se bortval) |
| Rabatt | `IDiscountStrategy`, `PercentageDiscount`, `FixedAmountDiscount` | Rabatttyp och beräkning | Inga automatiska villkor, kunden väljer rabatt själv |
| Leveransperson | `DeliveryPerson` | ID, namn, status (Available/Busy), nuvarande order | - |

## User stories

**US1** - Som kund vill jag se vilka restauranger som finns och vad de har på menyn, så att jag kan välja vad jag vill äta.
- Restaurangerna visas med namn och minsta beställningssumma
- Menyn visar varje rätt med pris

**US2** - Som kund vill jag lägga till flera rätter och välja antal, så att jag kan beställa till fler än mig själv.
- Man kan välja samma eller olika rätter flera gånger
- Antal mindre än 1 nekas
- Delsumman uppdateras efter varje rätt

**US3** - Som kund vill jag få veta direkt om min beställning är för liten, så att jag inte tror att den har gått igenom.
- En order under minsta summan nekas och minsta summan skrivs ut

**US4** - Som kund vill jag kunna använda en rabatt och se vad jag ska betala innan jag betalar, så att jag inte blir överraskad.
- Man kan välja ingen rabatt, 10% eller 50 kr
- Totalpriset visas med leveransavgiften inräknad innan man betalar
- En fast rabatt kan inte bli större än delsumman

**US5** - Som kund vill jag få besked när min order byter status, så att jag vet var maten är.
- Varje statusbyte skrivs ut, t.ex. "Order #1 is now Preparing"

**US6** - Som kund vill jag kunna avboka min order innan den har lämnat restaurangen, så att jag kan ångra mig.
- Avbokning går när ordern är Received eller Preparing
- När ordern är ute för leverans eller levererad får man ett felmeddelande istället
- En betald order som avbokas visas som återbetald i historiken

**US7** - Som leveransansvarig vill jag att ordrar går till det bud som varit ledigt längst, så att jobben fördelas rättvist.
- Lediga bud står i en kö (FIFO)
- Finns inget ledigt bud läggs ordern i en väntekö
- När ett bud är klart tar det nästa väntande order, annars ställer det sig sist i kön

**US8** - Som ägare av tjänsten vill jag se alla beställningar, så att jag har koll på vad som har hänt.
- Historiken visar varje order med restaurang, status, rätter, delsumma, leveransavgift och totalpris eller återbetalning

## Use cases

### UC1: Kund skapar en beställning

**Aktör:** Kund
**Förkrav:** Det finns minst en restaurang i systemet
**Huvudflöde:**
1. Kunden väljer New Order
2. Systemet visar restaurangerna
3. Kunden väljer restaurang
4. Systemet visar menyn och delsumman
5. Kunden väljer rätt och antal, steg 4-5 upprepas tills kunden väljer 0 (kassan)
6. Systemet kollar att minsta summan är uppnådd och sparar ordern i historiken
7. Kunden väljer rabatt (se UC3)
8. Systemet visar totalpriset och kunden väljer Pay
9. Systemet markerar ordern som betald och skickar den till restaurangen

**Efterkrav:** Ordern finns i historiken, är betald och har status Preparing
**Alternativflöden:**
- 3a. Restaurangen finns inte: systemet skriver "Restaurant not found" och går tillbaka till huvudmenyn
- 5a. Ogiltigt rättnummer eller antal: systemet skriver ett felmeddelande och visar menyn igen
- 6a. Ordern är under minsta summan: systemet nekar ordern och skriver ut minsta summan
- 8a. Kunden väljer Cancel: ordern avbokas och visas som "Not paid" i historiken

### UC2: Tilldela beställning till leveransperson

**Aktör:** Kund/personal (menyval 3)
**Förkrav:** Ordern har status Preparing
**Huvudflöde:**
1. Användaren väljer Deliver Order och skriver ordernumret
2. Systemet sätter ordern till Out for delivery
3. Systemet tar första lediga budet ur kön och tilldelar ordern, budet blir Busy
4. Budet levererar och ordern blir Delivered
5. Budet blir Available igen och ställer sig sist i kön

**Efterkrav:** Ordern är levererad och budet är ledigt
**Alternativflöden:**
- 1a. Ordern finns inte: systemet skriver "Order not found"
- 2a. Ordern har fel status (t.ex. redan levererad eller avbokad): systemet skriver felmeddelandet från statusklassen
- 3a. Inget bud är ledigt: ordern läggs i väntekön och tas av nästa bud som blir klart

### UC3: Beräkna rabatt på beställning

**Aktör:** Kund
**Förkrav:** Ordern har rätter och är godkänd mot minsta summan
**Huvudflöde:**
1. Systemet visar rabattvalen
2. Kunden väljer rabatt
3. Systemet skapar rätt rabattstrategi (`PercentageDiscount` eller `FixedAmountDiscount`)
4. `PriceCalculator` räknar delsumma - rabatt + leveransavgift
5. Priset sparas på ordern och visas för kunden

**Efterkrav:** Ordern har rätt totalpris och leveransavgift sparade
**Alternativflöden:**
- 2a. Kunden väljer ingen rabatt eller något ogiltigt val: ordern får priset utan rabatt
- 4a. Den fasta rabatten är större än delsumman: rabatten blir lika stor som delsumman, så priset blir aldrig negativt

### UC4: Kund avbokar en beställning

**Aktör:** Kund
**Förkrav:** Ordern finns i historiken
**Huvudflöde:**
1. Kunden väljer Cancel Order och skriver ordernumret
2. Systemet ber orderns nuvarande status om att avboka
3. Ordern blir Cancelled och kunden får en notis

**Efterkrav:** Ordern har status Cancelled, är den betald visas den som återbetald i historiken
**Alternativflöden:**
- 1a. Ordern finns inte: systemet skriver "Order not found"
- 2a. Ordern är Out for delivery, Delivered eller redan Cancelled: statusklassen nekar och systemet skriver felmeddelandet

## Bortval och motiveringar

**Kundinformation och inloggning** - Vi har ingen kundklass. Programmet körs av en person i taget i konsolen, så en inloggning hade inte tillfört något för att visa designen. Vill man lägga till det blir det en `Customer`-klass som `Order` refererar till.

**Budet sparas på DeliveryPerson istället för på Order** - Budet vet vilken order det har (`CurrentOrder`), men ordern vet inte vilket bud den har. Det håller `Order` oberoende av leveranssystemet, vilket kravspecen själv tar upp under Low Coupling.

**Dish refererar inte till Restaurant** - Restaurangen äger sin meny, så det är redan klart vilken restaurang en rätt tillhör. Om båda pekade på varandra skulle det bli en dubbelriktad koppling som måste hållas i synk.

**Ingen leveranstid** - Vi simulerar tiden med en slumpad väntan på 5-10 sekunder istället för att räkna ut en leveranstid per restaurang.

**Rabattvillkor** - Kunden väljer rabatt själv istället för att systemet kollar villkor. Strategy-mönstret gör att villkor kan läggas till i en ny rabattklass senare utan att `PriceCalculator` ändras.

**Strategy istället för if/switch i PriceCalculator** - Med en switch hade varje ny rabatt krävt att vi ändrar `PriceCalculator`, vilket bryter mot OCP. Nu blir en ny rabatt bara en ny klass.

**State istället för en enum** - En enum med switch hade samlat alla regler för statusbyten i `Order`. Med State vet varje status själv vad som är tillåtet, och `Order` behöver inte ändras när reglerna gör det. En ny status kräver fortfarande att statusen före den ändras så att den pekar på den nya, men `Order` rörs inte.

**Restauranger i koden istället för i en fil** - Vi pratade om att läsa in restaurangerna från en textfil, men valde att lägga dem i `Program.cs` för att hålla fokus på designen. Eftersom allt går via `RestaurantManager.AddRestaurant` skulle en fil eller en admin-meny kunna läggas till utan att någon annan klass ändras.

**Linjär sökning i GetOrderById** - Ordrarnas ID:n blir alltid större, så historiken är redan sorterad och binärsökning (O(log n)) hade gått. En `Dictionary<int, Order>` hade gett O(1). Vi behöll listan eftersom historiken ska visas i ordning och O(n) räcker för den mängd data vi har.

## Kända begränsningar

- En order som nekas för att den är under minsta summan har redan fått ett ID av `OrderFactory`, så det blir ett hopp i numreringen
- Väntekön för bud används inte i praktiken i konsolen, eftersom varje leverans körs klart innan nästa kan startas. Den finns och är testad i `DeliveryManagerTest`
