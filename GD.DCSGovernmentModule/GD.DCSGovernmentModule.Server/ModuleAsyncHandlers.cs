using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.DCSGovernmentModule.Server
{
  public class ModuleAsyncHandlers
  {
    public virtual void CompleteAssignmentAsync(GD.DCSGovernmentModule.Server.AsyncHandlerInvokeArgs.CompleteAssignmentAsyncInvokeArgs args)
    {
      var assignment = Sungero.Workflow.Assignments.GetAll(a => a.Id == args.AssignmentId).FirstOrDefault();      
      if (assignment == null)
      {
        Logger.ErrorFormat("CompleteAssignmentAsync. Start. AssignmentId with id {0} does not exists.", args.AssignmentId);
        return;
      }
      
      if (assignment.Status != Sungero.Workflow.Assignment.Status.InProcess)
      {
        Logger.DebugFormat("CompleteAssignmentAsync. Start. AssignmentId with id {0} not in process.", args.AssignmentId);
        return;
      }
      
      if (!Locks.TryLock(assignment))
      {
        Logger.DebugFormat("CompleteAssignmentAsync. Start. AssignmentId with id {0} is locked. Sent to retry.", args.AssignmentId);
        args.Retry = true;
        return;
      }
      
      try
      {
        var result = new Enumeration(args.Result);
        assignment.Complete(result);
        
        var completedBy = Users.Get(args.CompletedById);        
        if (completedBy != null)
        {
          assignment.CompletedBy = completedBy;
          assignment.Save();
        }
        else
          Logger.DebugFormat("CompleteAssignmentAsync. Start. User with id {0} not found for CompletedBy. AssignmentId with id {1}.", args.CompletedById, args.AssignmentId);
      }
      catch (Exception ex)
      {
        Logger.ErrorFormat("CompleteAssignmentAsync. An erroc occured while processing assignment with id {0}.", ex, args.AssignmentId);
        args.Retry = true;
        return;
      }
      finally
      {
        Locks.Unlock(assignment);
      }
    }

    public virtual void DeleteVersionsByDocumentId(GD.DCSGovernmentModule.Server.AsyncHandlerInvokeArgs.DeleteVersionsByDocumentIdInvokeArgs args)
    {
      var document = Sungero.Content.ElectronicDocuments.GetAll(d => d.Id.Equals(args.DocumentId)).FirstOrDefault();
      if (document != null)
      {
        var allUsers = Roles.AllUsers;
        
        if (document.HasVersions && Functions.Module.DeleteDocumentVersions(document))
          args.Retry = true;
        
        if (Sungero.Docflow.OfficialDocuments.Is(document) && !document.HasVersions)
        {
          var officialDocument = Sungero.Docflow.OfficialDocuments.As(document);          
          if (officialDocument.LifeCycleState != Sungero.Docflow.OfficialDocument.LifeCycleState.Obsolete)
            officialDocument.LifeCycleState = Sungero.Docflow.OfficialDocument.LifeCycleState.Obsolete;
          if (!officialDocument.AccessRights.IsGranted(DefaultAccessRightsTypes.Forbidden, allUsers))
            officialDocument.AccessRights.Grant(allUsers, DefaultAccessRightsTypes.Forbidden);
          officialDocument.Save();
        }
      }
    }

  }
}