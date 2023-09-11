using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using Sungero;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Shared;

namespace GD.DCSGovernmentModule.Server
{
  public class ModuleFunctions
  {
    /// <summary>
    /// Переполучает документ с сервера.
    /// </summary>
    /// <param name="document">Документ.</param>
    /// <returns>Документ</returns>
    /// <remarks>При передаче объекта в Remote-функцию у него обновляются все свойства.</remarks>
    [Remote]
    public virtual Sungero.Docflow.IOfficialDocument RefreshDocument(Sungero.Docflow.IOfficialDocument document)
    {
      return document;
    }
    
    /// <summary>
    /// Выполняет задание синхронно, в случае ошибки запускает асинхронный обработчик для выполнения заданий.
    /// </summary>
    /// <param name="assignment">Задание, которое необходимо выполнить.</param>
    /// <param name="result">Результат, с которым необходимо выполнить задание.</param>
    /// <returns>True - если запущен АО, false - задание выполнено синхронно.</returns>
    [Remote]
    public virtual bool TryCompleteAssignmentSyncThenAsync(Sungero.Workflow.IAssignment assignment, Enumeration result)
    {
      try
      {
        assignment.Complete(result);
        return false;
      }
      catch
      {
        var asyncHandler = AsyncHandlers.CompleteAssignmentAsync.Create();
        asyncHandler.AssignmentId = assignment.Id;
        asyncHandler.Result = result.Value;
        asyncHandler.CompletedById = Users.Current.Id;
        asyncHandler.ExecuteAsync();
        return true;
      }
    }
    
    /// <summary>
    /// Создает архив с файлом.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <returns>Путь до архива.</returns>
    [Public]
    public string CreateZipFromFile(string filePath)
    {
      var extension = Path.GetExtension(filePath);
      var zipFilePath = Path.ChangeExtension(extension, ".zip");
      var index = 1;
      while (File.Exists(zipFilePath))
      {
        zipFilePath = filePath.Replace(extension, string.Format("_{0}.zip", index.ToString()));
        index++;
      }
      using (var zipFileStream = new FileStream(zipFilePath, FileMode.Create))
      {
        using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
        {
          archive.CreateEntry(filePath);
        }
      }
      return zipFilePath;
    }
    
    /// <summary>
    /// Получает персону по e-mail.
    /// </summary>
    /// <param name="email">Почта.</param>
    /// <returns>Персона.</returns>
    [Public]
    public static Sungero.Parties.IPerson GetPersonByEmail(string email)
    {
      if (string.IsNullOrEmpty(email))
        return Sungero.Parties.People.Null;
      return Sungero.Parties.People.GetAll(c => c.Status == Sungero.CoreEntities.DatabookEntry.Status.Active && c.Email != null && string.Equals(c.Email.ToLower(), email.ToLower())).FirstOrDefault();
    }

    /// <summary>
    /// Получает контрагента по e-mail.
    /// </summary>
    /// <param name="email">Почта.</param>
    /// <returns>Контрагент.</returns>
    [Public]
    public static Sungero.Parties.ICompany GetCompanyByEmail(string email)
    {
      if (string.IsNullOrEmpty(email))
        return Sungero.Parties.Companies.Null;
      return Sungero.Parties.Companies.GetAll(c => c.Status == Sungero.CoreEntities.DatabookEntry.Status.Active && c.Email != null && string.Equals(c.Email.ToLower(), email.ToLower())).FirstOrDefault();
    }
    
    /// <summary>
    /// Получает нашу организацию по e-mail.
    /// </summary>
    /// <param name="email">Почта.</param>
    /// <returns>Наша организация.</returns>
    [Public]
    public static Sungero.Company.IBusinessUnit GetBusinessUnitByEmail(string email)
    {
      if (string.IsNullOrEmpty(email))
        return Sungero.Company.BusinessUnits.Null;
      return Sungero.Company.BusinessUnits.GetAll(c => c.Status == Sungero.CoreEntities.DatabookEntry.Status.Active && c.Email != null && string.Equals(c.Email.ToLower(), email.ToLower())).FirstOrDefault();
    }
    
    [Public]
    public static List<string> GetListMail(string file)
    {
      var emails = new List<string>();
      var content = string.Empty;
      try
      {
        file = file.Remove(0, 36);
        using (var ms = new MemoryStream(Convert.FromBase64String(file)))
        {
          using (StreamReader sr = new StreamReader(ms))
          {
            content = GetPlainTextFromHtml(sr.ReadToEnd());
            var indFrom = content.IndexOf("From");
            var indTo = content.IndexOf("To");
            var subContent = string.Empty;
            if (indFrom > -1 && indTo > -1)
            {
              subContent = content.Substring(indFrom, indTo - indFrom);
              emails = SearchEmailsFromText(subContent);
              if (emails.Count > 0)
                return emails;
            }
            return SearchEmailsFromText(content);
          }
        }
      }
      catch (Exception ex)
      {
        throw AppliedCodeException.Create(ex.Message);
      }
    }
    
    [Public]
    public static List<string> GetListMailText(string file)
    {
      var emails = new List<string>();
      var content = string.Empty;
      try
      {
        file = file.Remove(0, 36);
        content = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(file));
        content = Regex.Replace(content, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
        content = content.Replace("&nbsp;", string.Empty);

        var indFrom = content.IndexOf("From");
        var indTo = content.IndexOf("To");
        if (indTo < indFrom)
        {
          var newContent = content.Substring(indFrom);
          indTo = newContent.IndexOf("To");
        }
        var subContent = string.Empty;
        if (indFrom > -1 && indTo > -1 && indTo > indFrom)
        {
          subContent = content.Substring(indFrom, indTo - indFrom);
          emails = SearchEmailsFromText(subContent);
          if (emails.Count > 0)
            return emails;
        }
        return SearchEmailsFromText(content);
      }
      catch (Exception ex)
      {
        throw AppliedCodeException.Create(ex.Message);
      }
    }
    
    private static string GetPlainTextFromHtml(string htmlString)
    {
      string htmlTagPattern = "<.*?>";
      var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
      htmlString = regexCss.Replace(htmlString, string.Empty);
      htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
      htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
      htmlString = htmlString.Replace("&nbsp;", string.Empty);
      return htmlString;
    }
    
    /// <summary>
    /// Поиск email в тексте
    /// </summary>
    /// <param name="content">Текст, в котором необходимо найти email</param>
    /// <returns>Список email</returns>
    private static List<string> SearchEmailsFromText(string content)
    {
      Regex regex = new Regex(@"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      MatchCollection matches = regex.Matches(content);
      List<string> result = new List<string>();

      if (matches.Count > 0)
      {
        foreach (Match match in matches)
        {
          result.Add(match.Value.Trim());
        }
      }
      return result;
    }

    /// <summary>
    /// Удалить все версии документа.
    /// </summary>
    /// <param name="DocumentID">ИД документа, версии которого нужно удалить.</param>
    /// <returns>При удалении версий произошли ошибки?</returns>
    public static bool DeleteDocumentVersions(Sungero.Content.IElectronicDocument document)
    {
      do
      {
        if (document.VersionsLocked)
          return true;
        try
        {
          document.DeleteVersion(document.LastVersion);
          document.Save();
        }
        catch (Exception ex)
        {
          Logger.ErrorFormat("Error in AsyncHandlers. DeleteDocumentVersions. Message:{0}", ex.Message);
          return true;
        }
      }
      while(document.HasVersions);
      return false;
    }
    
    /// <summary>
    /// Перемещение тел между документами.
    /// </summary>
    /// <param name="firstDocument">Документ.</param>
    /// <param name="secondDocument">Документ.</param>
    [Remote]
    public static void ChangeDocumentsBody(Sungero.Docflow.IOfficialDocument firstDocument, Sungero.Docflow.IOfficialDocument secondDocument)
    {
      // Изменить тела
      var versionFirstDocument = firstDocument.LastVersion;
      var versionSecondDocument = secondDocument.LastVersion;
      
      var signaturesFirstDocument = Signatures.Get(versionFirstDocument);
      var signaturesSecondDocument = Signatures.Get(versionSecondDocument);
      
      using (var inputStream = new System.IO.MemoryStream())
      {
        versionSecondDocument.Body.Read().CopyTo(inputStream);
        firstDocument.CreateVersionFrom(inputStream, versionSecondDocument.AssociatedApplication.Extension);
      }
      using (var inputStream = new System.IO.MemoryStream())
      {
        versionFirstDocument.Body.Read().CopyTo(inputStream);
        secondDocument.CreateVersionFrom(inputStream, versionFirstDocument.AssociatedApplication.Extension);
      }
      firstDocument.DeleteVersion(versionFirstDocument);
      secondDocument.DeleteVersion(versionSecondDocument);
      foreach (var signature in signaturesFirstDocument)
        Signatures.Import(secondDocument, signature.SignatureType, signature.SignatoryFullName, signature.GetDataSignature(), secondDocument.LastVersion);
      
      foreach (var signature in signaturesSecondDocument)
        Signatures.Import(firstDocument, signature.SignatureType, signature.SignatoryFullName, signature.GetDataSignature(), firstDocument.LastVersion);
      
      
      // Изменить содержание
      var subjectFirstDocument = firstDocument.Subject;
      firstDocument.Subject = secondDocument.Subject;
      secondDocument.Subject = subjectFirstDocument;
      
      firstDocument.Save();
      secondDocument.Save();
    }
    
    /// <summary>
    /// Копирование тела документа.
    /// </summary>
    /// <param name="sourceDocument">Документ-источник.</param>
    /// <param name="receiverDocument">Документ-приемник.</param>
    /// <returns>Тело перенесено?</returns>
    public static bool CopyDocumentBody(Sungero.Docflow.IOfficialDocument sourceDocument, Sungero.Docflow.IOfficialDocument receiverDocument)
    {
      var versionSourceDocument = sourceDocument.LastVersion;
      
      using (var inputStream = new System.IO.MemoryStream())
      {
        versionSourceDocument.Body.Read().CopyTo(inputStream);
        receiverDocument.CreateVersionFrom(inputStream, versionSourceDocument.AssociatedApplication.Extension);
      }
      return receiverDocument.Versions.Any();
    }
    
    
    /// <summary>
    /// Копирование связей документа.
    /// </summary>
    /// <param name="sourceDocument">Документ-источник.</param>
    /// <param name="receiverDocument">Документ-приемник.</param>
    public static void CopyDocumentRelations(Sungero.Docflow.IOfficialDocument sourceDocument, Sungero.Docflow.IOfficialDocument receiverDocument)
    {
      var relatedDocuments = sourceDocument.Relations.GetRelated().Union(sourceDocument.Relations.GetRelatedFrom()).Where(d => d.HasVersions);
      foreach (var doc in relatedDocuments)
      {
        receiverDocument.Relations.Add(Sungero.Docflow.PublicConstants.Module.AddendumRelationName, doc);
        if (Sungero.Docflow.Addendums.Is(doc))
          Sungero.Docflow.Addendums.As(doc).LeadingDocument = receiverDocument;
      }
    }
    
    /// <summary>
    /// Копирование подписей документа.
    /// </summary>
    /// <param name="sourceDocument">Документ-источник.</param>
    /// <param name="receiverDocument">Документ-приемник.</param>
    public static void CopyDocumentSignatures(Sungero.Docflow.IOfficialDocument sourceDocument, Sungero.Docflow.IOfficialDocument receiverDocument)
    {
      foreach (var signature in Signatures.Get(sourceDocument.LastVersion).Where(s => s.SignCertificate != null))
      {
        var signatureText = signature.GetDataSignature();
        try
        {
          Signatures.Import(receiverDocument, signature.SignatureType, signature.SignatoryFullName, signatureText, receiverDocument.LastVersion);
        }
        catch (Exception ex)
        {
          Logger.Error(ex.Message, ex);
        }
      }
    }
    
    /// <summary>
    /// Удаление версий и пометка документа как устаревшего.
    /// </summary>
    /// <param name="sourceDocument">Документ-источник.</param>
    /// <param name="receiverDocument">Документ-приемник.</param>
    public static void MarkDocumentAsObsolete(Sungero.Docflow.IOfficialDocument document)
    {
      var typeRelations = new List<string>() {Sungero.Docflow.PublicConstants.Module.CorrespondenceRelationName, Sungero.Docflow.PublicConstants.Module.ResponseRelationName,
        Sungero.Docflow.PublicConstants.Module.SimpleRelationName, Sungero.Docflow.PublicConstants.Module.AddendumRelationName};
      
      document.LifeCycleState = Sungero.Docflow.OfficialDocument.LifeCycleState.Obsolete;
      if (document.HasRelations)
      {
        var relatedDocuments = document.Relations.GetRelated();
        foreach (var relatedDocument in relatedDocuments)
        {
          foreach(var typeRelation in typeRelations)
          {
            try
            {
              document.Relations.Remove(typeRelation, relatedDocument);
            }
            catch
            {
              continue;
            }
          }
        }
      }
      var deleteVersionAsynch = AsyncHandlers.DeleteVersionsByDocumentId.Create();
      deleteVersionAsynch.DocumentId = document.Id;
      deleteVersionAsynch.ExecuteAsync();
    }
    
    /// <summary>
    /// Проверка наличия регистратора входящего документа в нашей организации.
    /// </summary>
    /// <param name="businessUnit">Наша организация.</param>
    /// <param name="isRequest">Признак поиск регистратора в настройках обращений.</param>
    /// <returns>Регистратор найден?</returns>
    [Remote]
    public static bool ExistRegistrarForDocument(Sungero.Company.IBusinessUnit businessUnit, bool isRequest)
    {
      var existRegistrar = false;
      if (isRequest)
        existRegistrar = GD.CitizenRequests.CitizenRequestSettings.GetAll(x => Equals(x.BusinessUnit, businessUnit) &&
                                                                          x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active && x.RequestsRegistrators.Any()).Any();
      else
      {
        var documentKind = Sungero.Docflow.DocumentKinds.GetAll(x => x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active &&
                                                                x.DocumentType.DocumentTypeGuid == Sungero.RecordManagement.PublicConstants.Module.IncomingLetterGuid &&
                                                                x.IsDefault == true).FirstOrDefault();
        existRegistrar = Sungero.Docflow.PublicFunctions.Module.Remote.GetRegistrationSettings(Sungero.Docflow.RegistrationSetting.SettingType.Registration,
                                                                                               businessUnit, documentKind, Sungero.Company.Departments.Null).
          Where(x => x.DocumentRegister.Status == Sungero.CoreEntities.DatabookEntry.Status.Active && x.DocumentRegister.RegistrationGroup.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).Any();
      }
      return existRegistrar;
    }
  }
}