using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Shared;
using DirRX.DCTSIntegration;
using GD.DCSGovernmentModule.ProcessingRuleRequest;

namespace GD.DCSGovernmentModule.Server
{
  partial class ProcessingRuleRequestFunctions
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
        var doc = GD.CitizenRequests.Requests.As(entity);
        
        if (doc == null)
          throw new ArgumentException(string.Format(ProcessingRuleDocBases.Resources.IncorrectTypeOfCreatedEntity, _obj.Name), "entity");
        
        // Заполнение корреспондента.
        if (_obj.IsAutoCalcCorrespondent.HasValue && _obj.IsAutoCalcCorrespondent.Value)
        {
          // Определить email заявителя (так как письма будут перенаправляться)
          var sender = pars[DirRX.DCTSIntegration.ProcessingRuleDocBases.Resources.ParamNameSender];
          var declarant = Sungero.Parties.People.As(processingRuleItem.Correspondent);
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
            var isDeclarantIdentified = false;
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
              if (!isDeclarantIdentified)
              {
                var foundDeclarant = GD.DCSGovernmentModule.PublicFunctions.Module.GetPersonByEmail(mail);
                if (foundDeclarant != null)
                {
                  declarant = foundDeclarant;
                  isDeclarantIdentified = true;
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
          doc.Correspondent = declarant;
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
          doc.CreateVersionFrom(stream, Path.GetExtension(files.First().Value));
        }
        doc.Versions.Last().Note = this.GetVersionNote();
        doc.DocumentKind = _obj.DocumentKind;
        this.FillEntityName(doc, files, pars);
        
        this.FillEntitySubject(doc, files, pars);
      }
      else
      {        
        var doc = GD.CitizenRequests.Requests.As(entity);        
        doc.Correspondent = processingRuleItem.Correspondent;
        if (doc.Correspondent == null)
          throw new InvalidOperationException(ProcessingRuleIncomingLetters.Resources.IncorrectDefaultCorrespondent);        
        // Заполнение подразделения.
        doc.Department = processingRuleItem.Department;
        if (doc.Department == null)
          throw new InvalidOperationException(ProcessingRuleIncomingLetters.Resources.IncorrectDefaultDepartment);        
        // Заполнение НОР.
        doc.BusinessUnit = processingRuleItem.Department.BusinessUnit;
        if (doc.BusinessUnit == null)
          throw new InvalidOperationException(ProcessingRuleIncomingLetters.Resources.IncorrectDefaultBussinesUnit);
        doc.Addressee = doc.BusinessUnit.CEO;
        base.FillProperties(entity, files, pars);        
        this.FillEntitySubject(doc, files, pars);
      }
    }
    
    /// <summary>
    /// Возвращает тип документа.
    /// </summary>
    /// <returns>Тип документа.</returns>
    public override Sungero.Docflow.IDocumentType GetDocumentType()
    {
      return Sungero.Docflow.DocumentTypes.GetAll().FirstOrDefault(_ => _.DocumentTypeGuid == "77300c65-4db1-4baf-9321-7e8efb7805cb");
    }
    
    /// <summary>
    /// Возвращает правило обработки обращения.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <returns>Правило обработки обращения.</returns>
    [Remote]
    public static IProcessingRuleRequest GetProcessingRule(long id)
    {
      return ProcessingRuleRequests.GetAll(rule => Equals(rule.Id, id)).FirstOrDefault();
    }
    
    /// <summary>
    /// Создает официальный документ.
    /// </summary>
    /// <returns>Официальный документ.</returns>
    [Remote]
    public override Sungero.Docflow.IOfficialDocument CreateDocument()
    {
      return GD.CitizenRequests.Requests.Create();
    }

    /// <summary>
    /// Заполняет содержание документа.
    /// </summary>
    /// <param name="doc">Созданный документ.</param>
    /// <param name="files">Словарь с захваченными файлами. Ключ - файл в формате Base64, значение - наименование файла.</param>
    /// <param name="pars">Словарь с параметрами захвата. Ключ - имя параметра, значение - значение параметра.</param>
    protected virtual void FillEntitySubject(GD.CitizenRequests.IRequest doc, Dictionary<string, string> files, Dictionary<string, string> pars)
    {
      // Если в правиле установлен флажок "Автозаполнение" для шаблона содержания, то в качестве содержания документа, поступившего из почты, задать тему письма, из файловой системы - имя файла.
      var isMail = _obj.CaptureService.HasValue && _obj.CaptureService.Value == DirRX.DCTSIntegration.ProcessingRuleBase.CaptureService.Mail;
      
      if (isMail && _obj.FillFromSubject.HasValue && _obj.FillFromSubject.Value)
        doc.Subject = pars[ProcessingRuleDocBases.Resources.ParamNameSubject];
      
      if (!isMail && _obj.FillFromFileName.HasValue && _obj.FillFromFileName.Value)
        doc.Subject = Path.GetFileNameWithoutExtension(files.First().Value);
      
      // Заполнить по шаблону.
      if (!string.IsNullOrWhiteSpace(_obj.SubjectPattern) && _obj.FillFromFileName.HasValue && !_obj.FillFromFileName.Value && _obj.FillFromSubject.HasValue && !_obj.FillFromSubject.Value)
        doc.Subject = this.FillPattern(_obj.SubjectPattern, doc);
    }
    
    /// <summary>
    /// Заполнение шаблона.
    /// </summary>
    /// <param name="pattern">Шаблон.</param>
    /// <param name="entity">Сущность для которой применяется шаблон.</param>
    /// <returns>Результат применения шаблона.</returns>
    protected override string FillPattern(string pattern, IEntity entity)
    {
      var result = base.FillPattern(pattern, entity);
      if (string.IsNullOrWhiteSpace(result))
        return null;
      
      var doc = GD.CitizenRequests.Requests.As(entity);
      if (doc == null)
        throw new ArgumentException(string.Format(ProcessingRuleDocBases.Resources.IncorrectTypeOfCreatedEntity, _obj.Name), "entity");
      
      return result.Replace(Constants.Module.DeclarantPattern, doc.Correspondent != null ? doc.Correspondent.Name : string.Empty);
    }
  }
}