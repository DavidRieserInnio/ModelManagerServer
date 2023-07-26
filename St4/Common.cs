using System.Text.RegularExpressions;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Common.cs" />
    /// </summary>
    public static class Common
    {
        public static bool DevMode { get; internal set; } = true;
        public static Guid AppId_Configurator { get; } = Guid.Parse("cc96031c-8e2e-49fd-af1f-c734f55948fe");
        public static string FallbackLanguage { get; } = "de";
        public static string ComboEmptyValue { get; } = "--";
        public static string TextBoxMaxLengthDefaultValue { get; } = "1000";
        public static bool CheckForSpecialPartPreliminary { get; } = false;
        public static bool CheckForSpecialPartSalesOrder { get; } = false;
        public static Regex RegexDeviceNumber { get; } = new Regex(@"[A-Z0-9]{4}", RegexOptions.Compiled, TimeSpan.FromSeconds(1));


        #region DataSources

        public static string DataSourceManual { get; } = "DS_01";
        public static string DataSourceManualParent { get; } = "DS_02";
        public static string DataSourceWiringDiagramPlc { get; } = "DS_03";
        public static string DataSourceMechanicalScheme { get; } = "DS_04";
        public static string DataSourceWiringDiagramPid { get; } = "DS_05";
        public static string DataSourceOracle { get; } = "DS_06";
        public static string DataSourceNotizen { get; } = "DS_07";
        public static string DataSourceEChecklist { get; } = "DS_08";
        public static string DataSourceNoInput { get; } = "DS_09";
        public static string DataSourceOldConfigurator { get; } = "DS_10";
        public static string DataSourceConfigurationTemplate { get; } = "DS_11";
        public static string DataSourceDefaultValue { get; } = "DS_12";

        #endregion

        #region Reviews

        public static string ReviewManual { get; } = "R_01";
        public static string ReviewManualParent { get; } = "R_02";
        public static string ReviewNone { get; } = "R_03";

        #endregion

        #region Milestones

        public static string MilestoneBase { get; } = "MS_01";
        public static string MilestoneUpcoming { get; } = "MS_02";
        public static string MilestoneReached { get; } = "MS_03";

        #endregion

        #region Parts Special Usage Property

        public static string PartSpecialUsageDeviceNumber { get; } = "JNr";
        public static string PartSpecialUsageSalesOrder { get; } = "SO";
        public static string PartSpecialUsageKeyWord { get; } = "KeyWord";
        public static string PartSpecialUsagePreliminary { get; } = "Preliminary";
        public static string PartSpecialUsageEngineCount { get; } = "EngineCount";
        public static string PartSpecialUsageLanguage { get; } = "Language";
        public static string PartSpecialUsageDevice { get; } = "Device";
        public static string PartSpecialUsageCountry { get; } = "Country";

        #endregion

        #region Parts Names

        public static string PartNameDeviceNumber { get; } = "FD.DeviceNumber";

        #endregion

        #region Properties Keys

        public static string PropertyVisibleRule { get; } = "VisibleRule";
        public static string PropertyCircuit { get; } = "Circuit";
        public static string PropertyGroup { get; } = "Group";
        public static string PropertyName { get; } = "Name";
        public static string PropertyWatermark { get; } = "Watermark";
        public static string PropertyUnit { get; } = "Unit";
        public static string PropertyArticleCode { get; } = "ArticleCode";
        public static string PropertyComboItemText { get; } = "ItemText";
        public static string PropertyComboItemSortMode { get; } = "ComboItemSortMode";

        public static string PropertyMandatory { get; } = "Mandatory";
        public static string PropertySpecialFlag { get; } = "Special";
        public static string PropertyRegex { get; } = "Regex";
        public static string PropertyMaxLength { get; } = "MaxLength";
        public static string PropertyElementText { get; } = "ElementText";
        public static string PropertyInfoDocument { get; } = "DOC";
        public static string PropertyInfoPlcLabel { get; } = "SPS";
        public static string PropertyInfoParameter { get; } = "PAR";
        public static string PropertyInfoPid { get; } = "P&ID";
        public static string PropertyInfoSsl { get; } = "SSL";
        public static string PropertyInfoOracleKey { get; } = "Oracle Key";
        public static string PropertyInfoOracleValue { get; } = "Oracle Value";
        public static string PropertyInfoInternal { get; } = "INF";
        public static string PropertyInfoExternal { get; } = "Info Partner";
        public static string PropertyMilestoneId { get; } = "Milestone Id";
        public static string PropertyOverhaulAndRepairType { get; } = "Overhaul And Repair";
        public static string PropertySeparatorBefore { get; } = "Separator Before";
        public static string PropertyTextboxType { get; } = "InputType";
        public static string PropertyDefaultValue { get; } = "DefaultValue";

        public static List<string> TranslatablePropertyKeys { get; } = new List<string>
        {
            PropertyElementText,
            PropertyGroup,
            PropertyCircuit,
            PropertyWatermark,
            PropertyInfoInternal,
            PropertyInfoExternal,
            PropertyComboItemText
        };

        #endregion

        #region Document Shortcuts

        public static string DocumentAlarmlist { get; } = "AL";
        public static string DocumentChecklist { get; } = "CL";
        public static string DocumentTechnicalSpecification { get; } = "TSOC";
        public static string DocumentInterfaceList { get; } = "SSL";
        public static string DocumentWiringDiagram { get; } = "WD";
        public static string DocumentSoftware { get; } = "SW";
        public static string DocumentParameter { get; } = "Para";

        #endregion

        #region MessageBox IDs

        public static IdGenerator MessagBoxId { get; } = new IdGenerator();

        public class IdGenerator
        {
            public string this[int index] { get => $"E#{index:0000}"; }
        }

        #endregion

        #region EAI

        public static string EaiDirectory { get; } = @"\\jenwap040.ad.innio.com\Shared\Schema_ST4\EAI_Connector\";
        public static string EaiCreationFileNamespace { get; } = @"http://www.schema.de/2012/ST4/APX/ControlFile";
        public static string St4TpeTemplatesFolder { get; } = @"/SystemFolder/SystemFolder[@Title = 'Content']/*[@Title='Default']/*[@Title='Vorlagen']/*[@Title='ETechnik']/*[@Title='EAI Connector']/*";
        public static string St4TpeProductionLayout { get; } = @"string:/Production/GE Jenbacher - TPE_INNIO/";
        public static string St4TpeProductionLayout_myPlant { get; } = @"string:/Production/Export Layouts/";
        public static string St4TpeProductionLayout_ClTestbench { get; } = @"QML to Qdas";
        public static string St4TpeProductionLayout_ClService { get; } = @"Export Checkliste myPlant [multilanguage]";
        public static string myPlantExportPath { get; } = @"string:\\jenwap040.ad.innio.com\Shared\checklist_transfer\open\";
        public static string TitleBackPageStPath { get; } = @".//InfoType15/TextModule2[@Title = 'Änderungen [->Sprachneutral]']/@Content/@All";

        public static string EaiFilterFileName { get; } = "TPE_Filter.csv";
        public static string EaiCsvFileName { get; } = "TPE_ProjectVariables.csv";
        #endregion

    }
}
