
// ==================================================================
// ProcessingRuleRequestPerformers.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Client
{
  public class ProcessingRuleRequestPerformers :
    global::GD.DCSGovernment.Client.ProcessingRuleDocBasePerformers, global::GD.DCSGovernmentModule.IProcessingRuleRequestPerformers
  {
    #region Fields and properties

    public static new readonly global::System.Guid ClassTypeGuid = global::System.Guid.Parse("3f8c9aa0-6e4f-4c0d-985c-9a0532c4d278");

    public override global::System.Guid TypeGuid
    {
      get { return global::GD.DCSGovernmentModule.Client.ProcessingRuleRequestPerformers.ClassTypeGuid; }
    }

    public override string TypeName
    {
      get { return "GD.DCSGovernmentModule.IProcessingRuleRequestPerformers, Sungero.Domain.Interfaces"; }
    }

    public new global::GD.DCSGovernmentModule.IProcessingRuleRequestPerformersState State
    {
      get
      {
        return (global::GD.DCSGovernmentModule.IProcessingRuleRequestPerformersState)base.State;
      }
    }

    protected override global::Sungero.Domain.Shared.IEntityState CreateState()
    {
      return new global::GD.DCSGovernmentModule.Shared.ProcessingRuleRequestPerformersState(this);
    }

    public new global::GD.DCSGovernmentModule.IProcessingRuleRequestPerformersInfo Info
    {
      get
      {
        return (global::GD.DCSGovernmentModule.IProcessingRuleRequestPerformersInfo)base.Info;
      }
    }










    #endregion

    #region Methods

    #endregion

    #region Framework events





    #endregion

    #region Constructors




    public ProcessingRuleRequestPerformers()
    {








    }

    #endregion

  }
}
