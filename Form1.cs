using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;


namespace pokerGui
{
    public partial class Form1 : Form
    {
        DataTable table;
        DataTable table2;
        string file;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileName(openFileDialog1.FileName);
                string filePath = openFileDialog1.FileName;
                
                file = filePath;
                ProgramCore("All",file);
            }
            
                


            

        }

         void ProgramCore(string command,string file)
        {
            table = new DataTable();
            table.Columns.Add("username", typeof(string));
            table.Columns.Add("Rounds", typeof(int));
            table.Columns.Add("See flop %", typeof(int));
            table.Columns.Add("Preflop raise/Total hands %", typeof(int));
            table.Columns.Add("Preflop raise/Times saw flop %", typeof(int));
            table.Columns.Add("Win by fold", typeof(int));
            table.Columns.Add("ShowDown Win", typeof(string));
            table.Columns.Add("ShowDown Loss", typeof(string));
            table.Columns.Add("Preflop fold", typeof(string));
            table.Columns.Add("Flop fold", typeof(string));
            table.Columns.Add("Turn fold", typeof(string));
            table.Columns.Add("River fold", typeof(string));

            table2 = new DataTable();
            table2.Columns.Add("username", typeof(string));
            table2.Columns.Add("Rounds", typeof(int));
            table2.Columns.Add("Total raises", typeof(int));
            table2.Columns.Add("Raise preflop", typeof(int));
            table2.Columns.Add("Raise flop", typeof(int));
            table2.Columns.Add("Raise turn", typeof(int));
            table2.Columns.Add("Raise river", typeof(int));
           

            dataGridView1.DataSource = table;
            dataGridView2.DataSource = table2;

            if (file == null)
            {
                file = "C:";
            }
            string[] lines = System.IO.File.ReadAllLines(@file);



            bool record = false;
            List<string> latestPlayers = new List<string>();
            Tuple<string, string> data;
            Tuple<int, int, int, int, int, int, int> showCards;
            List<Player> playersList = new List<Player>();

            int preflopRaise = 0;
            int flopRaise = 0;
            int turnRaise = 0;
            int riverRaise = 0;

            bool recordPreflop = false;
            bool recordFlop = false;
            bool recordTurn = false;
            bool recordRiver = false;

            foreach (string line in lines)
            {
                if (line.Contains("PokerStars"))
                {
                    record = false;
                    recordPreflop = false;
                    recordFlop = false;
                    recordTurn = false;
                    recordRiver = false;

                    Console.WriteLine('\n');
                }
                if (line.Contains("*** ΚΡΥΦΑ ΦΥΛΛΑ ***"))
                {
                    record = false;
                    recordPreflop = true;
                    recordFlop = false;
                    recordTurn = false;
                    recordRiver = false;

                    Console.WriteLine('\n');
                }
                if (line.Contains("*** FLOP ***"))
                {
                    record = false;
                    recordPreflop = false;
                    recordFlop = true;
                    recordTurn = false;
                    recordRiver = false;

                    Console.WriteLine('\n');
                }
                if (line.Contains("*** TURN ***"))
                {
                    record = false;
                    recordPreflop = false;
                    recordFlop = false;
                    recordTurn = true;
                    recordRiver = false;

                    Console.WriteLine('\n');
                }
                if (line.Contains("*** RIVER ***"))
                {
                    record = false;
                    recordPreflop = false;
                    recordFlop = false;
                    recordTurn = false;
                    recordRiver = true;

                    Console.WriteLine('\n');
                }
                if (line.Contains("*** ΣΥΝΟΨΗ ***"))
                {
                    record = true;
                    recordPreflop = false;
                    recordFlop = false;
                    recordTurn = false;
                    recordRiver = false;

                }
                if (recordPreflop == true && line.Contains(":"))
                {
                    string[] lineSplit = line.Split(':');
                    if (lineSplit[1].Contains("raise"))
                    {
                        var obj = playersList.FirstOrDefault(user => user.Name.Contains(lineSplit[0]));
                        if(obj != null)
                        {
                            obj.PreflopRaise = obj.PreflopRaise + 1;
                        }
                        else
                        {
                            var player = new Player(lineSplit[0], 0, 0, 0, 0, 0, 0, 0, 0,1,0,0,0,0);
                        }
                    }

                }
                if (recordFlop == true && line.Contains(":"))
                {
                    string[] lineSplit = line.Split(':');
                    if (lineSplit[1].Contains("raise"))
                    {
                        var obj = playersList.FirstOrDefault(user => user.Name.Contains(lineSplit[0]));
                        if (obj != null)
                        {
                            obj.FlopRaise = obj.FlopRaise + 1;
                        }
                        else
                        {
                            var player = new Player(lineSplit[0], 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0,0);
                        }
                    }

                }
                if (recordTurn == true && line.Contains(":"))
                {
                    string[] lineSplit = line.Split(':');
                    if (lineSplit[1].Contains("raise"))
                    {
                        var obj = playersList.FirstOrDefault(user => user.Name.Contains(lineSplit[0]));
                        if (obj != null)
                        {
                            obj.TurnRaise = obj.TurnRaise + 1;
                        }
                        else
                        {
                            var player = new Player(lineSplit[0], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0,0);
                        }
                    }

                }
                if (recordRiver == true && line.Contains(":"))
                {
                    string[] lineSplit = line.Split(':');
                    if (lineSplit[1].Contains("raise"))
                    {
                        var obj = playersList.FirstOrDefault(user => user.Name.Contains(lineSplit[0]));
                        if (obj != null)
                        {
                            obj.RiverRaise = obj.RiverRaise + 1;
                        }
                        else
                        {
                            var player = new Player(lineSplit[0], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,0);
                        }
                    }

                }
                if (record == true && line.Contains("Θέση"))
                {
                    data = collectData(line);
                    latestPlayers.Add(data.Item1);
                    showCards = playSawCards(data);


                    //Console.WriteLine(line);
                    
                    var obj = playersList.FirstOrDefault(user => data.Item1.Contains(user.Name));
                    var obj2 = playersList.FirstOrDefault(user => user.Name.Contains(data.Item1));
                 
                    if (obj != null)
                    {
                        
                        obj.Flop = obj.Flop + showCards.Item2;
                        obj.Preflop = obj.Preflop + showCards.Item1;
                        obj.Turn = obj.Turn + showCards.Item3;
                        obj.River = obj.River + showCards.Item4;
                        obj.Winpreriver = obj.Winpreriver + showCards.Item5;
                        obj.Winriver = obj.Winriver + showCards.Item6;
                        obj.Loseriver = obj.Loseriver + showCards.Item6;
                        obj.Round = obj.Round + 1;

                    }
                    else if (obj2 != null)
                    {
                        obj2.Flop = obj2.Flop + showCards.Item2;
                        obj2.Preflop = obj2.Preflop + showCards.Item1;
                        obj2.Turn = obj2.Turn + showCards.Item3;
                        obj2.River = obj2.River + showCards.Item4;
                        obj2.Winpreriver = obj2.Winpreriver + showCards.Item5;
                        obj2.Winriver = obj2.Winriver + showCards.Item6;
                        obj2.Loseriver = obj2.Loseriver + showCards.Item6;
                        obj2.Round = obj2.Round + 1;
                    }
                    else
                    {
                        var player = new Player(data.Item1, showCards.Item1, showCards.Item2, showCards.Item3, showCards.Item4, showCards.Item5, showCards.Item6, showCards.Item7, 1, 0, 0, 0, 0,0);
                        playersList.Add(player);
                    }


                }
                if (line.Contains("PokerStars"))
                {
                    record = false;
                    Console.WriteLine('\n');
                }


            }
            if (command == "small")
            {
                while (latestPlayers.Count > 27)
                {
                    Console.WriteLine(latestPlayers[0]);
                    latestPlayers.RemoveAt(0);
                }
                
            }
            if (command == "large")
            {
                while (latestPlayers.Count > 45)
                {
                    Console.WriteLine(latestPlayers[0]);
                    latestPlayers.RemoveAt(0);
                    
                }
            }
            
            foreach (Player player in playersList)
            {
                if (latestPlayers.Contains(player.Name))
                {
                    try
                    {
                        table.Rows.Add(player.Name, player.Round, player.SeeFlopPerc(), player.PreflopRaise * 100 / player.Round, player.PreflopRaise * 100 / (player.Round - player.Preflop), player.Winpreriver, player.Winriver, player.Loseriver, player.Preflop, player.Flop, player.Turn, player.River);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }
                    
                        
                    

                    table2.Rows.Add(player.Name, player.Round, player.PreflopRaise+ player.FlopRaise+ player.TurnRaise+ player.RiverRaise, player.PreflopRaise, player.FlopRaise, player.TurnRaise, player.RiverRaise);
                }
                
            }
                


            
        }
        
       
        public static Tuple<int, int, int, int, int, int, int> playSawCards(Tuple<string, string> data)
        {
            int preflop = 0;
            int flop = 0;
            int turn = 0;
            int river = 0;
            int winpreriver = 0;
            int winriver = 0;
            int loseriver = 0;


            if (data.Item2.Contains("έκανε fold πριν Flop"))
            {
                preflop = 1;
                //Console.WriteLine("preflop");
            }
            if (data.Item2.Contains("έκανε fold στο Flop"))
            {
                flop = 1;
                //Console.WriteLine("flop");
            }
            if (data.Item2.Contains("έκανε fold στο Turn"))
            {
                turn = 1;
                //Console.WriteLine("turn");
            }
            if (data.Item2.Contains("έκανε fold στο River"))
            {
                river = 1;
                //Console.WriteLine("river");
            }
            if (data.Item2.Contains("απεκόμισε"))
            {
                winpreriver = 1;
                //Console.WriteLine("apekomise");
            }
            if (data.Item2.Contains("κέρδισε"))
            {
                winriver = 1;
                //Console.WriteLine("kerdise");
            }
            if (data.Item2.Contains("έχασε")  || data.Item2.Contains("mucked"))
            {
                loseriver = 1;
                //Console.WriteLine("exase");
            }

            return Tuple.Create(preflop, flop, turn, river, winpreriver, winriver, loseriver);
        }
        public static Tuple<string, string> collectData(string line)
        {
            try
            {
                string[] lineSplit = line.Split(':');
                string username;
                string position;
                string action;
                if (line.Contains("button") || line.Contains("blind"))
                {
                    string[] lineSplit2 = lineSplit[1].Split('(');
                    string[] lineSplit3 = lineSplit2[1].Split(')');
                    position = lineSplit[0];
                    username = lineSplit2[0];
                    action = lineSplit3[1];
                    string positionActual = lineSplit3[0];
                }
                else
                {
                    string[] lineSplit2 = lineSplit[1].Split(' ');

                    position = lineSplit[0];
                    username = string.Concat(lineSplit2[1].Where(c => !char.IsWhiteSpace(c)));
                    action = string.Join(" ", lineSplit2);

                }



                return Tuple.Create(username, action);
            }
            catch
            {
                Console.WriteLine("Next");
                return Tuple.Create("NONE", "NONE");
            }


        }
        

       

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProgramCore("large",file);
        }

        

        

        

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileName(openFileDialog1.FileName);
                string filePath = openFileDialog1.FileName;
                MessageBox.Show(fileName + " - " + filePath);
                file = filePath;
                ProgramCore("All", file);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ProgramCore("All", file);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ProgramCore("small", file);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
        }
    }
}
