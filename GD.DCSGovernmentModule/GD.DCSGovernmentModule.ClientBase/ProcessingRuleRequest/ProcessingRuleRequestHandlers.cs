using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernmentModule.ProcessingRuleRequest;

namespace GD.DCSGovernmentModule
{
  partial class ProcessingRuleRequestClientHandlers
  {

    public override void Refresh(Sungero.Presentation.FormRefreshEventArgs e)
    {
      base.Refresh(e);
      
      Functions.ProcessingRuleRequest.SetPropertiesVisibility(_obj);
      Functions.ProcessingRuleRequest.SetPropertiesAvailability(_obj);
    }

    public override void Showing(Sungero.Presentation.FormShowingEventArgs e)
    {
      base.Showing(e);
      
      if (_obj.Department != null && _obj.Department.BusinessUnit == null)
        e.AddWarning(DCSGovernmentModule.ProcessingRuleRequests.Resources.BusinessUnitUnfilled);
    }

    public virtual void DepartmentValueInput(GD.DCSGovernmentModule.Client.ProcessingRuleRequestDepartmentValueInputEventArgs e)
    {
      if (e.NewValue != null && e.NewValue.BusinessUnit == null)
        e.AddWarning(DCSGovernmentModule.ProcessingRuleRequests.Resources.BusinessUnitUnfilled);      
    }

  }
}