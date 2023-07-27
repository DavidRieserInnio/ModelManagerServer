using ModelManagerServer.St4.Enums;
using System;

namespace ModelManagerServer.St4
{
    public class RuleMethod
    {
        #region Properties
        //Resharper disable InconsistentNaming
        public Guid RuleMethods_Id { get; set; }

        public int RuleMethods_Version { get; set; }

        public string RuleMethods_Name { get; set; }

        public string RuleMethods_Description { get; set; }


        public string RuleMethods_Content { get; set; }


        public string RuleMethods_Comments { get; set; }

        public int RuleMethods_Position { get; set; }

        public MethodType RuleMethods_Type { get; set; }

        public RuleMethodState RuleMethods_State { get; set; }

        public Guid? RuleMethods_ParentId { get; set; }

        public int? RuleMethods_ParentVersion { get; set; }


        /// <summary>
        /// SSO
        /// </summary>
        public Guid? RuleMethods_SaveBy_Users_Id { get; set; }

        public string RuleMethods_SaveBy { get; set; } // zusatz: aufgelöst auf Namen, so dass die Anzeige des Usernamen lesbar wird


        public DateTime? RuleMethods_SaveTime { get; set; }

        public Guid RuleMethods_CreatedBy_Users_Id { get; set; }
        public string RuleMethods_CreatedBy { get; set; } // zusatz: aufgelöst auf Namen, so dass die Anzeige des Usernamen lesbar wird

        public DateTime RuleMethods_CreationTime { get; set; }

        public Guid? RuleMethods_LockedBy_Users_Id { get; set; }
        public string RuleMethods_LockedBy { get; set; } // zusatz: aufgelöst auf Namen, so dass die Anzeige des Usernamen lesbar wird


        public bool Editable { get; set; }

        //Resharper enable InconsistentNaming
        #endregion

        public override string ToString()
        {
            return $"{RuleMethods_Name}";
        }
    }
}