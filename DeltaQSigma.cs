namespace Thompson
{
  /// Delta: Q x Sigma -> Q
  public  class DeltaQSigma {
    public Symbol LHSQ { set; get; } = null; ///< Q
    public Symbol LHSS { set; get; } = null; ///< Sigma
    public List<Symbol> RHSQ { set; get; } = null; ///< Q
    public DeltaQSigma(Symbol LHSQ,Symbol LHSS,List<Symbol> RHSQ) {
      this.LHSQ=LHSQ;
      this.LHSS=LHSS;
      this.RHSQ=RHSQ;
    }

    
  } 
}
