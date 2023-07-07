using System.Collections.Generic;
using System.Drawing;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Common.cs" />
    /// </summary>
    public static class Common
    {
        public const bool RightFactoryEnable = true;

        /// <summary>the value displayed for an empty ComboBox</summary>
        /// <value>--</value>
        public const string ComboEmptyValue = "--";

        public const string TextBoxMaxLengthDefaultValue = "1000";

        public const string FallbackLanguage = "de";

        public static bool CheckForSpecialPartPreliminary { get; } = false;
        public static bool CheckForSpecialPartSalesOrder { get; } = false;

        #region File System

        /// <summary>Filename of the usersettings file in temp folder</summary>
        public const string UserSettingsFolderName = "ST4_Konfigurator";

        public const string UserSettingsFileName = "UserSettings.xml";
        public static string AppSettingsFileName { get; } = "AppSettings.xml";
        public const string ConnectionStringFileName = "Configurator.Database.ConnString.bin";

        #endregion

        #region Colors

        // infobox und notifybox header
        public static readonly Color ViewletHeaderBackcolorSelected = Color.FromArgb(0, 102, 182);
        public static readonly Color ViewletHeaderBackcolorUnSelected = Color.FromArgb(240, 240, 240);

        public static readonly Color FilterActiveColor = Color.Bisque;
        public static readonly Color InputControlBackcolor = Color.White; //Color.FromArgb(240, 240, 240);

        #endregion

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

        #endregion

        #region Reviews

        public static string ReviewManual { get; } = "R_01";
        public static string ReviewManualParent { get; } = "R_02";
        public static string ReviewNone { get; } = "R_03";

        #endregion

        #region Parts Special Usage Property

        public static string PartSpecialUsageDeviceNumber { get; } = "JNr";
        public static string PartSpecialUsageSalesOrder { get; } = "SO";
        public static string PartSpecialUsageKeyWord { get; } = "KeyWord";
        public static string PartSpecialUsagePreliminary { get; } = "Preliminary";
        public static string PartSpecialUsageEngineCount { get; } = "EngineCount";
        public static string PartSpecialUsageLanguage { get; } = "Language";

        #endregion

        #region Properties Keys

        public const string PropertyVisibleRule = "VisibleRule";
        public const string PropertyCircuit = "Circuit";
        public const string PropertyGroup = "Group";
        public const string PropertyName = "Name";
        public const string PropertyWatermark = "Watermark";
        public const string PropertyUnit = "Unit";
        public const string PropertyArticleCode = "ArticleCode";
        public const string PropertyComboItemText = "ItemText";

        public const string PropertyMandatory = "Mandatory";
        public const string PropertySpecialFlag = "Special";
        public const string PropertyRegex = "Regex";
        public const string PropertyMaxLength = "MaxLength";
        public const string PropertyElementText = "ElementText";
        public const string PropertyInfoDocument = "DOC";
        public const string PropertyInfoPlcLabel = "SPS";
        public const string PropertyInfoParameter = "PAR";
        public const string PropertyInfoPid = "P&ID";
        public const string PropertyInfoSsl = "SSL";
        public const string PropertyInfoOracleKey = "Oracle Key";
        public const string PropertyInfoOracleValue = "Oracle Value";
        public const string PropertyInfo = "INF";
        public const string PropertyMilestoneId = "Milestone Id";
        public const string PropertyModelInstanceId = "Model Instance Id";

        public static List<string> TranslatablePropertyKeys { get; } = new List<string>
        {
            PropertyElementText,
            PropertyGroup,
            PropertyCircuit,
            PropertyWatermark,
            PropertyInfo,
            PropertyComboItemText
        };

        #endregion

        #region Document Shortcuts

        public static string DocumentAlarmlist { get; } = "AL";
        public static string DocumentChecklist { get; } = "CL";
        public static string DocumentTechnicalSpecification { get; } = "TSOC";
        public static string DocumentInterfaceList { get; } = "SSL";

        #endregion

        #region MessageBox IDs

        public static IdGenerator MessagBoxId { get; } = new IdGenerator();

        public class IdGenerator
        {
            public string this[int index] { get => $"E#{index:0000}"; }
        }

        #endregion

        #region Software Function Rights

        public static string DeleteUnreleasedRightName { get; } = "DeleteUnreleasedProject";
        public static string DeleteReleasedRightName { get; } = "DeleteReleasedProject";
        public static string IgnoreMaintenanceMode { get; } = "IgnoreMaintenanceMode";

        public static string ReleaseProjectObsoleteConfigVersionRightName { get; } = "ReleaseProjectObsoleteConfigVersionRightName";
        public static string ExportFilesObsoleteConfigVersionRightName { get; } = "ExportFilesObsoleteConfigVersionRightName";
        public static string ExportUnreleasedProjectRightName { get; } = "ExportUnreleasedProjectRightName";

        public static string ValidationModeRightName { get; } = "ValidationModeRightName";

        #endregion
    }
}