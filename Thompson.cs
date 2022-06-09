namespace Thompson
{

    public abstract class Automate
    {
        public List<Symbol> Q = null; ///< множество состояний
        public List<Symbol> Sigma = null; ///< множество алфавит
        public dynamic Delta = null;  ///< множество правил перехода
        public Symbol Q0 = null; ///< начальное состояние
        public List<Symbol> F = null; ///< множество конечных состояний
        private List<Symbol> config = new List<Symbol>();
        private List<DeltaQSigma> DeltaD = new List<DeltaQSigma>(); ///< правила детерминированного автомата

        public Automate() { }

        public Automate(List<Symbol> Q, List<Symbol> Sigma, List<Symbol> F, Symbol q0)
        {
            this.Q = Q;
            this.Sigma = Sigma;
            this.Q0 = q0;
            this.F = F;
            this.Delta = new List<DeltaQSigma>();
        }

        public void AddRule(string state, string term, string nextState) { this.Delta.Add(new DeltaQSigma(state, term, new List<Symbol> { new Symbol(nextState) })); }

     

        private List<Symbol> EpsClosure(List<Symbol> currStates)
        {
            return EpsClosure(currStates, null);
        }

        private List<Symbol> EpsClosure(List<Symbol> currStates, List<Symbol> ReachableStates)
        {
            if (ReachableStates == null)
                ReachableStates = new List<Symbol>();
            List<Symbol> nextStates = null;
            var next = new List<Symbol>();
            int count = currStates.Count;
            for (int i = 0; i < count; i++)
            {

                nextStates = FromStateToStates(currStates[i].ToString(), "");

                if (!ReachableStates.Contains(currStates[i]))
                {
                    ReachableStates.Add(new Symbol(currStates[i].ToString()));

                }
                if (nextStates != null)
                {

                    foreach (var nxt in nextStates)
                    {
                        ReachableStates.Add(nxt);
                        next.Add(nxt);
                    }

                }
            }

            if (nextStates == null)
                return ReachableStates;
            else
                return EpsClosure(next, ReachableStates);
        }

        private List<Symbol> move(List<Symbol> currStates, string term)
        {
            var ReachableStates = new List<Symbol>();
            var nextStates = new List<Symbol>();
            foreach (var s in currStates)
            {
                nextStates = FromStateToStates(s.symbol, term);
                if (nextStates != null)
                    foreach (var st in nextStates)
                        if (!ReachableStates.Contains(st))
                            ReachableStates.Add(st);
            }
            return ReachableStates;
        }

        private List<Symbol> FromStateToStates(string currState, string term)
        {
            var NextStates = new List<Symbol>(); 
            bool flag = false;
            foreach (var d in Delta)
            {
                if (d.LHSQ == currState && d.LHSS == term)
                {
                    NextStates.Add(new Symbol(d.RHSQ[0].ToString()));
                    flag = true;
                }
            }
            if (flag) return NextStates;
            return null;
        }

        private void BuildWithQueue(Symbol Q0)
        {
            List<List<Symbol>> queue = new List<List<Symbol>>(); // Имитируем очередь, ибо нельзя сделать queue<list<symbol>>
            List<Symbol> curStates = null;
            List<Symbol> newStates = null;
            bool is_start = true;
            queue.Add(new List<Symbol> { Q0 });
            while (queue.Count != 0)
            {
                curStates = EpsClosure(queue[0]);
                queue.RemoveAt(0);
                foreach (var a in Sigma)
                {
                    newStates = move(curStates, a.symbol);
                    if (!config.Contains(SetName(EpsClosure(newStates))))
                    {
                        if (is_start)
                        {
                            config.Add(SetName(curStates));
                            is_start = false;
                        }
                        queue.Add(newStates);
                        config.Add(SetName(EpsClosure(newStates)));
                    }
                    if (newStates.Count != 0)
                    {
                        if (SetName(curStates) != SetName(EpsClosure(newStates)))
                        {
                            var delta = new DeltaQSigma(SetName(curStates), a.symbol, new List<Symbol> { SetName(EpsClosure(newStates)) });
                            DeltaD.Add(delta);
                            
                        }

                    }
                }
            }
        }


        public void BuildDeltaDKAutomate(FSAutomate ndka)
        {
            this.Sigma = ndka.Sigma;
            this.Delta = ndka.Delta;
            BuildWithQueue(ndka.Q0);
            this.Q = config;
            this.Q0 = this.Q[0].ToString();
            this.Delta = DeltaD;
            this.F = getF(config, ndka.F);
        }

        private List<Symbol> getF(List<Symbol> config, List<Symbol> F)
        {
            var F_ = new List<Symbol>();
            foreach (var f in F)
            {
                foreach (var name in this.config)
                {
                    if (name.symbol != null && name.symbol.Contains(f.symbol))
                    {

                        F_.Add(name);
                    }
                }
            }
            return F_;
        }

        private string SetName(List<Symbol> list)
        {
            string line = null;
            if (list == null)
            {
                return "";
            }
            foreach (var sym in list)
                line += sym.symbol;
        
            return line;
           
        }


        public void Debug(string step, string line)
        {
            Console.Write(step + ": ");
            Console.WriteLine(line);
        }

        public void Debug(string step, List<Symbol> list)
        {
            Console.Write(step + ": ");
            if (list == null)
            {
                Console.WriteLine("null");
                return;
            }
            for (int i = 0; i < list.Count; i++)
                if (list[i] != null)
                    Console.Write(list[i].ToString() + " ");
            Console.Write("\n");
        }

        public void Debug(List<Symbol> list)
        {
            Console.Write("{ ");
            if (list == null)
            {
                Console.WriteLine("null");
                return;
            }
            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i].ToString() + " ");
            Console.Write(" }\n");
        }

        public void DebugAuto()
        {
            Console.WriteLine("\nAutomate definition:");
            Debug("Q", this.Q);
            Debug("Sigma", this.Sigma);
            Debug("Q0", this.Q0.symbol);
            Debug("F", this.F);
            Console.WriteLine("DeltaList:");
            foreach (var d in this.Delta) 
                d.Debug();
        }
    } 

}

