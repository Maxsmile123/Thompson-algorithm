


namespace Thompson
{
    class Program
    {


        static void Main()
        {
            // "ε", "\u03B5"  Greek alphabet
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            var ndfsa = new FSAutomate(new List<Symbol>() { "S0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "qf" },
                                      new List<Symbol>() { "1", "0", "+", "2" },
                                      new List<Symbol>() { "qf" },
                                      "S0");
            ndfsa.AddRule("S0", "1", "1");          //W1
            ndfsa.AddRule("1", "0", "2");
            ndfsa.AddRule("2", "+", "3");

            ndfsa.AddRule("3", "", "4");            //W2
            ndfsa.AddRule("4", "", "5");
            ndfsa.AddRule("4", "", "7");
            ndfsa.AddRule("4", "", "9");
            ndfsa.AddRule("5", "1", "6");
            ndfsa.AddRule("7", "2", "8");
            ndfsa.AddRule("6", "", "9");
            ndfsa.AddRule("8", "", "9");
            ndfsa.AddRule("9", "", "4");
            ndfsa.AddRule("9", "", "10");

            ndfsa.AddRule("10", "1", "11");          //W3
            ndfsa.AddRule("11", "0", "12");
            ndfsa.AddRule("12", "", "13");
            ndfsa.AddRule("13", "", "9");
            ndfsa.AddRule("13", "", "14");

            ndfsa.AddRule("14", "", "15");           //W4
            ndfsa.AddRule("14", "", "17");
            ndfsa.AddRule("15", "0", "16");
            ndfsa.AddRule("17", "1", "18");
            ndfsa.AddRule("16", "", "19");
            ndfsa.AddRule("18", "", "19");
            ndfsa.AddRule("19", "", "14");
            ndfsa.AddRule("19", "", "20");
            ndfsa.AddRule("20", "", "15");
            ndfsa.AddRule("14", "", "qf");
            ndfsa.AddRule("20", "", "qf");

            var dka = new FSAutomate();
            dka.BuildDeltaDKAutomate(ndfsa);
            dka.DebugAuto();
            Console.WriteLine("Enter line to execute :");
            dka.Execute(Console.ReadLine());



        }
    }
}


 
