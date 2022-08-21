using System.Collections.Generic;

namespace PokerSim
{
    class CardColorComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            if(x.zeichen == Zeichen.Herz || x.zeichen == Zeichen.Karo)
            {
                if (y.zeichen == Zeichen.Herz || y.zeichen == Zeichen.Karo)
                    return 0;

                return -1;
            }

            if (y.zeichen == Zeichen.Herz || y.zeichen == Zeichen.Karo)
                return 1;

            return 0;
        }
    }
}
