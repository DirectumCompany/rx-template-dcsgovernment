using System;
using System.Collections.Generic;
using System.Linq;
using GD.DCSGovernmentModule;
using GD.CitizenRequests;
using Sungero.Docflow;
using Sungero.Company;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  partial class ModuleFunctions
  {
    /// <summary>
    /// Регистратор входящего документа.
    /// </summary>
    /// <param name="document">Документ.</param>
    /// <param name="task">Задача на регистрацию.</param>
    /// <returns>Регистраторы</returns>
    [ExpressionElement("Регистраторы входящего документа", "Возвращает регистраторов входящего докумета", "Задача на регистрацию")]
    public List<IRecipient> GetClerkForIncommingDocument(IIncomingDocumentBase	document, IDocumentRegisterTask task)
    {
      var clerks = new List<IRecipient>();
      if (task.WasRedirected == true && task.Registrar != null)
        clerks.Add(task.Registrar);
      else
      {
        if (Sungero.RecordManagement.IncomingLetters.Is(document))
        {
          var registrar = Sungero.Docflow.PublicFunctions.Module.Remote.GetClerk(document);
          if (registrar != null)
            clerks.Add(registrar);
        }
        else if (Requests.Is(document))
        {
          var settings = CitizenRequestSettings.GetAll(x => x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active && Equals(x.BusinessUnit, document.BusinessUnit)).FirstOrDefault();
          if (settings != null)
            clerks = settings.RequestsRegistrators.Select(x => x.Registrator).ToList();
        }
      }
      return clerks;
    }    
  }
}