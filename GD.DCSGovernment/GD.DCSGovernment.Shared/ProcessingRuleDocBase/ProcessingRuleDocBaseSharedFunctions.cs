using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernment.ProcessingRuleDocBase;

namespace GD.DCSGovernment.Shared
{
  partial class ProcessingRuleDocBaseFunctions
  {
    /// <summary>
    /// Установить обязательность свойств.
    /// </summary>
    public override void SetRequiredProperties()
    {      
      var isAnyFill = _obj.TaskType.HasValue && _obj.TaskType != DCSGovernment.ProcessingRuleDocBase.TaskType.RegisterTask || !string.IsNullOrEmpty(_obj.TaskSubject) || _obj.Performers.Any();
      var isAssignment = _obj.TaskType.HasValue && _obj.TaskType.Value == DirRX.DCTSIntegration.ProcessingRuleBase.TaskType.Assignment;
      
      _obj.State.Properties.DeadlineInDays.IsRequired = isAssignment && isAnyFill && !_obj.DeadlineInHours.HasValue;
      _obj.State.Properties.DeadlineInHours.IsRequired = isAssignment && isAnyFill && !_obj.DeadlineInDays.HasValue;
      _obj.State.Properties.TaskType.IsRequired = isAnyFill;
      _obj.State.Properties.TaskSubject.IsRequired = isAnyFill;
      _obj.State.Properties.Performers.IsRequired = isAnyFill;
    }
    
    /// <summary>
    /// Установить видимость свойств.
    /// </summary>
    public override void SetPropertiesVisibility()
    {
      base.SetPropertiesVisibility();
      var isNotRegisterTask = _obj.TaskType.HasValue && _obj.TaskType != DCSGovernment.ProcessingRuleDocBase.TaskType.RegisterTask;
      _obj.State.Properties.TaskSubject.IsVisible = isNotRegisterTask;
      _obj.State.Properties.Performers.IsVisible = isNotRegisterTask;
    }
  }
}