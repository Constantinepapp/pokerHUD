using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokerGui
{
    class Player
    {
        public string Name;
        public int Preflop;
        public int Flop;
        public int Turn;
        public int River;
        public int Winpreriver;
        public int Winriver;
        public int Loseriver;
        public int Round;
        public int PreflopRaise;
        public int FlopRaise;
        public int TurnRaise;
        public int RiverRaise;
        public int FoldAfterRaise;
        public Player(string name, int preflop, int flop, int turn, int river, int winpreriver, int winriver, int loseriver, int round,int preflopraise,int flopraise,int turnraise,int riverraise,int foldafterraise)
        {
            Name = name;
            Round = round;
            Preflop = preflop;
            Flop = flop;
            Turn = turn;
            River = river;
            Winpreriver = winpreriver;
            Winriver = winriver;
            Loseriver = loseriver;
            PreflopRaise = preflopraise;
            FlopRaise = flopraise;
            TurnRaise = turnraise;
            RiverRaise = riverraise;
            FoldAfterRaise = foldafterraise;
    }
        public float SeeFlopPerc()
        {
            float seeFlop = ((float)Round - (float)Preflop) * 100 / (float)Round;
            return (seeFlop);
        }




    }
}
