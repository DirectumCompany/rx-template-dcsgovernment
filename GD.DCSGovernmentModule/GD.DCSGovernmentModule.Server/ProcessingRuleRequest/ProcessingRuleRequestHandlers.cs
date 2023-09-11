using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernmentModule.ProcessingRuleRequest;

namespace GD.DCSGovernmentModule
{
  partial class ProcessingRuleRequestServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      base.Created(e);
      
      if (!_obj.State.IsCopied)
      {
        _obj.FillFromSubject = false;
        _obj.FillFromFileName = false;
        _obj.IsAutoCalcCorrespondent = false;
      }
    }

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      base.BeforeSave(e);
      
      // Прервать сохранение, если все параметры заполнения содержания пусты.
      if (string.IsNullOrWhiteSpace(_obj.SubjectPattern) && 
          (_obj.FillFromSubject.HasValue && !_obj.FillFromSubject.Value) && (_obj.FillFromFileName.HasValue && !_obj.FillFromFileName.Value))
        e.AddError(ProcessingRuleRequests.Resources.RequiredSubjectPattern);
    }
  }

}