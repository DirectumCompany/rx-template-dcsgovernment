using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernmentModule.RegistrationAssignment;

namespace GD.DCSGovernmentModule.Client
{
  partial class RegistrationAssignmentActions
  {
    public virtual bool CanRegistration(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.Status == Sungero.Workflow.Assignment.Status.InProcess && _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault().RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.NotRegistered;
    }

    public virtual void Registration(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var document = Sungero.Docflow.OfficialDocuments.As(_obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault());
      document.ShowModal();
      document = Functions.Module.Remote.RefreshDocument(document);
      if (document.RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.Registered &&
          _obj.Status == Status.InProcess)
      {
        var action = new Sungero.Workflow.Client.ExecuteResultActionArgs(e.FormType, e.Entity, e.Action);
        this.Register(action);
        
        e.CloseFormAfterAction = true;
        Functions.Module.Remote.TryCompleteAssignmentSyncThenAsync(_obj, Result.Register);
      }
    }

    public virtual void ToLetter(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var mainDocument = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
      if (!Functions.Module.Remote.ExistRegistrarForDocument(mainDocument.BusinessUnit, false))
        e.AddError(RegistrationAssignments.Resources.RegistrarNotFoundLetter);
      else
        _obj.ActiveText = RegistrationAssignments.Resources.ReformRequestToLetter;
    }

    public virtual bool CanToLetter(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      var doc = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
      return CitizenRequests.Requests.Is(doc) && doc.RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.NotRegistered;
    }

    public virtual void ToRequest(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var mainDocument = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
      if (!Functions.Module.Remote.ExistRegistrarForDocument(mainDocument.BusinessUnit, true))
        e.AddError(RegistrationAssignments.Resources.RegistrarNotFoundRequest);
      else
        _obj.ActiveText = RegistrationAssignments.Resources.ReformLetterToRequest;
    }

    public virtual bool CanToRequest(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      var doc = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
      return Sungero.RecordManagement.IncomingLetters.Is(doc) && doc.RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.NotRegistered;
    }

    public virtual void ToBusinessUnit(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var businessUnit = _obj.BusinessUnit;
      if (businessUnit == null)
        e.AddError(RegistrationAssignments.Resources.NeedFillBusinessUnit, _obj.Info.Actions.ToBusinessUnit);
      else
      {
        var isDocumentRequest = CitizenRequests.Requests.Is(_obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault());
        if (Functions.Module.Remote.ExistRegistrarForDocument(businessUnit, isDocumentRequest))
          _obj.ActiveText = RegistrationAssignments.Resources.ForwardedToBusinessUnitFormat(_obj.BusinessUnit.Name);
        else
        {
          if (isDocumentRequest)
            e.AddError(RegistrationAssignments.Resources.RegistrarNotFoundRequest, _obj.Info.Actions.ToBusinessUnit);
          else
            e.AddError(RegistrationAssignments.Resources.RegistrarNotFoundLetter, _obj.Info.Actions.ToBusinessUnit);
        }
      }
    }

    public virtual bool CanToBusinessUnit(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault().RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.NotRegistered;
    }

    public virtual void ToRegistrar(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      if (_obj.Registrar == null)
        e.AddError(RegistrationAssignments.Resources.NeedFillRegistrar, _obj.Info.Actions.ToRegistrar);
      else
        _obj.ActiveText = RegistrationAssignments.Resources.ForwardedToRegistrarFormat(_obj.Registrar.Name);
    }

    public virtual bool CanToRegistrar(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault().RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.NotRegistered;
    }

    public virtual void Decline(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      if (string.IsNullOrEmpty(_obj.ActiveText))
        e.AddError(RegistrationAssignments.Resources.NeedFillReasonsForRejection);
      else
        _obj.ActiveText = RegistrationAssignments.Resources.DeclineTextFormat(_obj.ActiveText);
    }

    public virtual bool CanDecline(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      var doc =_obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
      return Sungero.RecordManagement.IncomingLetters.Is(doc) && doc.RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.NotRegistered;
    }

    public virtual void Register(Sungero.Workflow.Client.ExecuteResultActionArgs e)
    {
      var doc = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault();
      if (doc.RegistrationState != Sungero.Docflow.OfficialDocument.RegistrationState.Registered)
        e.AddError(RegistrationAssignments.Resources.DocumentNotRegistered);
      else
        _obj.ActiveText = RegistrationAssignments.Resources.RegisterText;
    }

    public virtual bool CanRegister(Sungero.Workflow.Client.CanExecuteResultActionArgs e)
    {
      return true;
    }

    public virtual void ChangeLeadingDocument(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var otherDocuments = _obj.OtherGroup.All.Where(x => Sungero.Docflow.OfficialDocuments.Is(x)).Select(x => Sungero.Docflow.OfficialDocuments.As(x));
      if (otherDocuments.Any())
      {
        var dialogSelectDocument = Dialogs.CreateInputDialog(RegistrationAssignments.Resources.SelectLeadingDocument);
        var newMaindocument = dialogSelectDocument.AddSelect(RegistrationAssignments.Resources.Document, true, Sungero.Docflow.OfficialDocuments.Null).From(otherDocuments);
        if (dialogSelectDocument.Show() == DialogButtons.Ok)
        {
          try
          {
            Functions.Module.Remote.ChangeDocumentsBody(_obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault(), newMaindocument.Value);
          }
          catch (Exception ex)
          {
            e.AddError(RegistrationAssignments.Resources.UnableChangeMainDocumentFormat(ex.Message));
          }
          e.AddInformation(RegistrationAssignments.Resources.SuccessfullyDocument);
        }
      }
    }

    public virtual bool CanChangeLeadingDocument(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _obj.Status == Sungero.Workflow.Assignment.Status.InProcess && _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault().RegistrationState == Sungero.Docflow.OfficialDocument.RegistrationState.NotRegistered;
    }

  }

}