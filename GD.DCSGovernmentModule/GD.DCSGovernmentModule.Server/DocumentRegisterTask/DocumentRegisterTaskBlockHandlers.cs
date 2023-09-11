using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Workflow;
using GD.DCSGovernmentModule.DocumentRegisterTask;

namespace GD.DCSGovernmentModule.Server.DocumentRegisterTaskBlocks
{
  partial class RegistrationAssignmentBlockHandlers
  {

    public virtual void RegistrationAssignmentBlockStart()
    {
      foreach (var attachment in _obj.AllAttachments.Where(x => Sungero.Content.ElectronicDocuments.Is(x)))
      {
        var document = Sungero.Content.ElectronicDocuments.As(attachment);
        foreach (var recipient in _block.Performers)
        {
          if (!document.AccessRights.IsGranted(DefaultAccessRightsTypes.FullAccess, recipient))
            document.AccessRights.Grant(recipient, DefaultAccessRightsTypes.FullAccess);
        }        
      }
      var department = _block.Performers.Where(d => Sungero.Company.Employees.Is(d)).Select(d => Sungero.Company.Employees.As(d).Department).FirstOrDefault();
      if (department != null)
      {
        var document = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
        document.Department = department;
        document.Save();
      }
    }

    public virtual void RegistrationAssignmentBlockEnd(System.Collections.Generic.IEnumerable<GD.DCSGovernmentModule.IRegistrationAssignment> createdAssignments)
    {
      var lastAssignment = createdAssignments.FirstOrDefault();
      if (lastAssignment != null)
      {
        
        _obj.Registrar = lastAssignment.Registrar;
        _obj.BusinessUnit = lastAssignment.BusinessUnit;
        
        var completeResult = lastAssignment.Result;
        _obj.WasRedirected = completeResult == GD.DCSGovernmentModule.RegistrationAssignment.Result.ToRegistrar;
        
        var document = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
        if (lastAssignment.BusinessUnit != null)
          document.BusinessUnit = lastAssignment.BusinessUnit;
        if (lastAssignment.Registrar != null)
        {
          var registrator = lastAssignment.Registrar;
          if (registrator != null)
            document.Department = registrator.Department;
        }
        document.Save();
        
        if (completeResult == GD.DCSGovernmentModule.RegistrationAssignment.Result.ToLetter)
          Functions.DocumentRegisterTask.ReformRequestToLetter(_obj);        
        
        if (completeResult == GD.DCSGovernmentModule.RegistrationAssignment.Result.ToRequest)
          Functions.DocumentRegisterTask.ReformLetterToRequest(_obj);
      }
    }

    public virtual void RegistrationAssignmentBlockCompleteAssignment(GD.DCSGovernmentModule.IRegistrationAssignment assignment)
    {
      // Завершить задания других пользователей если они есть.
      var completeResult = assignment.Result;
      var registrationAssignments = RegistrationAssignments.GetAll(j => Equals(j.Task, assignment.Task) &&
                                                                   j.Status == Sungero.Workflow.Task.Status.InProcess);
      foreach (var registrationAssignment in registrationAssignments)
      {
        registrationAssignment.Registrar = assignment.Registrar;
        registrationAssignment.BusinessUnit = assignment.BusinessUnit;
        registrationAssignment.ActiveText = DCSGovernmentModule.DocumentRegisterTasks.Resources.CompleteMessageTemplateFormat(assignment.Performer.Name);
        registrationAssignment.Complete(completeResult);
      }
    }
  }

}