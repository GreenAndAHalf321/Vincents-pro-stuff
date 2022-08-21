using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSim
{

    class Simulation
    {
        public static int Tries { get; private set; } = 0;
        public static int RoyalFlush { get; private set; } = 0;
        public static int StraightFlush { get; private set; } = 0;
        public static int Vierling { get; private set; } = 0;
        public static int FullHouse { get; private set; } = 0;
        public static int Flush { get; private set; } = 0;
        public static int Strasse { get; private set; } = 0;
        public static int Drilling { get; private set; } = 0;
        public static int DoppelPaar { get; private set; } = 0;
        public static int Paar { get; private set; } = 0;
        public static int Nothing { get; private set; } = 0;

        private static Random rnd = new Random();

        private class Player
        {
            public LinkedList<Card> cards = new LinkedList<Card>();

            public void AddCard(Card card)
            {
                if (cards.Count == 2)
                    return;

                cards.AddLast(card);
            }
        }


        List<Card> cardDeck = new List<Card>();
        List<Card> cardsOnTheTable = new List<Card>();
        List<Player> players = new List<Player>();

        int playerCount;

        public Simulation(int playerCount)
        {
            this.playerCount = playerCount;
            StartSimulation();
        }

        private void StartSimulation()
        {
            //Karten erstellen
            for (int zeichenNummer = 0; zeichenNummer < 4; zeichenNummer++)
                for (int kartenWert = 2; kartenWert <= 14; kartenWert++)
                    cardDeck.Add(new Card((Zeichen)Enum.ToObject(typeof(Zeichen), zeichenNummer), kartenWert));

            //Spieler erstellen
            for (int i = 0; i < playerCount; i++)
                players.Add(new Player());

            //Karten austeilen
            int card;

            for(int i = 0; i < 2; i++)
            {
                //Eine Karte jedem Spieler geben
                foreach (Player player in players)
                {
                    card = rnd.Next(cardDeck.Count);
                    player.AddCard(cardDeck.ElementAt(card));
                    cardDeck.RemoveAt(card);
                }

                //Karten auf den Tisch legen
                for (int j = 0; j < 3 - i; j++)
                {
                    card = rnd.Next(cardDeck.Count);
                    cardsOnTheTable.Add(cardDeck.ElementAt(card));
                    cardDeck.RemoveAt(card);
                }
            }

            //Überprüfen was der Spieler hat

            Tries++;

            foreach(Player player in players)
            {
                if(CheckFlush(player))
                {
                    List<Card> flush = GetFlush(player);
                    if(CeckStreet(flush)) //Wenn das Flush auch eine Straße ist, ist es ein Straight flush
                    {
                        flush.Sort();
                        if(flush.ElementAt(4).Value == 14) //Wenn dieses Straight Flush jetzt auch noch als höchste Karte ein Ass hat ist ein ein Royal Flush. Das beste was man haben kann.
                        {
                            RoyalFlush++;
                            break;
                        }

                        StraightFlush++;
                        break;
                    }

                    Flush++;
                    break;
                }

                if(CeckStreet(player))
                {
                    Strasse++;
                    break;
                }

                if(CeckVireling(player))
                {
                    Vierling++;
                    break;
                }

                if (CeckDrilling(player))
                {
                    if(CheckPaarWithou(player, GetDrilling(player))) //Falls es ein Paar und ein Drilling gibt ist das ein Full House
                    {
                        FullHouse++;
                        break;
                    }

                    Drilling++;
                    break;
                }

                if(CheckDoublePaar(player))
                {
                    DoppelPaar++;
                    break;
                }

                if (CheckPaar(player))
                {
                    Paar++;
                    break;
                }

                Nothing++;
            }

            double royalFlushProzentage = RoyalFlush / (double)Tries * 100;
            double straightFlushProzentage = StraightFlush / (double)Tries * 100;
            double VierlingeProzentage = Vierling / (double)Tries * 100;
            double fullHouseProzentage = FullHouse / (double)Tries * 100;
            double flushProzentage = Flush / (double)Tries * 100;
            double StreetProzentage = Strasse / (double)Tries * 100;
            double drillingProzentage = Drilling / (double)Tries * 100;
            double doppelPaarProzentage = DoppelPaar / (double)Tries * 100;
            double paarProzentage = Paar / (double)Tries * 100;
            double nothingProzentage = Nothing / (double)Tries * 100;

            Console.WriteLine($"Simulation #{String.Format("{0:00000000}", Tries)} beendet: \n" +
                $"Royal Flush: {String.Format("{0:0.00}", royalFlushProzentage)}% - {RoyalFlush}\n" +
                $"Straight Flush: {String.Format("{0:0.00}", straightFlushProzentage)}% - {StraightFlush}\n" +
                $"Vierling: {String.Format("{0:0.00}", VierlingeProzentage)}% - {Vierling}\n" +
                $"Full House: {String.Format("{0:0.00}", fullHouseProzentage)}% - {FullHouse}\n" +
                $"Flush: {String.Format("{0:0.00}", flushProzentage)}% - {Flush}\n" +
                $"Straße: {String.Format("{0:0.00}", StreetProzentage)}% - {Strasse}\n" +
                $"Drilling: {String.Format("{0:0.00}", drillingProzentage)}% - {Drilling}\n" +
                $"Doppeltes Paar: {String.Format("{0:0.00}", doppelPaarProzentage)}% - {DoppelPaar}\n" +
                $"Paar: {String.Format("{0:0.00}", paarProzentage)}% - {Paar}\n" +
                $"Nichts: {String.Format("{0:0.00}", nothingProzentage)}% - {Nothing}\n" );
        }


        private List<Card> GetTableDeckAndPlayerDeck(Player player)
        {
            List<Card> cardsOnTablePlusPlayerCards = new List<Card>();

            foreach (Card card in cardsOnTheTable)
                cardsOnTablePlusPlayerCards.Add(card);

            foreach (Card card in player.cards)
                cardsOnTablePlusPlayerCards.Add(card);

            cardsOnTablePlusPlayerCards.Sort();

            return cardsOnTablePlusPlayerCards;
        }

        private List<Card> GetFlush(Player player)
        {
            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            cardsOnTablePlusPlayerCards.Sort(new CardColorComparer());

            List<Card> flush = new List<Card>();

            flush.Add(cardsOnTablePlusPlayerCards.ElementAt(0));

            for (int i = 0; i < 6; i++)
            {
                if (cardsOnTablePlusPlayerCards.ElementAt(i) | cardsOnTablePlusPlayerCards.ElementAt(i + 1))
                {
                    flush.Add(cardsOnTablePlusPlayerCards.ElementAt(i + 1));

                    if (flush.Count == 5)
                        return flush;

                    continue;
                }

                flush.Clear(); ;
            }

            return null;
        }

        private bool CheckFlush(Player player)
        {
            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            cardsOnTablePlusPlayerCards.Sort(new CardColorComparer());

            int equalColorCount = 0;

            for(int i = 0; i < 6; i++)
            {
                if (cardsOnTablePlusPlayerCards.ElementAt(i) | cardsOnTablePlusPlayerCards.ElementAt(i + 1))
                {
                    equalColorCount++;

                    if (equalColorCount == 5)
                        return true;

                    continue;
                }

                equalColorCount = 0;
            }

            return false;
        }

        private bool CeckStreet(List<Card> cardDeck)
        {
            int nextValue = 0;
            int streetCount = 1;

            foreach (Card card in cardDeck)
            {
                if (card != nextValue)
                {
                    nextValue = card.Value + 1;
                    streetCount = 1;
                    continue;
                }

                streetCount++;
                nextValue++;

                if (streetCount == 5)
                    return true;
            }

            return false;
        }

        private bool CeckStreet(Player player)
        {
            return CeckStreet(GetTableDeckAndPlayerDeck(player));
        }

        private bool CeckVireling(Player player)
        {
            int countEqualCards = 1;

            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            foreach (Card cardA in cardsOnTablePlusPlayerCards)
            {
                foreach (Card cardB in cardsOnTablePlusPlayerCards)
                {
                    if (cardA == cardB)
                        continue;

                    if (cardA.Value == cardB.Value)
                    {
                        countEqualCards++;
                        if (countEqualCards == 4)
                            return true;
                    }

                }
                countEqualCards = 1;
            }

            return false;
        }

        private int GetDrilling(Player player)
        {
            int countEqualCards = 1;

            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            foreach (Card cardA in cardsOnTablePlusPlayerCards)
            {
                foreach (Card cardB in cardsOnTablePlusPlayerCards)
                {
                    if (cardA == cardB)
                        continue;

                    if (cardA.Value == cardB.Value)
                    {
                        countEqualCards++;
                        if (countEqualCards == 3)
                            return cardA.Value;
                    }

                }

                countEqualCards = 1;
            }

            return 0;
        }

        private bool CeckDrilling(Player player)
        {
            int countEqualCards = 1;

            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            foreach (Card cardA in cardsOnTablePlusPlayerCards)
            {
                foreach (Card cardB in cardsOnTablePlusPlayerCards)
                {
                    if (cardA == cardB)
                        continue;

                    if (cardA.Value == cardB.Value)
                    {
                        countEqualCards++;
                        if (countEqualCards == 3)
                            return true;
                    }

                }
                countEqualCards = 1 ;
            }

            return false;
        }

        private bool CheckPaarWithou(Player player, int forbittenValue)
        {
            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            foreach (Card cardA in cardsOnTablePlusPlayerCards)
            {
                if (cardA.Value == forbittenValue)
                    continue;

                foreach (Card cardB in cardsOnTablePlusPlayerCards)
                    if(cardA != cardB)
                        if (cardA.Value == cardB.Value)
                            return true;
            }


            return false;
        }

        private bool CheckDoublePaar(Player player)
        {
            if(CheckPaar(player))
            {
                int paarOne = GetPaar(player);
                if (CheckPaarWithou(player, paarOne))
                    return true;
            }

            return false;
        }

        private int GetPaar(Player player)
        {
            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            foreach (Card cardA in cardsOnTablePlusPlayerCards)
                foreach (Card cardB in cardsOnTablePlusPlayerCards)
                    if(cardA != cardB)
                        if (cardA.Value == cardB.Value)
                            return cardA.Value;

            return 0;
        }

        private bool CheckPaar(Player player)
        {
            List<Card> cardsOnTablePlusPlayerCards = GetTableDeckAndPlayerDeck(player);

            foreach (Card cardA in cardsOnTablePlusPlayerCards)
                foreach (Card cardB in cardsOnTablePlusPlayerCards)
                    if(cardA != cardB)
                        if (cardA.Value == cardB.Value)
                            return true;

            return false;
        }

    }
}
