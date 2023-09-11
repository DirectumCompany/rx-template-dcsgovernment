using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernmentModule.DocumentRegisterTask;

namespace GD.DCSGovernmentModule
{
  partial class DocumentRegisterTaskServerHandlers
  {

    public override void BeforeSave(Sungero.Domain.BeforeSaveEventArgs e)
    {
      var document = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
      var subject = Sungero.Docflow.PublicFunctions.Module.TrimSpecialSymbols(DocumentRegisterTasks.Resources.DocumentRegisterTaskSubjectFormat(document.Name));
      if (subject != _obj.Subject)
        _obj.Subject = subject;
    }

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      _obj.Subject = Sungero.Docflow.Resources.AutoformatTaskSubject;
    }
  }

}