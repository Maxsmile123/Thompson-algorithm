
namespace Thompson
{

    /// Finite State automata (КА)
    public class FSAutomate : Automate
    {
        public FSAutomate(List<Symbol> Q, List<Symbol> Sigma, List<Symbol> F, string q0) : base(Q, Sigma, F, q0) { }
        public FSAutomate() : base() { }
        public void Execute(string chineSymbol)
        {
            var currState = this.Q0;
            int flag = 0;
            int i = 0;
            for (; i < chineSymbol.Length; i++)
            {
                flag = 0;
                foreach (var d in this.Delta)
                {
                    if (d.LHSQ == currState && d.LHSS == chineSymbol.Substring(i, 1))
                    {
                        currState = d.RHSQ[0].symbol; // Для детерминированного К автомата
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0) break;
            } 

            Console.WriteLine("Length: " + chineSymbol.Length);
            Console.WriteLine(" i :" + i.ToString());

            if (this.F.Contains(currState) && i == chineSymbol.Length)
                Console.WriteLine("chineSymbol belongs to language");
            else
                Console.WriteLine("chineSymbol doesn't belong to language");
        }

        public bool Execute_FSA(string chineSymbol)
        {  
            var currState = this.Q0;
            int flag = 0;
            int i = 0;
            for (; i < chineSymbol.Length; i++)
            {
                flag = 0;
                foreach (var d in this.Delta)
                { 
                    if (d.LHSQ == currState && d.LHSS == chineSymbol.Substring(i, 1))
                    {
                        currState = d.RHSQ[0]; // Для детерминированного КA
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0) break;
            } 

           
            return (this.F.Contains(currState) && i == chineSymbol.Length);
        } 

    } 

}