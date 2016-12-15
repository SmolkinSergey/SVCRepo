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
        // 1.1. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЭА»
        [OperationContract]
        ElectronicInterchangeType SendNotificationEAChanges(NotificationEAChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationEAChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.2. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ОК»
        [OperationContract]
        ElectronicInterchangeType SendNotificationOKChanges(NotificationOKChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationOKChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.3. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЕП»
        [OperationContract]
        ElectronicInterchangeType SendNotificationEPChanges(NotificationEPChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationEPChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.4. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-Д»
        [OperationContract]
        ElectronicInterchangeType SendNotificationOKDChanges(NotificationOKDChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationOKDChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.5. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-ОУ»
        [OperationContract]
        ElectronicInterchangeType SendNotificationOKOUChanges(NotificationOKOUChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationOKOUChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.6. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗК»
        [OperationContract]
        ElectronicInterchangeType SendNotificationZKChanges(NotificationZKChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationZKChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.7. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗП»
        [OperationContract]
        ElectronicInterchangeType SendNotificationZPChanges(NotificationZPChangesType ContractPayments);
        [OperationContract]
        ProcessingResultsType NotificationZPChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 2. Запрос на передачу государственных контрактов
        [OperationContract]
        ElectronicInterchangeType SendContractTransfers(SendContractTransfersType ContractPayments);
        [OperationContract]
        ProcessingResultsType ContractTransfersResult(ElectronicInterchangeType ElectronicInterchange);
        // 3. Запрос на передачу информации о расторжении государственных контрактов
        //[OperationContract]
        //ElectronicInterchangeType SendContractTransfers(SendContractTransfersType ContractPayments);
        //[OperationContract]
        //ProcessingResultsType ContractTransfersResult(ElectronicInterchangeType ElectronicInterchange);
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

    #region 1. Атрибуты извещения (изменения)

    // Строка спецификации с суммой
    [DataContract]
    public class NotificationChangeSpecWithSumm
    {
        // Начальная (максимальная) цена контракта
        double maxPrice;

        // Номер лота
        int lotNumber;
        
        // Код валюты
        string currencyCode;

        // Цена за единицу измерения
        double price;

        // Номенклатура
        string codeKOZ;

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

        // Номер лота
        [DataMember(IsRequired = true, Order = 2)]
        public int LotNumber
        {
            get { return lotNumber; }
            set { lotNumber = value; }
        }

        // Код валют
        [DataMember(IsRequired = false, Order = 3)]
        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        // Цена за единицу измерения
        [DataMember(IsRequired = true, Order = 4)]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        // Номенклатура
        [DataMember(IsRequired = true, Order = 5)]
        public string CodeKOZ
        {
            get { return codeKOZ; }
            set { codeKOZ = value; }
        }

        // Общее количество
        [DataMember(IsRequired = true, Order = 6)]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        // Структура расходов
        [DataMember(IsRequired = true, Order = 7)]
        public string KbkCode
        {
            get { return kbkCode; }
            set { kbkCode = value; }
        }

        // Год
        [DataMember(IsRequired = true, Order = 8)]
        public DateTime Year
        {
            get { return year; }
            set { year = value; }
        }

        // Сумма контракта на год
        [DataMember(IsRequired = true, Order = 9)]
        public double Sum
        {
            get { return sum; }
            set { sum = value; }
        }
    }

    // Спецификация с суммой
    [CollectionDataContract(Name = "CustomerRequirements", ItemName = "CustomerRequirement", ValueName = "Requirement")]
    public class NotificationChangeSpecWithSumDictionary : List<NotificationChangeSpecWithSumm> { };

    // Строка спецификации без суммы
    [DataContract]
    public class NotificationChangeSpecWithoutSumm
    {
        // Начальная (максимальная) цена контракта
        double maxPrice;

        // Номер лота
        int lotNumber;

        // Код валюты
        string currencyCode;

        // Цена за единицу измерения
        double price;

        // Номенклатура
        string codeKOZ;

        // Общее количество
        int quantity;

        // Структура расходов
        string kbkCode;

        // Начальная (максимальная) цена контракта
        [DataMember(IsRequired = true, Order = 1)]
        public double MaxPrice
        {
            get { return maxPrice; }
            set { maxPrice = value; }
        }

        // Номер лота
        [DataMember(IsRequired = true, Order = 2)]
        public int LotNumber
        {
            get { return lotNumber; }
            set { lotNumber = value; }
        }

        // Код валют
        [DataMember(IsRequired = false, Order = 3)]
        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        // Цена за единицу измерения
        [DataMember(IsRequired = true, Order = 4)]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        // Номенклатура
        [DataMember(IsRequired = true, Order = 5)]
        public string CodeKOZ
        {
            get { return codeKOZ; }
            set { codeKOZ = value; }
        }

        // Общее количество
        [DataMember(IsRequired = true, Order = 6)]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        // Структура расходов
        [DataMember(IsRequired = true, Order = 7)]
        public string KbkCode
        {
            get { return kbkCode; }
            set { kbkCode = value; }
        }
    }

    // Спецификация без суммы
    [CollectionDataContract(Name = "CustomerRequirements", ItemName = "CustomerRequirement", ValueName = "Requirement")]
    public class NotificationChangeSpecWithoutSumDictionary : List<NotificationChangeSpecWithoutSumm> { };

    #region 1.1. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЭА»
    
    // Извещение(изменение)
    [DataContract]
    public class NotificationEAChangeType
    {
        // Номер закупки
        string purchaseNumb;

        // Документ. Дата
        DateTime docPublishDate;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Организация-Принадлежность/Заказчик
        string regNum;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;
        
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
        NotificationChangeSpecWithSumDictionary specs;
        
        // Номер закупки
        [DataMember(IsRequired = true, Order = 1)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 2)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 3)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 4)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 5)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 6)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 8)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 9)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 10)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 11)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 12)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 13)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 14)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 15)]
        public NotificationChangeSpecWithSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationEAChanges", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationEAChangesType : Dictionary<string, NotificationEAChangeType> { };

    #endregion 1.1. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЭА»

    #region 1.2. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ОК»

    // Извещение(изменение)
    [DataContract]
    public class NotificationOKChangeType
    {
        // Номер закупки
        string purchaseNumb;

        // Документ. Дата
        DateTime docPublishDate;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Организация-Принадлежность/Заказчик
        string regNum;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;

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
        NotificationChangeSpecWithSumDictionary specs;

        // Номер закупки
        [DataMember(IsRequired = true, Order = 1)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 2)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 3)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 4)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 5)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 6)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 8)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 9)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 10)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 11)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 12)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 13)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 14)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 15)]
        public NotificationChangeSpecWithSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationEAChanges", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationOKChangesType : Dictionary<string, NotificationOKChangeType> { };

    #endregion 1.2. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ОК»

    #region 1.3. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЕП»

    // Извещение(изменение)
    [DataContract]
    public class NotificationEPChangeType
    {
        // Номер закупки
        string purchaseNumb;

        // Документ. Дата
        DateTime docPublishDate;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Организация-Принадлежность/Заказчик
        string regNum;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;

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
        NotificationChangeSpecWithSumDictionary specs;

        // Номер закупки
        [DataMember(IsRequired = true, Order = 1)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 2)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 3)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 4)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 5)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 6)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 8)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 9)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 10)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 11)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 12)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 13)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 14)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 15)]
        public NotificationChangeSpecWithSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationEAChanges", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationEPChangesType : Dictionary<string, NotificationEPChangeType> { };

    #endregion 1.3. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЕП»

    #region 1.4. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-Д»

    // Извещение(изменение)
    [DataContract]
    public class NotificationOKDChangeType
    {
        // Номер закупки
        string purchaseNumb;

        // Документ. Дата
        DateTime docPublishDate;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Организация-Принадлежность/Заказчик
        string regNum;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;

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
        NotificationChangeSpecWithSumDictionary specs;

        // Номер закупки
        [DataMember(IsRequired = true, Order = 1)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 2)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 3)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 4)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 5)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 6)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 8)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 9)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 10)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 11)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 12)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 13)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 14)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 15)]
        public NotificationChangeSpecWithSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationEAChanges", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationOKDChangesType : Dictionary<string, NotificationOKDChangeType> { };

    #endregion 1.4. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-Д»

    #region 1.5. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-ОУ»

    // Извещение(изменение)
    [DataContract]
    public class NotificationOKOUChangeType
    {
        // Номер закупки
        string purchaseNumb;

        // Документ. Дата
        DateTime docPublishDate;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Организация-Принадлежность/Заказчик
        string regNum;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;

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
        NotificationChangeSpecWithSumDictionary specs;

        // Номер закупки
        [DataMember(IsRequired = true, Order = 1)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 2)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 3)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 4)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 5)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 6)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 8)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 9)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 10)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 11)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 12)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 13)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 14)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 15)]
        public NotificationChangeSpecWithSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationEAChanges", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationOKOUChangesType : Dictionary<string, NotificationOKOUChangeType> { };

    #endregion 1.5. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-ОУ»

    #region 1.6. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗК»

    // Извещение(изменение)
    [DataContract]
    public class NotificationZKChangeType
    {
        // Номер закупки
        string purchaseNumb;

        // Документ. Дата
        DateTime docPublishDate;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Организация-Принадлежность/Заказчик
        string regNum;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;

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
        NotificationChangeSpecWithSumDictionary specs;

        // Номер закупки
        [DataMember(IsRequired = true, Order = 1)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 2)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 3)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 4)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 5)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 6)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 8)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 9)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 10)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 11)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 12)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 13)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 14)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 15)]
        public NotificationChangeSpecWithSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationEAChanges", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationZKChangesType : Dictionary<string, NotificationZKChangeType> { };

    #endregion 1.6. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗК»

    #region 1.7. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗП»

    // Извещение(изменение)
    [DataContract]
    public class NotificationZPChangeType
    {
        // Номер закупки
        string purchaseNumb;

        // Документ. Дата
        DateTime docPublishDate;

        // Описание объекта закупки 
        string purchaseObjectInfo;

        // Организация-Принадлежность/Заказчик
        string regNum;

        // Ссылка для скачивания документов
        string url;

        // Способ размещения заказа
        string placingWayCode;

        // Дата проведения аукциона
        DateTime biddingDate;

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
        NotificationChangeSpecWithSumDictionary specs;

        // Номер закупки
        [DataMember(IsRequired = true, Order = 1)]
        public string PurchaseNumb
        {
            get { return purchaseNumb; }
            set { purchaseNumb = value; }
        }

        // Документ. Дата
        [DataMember(IsRequired = true, Order = 2)]
        public DateTime DocPublishDate
        {
            get { return docPublishDate; }
            set { docPublishDate = value; }
        }

        // Описание объекта закупки 
        [DataMember(IsRequired = true, Order = 3)]
        public string PurchaseObjectInfo
        {
            get { return purchaseObjectInfo; }
            set { purchaseObjectInfo = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 4)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        //Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 5)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        //Способ размещения заказа
        [DataMember(IsRequired = true, Order = 6)]
        public string PlacingWayCode
        {
            get { return placingWayCode; }
            set { placingWayCode = value; }
        }

        //Дата проведения аукциона
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime BiddingDate
        {
            get { return biddingDate; }
            set { biddingDate = value; }
        }

        //Сумма обеспечения извещения
        [DataMember(IsRequired = true, Order = 8)]
        public double ApplicationGuaranteAmount
        {
            get { return applicationGuaranteAmount; }
            set { applicationGuaranteAmount = value; }
        }

        //Сумма обеспечения контракта
        [DataMember(IsRequired = true, Order = 9)]
        public double ContractGuaranteeAmount
        {
            get { return contractGuaranteeAmount; }
            set { contractGuaranteeAmount = value; }
        }

        //Реестровый номер план-графика
        [DataMember(IsRequired = false, Order = 10)]
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        //Номер позиции в плане-графике
        [DataMember(IsRequired = false, Order = 11)]
        public string PositionNumber
        {
            get { return positionNumber; }
            set { positionNumber = value; }
        }

        //Номер изменения
        [DataMember(IsRequired = false, Order = 12)]
        public int ModificationNumber
        {
            get { return modificationNumber; }
            set { modificationNumber = value; }
        }

        //Дата изменений
        [DataMember(IsRequired = false, Order = 13)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        //Описание изменений
        [DataMember(IsRequired = false, Order = 14)]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        //Спецификация
        [DataMember(IsRequired = true, Order = 15)]
        public NotificationChangeSpecWithSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationEAChanges", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationZPChangesType : Dictionary<string, NotificationZPChangeType> { };

    #endregion 1.7. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗП»

    #endregion 1. Атрибуты извещения (изменения)

    #region 2. Запрос на передачу государственных контрактов

    // Запрос на передачу государственных контрактов
    [DataContract]
    public class SendContractTransferType
    {
        // Идентификатор документа в УГЗ
        string contractId;

        // Документ. Внешний номер /Документ-основание. Номер
        string number;

        // Номер изменений
        string versionNumber;

        // Документ. Дата. /Документ-основание. Дата 
        DateTime signDate;

        // Номер реестровой записи
        string regnum;

        // Дата начала действия
        DateTime startDate;

        // Дата окончания действия
        DateTime endDate;

        // Печатная форма(ссылка)
        string printFormUrl;

        // Способ размещения заказа
        string placing;

        // Номер извещения о проведении торгов
        string notificationNumber;

        // Номер лота
        string lotNumber;

        // Основание заключения контракта с единственным поставщиком
        string singleCustomerReason;

        // Код валюты
        int currencyCode;

        // Суммы по контракту
        double contractSum;

        // Организация-Принадлежность/Заказчик
        string customerRegNum;

        // Поставщик. 
        string supplierName;

        // Поставщик. ИНН
        string supplierINN;

        // Поставщик.  КПП
        string supplierKPP;

        // Поставщик. Код OKTMO
        string supplierOKTMO;

        // Поставщик. Код ОГРН
        string supplierOGRN;

        // Поставщик. Расчетный счет поставщик
        string lagnSettlementAccount;

        // Поставщик. Лицевой счет
        string lagnPersonalAccount;

        // Поставщик. БИК банка
        string lagnBIKBank;

        // Поставщик. Корреспондентский счет
        string lagnCorrespondentAccount;

        // Поставщик. Фамилия
        string personLastName;

        // Поставщик. Имя
        string personFirstName;

        // Поставщик. Отчество
        string personMiddleName;

        // Поставщик. ИНН
        string personINN;

        // Поставщик. Расчетный счет поставщика (Расчетный счет УФК поставщика)
        string pagnSettlementAccount;

        // Поставщик. БИК банка
        string pagnBIKBank;

        // Поставщик. Корреспондентский счет
        string pagnCorrespondentAccount;

        // Номенклатура
        string productsCodeKOZ;

        // Код ОКЕИ. Номенклатура
        string productsOKEI;

        // Цена за единицу 
        double productsPrice;

        // Количество
        int productsQantity;

        // Сумма в валюте
        double productsSum;

        // Структура расходов
        string paymentKBK;

        // Дата 
        string paymentYear;

        // Дата 
        string paymentMonth;

        // Сумма. По контракту
        double paymentSum;

        // Описание внесенных изменений
        string description;

        // Причины изменений условий контракта
        string reasonCode;

        // Реквизиты документа-основания
        string reasonDocument;

        // Дата окончания обеспечения
        DateTime bankGuaranteeReturn;

        // Идентификатор документа в УГЗ
        [DataMember(IsRequired = true, Order = 1)]
        public string ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }

        // Документ. Внешний номер /Документ-основание. Номер
        [DataMember(IsRequired = true, Order = 2)]
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        // Номер изменений
        [DataMember(IsRequired = false, Order = 3)]
        public string VersionNumber
        {
            get { return versionNumber; }
            set { versionNumber = value; }
        }

        // Документ. Дата. /Документ-основание. Дата 
        [DataMember(IsRequired = true, Order = 4)]
        public DateTime SignDate
        {
            get { return signDate; }
            set { signDate = value; }
        }

        // Номер реестровой записи
        [DataMember(IsRequired = false, Order = 5)]
        public string Regnum
        {
            get { return regnum; }
            set { regnum = value; }
        }

        // Дата начала действия
        [DataMember(IsRequired = true, Order = 6)]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        // Дата окончания действия
        [DataMember(IsRequired = false, Order = 7)]
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        // Печатная форма(ссылка)
        [DataMember(IsRequired = false, Order = 8)]
        public string PrintFormUrl
        {
            get { return printFormUrl; }
            set { printFormUrl = value; }
        }

        // Способ размещения заказа
        [DataMember(IsRequired = false, Order = 9)]
        public string Placing
        {
            get { return placing; }
            set { placing = value; }
        }

        // Номер извещения о проведении торгов
        [DataMember(IsRequired = false, Order = 10)]
        public string NotificationNumber
        {
            get { return notificationNumber; }
            set { notificationNumber = value; }
        }

        // Номер лота
        [DataMember(IsRequired = false, Order = 11)]
        public string LotNumber
        {
            get { return lotNumber; }
            set { lotNumber = value; }
        }

        // Основание заключения контракта с единственным поставщиком
        [DataMember(IsRequired = false, Order = 12)]
        public string SingleCustomerReason
        {
            get { return singleCustomerReason; }
            set { singleCustomerReason = value; }
        }

        // Код валюты
        [DataMember(IsRequired = false, Order = 13)]
        public int CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        // Суммы по контракту
        [DataMember(IsRequired = true, Order = 14)]
        public double ContractSum
        {
            get { return contractSum; }
            set { contractSum = value; }
        }

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 15)]
        public string CustomerRegNum
        {
            get { return customerRegNum; }
            set { customerRegNum = value; }
        }

        // Поставщик. 
        [DataMember(IsRequired = true, Order = 16)]
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        // Поставщик. ИНН
        [DataMember(IsRequired = true, Order = 17)]
        public string SupplierINN
        {
            get { return supplierINN; }
            set { supplierINN = value; }
        }

        // Поставщик.  КПП
        [DataMember(IsRequired = false, Order = 18)]
        public string SupplierKPP
        {
            get { return supplierKPP; }
            set { supplierKPP = value; }
        }

        // Поставщик. Код OKTMO
        [DataMember(IsRequired = true, Order = 19)]
        public string SupplierOKTMO
        {
            get { return supplierOKTMO; }
            set { supplierOKTMO = value; }
        }

        // Поставщик. Код ОГРН
        [DataMember(IsRequired = true, Order = 20)]
        public string SupplierOGRN
        {
            get { return supplierOGRN; }
            set { supplierOGRN = value; }
        }

        // Поставщик. Расчетный счет поставщик
        [DataMember(IsRequired = true, Order = 21)]
        public string LagnSettlementAccount
        {
            get { return lagnSettlementAccount; }
            set { lagnSettlementAccount = value; }
        }

        // Поставщик. Лицевой счет
        [DataMember(IsRequired = true, Order = 22)]
        public string LagnPersonalAccount
        {
            get { return lagnPersonalAccount; }
            set { lagnPersonalAccount = value; }
        }

        // Поставщик. БИК банка
        [DataMember(IsRequired = true, Order = 23)]
        public string LagnBIKBank
        {
            get { return lagnBIKBank; }
            set { lagnBIKBank = value; }
        }

        // Поставщик. Корреспондентский счет
        [DataMember(IsRequired = false, Order = 24)]
        public string LagnCorrespondentAccount
        {
            get { return lagnCorrespondentAccount; }
            set { lagnCorrespondentAccount = value; }
        }

        // Поставщик. Фамилия
        [DataMember(IsRequired = true, Order = 25)]
        public string PersonLastName
        {
            get { return personLastName; }
            set { personLastName = value; }
        }

        // Поставщик. Имя
        [DataMember(IsRequired = true, Order = 26)]
        public string PersonFirstName
        {
            get { return personFirstName; }
            set { personFirstName = value; }
        }

        // Поставщик. Отчество
        [DataMember(IsRequired = true, Order = 27)]
        public string PersonMiddleName
        {
            get { return personMiddleName; }
            set { personMiddleName = value; }
        }

        // Поставщик. ИНН
        [DataMember(IsRequired = true, Order = 28)]
        public string PersonINN
        {
            get { return personINN; }
            set { personINN = value; }
        }

        // Поставщик. Расчетный счет поставщика (Расчетный счет УФК поставщика)
        [DataMember(IsRequired = true, Order = 29)]
        public string PagnSettlementAccount
        {
            get { return pagnSettlementAccount; }
            set { pagnSettlementAccount = value; }
        }

        // Поставщик. БИК банка
        [DataMember(IsRequired = true, Order = 30)]
        public string PagnBIKBank
        {
            get { return pagnBIKBank; }
            set { pagnBIKBank = value; }
        }

        // Поставщик. Корреспондентский счет
        [DataMember(IsRequired = false, Order = 31)]
        public string PagnCorrespondentAccount
        {
            get { return pagnCorrespondentAccount; }
            set { pagnCorrespondentAccount = value; }
        }

        // Номенклатура
        [DataMember(IsRequired = true, Order = 32)]
        public string ProductsCodeKOZ
        {
            get { return productsCodeKOZ; }
            set { productsCodeKOZ = value; }
        }

        // Код ОКЕИ. Номенклатура
        [DataMember(IsRequired = true, Order = 33)]
        public string ProductsOKEI
        {
            get { return productsOKEI; }
            set { productsOKEI = value; }
        }

        // Цена за единицу 
        [DataMember(IsRequired = true, Order = 34)]
        public double ProductsPrice
        {
            get { return productsPrice; }
            set { productsPrice = value; }
        }

        // Количество
        [DataMember(IsRequired = true, Order = 35)]
        public int ProductsQantity
        {
            get { return productsQantity; }
            set { productsQantity = value; }
        }

        // Сумма в валюте
        [DataMember(IsRequired = true, Order = 36)]
        public double ProductsSum
        {
            get { return productsSum; }
            set { productsSum = value; }
        }

        // Структура расходов
        [DataMember(IsRequired = true, Order = 37)]
        public string PaymentKBK
        {
            get { return paymentKBK; }
            set { paymentKBK = value; }
        }

        // Дата 
        [DataMember(IsRequired = true, Order = 38)]
        public string PaymentYear
        {
            get { return paymentYear; }
            set { paymentYear = value; }
        }

        // Дата 
        [DataMember(IsRequired = true, Order = 39)]
        public string PaymentMonth
        {
            get { return paymentMonth; }
            set { paymentMonth = value; }
        }

        // Сумма. По контракту
        [DataMember(IsRequired = true, Order = 40)]
        public double PaymentSum
        {
            get { return paymentSum; }
            set { paymentSum = value; }
        }

        // Описание внесенных изменений
        [DataMember(IsRequired = false, Order = 41)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // Причины изменений условий контракта
        [DataMember(IsRequired = false, Order = 42)]
        public string ReasonCode
        {
            get { return reasonCode; }
            set { reasonCode = value; }
        }

        // Реквизиты документа-основания
        [DataMember(IsRequired = false, Order = 43)]
        public string ReasonDocument
        {
            get { return reasonDocument; }
            set { reasonDocument = value; }
        }

        // Дата окончания обеспечения
        [DataMember(IsRequired = false, Order = 44)]
        public DateTime BankGuaranteeReturn
        {
            get { return bankGuaranteeReturn; }
            set { bankGuaranteeReturn = value; }
        }
    }

    // Коллекция запросов на передачу государственных контрактов
    [CollectionDataContract(Name = "ContractTransfers", ItemName = "ContractTransfer", KeyName = "ContractId", ValueName = "ContractTransfer")]
    public class SendContractTransfersType : Dictionary<string, SendContractTransferType> { };

    #endregion 2. Запрос на передачу государственных контрактов

    #region 3. Запрос на передачу информации о расторжении государственных контрактов
    #endregion 3. Запрос на передачу информации о расторжении государственных контрактов

}