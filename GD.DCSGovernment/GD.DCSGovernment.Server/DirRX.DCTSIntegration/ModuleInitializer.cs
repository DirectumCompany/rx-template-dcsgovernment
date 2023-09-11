using System;
using System.Collections.Generic;
using System.Linq;
using GD.DCSGovernmentModule;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;
using Sungero.Docflow;
using Sungero.Parties;
using Sungero.Company;

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  public partial class ModuleInitializer
  {
    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      base.Initializing(e);
      InitializationLogger.Debug("Create rule: Request");
      var department = GetDepartment(DirRX.DCTSIntegration.Resources.DepartmentSampleName);
      var correspondent = Companies.GetAll(c => c.Name == DirRX.DCTSIntegration.Resources.Correspondent).FirstOrDefault();
      var request = Sungero.Docflow.PublicFunctions.DocumentKind.GetNativeDocumentKind(GD.CitizenRequests.PublicConstants.Module.AppealKind);
      CreateRuleRequest(DCTSIntegration.Resources.RequestRuleName, DCTSIntegration.Resources.RequestLineToSystem,
                        DirRX.DCTSIntegration.ProcessingRuleBase.CaptureService.Mail, request, true, correspondent, true, department);
    }
    
    /// <summary>
    /// Создать правило обработки обращений.
    /// </summary>
    /// <param name="name">Имя.</param>
    /// <param name="line">Линия.</param>
    /// <param name="captureService">Модуль ввода.</param>
    /// <param name="documentKind">Вид документа.</param>
    /// <param name="fillFromSubject">Заполнять из темы письма.</param>
    /// <param name="correspondent">Корреспондент.</param>
    /// <param name="isAutoCalcCor">Определять по эл. почте.</param>
    /// <param name="department">Подразделение.</param>
    /// <returns>Правило.</returns>
    private IProcessingRuleRequest CreateRuleRequest(string name, string line, Enumeration? captureService, IDocumentKind documentKind, bool? fillFromSubject,
                                                                    ICounterparty correspondent, bool? isAutoCalcCor, IDepartment department)
    {   
      var rule = ProcessingRuleRequests.GetAll().FirstOrDefault(p => p.Name == name);
      try
      {
        if (rule == null)
        {
          rule = ProcessingRuleRequests.Create();
          rule.Name = name;
          rule.Line = line;
          rule.CaptureService = captureService;
          rule.DocumentKind = documentKind;
          rule.FillFromSubject = fillFromSubject;
          rule.Correspondent = GD.GovernmentSolution.PublicFunctions.Person.Remote.GetAnonymousPerson();
          rule.IsAutoCalcCorrespondent = isAutoCalcCor;
          rule.Department = department;
          rule.Save();
          InitializationLogger.DebugFormat("ProcessingRuleRequests \"{0}\" created", name);
        }
      }
      catch (Exception ex)
      {
        InitializationLogger.DebugFormat("Error: ProcessingRuleRequests \"{0}\" {1}{2}", name, Environment.NewLine, ex.Message);
      }
      return rule;    
    }
  }
}