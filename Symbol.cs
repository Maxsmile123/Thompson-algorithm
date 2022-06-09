namespace Thompson
{
  public class Symbol { 
      public string symbol; ///< Строковое значение/имя символа
      public List<Symbol> attr { set; get;} = null; ///< Множество атрибутов символа


      public int production { set; get;} = 0; 
      public int symbolPosition { set; get; } = 0;

      public Symbol() {}
      public Symbol(string s, int production, int symbolPosition)
      {
            this.symbol = s;
            this.production = production;
            this.symbolPosition = symbolPosition;
      }
      public Symbol(string s, List<Symbol> attr)
      {
          this.symbol = s;
          attr = new List<Symbol>(attr);
          this.production = 0;
          this.symbolPosition = 0;
      }

      public Symbol(string value) 
      {
          this.symbol = value;
          this.attr = null;
          this.production = 0;
          this.symbolPosition = 0;
      }

      /// Неявное преобразование строки в Symbol
      public static implicit operator Symbol(string str) => new Symbol(str);
      public override bool Equals(object other)
      {
          return (other is Symbol) && (this.symbol == ((Symbol)other).symbol);
      }


      public override string ToString() => this != Epsilon ? this.symbol : "e";
      public static readonly Symbol Epsilon = new Symbol(""); ///< Пустой символ
      public static readonly Symbol Sentinel = new Symbol("$"); ///< Cимвол конца строки / Символ дна стека
  }
}
