using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FSS_UGZ_Service
{
    public class UgzFssService : IUgzFssService
    {
        public ElectronicInterchangeType SendNotificationChanges(NotificationChangesType ContractPayments)
        {
            throw new NotImplementedException();
        }

        public ProcessingResultsType NotificationChangesResult(ElectronicInterchangeType ElectronicInterchange)
        {
            throw new NotImplementedException();
        }
    }
}
