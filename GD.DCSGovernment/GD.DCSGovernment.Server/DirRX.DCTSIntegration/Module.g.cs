
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

namespace GD.DCSGovernment.Module.DCTSIntegration.Functions
{
  internal static partial class Module
  {
    /// <redirect project="GD.DCSGovernment.Server" type="GD.DCSGovernment.Module.DCTSIntegration.Server.ModuleFunctions" />
    [global::Sungero.Core.ExpressionElementAttribute("Регистраторы входящего документа", "Возвращает регистраторов входящего докумета", "Задача на регистрацию")]
    internal static global::System.Collections.Generic.List<global::Sungero.CoreEntities.IRecipient> GetClerkForIncommingDocument(global::Sungero.Docflow.IIncomingDocumentBase document, global::GD.DCSGovernmentModule.IDocumentRegisterTask task)
    {
      var __moduleId = new global::System.Guid("ead5cbd0-2ebd-4a37-91a1-ec3879648e9c");
      var __finalModuleMetadatda = global::Sungero.Metadata.MetadataExtension.GetFinal(global::Sungero.Metadata.Services.MetadataSearcher.FindModuleMetadata(__moduleId) ?? global::Sungero.Metadata.Services.MetadataSearcher.FindLayerModuleMetadata(__moduleId));
      var __funcNamespace = "Server" == "ClientBase" ? __finalModuleMetadatda.ClientNamespace : __finalModuleMetadatda.ServerNamespace;
      var __typeName = __funcNamespace + ".ModuleFunctions, GD.DCSGovernment.Server";
      var __type = global::Sungero.Metadata.Services.AppliedTypesManager.Instance.Resolve(__typeName);
      if (__type != null)
      {
        var __instance = global::Sungero.Metadata.Services.AppliedTypesManager.CreateTypeInstance(__type);
        var __methodInfo = __type.GetMethod("GetClerkForIncommingDocument", new global::System.Type[]{typeof(global::Sungero.Docflow.IIncomingDocumentBase), typeof(global::GD.DCSGovernmentModule.IDocumentRegisterTask)});
        return (global::System.Collections.Generic.List<global::Sungero.CoreEntities.IRecipient>)global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(__methodInfo, __instance, new object[]{document, task});
      }
      else
      {
        return ((global::GD.DCSGovernment.Module.DCTSIntegration.Server.ModuleFunctions)GetFinalFunctionsContainer(global::Sungero.Metadata.ModuleProjectType.Server, __finalModuleMetadatda)).GetClerkForIncommingDocument(document, task);
      }
    }

    private static object GetFunctionsContainer()
    {
      return new global::GD.DCSGovernment.Module.DCTSIntegration.Server.ModuleFunctions();
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
namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  public partial class ModuleFunctions: global::DirRX.DCTSIntegration.Server.ModuleFunctions
  {

  }
}

// ==================================================================
// ModuleInitializationInstantiation.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Module.DCTSIntegration.ModuleInitialization
{
  internal static partial class Module
  {
      /// <redirect project="GD.DCSGovernment.Server" type="GD.DCSGovernment.Module.DCTSIntegration.Server.ModuleInitializer" />
      internal static void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
      {
        var __moduleId = new global::System.Guid("ead5cbd0-2ebd-4a37-91a1-ec3879648e9c");
        var __finalModuleMetadatda = global::Sungero.Metadata.MetadataExtension.GetFinal(global::Sungero.Metadata.Services.MetadataSearcher.FindModuleMetadata(__moduleId) ?? global::Sungero.Metadata.Services.MetadataSearcher.FindLayerModuleMetadata(__moduleId));
        var __funcNamespace = "Server" == "ClientBase" ? __finalModuleMetadatda.ClientNamespace : __finalModuleMetadatda.ServerNamespace;
        var __typeName = __funcNamespace + ".ModuleFunctions, GD.DCSGovernment.Server";
        var __type = global::Sungero.Metadata.Services.AppliedTypesManager.Instance.Resolve(__typeName);
        if (__type != null)
        {
          var __instance = global::Sungero.Metadata.Services.AppliedTypesManager.CreateTypeInstance(__type);
          var __methodInfo = __type.GetMethod("Initializing", new global::System.Type[]{typeof(Sungero.Domain.ModuleInitializingEventArgs)});
      global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(__methodInfo, __instance, new object[]{e});
        }
        else
        {
      ((global::GD.DCSGovernment.Module.DCTSIntegration.Server.ModuleInitializer)GetFinalFunctionsContainer(global::Sungero.Metadata.ModuleProjectType.Server, __finalModuleMetadatda)).Initializing(e);
        }
      }


    private static object GetFunctionsContainer()
    {
      return new global::GD.DCSGovernment.Module.DCTSIntegration.Server.ModuleInitializer();
    }

    private static object GetFinalFunctionsContainer(global::Sungero.Metadata.ModuleProjectType projectType, global::Sungero.Metadata.ModuleMetadata finalModuleMetadatda)
    {
      var assemblyName = finalModuleMetadatda.GetAssemblyName(projectType);
      var moduleFunctionsType = global::System.Type.GetType(global::System.String.Format("{0}.{1}, {2}", finalModuleMetadatda.ModuleInitializationNamespace, "Module", assemblyName));
      var methodInfo = moduleFunctionsType.GetMethod("GetFunctionsContainer", global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Static);
      return global::CommonLibrary.ReflectionExtensions.ReflectionInvoke(methodInfo, null, null);
    }

  }
}


// ==================================================================
// ModuleJobs.g.cs
// ==================================================================

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  public partial class ModuleJobs : global::DirRX.DCTSIntegration.Server.ModuleJobs
  {

  }
}

// ==================================================================
// ModuleAsyncHandlers.g.cs
// ==================================================================

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  public partial class ModuleAsyncHandlers : global::DirRX.DCTSIntegration.Server.ModuleAsyncHandlers
  {

  }
}

// ==================================================================
// ModuleServerPublicFunctions.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  public partial class ModuleServerPublicFunctions : global::GD.DCSGovernment.Module.DCTSIntegration.Server.IModuleServerPublicFunctions
  {
  }
}

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  public partial class ModuleServerPublicFunctions : global::GD.DCSGovernment.Module.DCTSIntegration.Server.IModuleServerPublicFunctions
  {

 

  }
}

// ==================================================================
// ModuleServerInitializationFunctions.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
  public partial class ModuleServerInitializationFunctions : global::GD.DCSGovernment.Module.DCTSIntegration.Server.IModuleServerInitializationFunctions
  {
  }
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

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
}

// ==================================================================
// ModuleEventArgs.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
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

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
}

// ==================================================================
// ModuleBlockHandlers.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Module.DCTSIntegration.Server.DCTSIntegrationBlocks
{
}

namespace Sungero.Workflow
{
}

// ==================================================================
// ModuleQueries.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Module.DCTSIntegration.Queries
{
  public class Module
  {
    private static global::Sungero.Domain.SqlQueryResolver resolver = new global::Sungero.Domain.SqlQueryResolver("GD.DCSGovernment.Server.DirRX.DCTSIntegration.ModuleQueries.xml", System.Reflection.Assembly.GetExecutingAssembly());
  }
}

// ==================================================================
// ModuleInitializer.g.cs
// ==================================================================

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GD.DCSGovernment.Module.DCTSIntegration.Server
{
   public  partial class ModuleInitializer:  global::DirRX.DCTSIntegration.Server.ModuleInitializer 
  {
  }
}

