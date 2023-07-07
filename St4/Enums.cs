using System;
using System.ComponentModel;

namespace ModelManagerServer.St4.Enums
{
    public enum St4Milestone
    {
        None = 0,
        [Description("LongLead")]
        LongLead,
        [Description("WD")]
        WiringDiagram,
        [Description("SW")]
        Software,
        [Description("Preliminary")]
        Preliminary,
    }

    public enum St4RightGroup
    {
        [Description("Admin Group")]
        Admin,
        [Description("Base Group")]
        BaseGroup,
        [Description("Channel Partner")]
        ChannelPartner,
        [Description("Clarke Energy")]
        ClarkeEnergy,
        [Description("PM Base User")]
        PMBaseUser,
        [Description("PM Key User")]
        PMKeyUser,
        [Description("PPE Key User")]
        PPEKeyUser,
        [Description("TPE Standard User")]
        TPEStandarUser,
        [Description("TPE Teamleader")]
        TPETeamleader,
        [Description("TPR Key User")]
        TPRKeyUser
    }

    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Enums.cs" />
    /// </summary>
    public enum St4ApplicationStatus
    {
        None,
        Error,
        Working,
        Ready
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4AppVersionsState
    {
        Released = St4ConfigState.Released,
        Forbidden = St4ConfigState.Forbidden
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4ElementType
    {
        Combo = 1,
        CheckBox = 2,
        TextBox = 3
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4PropertyTranslationState
    {
        LanguageNeutral = 1,
        NotUpToDate = 2,
        UpToDate = 4,
        ExportedForTranslation = 8
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4Permission
    {
        None = 0,
        Read = 1,
        Write = 2
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4PartState
    {
        Working = St4ConfigState.Working,
        Released = St4ConfigState.Released
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4RuleMethodState
    {
        Working = St4ConfigState.Working,
        Released = St4ConfigState.Released
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4ProjectNameChanged
    {
        Rename,
        Copy
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4ProjectState
    {
        Working = St4ConfigState.Working,
        // Test = St4ConfigState.Test,
        Released = St4ConfigState.Released,
        Obsolete = St4ConfigState.Obsolete
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4ProjectTemplateState
    {
        Working = St4ConfigState.Working,
        // Test = St4ConfigState.Test,
        Released = St4ConfigState.Released,
        Obsolete = St4ConfigState.Obsolete
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4ProjectParentMode
    {
        /// <summary>created new project</summary>
        Empty = 0,

        /// <summary>dependency only visible for internal usage e.g. JNr changed</summary>
        Data = 10,

        /// <summary>reference to parent project visible for standard user</summary>
        Link = 20
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4OpenProjectFilterMode
    {
        All = 10,
        My_Projects = 20,
        Last_3_Months = 30,
        Filter_by_JNr = 40,
        Filter_by_Software_Version = 50
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4ConfigState
    {
        Working = 10,
        Test = 20,
        Released = 30,
        Obsolete = 40,
        Forbidden = 50
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4MethodType
    {
        Executable = 1,
        Help = 2,
        HelpExtended = 3
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4MessageType
    {
        Info,
        Success,
        Warning,
        Error
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4PartValueChangeType
    {
        ReviewClick,
        ValueClick,
        ReviewByParent,
        ImportValueChanging,
        ImportNoValueChanging,
        RemoveReview
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4FilterMode
    {
        Text,
        DataSource,
        ReviewState
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4FilterOperation
    {
        Contains,
        NotContains,
        Equals,
        NotEquals,
        RegEx
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4LoadingConfigurationStartup
    {
        Ask,
        LastSelected,
        Newest
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4Logging
    {
        None = 0,
        Errors = 10,
        Warnings = 20,
        Messages = 30, // error warning and info
        Advanced = 40
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4ModificationType
    {
        None,
        New,
        Deleted,
        Modified
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4RestoreConfigParentMode
    {
        None,
        NewSubContext,
        NewSubContextVersion
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4MaintenanceState
    {
        Planned = 10,
        InProgress = 20,
        Canceled = 30,
        Complete = 40
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4MaintenanceMode
    {
        Shutdown = 10,
        NoShutdown = 20
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4MessageSeverity
    {
        Info = St4MessageType.Info,
        Warning = St4MessageType.Warning,
        Error = St4MessageType.Error,
        Fatal
    }


    /// <inheritdoc cref="St4ApplicationStatus" />
    [Flags]
    public enum St4PartVisibility
    {
        Visible = 0,
        HiddenByRule = 1,
        HiddenByFilter = 2
    }

    /// <inheritdoc cref="St4ApplicationStatus" />
    public enum St4MilestonesFallbackStrategy
    {
        ExworksBackward = 1,
        OrderBookingForward = 2
    }
}
