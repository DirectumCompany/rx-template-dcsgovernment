using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernmentModule.DocumentRegisterTask;

namespace GD.DCSGovernmentModule.Server
{
  partial class DocumentRegisterTaskFunctions
  {

    /// <summary>
    /// Преобразовать обращение во входящее письмо.
    /// </summary>       
    public void ReformRequestToLetter()
    {
      var request = CitizenRequests.Requests.As(_obj.DocumentGroup.All.FirstOrDefault());
      Locks.Lock(request);
      var letter = Sungero.RecordManagement.IncomingLetters.Create();
      letter.Subject = request.Subject;
      letter.Correspondent = Sungero.Parties.Companies.As(request.Organization);
      letter.BusinessUnit = request.BusinessUnit;
      letter.Department = request.Department;
      letter.Note = request.Note;
      letter.Addressee = request.Addressee;
      letter.DeliveryMethod = request.DeliveryMethod;
      
      var existsNewVersion = Functions.Module.CopyDocumentBody(request, letter);
      if (!existsNewVersion)
      {
        _obj.Abort();
        _obj.ActiveText = DocumentRegisterTasks.Resources.ReformDocumentError;
      }
      else
      {
        try
        {
          letter.Save();
        }
        catch (Exception ex)
        {
          throw Sungero.Core.AppliedCodeException.Create(DocumentRegisterTasks.Resources.ReformDocumentError, ex);
        }
      }
      
      if (request.HasRelations)
        Functions.Module.CopyDocumentRelations(request, letter);
      
      Functions.Module.CopyDocumentSignatures(request, letter);
      
      if (letter.HasVersions)
      {
        try
        {
          _obj.DocumentGroup.IncomingDocumentBases.Remove(request);
          _obj.DocumentGroup.IncomingDocumentBases.Add(letter);
        }
        catch (Exception ex)
        {
          throw Sungero.Core.AppliedCodeException.Create(DocumentRegisterTasks.Resources.ReformDocumentError, ex);
        }
        _obj.Request = null;
        _obj.Letter = letter;
        
        Functions.Module.MarkDocumentAsObsolete(request);
      }
      Locks.Unlock(request);
    }

    /// <summary>
    /// Преобразовать входящее письмо в обращение.
    /// </summary>       
    public void ReformLetterToRequest()
    {
      var letter = Sungero.RecordManagement.IncomingLetters.As(_obj.DocumentGroup.All.FirstOrDefault());
      
      Locks.Lock(letter);
      var request = CitizenRequests.Requests.Create();
      request.RequestType = CitizenRequests.Request.RequestType.Anonymous;
      var correspondent = letter.Correspondent;
      if (Sungero.Parties.Companies.Is(correspondent))
        request.Organization = Sungero.Parties.Companies.As(letter.Correspondent);
      if (Sungero.Parties.People.Is(correspondent))
        request.Correspondent = Sungero.Parties.People.As(letter.Correspondent);
      request.Subject = letter.Subject;
      request.BusinessUnit = letter.BusinessUnit;
      request.Name = letter.Name;
      request.NotifyByEmail = false;
      // request.Email = _obj.MailSender;
      request.Note = letter.Note;
      
      request.Addressee = letter.Addressee;
      request.DeliveryMethod = letter.DeliveryMethod;
      request.Department = letter.Department;

      var existsNewVersion = Functions.Module.CopyDocumentBody(letter, request);
      if (!existsNewVersion)
      {
        _obj.Abort();
        _obj.ActiveText = DocumentRegisterTasks.Resources.ReformDocumentError;
      }
      else
      {
        try
        {
          request.Save();
        }
        catch (Exception ex)
        {
          throw Sungero.Core.AppliedCodeException.Create(DocumentRegisterTasks.Resources.ReformDocumentError, ex);
        }
      }
      
      if (letter.HasRelations)
        Functions.Module.CopyDocumentRelations(letter, request);
      
      Functions.Module.CopyDocumentSignatures(letter, request);
      
      if (request.HasVersions)
      {
        try
        {
          _obj.DocumentGroup.IncomingDocumentBases.Remove(letter);
          _obj.DocumentGroup.IncomingDocumentBases.Add(request);
        }
        catch (Exception ex)
        {
          throw Sungero.Core.AppliedCodeException.Create(DocumentRegisterTasks.Resources.ReformDocumentError, ex);
        }
        _obj.Letter = null;
        _obj.Request = request;
        
        Functions.Module.MarkDocumentAsObsolete(letter);
      }
      Locks.Unlock(letter);
    }

  }
}