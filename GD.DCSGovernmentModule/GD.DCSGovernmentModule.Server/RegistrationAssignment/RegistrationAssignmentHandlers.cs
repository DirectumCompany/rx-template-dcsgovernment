using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.DCSGovernmentModule.RegistrationAssignment;

namespace GD.DCSGovernmentModule
{
  partial class RegistrationAssignmentBusinessUnitPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> BusinessUnitFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      var businessUnit = _obj.DocumentGroup.IncomingDocumentBases.FirstOrDefault().BusinessUnit;
      return query.Where(x => !Equals(x, businessUnit));
    }
  }

  partial class RegistrationAssignmentRegistrarPropertyFilteringServerHandler<T>
  {

    public virtual IQueryable<T> RegistrarFiltering(IQueryable<T> query, Sungero.Domain.PropertyFilteringEventArgs e)
    {
      var registrationIncomingDocumentRole = Roles.GetAll(r => r.Sid == Sungero.Docflow.PublicConstants.Module.RoleGuid.RegistrationIncomingDocument).FirstOrDefault();
      var registrationIncomingDocument = Sungero.Company.BusinessUnits.GetAllUsersInGroup(registrationIncomingDocumentRole);
      if (Sungero.Company.Employees.Current != null)
      {        
        return query.Where(x => x.Department != null &&
                           Equals(x.Department.BusinessUnit, Sungero.Company.Employees.Current.Department.BusinessUnit) &&
                           registrationIncomingDocument.Contains(x));
      }
      else
        return query.Where(x => registrationIncomingDocument.Contains(x) && !Equals(Sungero.Company.Employees.Current, x));
    }
  }

}