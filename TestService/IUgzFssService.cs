using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace UGZ_FSS_Service
{
    [ServiceContract(Namespace = "www.parus.ru", ProtectionLevel = ProtectionLevel.None)]
    public interface IUgzFssService
    {
        // 1. Запрос на передачу извещений (изменений)
        [OperationContract]
        ElectronicInterchangeType SendNotificationChanges(NotificationChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationChangesResult(ElectronicInterchangeType ElectronicInterchange);
    }

    #region Общие типы

    [DataContract]
    public class ElectronicInterchangeType
    {
        // ГУИД электронного обмена
        string eiGUID;

        // ГУИД электронного обмена
        [DataMember(IsRequired = true, Order = 1)]
        public string EIGUID
        {
            get { return eiGUID; }
            set { eiGUID = value; }
        }
    }

    [DataContract(Name = "ProcessingStatusEnum")]
    public enum ProcessingStatusEnum
    {
        // 1 - В процессе обработки
        [EnumMember]
        DataDuringProcessing = 1,
        // 2 - Не пройден ФЛК - конечный статус (вина клиента)
        [EnumMember]
        FLCFails = 2,
        // 3 - Успешно обработан - конечный статус
        [EnumMember]
        SuccessfullyProcessed = 3,
        // 4 - Ошибка обработки - конечный статус
        [EnumMember]
        ProcessingError = 4
    }

    [DataContract]
    public class ProcessingStatusType
    {
        // Статус обработки
        ProcessingStatusEnum processingStatus;
        // Описание статуса обработки (текстовый комментарий)
        string statusComment = "";

        [DataMember]
        public ProcessingStatusEnum ProcessingStatus
        {
            get { return processingStatus; }
            set { processingStatus = value; }
        }

        [DataMember]
        public string StatusComment
        {
            get { return statusComment; }
            set { statusComment = value; }
        }
    }

    [CollectionDataContract(Name = "ProcessingResults",
                            ItemName = "ContractPayment",
                            KeyName = "BankDocGUID",
                            ValueName = "ProcessingStatus")]
    public class ProcessingResultsType : Dictionary<string, ProcessingStatusType> { };

    #endregion Общие типы

    #region 1. Запрос на передачу извещений (изменений)

    // Строка спецификации
    [DataContract]
    public class NotificationChangeSpec
    {
        // Начальная (максимальная) цена контракта
        double maxPrice;

        // Код валюты
        string currencyCode;

        // Цена за единицу измерения
        double price;

        // Общее количество
        int quantity;

        // Структура расходов
        string kbkCode;

        // Год
        DateTime year;

        // Сумма контракта на год
        double sum;

        // Начальная (максимальная) цена контракта
        [DataMember(IsRequired = true, Order = 1)]
        public double MaxPrice
        {
            get { return maxPrice; }
            set { maxPrice = value; }
        }

        // Код валют
        [DataMember(IsRequired = false, Order = 2)]
        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        // Цена за единицу измерения
        [DataMember(IsRequired = true, Order = 3)]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        // Общее количество
        [DataMember(IsRequired = true, Order = 4)]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        // Структура расходов
        [DataMember(IsRequired = true, Order = 5)]
        public string KbkCode
        {
            get { return kbkCode; }
            set { kbkCode = value; }
        }

        // Год
        [DataMember(IsRequired = true, Order = 6)]
        public DateTime Year
        {
            get { return year; }
            set { year = value; }
        }

        // Сумма контракта на год
        [DataMember(IsRequired = true, Order = 7)]
        public double Sum
        {
            get { return sum; }
            set { sum = value; }
        }
    }

    // Спецификация
    [CollectionDataContract(Name = "CustomerRequirements", ItemName = "CustomerRequirement", KeyName = "CodeKOZ", ValueName = "Requirement")]
    public class NotificationChangeSpecDictionary : Dictionary<string, NotificationChangeSpec> { };

    // Извещение(изменение)
    [DataContract]
    public class NotificationChangeType
    {
        // Код организации - Принадлежность/Заказчик
        string organizationCode;

        // Идентификатор документа в УГЗ
        string docGUID;

        // Документ. Дата
        DateTime docPublishDate;

        // Номер закупки
        string purchaseNumb;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;

        //Номер лота
        int lotNumber;

        //Сумма обеспечения извещения
        double applicationGuaranteAmount;

        //Сумма обеспечения контракта
        double contractGuaranteeAmount;

        //Реестровый номер план-графика
        string planNumber;

        //Номер позиции в плане-графике
        string positionNumber;

        //Номер изменения
        int modificationNumber;

        //Дата изменений
        DateTime modificationDate;

        //Описание изменений
        string info;

        //Спецификация
        NotificationChangeSpecDictionary specs;

        // Код организации - Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 1)]
        public string OrganizationCode
        {
            get { return organizationCode; }
            set { organizationCode = value; }
        }

        // Идентификатор документа в УГЗ
        [DataMember(IsRequired = true, Order = 2)]
        public string DocGUID
        {
            get { return docGUID; }
            set { docGUID = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 3)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Номер закупки
        [DataMember(IsRequired = true, Order = 4)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 5)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 6)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 7)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 8)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Номер лота
        [DataMember(IsRequired = true, Order = 9)]
        public int LotNumber
        {
            get { return lotNumber; }
            set { lotNumber = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 10)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 11)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 12)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 13)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 14)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 15)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 16)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 17)]
        public NotificationChangeSpecDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationChanges", ItemName = "NotificationChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationChangesType : Dictionary<string, NotificationChangeType> { };

    #endregion 1. Запрос на передачу извещений (изменений)
}
