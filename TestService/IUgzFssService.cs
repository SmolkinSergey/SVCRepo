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
        ElectronicInterchangeType SendNotificationEAChanges(NotificationEAChangesType NotificationEAChanges);
        [OperationContract]
        NotificationEAChangesResultsType NotificationEAChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.2. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ОК»
        [OperationContract]
        ElectronicInterchangeType SendNotificationOKChanges(NotificationOKChangesType NotificationOKChanges);
        [OperationContract]
        NotificationOKChangesResultsType NotificationOKChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.3. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЕП»
        [OperationContract]
        ElectronicInterchangeType SendNotificationEPChanges(NotificationEPChangesType NotificationEPChanges);
        [OperationContract]
        NotificationEPChangesResultsType NotificationEPChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.4. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-Д»
        [OperationContract]
        ElectronicInterchangeType SendNotificationOKDChanges(NotificationOKDChangesType NotificationOKDChanges);
        [OperationContract]
        NotificationOKDChangesResultsType NotificationOKDChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.5. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении OK-ОУ»
        [OperationContract]
        ElectronicInterchangeType SendNotificationOKOUChanges(NotificationOKOUChangesType NotificationOKOUChanges);
        [OperationContract]
        NotificationOKOUChangesResultsType NotificationOKOUChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.6. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗК»
        [OperationContract]
        ElectronicInterchangeType SendNotificationZKChanges(NotificationZKChangesType NotificationZKChanges);
        [OperationContract]
        NotificationZKChangesResultsType NotificationZKChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 1.7. Атрибуты извещения (изменения) – Тип «Проект извещения о проведении ЗП»
        [OperationContract]
        ElectronicInterchangeType SendNotificationZPChanges(NotificationZPChangesType NotificationZPChanges);
        [OperationContract]
        NotificationZPChangesResultsType NotificationZPChangesResult(ElectronicInterchangeType ElectronicInterchange);
        // 2. Запрос на передачу государственных контрактов
        [OperationContract]
        ElectronicInterchangeType SendContractTransfers(SendContractTransfersType SendContractTransfers);
        [OperationContract]
        ContractTransferResultsType ContractTransfersResult(ElectronicInterchangeType ElectronicInterchange);
        // 3. Запрос на передачу информации о расторжении государственных контрактов
        [OperationContract]
        ElectronicInterchangeType SendCancellationsContract(CancellationsContractType CancellationsContract);
        [OperationContract]
        CancellationsContractResultsType CancellationsContractResult(ElectronicInterchangeType ElectronicInterchange);
        // 4. Запрос на передачу банковской гарантии (ее изменения)
        [OperationContract]
        ElectronicInterchangeType SendBankGuarantees(BankGuaranteesType BankGuarantees);
        [OperationContract]
        BankGuaranteesResultsType BankGuaranteesResult(ElectronicInterchangeType ElectronicInterchange);
        // 5. Запрос на передачу сведений необходимости возврата финансового обеспечения
        [OperationContract]
        ElectronicInterchangeType SendReturnsFin(ReturnsFinType ReturnsFin);
        [OperationContract]
        ReturnsFinResultsType ReturnsFinResult(ElectronicInterchangeType ElectronicInterchange);
        // 6. Запрос на передачу начисления неустойки
        [OperationContract]
        ElectronicInterchangeType SendPenaltiesCalculation(PenaltiesCalculationType PenaltiesCalculation);
        [OperationContract]
        PenaltiesCalculationResultsType PenaltiesCalculationResult(ElectronicInterchangeType ElectronicInterchange);
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

    // Текущее состояние контракта
    [DataContract(Name = "ContractStagesEnum")]
    public enum ContractStagesEnum
    {
        // 1 - Исполнение → нет действий
        [EnumMember]
        E = 1,
        // 2 - Исполнение прекращено → в поле «Исполнение. Состояние» - значение «Исполнение прекращено»
        [EnumMember]
        ET = 2,
        // 3 - Исполнение завершено → в поле «Исполнение. Состояние» - значение «Исполнение завершено»
        [EnumMember]
        EC = 3,
        // 4 - Aннулировано → в поле «Исполнение. Состояние» - значение «Aннулировано»
        [EnumMember]
        IN = 4
    }

    // Реквизиты банковского счёта
    [DataContract(Name = "BankAccount")]
    public class BankAccount
    {
        // Расчетный счет
        string settlementAccount;

        // Лицевой счет
        string personalAccount;

        // БИК
        string bankIdentifierCode;

        // Корреспондентский счет
        string correspondentAccount;

        // Расчетный счет
        [DataMember(IsRequired = true, Order = 1)]
        public string SettlementAccount
        {
            get { return settlementAccount; }
            set { settlementAccount = value; }
        }

        // Лицевой счет
        [DataMember(IsRequired = true, Order = 2)]
        public string PersonalAccount
        {
            get { return personalAccount; }
            set { personalAccount = value; }
        }

        // БИК
        [DataMember(IsRequired = true, Order = 3)]
        public string BankIdentifierCode
        {
            get { return bankIdentifierCode; }
            set { bankIdentifierCode = value; }
        }

        // Корреспондентский счет
        [DataMember(IsRequired = false, Order = 4)]
        public string CorrespondentAccount
        {
            get { return correspondentAccount; }
            set { correspondentAccount = value; }
        }
    }

    #endregion Общие типы

    #region 1. Атрибуты извещения (изменения)

    #region Варианты спецификаций

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

    #endregion Варианты спецификаций

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

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "NotificationEAChange", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class NotificationEAChangesResultsType : Dictionary<string, ProcessingStatusType> { };

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
    [CollectionDataContract(Name = "NotificationOKChanges", ItemName = "NotificationOKChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationOKChangesType : Dictionary<string, NotificationOKChangeType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "NotificationOKChange", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class NotificationOKChangesResultsType : Dictionary<string, ProcessingStatusType> { };

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
    [CollectionDataContract(Name = "NotificationEPChanges", ItemName = "NotificationEPChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationEPChangesType : Dictionary<string, NotificationEPChangeType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "NotificationEPChange", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class NotificationEPChangesResultsType : Dictionary<string, ProcessingStatusType> { };

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
        NotificationChangeSpecWithoutSumDictionary specs;

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
        public NotificationChangeSpecWithoutSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationOKDChanges", ItemName = "NotificationOKDChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationOKDChangesType : Dictionary<string, NotificationOKDChangeType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "NotificationOKDChange", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class NotificationOKDChangesResultsType : Dictionary<string, ProcessingStatusType> { };

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
        NotificationChangeSpecWithoutSumDictionary specs;

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
        public NotificationChangeSpecWithoutSumDictionary Specs
        {
            get { return specs; }
            set { specs = value; }
        }
    }

    // Коллекция извещений(изменений)
    [CollectionDataContract(Name = "NotificationOKOUChanges", ItemName = "NotificationOKOUChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationOKOUChangesType : Dictionary<string, NotificationOKOUChangeType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "NotificationOKOUChange", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class NotificationOKOUChangesResultsType : Dictionary<string, ProcessingStatusType> { };

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
    [CollectionDataContract(Name = "NotificationZKChanges", ItemName = "NotificationZKChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationZKChangesType : Dictionary<string, NotificationZKChangeType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "NotificationZKChange", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class NotificationZKChangesResultsType : Dictionary<string, ProcessingStatusType> { };

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
    [CollectionDataContract(Name = "NotificationZPChanges", ItemName = "NotificationZPChange", KeyName = "PurchaseGUID", ValueName = "Purchase")]
    public class NotificationZPChangesType : Dictionary<string, NotificationZPChangeType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "NotificationZPChange", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class NotificationZPChangesResultsType : Dictionary<string, ProcessingStatusType> { };

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
        BankAccount lagnBankAccount;

        // Поставщик. Фамилия
        string personLastName;

        // Поставщик. Имя
        string personFirstName;

        // Поставщик. Отчество
        string personMiddleName;

        // Поставщик. ИНН
        string personINN;

        // Поставщик. Расчетный счет поставщика (Расчетный счет УФК поставщика)
        BankAccount pagnBankAccount;

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
        public BankAccount LagnBankAccount
        {
            get { return lagnBankAccount; }
            set { lagnBankAccount = value; }
        }

        // Поставщик. Фамилия
        [DataMember(IsRequired = true, Order = 22)]
        public string PersonLastName
        {
            get { return personLastName; }
            set { personLastName = value; }
        }

        // Поставщик. Имя
        [DataMember(IsRequired = true, Order = 23)]
        public string PersonFirstName
        {
            get { return personFirstName; }
            set { personFirstName = value; }
        }

        // Поставщик. Отчество
        [DataMember(IsRequired = true, Order = 24)]
        public string PersonMiddleName
        {
            get { return personMiddleName; }
            set { personMiddleName = value; }
        }

        // Поставщик. ИНН
        [DataMember(IsRequired = true, Order = 25)]
        public string PersonINN
        {
            get { return personINN; }
            set { personINN = value; }
        }

        // Поставщик. Расчетный счет поставщика (Расчетный счет УФК поставщика)
        [DataMember(IsRequired = true, Order = 26)]
        public BankAccount PagnBankAccount
        {
            get { return pagnBankAccount; }
            set { pagnBankAccount = value; }
        }

        // Номенклатура
        [DataMember(IsRequired = true, Order = 27)]
        public string ProductsCodeKOZ
        {
            get { return productsCodeKOZ; }
            set { productsCodeKOZ = value; }
        }

        // Код ОКЕИ. Номенклатура
        [DataMember(IsRequired = true, Order = 28)]
        public string ProductsOKEI
        {
            get { return productsOKEI; }
            set { productsOKEI = value; }
        }

        // Цена за единицу 
        [DataMember(IsRequired = true, Order = 29)]
        public double ProductsPrice
        {
            get { return productsPrice; }
            set { productsPrice = value; }
        }

        // Количество
        [DataMember(IsRequired = true, Order = 30)]
        public int ProductsQantity
        {
            get { return productsQantity; }
            set { productsQantity = value; }
        }

        // Сумма в валюте
        [DataMember(IsRequired = true, Order = 31)]
        public double ProductsSum
        {
            get { return productsSum; }
            set { productsSum = value; }
        }

        // Структура расходов
        [DataMember(IsRequired = true, Order = 32)]
        public string PaymentKBK
        {
            get { return paymentKBK; }
            set { paymentKBK = value; }
        }

        // Дата 
        [DataMember(IsRequired = true, Order = 33)]
        public string PaymentYear
        {
            get { return paymentYear; }
            set { paymentYear = value; }
        }

        // Дата 
        [DataMember(IsRequired = true, Order = 34)]
        public string PaymentMonth
        {
            get { return paymentMonth; }
            set { paymentMonth = value; }
        }

        // Сумма. По контракту
        [DataMember(IsRequired = true, Order = 35)]
        public double PaymentSum
        {
            get { return paymentSum; }
            set { paymentSum = value; }
        }

        // Описание внесенных изменений
        [DataMember(IsRequired = false, Order = 36)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // Причины изменений условий контракта
        [DataMember(IsRequired = false, Order = 37)]
        public string ReasonCode
        {
            get { return reasonCode; }
            set { reasonCode = value; }
        }

        // Реквизиты документа-основания
        [DataMember(IsRequired = false, Order = 38)]
        public string ReasonDocument
        {
            get { return reasonDocument; }
            set { reasonDocument = value; }
        }

        // Дата окончания обеспечения
        [DataMember(IsRequired = false, Order = 39)]
        public DateTime BankGuaranteeReturn
        {
            get { return bankGuaranteeReturn; }
            set { bankGuaranteeReturn = value; }
        }
    }

    // Коллекция запросов на передачу государственных контрактов
    [CollectionDataContract(Name = "ContractTransfers", ItemName = "ContractTransfer", KeyName = "ContractGUID", ValueName = "ContractTransfer")]
    public class SendContractTransfersType : Dictionary<string, SendContractTransferType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "ContractTransfer", KeyName = "PurchaseGUID", ValueName = "ProcessingStatus")]
    public class ContractTransferResultsType : Dictionary<string, ProcessingStatusType> { };

    #endregion 2. Запрос на передачу государственных контрактов

    #region 3. Запрос на передачу информации о расторжении государственных контрактов

    [DataContract]
    public class SendCancellationContractType
    {
        // Организация-Принадлежность/Заказчик
        string regNum;

        // Дата расторжения контракта
        DateTime cancellationDate;

        // Причина расторжения контракта
        string cancellationReason;

        // Код в словаре «Основания расторжения контракта»
        string cancellationReasonCode;

        // Реквизиты документа-основания расторжения контракта
        string cancelletionDoc;

        // Дата уведомления (судебного решения) о расторжении контракта
        DateTime decisionDate;

        // Информационное поле
        ContractStagesEnum currentContractStage;

        // Исполнение. Дата смены состояния
        DateTime stageChangeDate;

        // Дата окончания обеспечения
        DateTime guaranteeTermination;

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 1)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        // Дата расторжения контракта
        [DataMember(IsRequired = false, Order = 2)]
        public DateTime CancellationDate
        {
            get { return cancellationDate; }
            set { cancellationDate = value; }
        }

        // Причина расторжения контракта
        [DataMember(IsRequired = false, Order = 3)]
        public string CancellationReason
        {
            get { return cancellationReason; }
            set { cancellationReason = value; }
        }

        // Код в словаре «Основания расторжения контракта»
        [DataMember(IsRequired = false, Order = 4)]
        public string CancellationReasonCode
        {
            get { return cancellationReasonCode; }
            set { cancellationReasonCode = value; }
        }

        // Реквизиты документа-основания расторжения контракта
        [DataMember(IsRequired = false, Order = 5)]
        public string CancelletionDoc
        {
            get { return cancelletionDoc; }
            set { cancelletionDoc = value; }
        }

        // Дата уведомления (судебного решения) о расторжении контракта
        [DataMember(IsRequired = false, Order = 6)]
        public DateTime DecisionDate
        {
            get { return decisionDate; }
            set { decisionDate = value; }
        }

        // Информационное поле
        [DataMember(IsRequired = false, Order = 7)]
        public ContractStagesEnum CurrentContractStage
        {
            get { return currentContractStage; }
            set { currentContractStage = value; }
        }

        // Исполнение. Дата смены состояния
        [DataMember(IsRequired = false, Order = 8)]
        public DateTime StageChangeDate
        {
            get { return stageChangeDate; }
            set { stageChangeDate = value; }
        }

        // Дата окончания обеспечения
        [DataMember(IsRequired = false, Order = 9)]
        public DateTime GuaranteeTermination
        {
            get { return guaranteeTermination; }
            set { guaranteeTermination = value; }
        }
    }

    // Коллекция запросов на передачу информации о расторжении государственных контрактов
    [CollectionDataContract(Name = "CancellationsContract", ItemName = "CancellationContract", KeyName = "ContractGUID", ValueName = "CancellationContract")]
    public class CancellationsContractType : Dictionary<string, SendCancellationContractType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "CancellationContract", KeyName = "ContractGUID", ValueName = "ProcessingStatus")]
    public class CancellationsContractResultsType : Dictionary<string, ProcessingStatusType> { };

    #endregion 3. Запрос на передачу информации о расторжении государственных контрактов

    #region 4. Запрос на передачу банковской гарантии (ее изменения)
    
    [DataContract]
    public class BankGuaranteeType
    {
        // GUID контракта
        string contractGUID;

        // GUID извещения
        string purchaseGUID;

        // Номер изменений
        string versionNumber;

        // Получатель/Принадлежность 
        string regnum;

        // Банк-Гарант.
        string bankFullName;

        // ИНН записи контрагента
        string bankINN;

        // КПП записи контрагента
        string bankKPP;

        // БИК записи контрагента
        string bankBIK;

        // Плательщик.
        string supplierFullName;

        // Плательщик. ИНН
        string supplierINN;

        // Плательщик. КПП
        string supplierKPP;

        // Плательщик. Код ОГРН
        string supplierOGRN;

        // Плательщик. Код OKATO
        string supplierOKATO;

        // Плательщик. Код OKTMO
        string supplierOKTMO;

        // Поставщик. Расчетный счет поставщик
        BankAccount lagnBankAccount;
        
        // Плательщик. Фамилия
        string pagnLastName;

        // Плательщик. Имя
        string pagnFirstName;

        // Плательщик. Отчество
        string pagnMiddleName;

        // Индивидуальный предприниматель
        string pagnIsIP;

        // Плательщик. ИНН
        string pagnINN;

        // Поставщик. ОГРН
        string pagnOGRNIP;

        // Поставщик. Расчетный счет поставщик
        BankAccount pagnBankAccount;

        // Входящий документ. Номер
        string guaranteeNumber;

        // Сумма обеспечения
        double guaranteeAmount;

        // Дата начала обеспечения.
        DateTime entryForceDate;

        // Дата окончания обеспечения.
        DateTime expireDate;

        // Код валюты
        int сurrencyCode;

        // Ссылка для скачивания документов
        string url;

        // Дата изменений
        DateTime modificationDate;

        // Описание изменений
        string modificationInfo;

        // GUID контракта
        [DataMember(IsRequired = true, Order = 1)]
        public string ContractGUID
        {
            get { return contractGUID; }
            set { contractGUID = value; }
        }

        // GUID извещения
        [DataMember(IsRequired = true, Order = 2)]
        public string PurchaseGUID
        {
            get { return purchaseGUID; }
            set { purchaseGUID = value; }
        }

        // Номер изменений
        [DataMember(IsRequired = false, Order = 3)]
        public string VersionNumber
        {
            get { return versionNumber; }
            set { versionNumber = value; }
        }

        // Получатель/Принадлежность 
        [DataMember(IsRequired = true, Order = 4)]
        public string Regnum
        {
            get { return regnum; }
            set { regnum = value; }
        }

        // Банк-Гарант.
        [DataMember(IsRequired = true, Order = 5)]
        public string BankFullName
        {
            get { return bankFullName; }
            set { bankFullName = value; }
        }

        // ИНН записи контрагента
        [DataMember(IsRequired = true, Order = 6)]
        public string BankINN
        {
            get { return bankINN; }
            set { bankINN = value; }
        }

        // КПП записи контрагента
        [DataMember(IsRequired = false, Order = 7)]
        public string BankKPP
        {
            get { return bankKPP; }
            set { bankKPP = value; }
        }

        // БИК записи контрагента
        [DataMember(IsRequired = true, Order = 8)]
        public string BankBIK
        {
            get { return bankBIK; }
            set { bankBIK = value; }
        }

        // Плательщик.
        [DataMember(IsRequired = true, Order = 9)]
        public string SupplierFullName
        {
            get { return supplierFullName; }
            set { supplierFullName = value; }
        }

        // Плательщик. ИНН
        [DataMember(IsRequired = true, Order = 10)]
        public string SupplierINN
        {
            get { return supplierINN; }
            set { supplierINN = value; }
        }

        // Плательщик. КПП
        [DataMember(IsRequired = false, Order = 11)]
        public string SupplierKPP
        {
            get { return supplierKPP; }
            set { supplierKPP = value; }
        }

        // Плательщик. Код ОГРН
        [DataMember(IsRequired = true, Order = 12)]
        public string SupplierOGRN
        {
            get { return supplierOGRN; }
            set { supplierOGRN = value; }
        }

        // Плательщик. Код OKATO
        [DataMember(IsRequired = true, Order = 13)]
        public string SupplierOKATO
        {
            get { return supplierOKATO; }
            set { supplierOKATO = value; }
        }

        // Плательщик. Код OKTMO
        [DataMember(IsRequired = true, Order = 14)]
        public string SupplierOKTMO
        {
            get { return supplierOKTMO; }
            set { supplierOKTMO = value; }
        }

        // Поставщик. Расчетный счет поставщик
        [DataMember(IsRequired = true, Order = 15)]
        public BankAccount LagnBankAccount
        {
            get { return lagnBankAccount; }
            set { lagnBankAccount = value; }
        }

        // Плательщик. Фамилия
        [DataMember(IsRequired = true, Order = 16)]
        public string PagnLastName
        {
            get { return pagnLastName; }
            set { pagnLastName = value; }
        }

        // Плательщик. Имя
        [DataMember(IsRequired = true, Order = 17)]
        public string PagnFirstName
        {
            get { return pagnFirstName; }
            set { pagnFirstName = value; }
        }

        // Плательщик. Отчество
        [DataMember(IsRequired = true, Order = 18)]
        public string PagnMiddleName
        {
            get { return pagnMiddleName; }
            set { pagnMiddleName = value; }
        }

        // Индивидуальный предприниматель
        [DataMember(IsRequired = true, Order = 19)]
        public string PagnIsIP
        {
            get { return pagnIsIP; }
            set { pagnIsIP = value; }
        }

        // Плательщик. ИНН
        [DataMember(IsRequired = true, Order = 20)]
        public string PagnINN
        {
            get { return pagnINN; }
            set { pagnINN = value; }
        }

        // Поставщик. ОГРН
        [DataMember(IsRequired = true, Order = 21)]
        public string PagnOGRNIP
        {
            get { return pagnOGRNIP; }
            set { pagnOGRNIP = value; }
        }

        // Поставщик. Расчетный счет поставщик
        [DataMember(IsRequired = true, Order = 22)]
        public BankAccount PagnBankAccount
        {
            get { return pagnBankAccount; }
            set { pagnBankAccount = value; }
        }

        // Входящий документ. Номер
        [DataMember(IsRequired = true, Order = 23)]
        public string GuaranteeNumber
        {
            get { return guaranteeNumber; }
            set { guaranteeNumber = value; }
        }

        // Сумма обеспечения
        [DataMember(IsRequired = true, Order = 24)]
        public double GuaranteeAmount
        {
            get { return guaranteeAmount; }
            set { guaranteeAmount = value; }
        }

        // Дата начала обеспечения.
        [DataMember(IsRequired = true, Order = 25)]
        public DateTime EntryForceDate
        {
            get { return entryForceDate; }
            set { entryForceDate = value; }
        }

        // Дата окончания обеспечения.
        [DataMember(IsRequired = false, Order = 26)]
        public DateTime ExpireDate
        {
            get { return expireDate; }
            set { expireDate = value; }
        }

        // Код валюты
        [DataMember(IsRequired = false, Order = 27)]
        public int СurrencyCode
        {
            get { return сurrencyCode; }
            set { сurrencyCode = value; }
        }

        // Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 28)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        // Дата изменений
        [DataMember(IsRequired = false, Order = 29)]
        public DateTime ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        // Описание изменений
        [DataMember(IsRequired = false, Order = 30)]
        public string ModificationInfo
        {
            get { return modificationInfo; }
            set { modificationInfo = value; }
        }
    }

    // Коллекция запросов на передачу информации о расторжении государственных контрактов
    [CollectionDataContract(Name = "BankGuarantees", ItemName = "BankGuarantee", KeyName = "bankgarantGUID", ValueName = "BankGuarantee")]
    public class BankGuaranteesType : Dictionary<string, BankGuaranteeType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "BankGuarantee", KeyName = "bankgarantGUID", ValueName = "ProcessingStatus")]
    public class BankGuaranteesResultsType : Dictionary<string, ProcessingStatusType> { };

    #endregion 4. Запрос на передачу банковской гарантии (ее изменения)

    #region 5. Запрос на передачу сведений необходимости возврата финансового обеспечения

    // Текущее состояние контракта
    [DataContract(Name = "ReturnFinAgentType")]
    public enum ReturnFinAgentType
    {
        // 1 - Физическое лицо РФ
        [EnumMember]
        P = 1,
        // 2 - Физическое лицо иностранного государства
        [EnumMember]
        PF = 2,
        // 3 - Юридическое лицо
        [EnumMember]
        U = 3,
        // 4 - Юридическое лицо иностранного государства
        [EnumMember]
        UF = 4
    }

    [DataContract]
    public class ReturnFinType
    {
        // Организация-Принадлежность/Заказчик
        string regNum;

        // Уникальный идентификатор платежного документа в подсистеме «Бюджетный учет»
        string bankDocumentsNumber;

        // GUID контракта 
        string contractGUID;

        // GUID извещения
        string purchaseGUID;

        // Дата окончания обеспечения
        DateTime returnDate;

        // Уникальный идентификатор платежного документа в подсистеме «Бюджетный учет»
        string documentGUID;

        // Контрагент.Тип
        ReturnFinAgentType participantType;

        // Поставщик.
        string organizationName;

        // Поставщик.  ИНН
        string organizationINN;

        // Поставщик.  КПП
        string organizationKPP;

        // Поставщик.  Код ОГРН
        string organizationOGRN;

        // Поставщик. Расчетный счет поставщик
        BankAccount organizationBankAccount;

        // Организация-Принадлежность/Заказчик
        [DataMember(IsRequired = true, Order = 1)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        // Уникальный идентификатор платежного документа в подсистеме «Бюджетный учет»
        [DataMember(IsRequired = true, Order = 2)]
        public string BankDocumentsNumber
        {
            get { return bankDocumentsNumber; }
            set { bankDocumentsNumber = value; }
        }

        // GUID контракта 
        [DataMember(IsRequired = true, Order = 3)]
        public string ContractGUID
        {
            get { return contractGUID; }
            set { contractGUID = value; }
        }

        // GUID извещения
        [DataMember(IsRequired = true, Order = 4)]
        public string PurchaseGUID
        {
            get { return purchaseGUID; }
            set { purchaseGUID = value; }
        }

        // Дата окончания обеспечения
        [DataMember(IsRequired = false, Order = 5)]
        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }

        // Уникальный идентификатор платежного документа в подсистеме «Бюджетный учет»
        [DataMember(IsRequired = true, Order = 6)]
        public string DocumentGUID
        {
            get { return documentGUID; }
            set { documentGUID = value; }
        }

        // Контрагент.Тип
        [DataMember(IsRequired = true, Order = 7)]
        public ReturnFinAgentType ParticipantType
        {
            get { return participantType; }
            set { participantType = value; }
        }

        // Поставщик.
        [DataMember(IsRequired = true, Order = 8)]
        public string OrganizationName
        {
            get { return organizationName; }
            set { organizationName = value; }
        }

        // Поставщик.  ИНН
        [DataMember(IsRequired = true, Order = 9)]
        public string OrganizationINN
        {
            get { return organizationINN; }
            set { organizationINN = value; }
        }

        // Поставщик.  КПП
        [DataMember(IsRequired = false, Order = 10)]
        public string OrganizationKPP
        {
            get { return organizationKPP; }
            set { organizationKPP = value; }
        }

        // Поставщик.  Код ОГРН
        [DataMember(IsRequired = true, Order = 11)]
        public string OrganizationOGRN
        {
            get { return organizationOGRN; }
            set { organizationOGRN = value; }
        }

        // Поставщик. Расчетный счет поставщик
        [DataMember(IsRequired = true, Order = 12)]
        public BankAccount OrganizationBankAccount
        {
            get { return organizationBankAccount; }
            set { organizationBankAccount = value; }
        }
    }

    // Коллекция запросов на передачу сведений необходимости возврата финансового обеспечения
    [CollectionDataContract(Name = "ReturnsFin", ItemName = "ReturnFin", KeyName = "documentGUID", ValueName = "ReturnFin")]
    public class ReturnsFinType : Dictionary<string, ReturnFinType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "ReturnFin", KeyName = "documentGUID", ValueName = "ProcessingStatus")]
    public class ReturnsFinResultsType : Dictionary<string, ProcessingStatusType> { };

    #endregion 5. Запрос на передачу сведений необходимости возврата финансового обеспечения

    #region 6. Запрос на передачу начисления неустойки

    // Тип взыскания
    [DataContract(Name = "PenaltyTypeEnum")]
    public enum PenaltyTypeEnum
    {
        // 1 - Штраф
        [EnumMember]
        F = 1,
        // 2 - Пени
        [EnumMember]
        I = 2
    }

    // Нарушитель
    [DataContract(Name = "ViolatorTypeEnum")]
    public enum ViolatorTypeEnum
    {
        // 1 - Заказчик
        [EnumMember]
        C = 1,
        // 2 - Поставщик
        [EnumMember]
        S = 2
    }

    [DataContract]
    public class PenaltyCalculationType
    {
        // ID контракта
        string contractGUID;

        // ID извещения
        string purchaseGUID;

        // Получатель/Принадлежность 
        string regNum;

        // Тип взыскания
        PenaltyTypeEnum penaltyType;

        // Нарушитель
        ViolatorTypeEnum contractParty;

        // Сумма неустойки
        double accrualAmount;

        // Причина начисления неустойки
        string penaltyReasonCode;

        // Входящий документ. Дата
        DateTime penaltyDocDate;

        // Входящий документ. Тип
        string penaltyDocName;

        // Входящий документ. Номер
        string penaltyDocNum;

        // GUID банковской гарантии
        string bankgarantGUID;

        // Сумма, требуемая к уплате
        string paymentAmount;

        // Сумма возврата аванса
        double refundAmount;

        // Сумма неустоек
        double penaltiesAmount;

        // Иные суммы
        double othersAmount;

        // Ссылка для скачивания документов
        string url;

        // Состояние контракта
        ContractStagesEnum currentContractStage;

        // ID контракта
        [DataMember(IsRequired = true, Order = 1)]
        public string ContractGUID
        {
            get { return contractGUID; }
            set { contractGUID = value; }
        }

        // ID извещения
        [DataMember(IsRequired = true, Order = 2)]
        public string PurchaseGUID
        {
            get { return purchaseGUID; }
            set { purchaseGUID = value; }
        }

        // Получатель/Принадлежность 
        [DataMember(IsRequired = true, Order = 3)]
        public string RegNum
        {
            get { return regNum; }
            set { regNum = value; }
        }

        // Тип взыскания
        [DataMember(IsRequired = true, Order = 4)]
        public PenaltyTypeEnum PenaltyType
        {
            get { return penaltyType; }
            set { penaltyType = value; }
        }

        // Нарушитель
        [DataMember(IsRequired = true, Order = 5)]
        public ViolatorTypeEnum ContractParty
        {
            get { return contractParty; }
            set { contractParty = value; }
        }

        // Сумма неустойки
        [DataMember(IsRequired = true, Order = 6)]
        public double AccrualAmount
        {
            get { return accrualAmount; }
            set { accrualAmount = value; }
        }

        // Причина начисления неустойки
        [DataMember(IsRequired = true, Order = 7)]
        public string PenaltyReasonCode
        {
            get { return penaltyReasonCode; }
            set { penaltyReasonCode = value; }
        }

        // Входящий документ. Дата
        [DataMember(IsRequired = true, Order = 8)]
        public DateTime PenaltyDocDate
        {
            get { return penaltyDocDate; }
            set { penaltyDocDate = value; }
        }

        // Входящий документ. Тип
        [DataMember(IsRequired = true, Order = 9)]
        public string PenaltyDocName
        {
            get { return penaltyDocName; }
            set { penaltyDocName = value; }
        }

        // Входящий документ. Номер
        [DataMember(IsRequired = true, Order = 10)]
        public string PenaltyDocNum
        {
            get { return penaltyDocNum; }
            set { penaltyDocNum = value; }
        }

        // GUID банковской гарантии
        [DataMember(IsRequired = false, Order = 11)]
        public string BankgarantGUID
        {
            get { return bankgarantGUID; }
            set { bankgarantGUID = value; }
        }

        // Сумма, требуемая к уплате
        [DataMember(IsRequired = false, Order = 12)]
        public string PaymentAmount
        {
            get { return paymentAmount; }
            set { paymentAmount = value; }
        }

        // Сумма возврата аванса
        [DataMember(IsRequired = false, Order = 13)]
        public double RefundAmount
        {
            get { return refundAmount; }
            set { refundAmount = value; }
        }

        // Сумма неустоек
        [DataMember(IsRequired = false, Order = 14)]
        public double PenaltiesAmount
        {
            get { return penaltiesAmount; }
            set { penaltiesAmount = value; }
        }

        // Иные суммы
        [DataMember(IsRequired = false, Order = 15)]
        public double OthersAmount
        {
            get { return othersAmount; }
            set { othersAmount = value; }
        }

        // Ссылка для скачивания документов
        [DataMember(IsRequired = false, Order = 16)]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        // Состояние контракта
        [DataMember(IsRequired = false, Order = 17)]
        public ContractStagesEnum CurrentContractStage
        {
            get { return currentContractStage; }
            set { currentContractStage = value; }
        }
    }

    // Коллекция запросов на передачу сведений необходимости возврата финансового обеспечения
    [CollectionDataContract(Name = "PenaltiesCalculation", ItemName = "PenaltyCalculation", KeyName = "penaltyGUID", ValueName = "PenaltyCalculation")]
    public class PenaltiesCalculationType : Dictionary<string, PenaltyCalculationType> { };

    [CollectionDataContract(Name = "ProcessingResults", ItemName = "PenaltyCalculation", KeyName = "penaltyGUID", ValueName = "ProcessingStatus")]
    public class PenaltiesCalculationResultsType : Dictionary<string, ProcessingStatusType> { };

    #endregion 6. Запрос на передачу начисления неустойки
}