using System;

namespace PokerSim
{
    enum Zeichen
    {
        Herz,
        Kreuz,
        Pik,
        Karo,
    }

    class Card : IComparable<Card>
    {
        public readonly Zeichen zeichen;

        private int value;
        public int Value
        {
            get
            {
                return value;
            }

            private set
            {
                if (value < 2 || value > 14)
                    throw new IndexOutOfRangeException();

                this.value = value;
            }
        }

        public Card(Zeichen zeichen, int value)
        {
            this.zeichen = zeichen;
            try
            {
                Value = value;
            }catch(IndexOutOfRangeException e)
            {
                Value = 2;
            }
        }

        //Größer kleiner
        public static bool operator <(Card a, Card b) => a.Value < b.Value;
        public static bool operator >(Card a, Card b) => a.Value > b.Value;
        //Equals value
        public static bool operator ==(Card card, int value) => card.Value == value;
        public static bool operator !=(Card card, int value) => card.Value != value;
        //Equals Color
        public static bool operator |(Card a, Card b)
        {
            if (a.zeichen == b.zeichen)
                return true;

            if (a.zeichen == Zeichen.Herz || a.zeichen == Zeichen.Karo)
            {
                if (b.zeichen == Zeichen.Herz || b.zeichen == Zeichen.Karo)
                    return true;
                
                return false;
            }

            if (b.zeichen == Zeichen.Herz || b.zeichen == Zeichen.Karo)
                return false;

            return true;
        }


        public int CompareTo(Card other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}
