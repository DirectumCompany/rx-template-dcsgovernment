
// ==================================================================
// ModuleFunctions.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Functions
{
  internal static partial class Module
  {
    internal static class Remote
    {
      /// <redirect project="GD.DCSGovernmentModule.Server" type="GD.DCSGovernmentModule.Server.ModuleFunctions" />
      internal static global::Sungero.Docflow.IOfficialDocument RefreshDocument(global::Sungero.Docflow.IOfficialDocument document)
      {
        return global::Sungero.Domain.Shared.RemoteFunctionExecutor.ExecuteWithResult<global::Sungero.Docflow.IOfficialDocument>(
          global::System.Guid.Parse("15db5ebc-a567-4d1a-b9d6-53fedc2030c0"),
          "RefreshDocument(global::Sungero.Docflow.IOfficialDocument)", document);
      }
      /// <redirect project="GD.DCSGovernmentModule.Server" type="GD.DCSGovernmentModule.Server.ModuleFunctions" />
      internal static global::System.Boolean TryCompleteAssignmentSyncThenAsync(global::Sungero.Workflow.IAssignment assignment, global::Sungero.Core.Enumeration result)
      {
        return global::Sungero.Domain.Shared.RemoteFunctionExecutor.ExecuteWithResult<global::System.Boolean>(
          global::System.Guid.Parse("15db5ebc-a567-4d1a-b9d6-53fedc2030c0"),
          "TryCompleteAssignmentSyncThenAsync(global::Sungero.Workflow.IAssignment,global::Sungero.Core.Enumeration)", assignment, result);
      }
      /// <redirect project="GD.DCSGovernmentModule.Server" type="GD.DCSGovernmentModule.Server.ModuleFunctions" />
      internal static void ChangeDocumentsBody(global::Sungero.Docflow.IOfficialDocument firstDocument, global::Sungero.Docflow.IOfficialDocument secondDocument)
      {
      global::Sungero.Domain.Shared.RemoteFunctionExecutor.Execute(
          global::System.Guid.Parse("15db5ebc-a567-4d1a-b9d6-53fedc2030c0"),
          "ChangeDocumentsBody(global::Sungero.Docflow.IOfficialDocument,global::Sungero.Docflow.IOfficialDocument)", firstDocument, secondDocument);
      }
      /// <redirect project="GD.DCSGovernmentModule.Server" type="GD.DCSGovernmentModule.Server.ModuleFunctions" />
      internal static global::System.Boolean ExistRegistrarForDocument(global::Sungero.Company.IBusinessUnit businessUnit, global::System.Boolean isRequest)
      {
        return global::Sungero.Domain.Shared.RemoteFunctionExecutor.ExecuteWithResult<global::System.Boolean>(
          global::System.Guid.Parse("15db5ebc-a567-4d1a-b9d6-53fedc2030c0"),
          "ExistRegistrarForDocument(global::Sungero.Company.IBusinessUnit,global::System.Boolean)", businessUnit, isRequest);
      }

    }
    private static object GetFunctionsContainer()
    {
      return new global::GD.DCSGovernmentModule.Client.ModuleFunctions();
    }

    private static object GetFinalFunctionsContainer(global::Sungero.Metadata.ModuleProjectType projectType, global::Sungero.Metadata.ModuleMetadata finalModuleMetadatda)
    {
      var assemblyName = finalModuleMetadatda.GetAssemblyName(projectType);
      var moduleFunctionsType = global::System.Type.GetType(global::System.String.Format("{0}.{1}, {2}", finalModuleMetadatda.FunctionNamespace, "Module", assemblyName));
      var methodInfo = moduleFunctionsType.GetMethod("GetFunctionsContainer", global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Static);
      return global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(methodInfo, null, null);
    }
  }
}

// ==================================================================
// ModuleClientPublicFunctions.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Client
{
  public partial class ModuleClientPublicFunctions : global::GD.DCSGovernmentModule.Client.IModuleClientPublicFunctions
  {
  }
}

// ==================================================================
// ModuleWidgetHandlers.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Client
{
}

// ==================================================================
// ModuleHandlers.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernmentModule.Client
{

}

