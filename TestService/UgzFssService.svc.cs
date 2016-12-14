using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UGZ_FSS_Service
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IUgzFssService
    {
        public ElectronicInterchangeType SendNotificationChanges(NotificationChangesType ContractPayments)
        {
            return new ElectronicInterchangeType()
            {
                EIGUID = @"0BC6FF94-F1A0-4A86-ACE5-5A42FFB59475"
            };
        }

        public ProcessingResultsType NotificationChangesResult(ElectronicInterchangeType ElectronicInterchange)
        {
            return new ProcessingResultsType() 
            {
                {"2192E48F-FE89-43A5-9722-50804BBBEEEF", 
                    new ProcessingStatusType()
                    {
                        ProcessingStatus = ProcessingStatusEnum.SuccessfullyProcessed
                    }
                },
                {"2C49BA5B-9E8C-438C-9259-E1DBF52EB51F", 
                    new ProcessingStatusType()
                    {
                        ProcessingStatus = ProcessingStatusEnum.ProcessingError,
                        StatusComment = "КОСГУ отсутствует в справочнике"
                    }
                },
                {"EC90B048-E871-46F6-9A4F-DD32AE7A269E", 
                    new ProcessingStatusType()
                    {
                        ProcessingStatus = ProcessingStatusEnum.DataDuringProcessing,
                        StatusComment = "Банковский документ в обработке. Оператор работает с 10:00 до 15:00."
                    }
                }
            };
        }
    }
}
