using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Docflow;
using GD.DCSGovernment.ProcessingRuleDocBase;

namespace GD.DCSGovernment.Server
{
  partial class ProcessingRuleDocBaseFunctions
  {
    /// <summary>
    /// Отправляет задачи с вложениями в соответствии с настроенными параметрами отправки.
    /// </summary>
    /// <param name="attachment">Вложение.</param>
    public override void SendTasks(Sungero.Domain.Shared.IEntity attachment)
    {
      if (_obj.TaskType == DirRX.DCTSIntegration.ProcessingRuleBase.TaskType.Assignment || _obj.TaskType == DirRX.DCTSIntegration.ProcessingRuleBase.TaskType.Notice)
        base.SendTasks(attachment);
      if (_obj.TaskType == GD.DCSGovernment.ProcessingRuleDocBase.TaskType.RegisterTask)
      {
        var doc = Sungero.Docflow.IncomingDocumentBases.As(attachment);
        var registerTask = GD.DCSGovernmentModule.DocumentRegisterTasks.Create();
        registerTask.DocumentGroup.IncomingDocumentBases.Add(doc);
        
        foreach (var addendum in Addendums.GetAll(a => Equals(a.LeadingDocument, attachment)))
          registerTask.OtherGroup.All.Add(addendum);
        
        registerTask.Save();
        registerTask.Start();
      }
    }
    
    /// <summary>
    /// Обрабатывает переданные вложения.
    /// </summary>
    /// <param name="entity">Созданный документ.</param>
    /// <param name="files">Словарь с захваченными файлами. Ключ - файл в формате Base64, значение - наименование файла.</param>
    /// <param name="pars">Словарь с параметрами захвата. Ключ - имя параметра, значение - значение параметра.</param>
    protected override void ProcessAttachments(Sungero.Domain.Shared.IEntity entity, Dictionary<string, string> files, Dictionary<string, string> pars)
    {
      IOfficialDocument doc = OfficialDocuments.As(entity);

      if (doc == null)
        throw new ArgumentException(string.Format(DirRX.DCTSIntegration.ProcessingRuleDocBases.Resources.IncorrectTypeOfCreatedEntity, _obj.Name), "entity");
      
      // Базовый метод не вызывается, так как он пустой.
      // Пропускаем два элемента (BODY.TXT и BODY.HTML), так как они отвечают за тело письма.
      foreach (var key in files.Where(f => f.Value.ToUpper() != DirRX.DCTSIntegration.ProcessingRuleDocBases.Resources.NameTXTFileBodyMail &&
                                      f.Value.ToUpper() != DirRX.DCTSIntegration.ProcessingRuleDocBases.Resources.NameHTMLFileBodyMail))
      {
        // Создать приложение к основному документу.
        var addendum = Sungero.Docflow.Addendums.Create();
        var filePath = key.Value;
        var extension = Path.GetExtension(filePath);
        if (Sungero.Content.AssociatedApplications.GetAll(x => x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active &&
                                                          x.Extension.ToLower() == extension.TrimStart('.').ToLower()).Any())
        {
          using (var stream = new System.IO.MemoryStream())
          {
            var file = Convert.FromBase64String(key.Key);
            stream.Write(file, 0, file.Length);
            addendum.CreateVersionFrom(stream, extension);
          }
        }
        else
        {
          var zipFilePath = GD.DCSGovernmentModule.PublicFunctions.Module.CreateZipFromFile(filePath);
          addendum.CreateVersionFrom(zipFilePath);
        }
        
        if (addendum.Versions.Any())
          addendum.Versions.Last().Note = this.GetVersionNote();
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        addendum.Name = fileName;
        addendum.Subject = fileName;
        addendum.LeadingDocument = doc;
        addendum.Save();
      }
    }
  }
}