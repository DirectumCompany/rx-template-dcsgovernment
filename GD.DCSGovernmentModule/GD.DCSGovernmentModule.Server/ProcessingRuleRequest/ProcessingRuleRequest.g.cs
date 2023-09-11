
// ==================================================================
// ProcessingRuleRequest.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Server
{
    public class ProcessingRuleRequestFilter<T> :
      global::GD.DCSGovernment.Server.ProcessingRuleDocBaseFilter<T>
      where T : class, global::GD.DCSGovernmentModule.IProcessingRuleRequest
    {
      private global::GD.DCSGovernmentModule.IProcessingRuleRequestFilterState filter
      {
        get
        {
          return (GD.DCSGovernmentModule.IProcessingRuleRequestFilterState)this.Filter;
        }
      }

      protected override global::System.Linq.IQueryable<T> ApplyAppliedFilter(global::System.Linq.IQueryable<T> query)
      {
        return base.ApplyAppliedFilter(query);
      }

      public ProcessingRuleRequestFilter(global::GD.DCSGovernmentModule.IProcessingRuleRequestFilterState filter)
      : base(filter)
      {
      }

      protected ProcessingRuleRequestFilter()
      {
      }
    }
      public class ProcessingRuleRequestUiFilter<T> :
        global::GD.DCSGovernment.Server.ProcessingRuleDocBaseUiFilter<T>
        where T : class, global::GD.DCSGovernmentModule.IProcessingRuleRequest
      {
        protected override global::System.Linq.IQueryable<T> ApplyAppliedFilter(global::System.Linq.IQueryable<T> query)
        {
          return base.ApplyAppliedFilter(query);
        }
      }

    public class ProcessingRuleRequestSearchDialogModel : global::GD.DCSGovernment.Server.ProcessingRuleDocBaseSearchDialogModel
        {
                  public override global::System.Int64? Id { get; protected set; }
                  public override global::System.String Name { get; protected set; }
                  public override global::System.String Note { get; protected set; }
                  public override global::System.String Line { get; protected set; }
                  public override global::System.String NamePattern { get; protected set; }
                  public override global::System.Int32? DeadlineInDays { get; protected set; }
                  public override global::System.Int32? DeadlineInHours { get; protected set; }
                  public override global::System.String TaskSubject { get; protected set; }


                  public override global::System.Collections.Generic.IEnumerable<Sungero.Core.Enumeration> Status { get; protected set; }
                  public override global::System.Collections.Generic.IEnumerable<Sungero.Docflow.IApprovalRuleBase> ApprovalRule { get; protected set; }
                  public override global::System.Collections.Generic.IEnumerable<Sungero.Core.Enumeration> TaskType { get; protected set; }
                  public override global::System.Collections.Generic.IEnumerable<Sungero.Core.Enumeration> CaptureService { get; protected set; }
                  public override global::System.Collections.Generic.IEnumerable<Sungero.Docflow.IDocumentKind> DocumentKind { get; protected set; }


                  public virtual global::System.String SubjectPattern { get; protected set; }
                  public virtual global::System.Boolean? FillFromSubject { get; protected set; }


                  public virtual global::System.Collections.Generic.IEnumerable<Sungero.Parties.IPerson> Correspondent { get; protected set; }
                  public virtual global::System.Collections.Generic.IEnumerable<Sungero.Company.IDepartment> Department { get; protected set; }


                   [Sungero.Domain.Shared.HideInDevStudio()]
                   public new ProcessingRuleRequestPerformersModel Performers { get { return (ProcessingRuleRequestPerformersModel)base.Performers; } protected set { base.Performers = value; } }

        }

      public class ProcessingRuleRequestPerformersModel : global::GD.DCSGovernment.Server.ProcessingRuleDocBasePerformersModel
          {
                      [Sungero.Domain.Shared.HideInDevStudio()]
                      public override global::System.Int64? Id { get; protected set; }




         }




  public class ProcessingRuleRequestFilterForCorrespondent<TQueryEntity, TSourceEntity>
    : global::Sungero.Domain.EntityPropertyFilterBase<TQueryEntity, TSourceEntity>
    where TQueryEntity : class, global::Sungero.Parties.IPerson
    where TSourceEntity : class, global::GD.DCSGovernmentModule.IProcessingRuleRequest
  {
    protected override global::System.Linq.IQueryable<TQueryEntity> ApplyAppliedFilter(global::System.Linq.IQueryable<TQueryEntity> query, TSourceEntity sourceEntity)
    {
      var args = new global::Sungero.Domain.PropertyFilteringEventArgs(sourceEntity);
      global::System.Linq.IQueryable<TQueryEntity> result;
      var filterType = global::Sungero.Metadata.Services.AppliedTypesManager.Instance.Resolve("GD.DCSGovernmentModule.ProcessingRuleRequestCorrespondentPropertyFilteringServerHandler`1");
      if (filterType != null)
      {
        var genericType = filterType.MakeGenericType(typeof(TQueryEntity));
        var instance = global::Sungero.Metadata.Services.AppliedTypesManager.CreateTypeInstance(genericType, new object[] { sourceEntity });
        var methodInfo = genericType.GetMethod("CorrespondentFiltering");
        result = (global::System.Linq.IQueryable<TQueryEntity>)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(methodInfo, instance, new object[] { query, args });
      }
      else
      {
        result = new global::GD.DCSGovernmentModule.ProcessingRuleRequestCorrespondentPropertyFilteringServerHandler<TQueryEntity>(sourceEntity).CorrespondentFiltering(query, args);
      }
      if (args.DisableUiFiltering)
        global::Sungero.Domain.UiFilteringEventManagementScope.DisableEvent<TQueryEntity>();
      return result;
    }

    public ProcessingRuleRequestFilterForCorrespondent(string propertyName)
      : base(propertyName)
    {
    }
  }

  public class ProcessingRuleRequestSearchFilterForCorrespondent<TQueryEntity>
    : global::Sungero.Domain.SearchDialogPropertyFilter<TQueryEntity>
    where TQueryEntity : class, global::Sungero.Parties.IPerson
  {
    protected override global::System.Linq.IQueryable<TQueryEntity> ApplyAppliedFilter(global::System.Linq.IQueryable<TQueryEntity> query, System.Guid entityType)
    {
      var args = new global::Sungero.Domain.PropertySearchDialogFilteringEventArgs(entityType);
      global::System.Linq.IQueryable<TQueryEntity> result;
      var filterType = global::Sungero.Metadata.Services.AppliedTypesManager.Instance.Resolve("GD.DCSGovernmentModule.ProcessingRuleRequestCorrespondentSearchPropertyFilteringServerHandler`1");
      if (filterType != null)
      {
        var genericType = filterType.MakeGenericType(typeof(TQueryEntity));
        var instance = global::Sungero.Metadata.Services.AppliedTypesManager.CreateTypeInstance(genericType);
        var methodInfo = genericType.GetMethod("CorrespondentSearchDialogFiltering");
        result = (global::System.Linq.IQueryable<TQueryEntity>)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(methodInfo, instance, new object[] { query, args });
      }
      else
      {
        result = new global::GD.DCSGovernmentModule.ProcessingRuleRequestCorrespondentSearchPropertyFilteringServerHandler<TQueryEntity>().CorrespondentSearchDialogFiltering(query, args);
      }
      if (args.DisableUiFiltering)
          global::Sungero.Domain.UiFilteringEventManagementScope.DisableEvent<TQueryEntity>();
      return result;
    }

    public ProcessingRuleRequestSearchFilterForCorrespondent(string propertyName)
      : base(propertyName)
    {
    }
  }

  public class ProcessingRuleRequestFilterForDepartment<TQueryEntity, TSourceEntity>
    : global::Sungero.Domain.EntityPropertyFilterBase<TQueryEntity, TSourceEntity>
    where TQueryEntity : class, global::Sungero.Company.IDepartment
    where TSourceEntity : class, global::GD.DCSGovernmentModule.IProcessingRuleRequest
  {
    protected override global::System.Linq.IQueryable<TQueryEntity> ApplyAppliedFilter(global::System.Linq.IQueryable<TQueryEntity> query, TSourceEntity sourceEntity)
    {
      var args = new global::Sungero.Domain.PropertyFilteringEventArgs(sourceEntity);
      global::System.Linq.IQueryable<TQueryEntity> result;
      var filterType = global::Sungero.Metadata.Services.AppliedTypesManager.Instance.Resolve("GD.DCSGovernmentModule.ProcessingRuleRequestDepartmentPropertyFilteringServerHandler`1");
      if (filterType != null)
      {
        var genericType = filterType.MakeGenericType(typeof(TQueryEntity));
        var instance = global::Sungero.Metadata.Services.AppliedTypesManager.CreateTypeInstance(genericType, new object[] { sourceEntity });
        var methodInfo = genericType.GetMethod("DepartmentFiltering");
        result = (global::System.Linq.IQueryable<TQueryEntity>)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(methodInfo, instance, new object[] { query, args });
      }
      else
      {
        result = new global::GD.DCSGovernmentModule.ProcessingRuleRequestDepartmentPropertyFilteringServerHandler<TQueryEntity>(sourceEntity).DepartmentFiltering(query, args);
      }
      if (args.DisableUiFiltering)
        global::Sungero.Domain.UiFilteringEventManagementScope.DisableEvent<TQueryEntity>();
      return result;
    }

    public ProcessingRuleRequestFilterForDepartment(string propertyName)
      : base(propertyName)
    {
    }
  }

  public class ProcessingRuleRequestSearchFilterForDepartment<TQueryEntity>
    : global::Sungero.Domain.SearchDialogPropertyFilter<TQueryEntity>
    where TQueryEntity : class, global::Sungero.Company.IDepartment
  {
    protected override global::System.Linq.IQueryable<TQueryEntity> ApplyAppliedFilter(global::System.Linq.IQueryable<TQueryEntity> query, System.Guid entityType)
    {
      var args = new global::Sungero.Domain.PropertySearchDialogFilteringEventArgs(entityType);
      global::System.Linq.IQueryable<TQueryEntity> result;
      var filterType = global::Sungero.Metadata.Services.AppliedTypesManager.Instance.Resolve("GD.DCSGovernmentModule.ProcessingRuleRequestDepartmentSearchPropertyFilteringServerHandler`1");
      if (filterType != null)
      {
        var genericType = filterType.MakeGenericType(typeof(TQueryEntity));
        var instance = global::Sungero.Metadata.Services.AppliedTypesManager.CreateTypeInstance(genericType);
        var methodInfo = genericType.GetMethod("DepartmentSearchDialogFiltering");
        result = (global::System.Linq.IQueryable<TQueryEntity>)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(methodInfo, instance, new object[] { query, args });
      }
      else
      {
        result = new global::GD.DCSGovernmentModule.ProcessingRuleRequestDepartmentSearchPropertyFilteringServerHandler<TQueryEntity>().DepartmentSearchDialogFiltering(query, args);
      }
      if (args.DisableUiFiltering)
          global::Sungero.Domain.UiFilteringEventManagementScope.DisableEvent<TQueryEntity>();
      return result;
    }

    public ProcessingRuleRequestSearchFilterForDepartment(string propertyName)
      : base(propertyName)
    {
    }
  }



  [global::Sungero.Domain.Filter(typeof(global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestFilter<global::GD.DCSGovernmentModule.IProcessingRuleRequest>))]
  [global::Sungero.Domain.UiFilter(typeof(global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestUiFilter<global::GD.DCSGovernmentModule.IProcessingRuleRequest>))]
  [global::Sungero.Domain.PropertyFilter(typeof(global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestFilterForCorrespondent<global::Sungero.Parties.IPerson, global::GD.DCSGovernmentModule.IProcessingRuleRequest>), "Correspondent")]
  [global::Sungero.Domain.SearchPropertyFilter(typeof(global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestSearchFilterForCorrespondent<global::Sungero.Parties.IPerson>), "Correspondent")]
  [global::Sungero.Domain.PropertyFilter(typeof(global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestFilterForDepartment<global::Sungero.Company.IDepartment, global::GD.DCSGovernmentModule.IProcessingRuleRequest>), "Department")]
  [global::Sungero.Domain.SearchPropertyFilter(typeof(global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestSearchFilterForDepartment<global::Sungero.Company.IDepartment>), "Department")]


  public class ProcessingRuleRequest :
    global::GD.DCSGovernment.Server.ProcessingRuleDocBase, global::GD.DCSGovernmentModule.IProcessingRuleRequest
  {
    public static new readonly global::System.Guid ClassTypeGuid = global::System.Guid.Parse("80051758-3f3f-4fcc-a772-f10c3c8e5bfd");

    public override global::System.Guid TypeGuid
    {
      get { return global::GD.DCSGovernmentModule.Server.ProcessingRuleRequest.ClassTypeGuid; }
    }

    public override string TypeName
    {
      get { return "GD.DCSGovernmentModule.IProcessingRuleRequest, Sungero.Domain.Interfaces"; }
    }

    public override string DisplayValue
    {
      get { return this.Name; }
      set { this.Name = value; }
    }

    public new virtual global::GD.DCSGovernmentModule.IProcessingRuleRequestState State
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequestState)base.State; }
    }

    protected override global::Sungero.Domain.Shared.IEntityState CreateState()
    {
      return new global::GD.DCSGovernmentModule.Shared.ProcessingRuleRequestState(this);
    }

    public new virtual global::GD.DCSGovernmentModule.IProcessingRuleRequestInfo Info
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequestInfo)base.Info; }
    }

    public new virtual global::GD.DCSGovernmentModule.IProcessingRuleRequestAccessRights AccessRights
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequestAccessRights)base.AccessRights; }
    }

    protected override global::Sungero.Domain.Shared.IEntityAccessRights CreateAccessRights()
    {
      return new global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestAccessRights(this);
    }

    protected override global::Sungero.Domain.EntityFunctions CreateServerFunctions()
    {
      return new global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestFunctions(this);
    }

    protected override global::Sungero.Domain.Shared.EntityFunctions CreateSharedFunctions()
    {
      return new global::GD.DCSGovernmentModule.Shared.ProcessingRuleRequestFunctions(this);
    }

    protected override object CreateHandlers() {
      return new global::GD.DCSGovernmentModule.ProcessingRuleRequestServerHandlers(this);
    }

    protected override object CreateSharedHandlers() {
      return new global::GD.DCSGovernmentModule.ProcessingRuleRequestSharedHandlers(this);
    }

    private global::System.String _SubjectPattern;
    public virtual global::System.String SubjectPattern
    {
      get
      {
        return this._SubjectPattern;
      }

      set
      {
        this.SetPropertyValue("SubjectPattern", this._SubjectPattern, value, (propertyValue) => { this._SubjectPattern = propertyValue; }, this.SubjectPatternChangedHandler);
      }
    }
    private global::System.Boolean? _FillFromSubject;
    public virtual global::System.Boolean? FillFromSubject
    {
      get
      {
        return this._FillFromSubject;
      }

      set
      {
        this.SetPropertyValue("FillFromSubject", this._FillFromSubject, value, (propertyValue) => { this._FillFromSubject = propertyValue; }, this.FillFromSubjectChangedHandler);
      }
    }
    private global::System.Boolean? _IsAutoCalcCorrespondent;
    public virtual global::System.Boolean? IsAutoCalcCorrespondent
    {
      get
      {
        return this._IsAutoCalcCorrespondent;
      }

      set
      {
        this.SetPropertyValue("IsAutoCalcCorrespondent", this._IsAutoCalcCorrespondent, value, (propertyValue) => { this._IsAutoCalcCorrespondent = propertyValue; }, this.IsAutoCalcCorrespondentChangedHandler);
      }
    }
    private global::System.Boolean? _FillFromFileName;
    public virtual global::System.Boolean? FillFromFileName
    {
      get
      {
        return this._FillFromFileName;
      }

      set
      {
        this.SetPropertyValue("FillFromFileName", this._FillFromFileName, value, (propertyValue) => { this._FillFromFileName = propertyValue; }, this.FillFromFileNameChangedHandler);
      }
    }







    private global::Sungero.Parties.IPerson _Correspondent;
    public virtual global::Sungero.Parties.IPerson Correspondent
    {
      get
      {
        return this._Correspondent;
      }

      set
      {
        this.SetPropertyValue("Correspondent", this._Correspondent, value, (propertyValue) => { this._Correspondent = propertyValue; }, this.CorrespondentChangedHandler);
      }
    }
    private global::Sungero.Company.IDepartment _Department;
    public virtual global::Sungero.Company.IDepartment Department
    {
      get
      {
        return this._Department;
      }

      set
      {
        this.SetPropertyValue("Department", this._Department, value, (propertyValue) => { this._Department = propertyValue; }, this.DepartmentChangedHandler);
      }
    }



    protected override global::Sungero.Domain.Shared.IChildEntityCollection<global::DirRX.DCTSIntegration.IProcessingRuleBasePerformers> CreatePerformersCollection()
    {
      return new global::Sungero.Domain.ChildEntityCollection<global::GD.DCSGovernmentModule.IProcessingRuleRequestPerformers>() { RootEntity = this };
    }


    protected override global::Sungero.Domain.Shared.EntityCreatingFromServerHandler CreateCreatingFromServerHandler(
      global::Sungero.Domain.Shared.IEntity entitySource)
    {
      var instance = Sungero.Metadata.Services.AppliedTypesManager.CreateInstance("GD.DCSGovernmentModule.ProcessingRuleRequestCreatingFromServerHandler", new object[] { (global::GD.DCSGovernmentModule.IProcessingRuleRequest)entitySource, this.Info });
      if (instance != null)
        return (global::Sungero.Domain.Shared.EntityCreatingFromServerHandler)instance;

      return new global::GD.DCSGovernmentModule.ProcessingRuleRequestCreatingFromServerHandler((global::GD.DCSGovernmentModule.IProcessingRuleRequest)entitySource, this.Info);
    }

    #region Framework events

    protected void SubjectPatternChangedHandler()
    {
      var args = new global::Sungero.Domain.Shared.StringPropertyChangedEventArgs(this.State.Properties.SubjectPattern, this.SubjectPattern, this);
     ((global::GD.DCSGovernmentModule.IProcessingRuleRequestSharedHandlers)this.SharedHandlers).SubjectPatternChanged(args);
    }

    protected void FillFromSubjectChangedHandler()
    {
      var args = new global::Sungero.Domain.Shared.BooleanPropertyChangedEventArgs(this.State.Properties.FillFromSubject, this.FillFromSubject, this);
     ((global::GD.DCSGovernmentModule.IProcessingRuleRequestSharedHandlers)this.SharedHandlers).FillFromSubjectChanged(args);
    }

    protected void CorrespondentChangedHandler()
    {
      var args = new global::GD.DCSGovernmentModule.Shared.ProcessingRuleRequestCorrespondentChangedEventArgs(this.State.Properties.Correspondent, this.Correspondent, this);
     ((global::GD.DCSGovernmentModule.IProcessingRuleRequestSharedHandlers)this.SharedHandlers).CorrespondentChanged(args);
    }

    protected void IsAutoCalcCorrespondentChangedHandler()
    {
      var args = new global::Sungero.Domain.Shared.BooleanPropertyChangedEventArgs(this.State.Properties.IsAutoCalcCorrespondent, this.IsAutoCalcCorrespondent, this);
     ((global::GD.DCSGovernmentModule.IProcessingRuleRequestSharedHandlers)this.SharedHandlers).IsAutoCalcCorrespondentChanged(args);
    }

    protected void DepartmentChangedHandler()
    {
      var args = new global::GD.DCSGovernmentModule.Shared.ProcessingRuleRequestDepartmentChangedEventArgs(this.State.Properties.Department, this.Department, this);
     ((global::GD.DCSGovernmentModule.IProcessingRuleRequestSharedHandlers)this.SharedHandlers).DepartmentChanged(args);
    }

    protected void FillFromFileNameChangedHandler()
    {
      var args = new global::Sungero.Domain.Shared.BooleanPropertyChangedEventArgs(this.State.Properties.FillFromFileName, this.FillFromFileName, this);
     ((global::GD.DCSGovernmentModule.IProcessingRuleRequestSharedHandlers)this.SharedHandlers).FillFromFileNameChanged(args);
    }




    #endregion


    public ProcessingRuleRequest()
    {
    }

  }
}

// ==================================================================
// ProcessingRuleRequestHandlers.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule
{
  public partial class ProcessingRuleRequestCorrespondentPropertyFilteringServerHandler<T>
    : global::Sungero.Domain.EntityPropertyFilteringServerHandler
    where T : class, global::Sungero.Parties.IPerson
  {
    private global::GD.DCSGovernmentModule.IProcessingRuleRequest _obj
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequest)this.Entity; }
    }

    public virtual global::System.Linq.IQueryable<T> CorrespondentFiltering(global::System.Linq.IQueryable<T> query, global::Sungero.Domain.PropertyFilteringEventArgs e)
    {
      return query;
    }

    public ProcessingRuleRequestCorrespondentPropertyFilteringServerHandler(global::GD.DCSGovernmentModule.IProcessingRuleRequest entity)
      : base(entity)
    {
    }
  }

  public partial class ProcessingRuleRequestCorrespondentSearchPropertyFilteringServerHandler<T>
    : global::Sungero.Domain.SearchPropertyFilteringServerHandler
    where T : class, global::Sungero.Parties.IPerson
  {

    public virtual global::System.Linq.IQueryable<T> CorrespondentSearchDialogFiltering(global::System.Linq.IQueryable<T> query, global::Sungero.Domain.PropertySearchDialogFilteringEventArgs e)
    {
      return query;
    }

    public ProcessingRuleRequestCorrespondentSearchPropertyFilteringServerHandler()
      : base()
    {
    }
  }

  public partial class ProcessingRuleRequestDepartmentPropertyFilteringServerHandler<T>
    : global::Sungero.Domain.EntityPropertyFilteringServerHandler
    where T : class, global::Sungero.Company.IDepartment
  {
    private global::GD.DCSGovernmentModule.IProcessingRuleRequest _obj
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequest)this.Entity; }
    }

    public virtual global::System.Linq.IQueryable<T> DepartmentFiltering(global::System.Linq.IQueryable<T> query, global::Sungero.Domain.PropertyFilteringEventArgs e)
    {
      return query;
    }

    public ProcessingRuleRequestDepartmentPropertyFilteringServerHandler(global::GD.DCSGovernmentModule.IProcessingRuleRequest entity)
      : base(entity)
    {
    }
  }

  public partial class ProcessingRuleRequestDepartmentSearchPropertyFilteringServerHandler<T>
    : global::Sungero.Domain.SearchPropertyFilteringServerHandler
    where T : class, global::Sungero.Company.IDepartment
  {

    public virtual global::System.Linq.IQueryable<T> DepartmentSearchDialogFiltering(global::System.Linq.IQueryable<T> query, global::Sungero.Domain.PropertySearchDialogFilteringEventArgs e)
    {
      return query;
    }

    public ProcessingRuleRequestDepartmentSearchPropertyFilteringServerHandler()
      : base()
    {
    }
  }



  public partial class ProcessingRuleRequestFilteringServerHandler<T>
    : global::GD.DCSGovernment.ProcessingRuleDocBaseFilteringServerHandler<T>  
    where T : class, global::GD.DCSGovernmentModule.IProcessingRuleRequest
  {
    private global::GD.DCSGovernmentModule.IProcessingRuleRequestFilterState _filter
    {
      get
      {
        return (GD.DCSGovernmentModule.IProcessingRuleRequestFilterState)this.Filter;
      }
    }

    public ProcessingRuleRequestFilteringServerHandler(global::GD.DCSGovernmentModule.IProcessingRuleRequestFilterState filter)
    : base(filter)
    {
    }

    protected ProcessingRuleRequestFilteringServerHandler()
    {
    }

    public override global::System.Linq.IQueryable<T> Filtering(global::System.Linq.IQueryable<T> query, global::Sungero.Domain.FilteringEventArgs e)
    {
      query = base.Filtering(query, e);
            return query;
    }


  }

  public partial class ProcessingRuleRequestUiFilteringServerHandler<T>
    : global::GD.DCSGovernment.ProcessingRuleDocBaseUiFilteringServerHandler<T>
    where T : class, global::GD.DCSGovernmentModule.IProcessingRuleRequest
  {
    public override global::System.Linq.IQueryable<T> Filtering(global::System.Linq.IQueryable<T> query, global::Sungero.Domain.UiFilteringEventArgs e)
    {
      query = base.Filtering(query, e);
            return query;
    }
  }

  public partial class ProcessingRuleRequestSearchDialogServerHandler : global::GD.DCSGovernment.ProcessingRuleDocBaseSearchDialogServerHandler
   {
     private global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestSearchDialogModel _dialog
     {
       get
       {
         return (global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestSearchDialogModel)this.Dialog;
       }
     }

     public ProcessingRuleRequestSearchDialogServerHandler(global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestSearchDialogModel dialog)
       : base(dialog)
     {
     }
   }

  public partial class ProcessingRuleRequestServerHandlers : global::GD.DCSGovernment.ProcessingRuleDocBaseServerHandlers
  {
    private global::GD.DCSGovernmentModule.IProcessingRuleRequest _obj
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequest)this.Entity; }
    }

    public ProcessingRuleRequestServerHandlers(global::GD.DCSGovernmentModule.IProcessingRuleRequest entity)
      : base(entity)
    {
    }
  }

  public partial class ProcessingRuleRequestCreatingFromServerHandler : global::GD.DCSGovernment.ProcessingRuleDocBaseCreatingFromServerHandler
  {
    private global::GD.DCSGovernmentModule.IProcessingRuleRequest _source
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequest)this.Source; }
    }

    private global::GD.DCSGovernmentModule.IProcessingRuleRequestInfo _info
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequestInfo)this._Info; }
    }

    public ProcessingRuleRequestCreatingFromServerHandler(global::GD.DCSGovernmentModule.IProcessingRuleRequest source, global::GD.DCSGovernmentModule.IProcessingRuleRequestInfo info)
      : base(source, info)
    {
    }
  }

}

// ==================================================================
// ProcessingRuleRequestEventArgs.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Server
{
}

// ==================================================================
// ProcessingRuleRequestAccessRights.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Server
{
  public class ProcessingRuleRequestAccessRights : 
    GD.DCSGovernment.Server.ProcessingRuleDocBaseAccessRights, GD.DCSGovernmentModule.IProcessingRuleRequestAccessRights
  {

    public ProcessingRuleRequestAccessRights(global::Sungero.Domain.Shared.IEntity entity) : base(entity)
    {
    }
  }

  public class ProcessingRuleRequestTypeAccessRights : 
    GD.DCSGovernment.Server.ProcessingRuleDocBaseTypeAccessRights, GD.DCSGovernmentModule.IProcessingRuleRequestAccessRights
  {

    public ProcessingRuleRequestTypeAccessRights(global::System.Type entityType) : base(entityType)
    {
    }
  }
}

// ==================================================================
// ProcessingRuleRequestRepositoryImplementer.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Server
{
    public class ProcessingRuleRequestRepositoryImplementer<T> : 
      global::GD.DCSGovernment.Server.ProcessingRuleDocBaseRepositoryImplementer<T>,
      global::GD.DCSGovernmentModule.IProcessingRuleRequestRepositoryImplementer<T>
      where T : global::GD.DCSGovernmentModule.IProcessingRuleRequest 
    {
       public new global::GD.DCSGovernmentModule.IProcessingRuleRequestAccessRights AccessRights
       {
          get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequestAccessRights)base.AccessRights; }
       }

       public new global::GD.DCSGovernmentModule.IProcessingRuleRequestInfo Info
       {
          get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequestInfo)base.Info; }
       }

       protected override global::Sungero.Domain.Shared.IEntityAccessRights CreateAccessRights()
       {
         return new global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestTypeAccessRights(typeof(T));
       }
    }
}

// ==================================================================
// ProcessingRuleRequestPanelNavigationFilters.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Server
{
}

// ==================================================================
// ProcessingRuleRequestServerFunctions.g.cs
// ==================================================================

namespace GD.DCSGovernmentModule.Server
{
  public partial class ProcessingRuleRequestFunctions : global::GD.DCSGovernment.Server.ProcessingRuleDocBaseFunctions
  {
    private global::GD.DCSGovernmentModule.IProcessingRuleRequest _obj
    {
      get { return (global::GD.DCSGovernmentModule.IProcessingRuleRequest)this.Entity; }
    }

    public ProcessingRuleRequestFunctions(global::GD.DCSGovernmentModule.IProcessingRuleRequest entity) : base(entity) { }
  }
}

// ==================================================================
// ProcessingRuleRequestFunctions.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Functions
{
  internal static class ProcessingRuleRequest
  {
    /// <redirect project="GD.DCSGovernmentModule.Server" type="GD.DCSGovernmentModule.Server.ProcessingRuleRequestFunctions" />
    internal static  global::Sungero.Docflow.IDocumentType GetDocumentType(global::GD.DCSGovernmentModule.IProcessingRuleRequest processingRuleRequest)
    {
      var __functions = ((global::Sungero.Domain.Shared.IEntityFunctions)processingRuleRequest).FunctionsContainer.ServerFunctions;
      var __funcMethod = __functions.GetType().GetMethod("GetDocumentType", new System.Type[] {  });
      return (global::Sungero.Docflow.IDocumentType)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(__funcMethod, __functions, new object[] {  });
    }
    /// <redirect project="GD.DCSGovernmentModule.Server" type="GD.DCSGovernmentModule.Server.ProcessingRuleRequestFunctions" />
    [global::Sungero.Core.RemoteAttribute()]
    internal static  global::GD.DCSGovernmentModule.IProcessingRuleRequest GetProcessingRule(global::System.Int64 id)
    {
      var __funcType = Sungero.Metadata.Services.AppliedTypesManager.Instance.Resolve("GD.DCSGovernmentModule.Server.ProcessingRuleRequestFunctions");
      if (__funcType != null)
      {    
        var __funcMethod = __funcType.GetMethod("GetProcessingRule",
          System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic,
          null, new System.Type[] { typeof(global::System.Int64) }, null);
        return (global::GD.DCSGovernmentModule.IProcessingRuleRequest)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(__funcMethod, null, new object[] { id });
      }
      else
      {
        return global::GD.DCSGovernmentModule.Server.ProcessingRuleRequestFunctions.GetProcessingRule(id);
      }
    }
    /// <redirect project="GD.DCSGovernmentModule.Server" type="GD.DCSGovernmentModule.Server.ProcessingRuleRequestFunctions" />
    [global::Sungero.Core.RemoteAttribute()]
    internal static  global::Sungero.Docflow.IOfficialDocument CreateDocument(global::GD.DCSGovernmentModule.IProcessingRuleRequest processingRuleRequest)
    {
      var __functions = ((global::Sungero.Domain.Shared.IEntityFunctions)processingRuleRequest).FunctionsContainer.ServerFunctions;
      var __funcMethod = __functions.GetType().GetMethod("CreateDocument", new System.Type[] {  });
      return (global::Sungero.Docflow.IOfficialDocument)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(__funcMethod, __functions, new object[] {  });
    }

    /// <redirect project="GD.DCSGovernmentModule.Shared" type="GD.DCSGovernmentModule.Shared.ProcessingRuleRequestFunctions" />
    internal static  void SetPropertiesVisibility(global::GD.DCSGovernmentModule.IProcessingRuleRequest processingRuleRequest)
    {
      var __functions = ((global::Sungero.Domain.Shared.IEntityFunctions)processingRuleRequest).FunctionsContainer.SharedFunctions;
      var __funcMethod = __functions.GetType().GetMethod("SetPropertiesVisibility", new System.Type[] {  });
    global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(__funcMethod, __functions, new object[] {  });
    }
    /// <redirect project="GD.DCSGovernmentModule.Shared" type="GD.DCSGovernmentModule.Shared.ProcessingRuleRequestFunctions" />
    internal static  void SetPropertiesAvailability(global::GD.DCSGovernmentModule.IProcessingRuleRequest processingRuleRequest)
    {
      var __functions = ((global::Sungero.Domain.Shared.IEntityFunctions)processingRuleRequest).FunctionsContainer.SharedFunctions;
      var __funcMethod = __functions.GetType().GetMethod("SetPropertiesAvailability", new System.Type[] {  });
    global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(__funcMethod, __functions, new object[] {  });
    }

  }
}

// ==================================================================
// ProcessingRuleRequestServerPublicFunctions.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Server
{
  public class ProcessingRuleRequestServerPublicFunctions : global::GD.DCSGovernmentModule.Server.IProcessingRuleRequestServerPublicFunctions
  {
  }
}

// ==================================================================
// ProcessingRuleRequestQueries.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Queries
{
  public class ProcessingRuleRequest
  {
    private static global::Sungero.Domain.SqlQueryResolver resolver = new global::Sungero.Domain.SqlQueryResolver("GD.DCSGovernmentModule.Server.ProcessingRuleRequest.ProcessingRuleRequestQueries.xml", System.Reflection.Assembly.GetExecutingAssembly());
  }
}