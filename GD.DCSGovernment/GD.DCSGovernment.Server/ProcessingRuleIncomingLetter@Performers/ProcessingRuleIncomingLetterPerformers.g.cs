
// ==================================================================
// ProcessingRuleIncomingLetterPerformers.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Server
{


  public class ProcessingRuleIncomingLetterPerformers :
    global::DirRX.DCTSIntegration.Server.ProcessingRuleIncomingLetterPerformers, global::GD.DCSGovernment.IProcessingRuleIncomingLetterPerformers
  {
    public static new readonly global::System.Guid ClassTypeGuid = global::System.Guid.Parse("46c35676-cf07-4eaa-b0ed-eb5f263ecc1f");

    public override global::System.Guid TypeGuid
    {
      get { return global::GD.DCSGovernment.Server.ProcessingRuleIncomingLetterPerformers.ClassTypeGuid; }
    }

    public override string TypeName
    {
      get { return "GD.DCSGovernment.IProcessingRuleIncomingLetterPerformers, Sungero.Domain.Interfaces"; }
    }

    public new virtual global::GD.DCSGovernment.IProcessingRuleIncomingLetterPerformersState State
    {
      get { return (global::GD.DCSGovernment.IProcessingRuleIncomingLetterPerformersState)base.State; }
    }

    protected override global::Sungero.Domain.Shared.IEntityState CreateState()
    {
      return new global::GD.DCSGovernment.Shared.ProcessingRuleIncomingLetterPerformersState(this);
    }

    public new virtual global::GD.DCSGovernment.IProcessingRuleIncomingLetterPerformersInfo Info
    {
      get { return (global::GD.DCSGovernment.IProcessingRuleIncomingLetterPerformersInfo)base.Info; }
    }










    #region Framework events



    #endregion


    public ProcessingRuleIncomingLetterPerformers()
    {
    }

  }
}

// ==================================================================
// ProcessingRuleIncomingLetterPerformersHandlers.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment
{

}

// ==================================================================
// ProcessingRuleIncomingLetterPerformersEventArgs.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Server
{
}