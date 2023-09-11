using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernment.ProcessingRuleIncomingLetter;
using DirRX.DCTSIntegration;

namespace GD.DCSGovernment.Server
{
  partial class ProcessingRuleIncomingLetterFunctions
  {
    /// <summary>
    /// Заполняет свойства создаваемого входящего письма.
    /// </summary>
    /// <param name="entity">Созданное входящее письмо.</param>
    /// <param name="files">Словарь с захваченными файлами. Ключ - файл в формате Base64, значение - наименование файла.</param>
    /// <param name="pars">Словарь с параметрами захвата. Ключ - имя параметра, значение - значение параметра.</param>
    protected override void FillProperties(Sungero.Domain.Shared.IEntity entity, Dictionary<string, string> files, Dictionary<string, string> pars)
    {
      // HACK: На клиентской строне свойства правила обработки не актуальны. Получить правило обработки для назначения для установки свойств обращения.
      var processingRuleItem = GetProcessingRule(_obj.Id);
      
      if (processingRuleItem == null)
        throw new InvalidOperationException(ProcessingRuleIncomingLetters.Resources.IncorrectProcessingRule);
      
      if (processingRuleItem.CaptureService == DirRX.DCTSIntegration.ProcessingRuleBase.CaptureService.Mail)
      {
        var doc = GD.GovernmentSolution.IncomingLetters.As(entity);
        
        if (doc == null)
          throw new ArgumentException(string.Format(ProcessingRuleDocBases.Resources.IncorrectTypeOfCreatedEntity, _obj.Name), "entity");
        
        // Заполнение корреспондента.
        if (_obj.IsAutoCalcCorrespondent.HasValue && _obj.IsAutoCalcCorrespondent.Value)
        {
          // Определить email заявителя (так как письма будут перенаправляться)
          var sender = pars[DirRX.DCTSIntegration.ProcessingRuleDocBases.Resources.ParamNameSender];
          var correspondent = Sungero.Parties.Companies.As(processingRuleItem.Correspondent);
          var businessUnit = processingRuleItem.Department.BusinessUnit;
          var bodyTXT = files.Where(f => f.Value.ToUpper() == ProcessingRuleDocBases.Resources.NameTXTFileBodyMail).FirstOrDefault();
          var bodyHTML = files.Where(f => f.Value.ToUpper() == ProcessingRuleDocBases.Resources.NameHTMLFileBodyMail).FirstOrDefault();
          var listMail = new List<string>();
          if (bodyTXT.Key != null)
            listMail.AddRange(GD.DCSGovernmentModule.PublicFunctions.Module.GetListMailText(bodyTXT.Key));
          if (bodyHTML.Key != null)
            listMail.AddRange(GD.DCSGovernmentModule.PublicFunctions.Module.GetListMail(bodyHTML.Key));
          if (listMail.Any())
          {
            var isCorrespondentIdentified = false;
            var isBusinessUnitIdentified = false;
            var foundBusinessUnit = GD.DCSGovernmentModule.PublicFunctions.Module.GetBusinessUnitByEmail(sender);
            if (foundBusinessUnit != null)
            {
              businessUnit = foundBusinessUnit;
              isBusinessUnitIdentified = true;
            }
            foreach (var mail in listMail)
            {
              if (mail.ToLower() == sender)
                continue;
              if (!isCorrespondentIdentified)
              {
                var foundCorrespondent = GD.DCSGovernmentModule.PublicFunctions.Module.GetCompanyByEmail(mail);
                if (foundCorrespondent != null)
                {
                  correspondent = foundCorrespondent;
                  isCorrespondentIdentified = true;
                }
              }
              if (!isBusinessUnitIdentified)
              {
                foundBusinessUnit = GD.DCSGovernmentModule.PublicFunctions.Module.GetBusinessUnitByEmail(mail);
                if (foundBusinessUnit != null)
                {
                  businessUnit = foundBusinessUnit;
                  
                  isBusinessUnitIdentified = true;
                }
              }
            }
          }
          doc.Correspondent = correspondent;
          doc.BusinessUnit = businessUnit;
        }
        if (doc.Correspondent == null)
          doc.Correspondent = processingRuleItem.Correspondent;
        if (doc.Correspondent == null)
          throw new InvalidOperationException(ProcessingRuleIncomingLetters.Resources.IncorrectDefaultCorrespondent);
        
        doc.Department = processingRuleItem.Department;
        if (doc.Department == null)
          throw new InvalidOperationException(ProcessingRuleIncomingLetters.Resources.IncorrectDefaultDepartment);
        
        if (doc.BusinessUnit == null)
          doc.BusinessUnit = processingRuleItem.Department.BusinessUnit;
        if (doc.BusinessUnit == null)
          throw new InvalidOperationException(ProcessingRuleIncomingLetters.Resources.IncorrectDefaultBussinesUnit);

        doc.Addressee = doc.BusinessUnit.CEO;
        doc.DeliveryMethod = Sungero.Docflow.MailDeliveryMethods.GetAll(n => n.Name == Sungero.Docflow.MailDeliveryMethods.Resources.EmailMethod).FirstOrDefault();
        
        using (var stream = new System.IO.MemoryStream())
        {
          var file = Convert.FromBase64String(files.First().Key);
          stream.Write(file, 0, file.Length);
          doc.CreateVersionFrom(stream, System.IO.Path.GetExtension(files.First().Value));
        }
        if (doc.Versions.Any())
          doc.Versions.Last().Note = this.GetVersionNote();
        doc.DocumentKind = _obj.DocumentKind;
        this.FillEntityName(doc, files, pars);
        
        this.FillEntitySubject(doc, files, pars);        
      }
      else
        base.FillProperties(entity, files, pars);
    }
  }
}