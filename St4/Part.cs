using System.ComponentModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.Collections.Generic;
using System.Linq;
using static Azure.Core.HttpHeader;
using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.St4
{
    /// <summary>
    ///     See ST4 Configurator: <see href="https://github.com/innio-etechnik/Configurator_V2/blob/master/Model/Config/Part.cs" />
    /// </summary>
    public class Part
    {
        #region Properties
        public Guid Parts_Id { get; set; }
        public int Parts_Version { get; set; }

        [Description("Möglichen Werte Text, Combo, Checkbox")]
        public St4ElementType Parts_Type { get; set; }

        /// <summary>
        /// Der Wert aus der Datenbank wird mit dem Wert aus dem item properties überschrieben
        /// </summary>
        public int Parts_Position { get; set; }

        public St4PartState Parts_State { get; set; }
        public string Parts_SpecialUsage { get; set; }

        public Guid? Parts_ParentId { get; set; }
        public int? Parts_ParentVersion { get; set; }

        [NotMapped] public List<Property> Properties { get; set; }
        [NotMapped] public List<PartPermission> PartPermissions { get; set; }

        public void AddPartPermissions(List<PartPermission> permissions)
        {
            if (this.PartPermissions == null || this.PartPermissions.Count == 0)
            {
                this.PartPermissions = permissions;
            }
            else
            {
                this.PartPermissions.AddRange(permissions);
            }
        }

        public string Circuit
        {
            get
            {
                string circuit = "";
                circuit = Properties.FirstOrDefault(x => x.Properties_Key.Equals(Common.PropertyCircuit))?.Properties_Value;
                return circuit;
            }
        }

        public string Group
        {
            get
            {
                string group = "";
                group = Properties.FirstOrDefault(x => x.Properties_Key.Equals(Common.PropertyGroup))?.Properties_Value;
                return group;
            }
        }

        #endregion

        #region CTOR

        public Part()
        {
            Properties = new List<Property>();
            Parts_Id = Guid.NewGuid();
            Parts_Version = 1;
            Parts_State = St4PartState.Working;
        }

        public Part(St4ElementType type)
        {
            Properties = new List<Property>();
            Parts_Id = Guid.NewGuid();
            Parts_Version = 1;
            Parts_Type = type;
            Parts_State = St4PartState.Working;
        }

        #endregion

        public void AddProperty(string key, string value, Guid? comboItemgroup = null)
        {
            bool translatable = Common.TranslatablePropertyKeys.Contains(key);
            var property = new Property(this, translatable);
            property.Properties_Position = Properties.Count + 1;
            property.Properties_Key = key;
            property.UpsertValue(Common.FallbackLanguage, value);
            property.Properties_ComboBoxItemGroup = comboItemgroup;

            Properties.Add(property);
        }

        public void SetProperties(List<Property> properties)
        {
            Properties = new List<Property>();
            Properties.AddRange(properties.Where(x => x.Parts_Id == Parts_Id && x.Parts_Version == Parts_Version).ToList());
        }

        public void SetPermissions(List<PartPermission> permissions)
        {
            PartPermissions = new List<PartPermission>();
            PartPermissions.AddRange(permissions.Where(x => x.Parts_Id == Parts_Id && x.Parts_Version == Parts_Version).ToList());
        }

        [NotMapped]
        public string ElementName
        {
            get
            {
                var property = Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyName);
                return property == null ? "" : property.Properties_Value;
            }
        }

        public bool Matches(Part part)
        {
            if (Parts_Type != part.Parts_Type) return false;

            // if unit of the part does not match
            if (Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyUnit)?.Properties_Value !=
                   part.Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyUnit)?.Properties_Value) return false;

            // if elementtext of the part does not match
            if (Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyElementText)?.Properties_Value !=
                   part.Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyElementText)?.Properties_Value) return false;

            // ------------ Checkbox / Textbox ------------
            if (Parts_Type == St4ElementType.CheckBox || Parts_Type == St4ElementType.TextBox)
            {
                // if article code does not match
                if (Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyArticleCode)?.Properties_Value !=
                   part.Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyArticleCode)?.Properties_Value) return false;
            }

            // ------------ Textbox ------------
            if (Parts_Type == St4ElementType.TextBox)
            {
                // if maxlength does not match
                if (Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyMaxLength)?.Properties_Value !=
                     part.Properties.FirstOrDefault(x => x.Properties_Key == Common.PropertyMaxLength)?.Properties_Value) return false;
            }

            // ------------ Combobox ------------
            if (Parts_Type == St4ElementType.Combo)
            {
                // if there is a different count of combo elements
                if (Properties.Where(x => x.Properties_ComboBoxItemGroup != null).Count() !=
                    part.Properties.Where(x => x.Properties_ComboBoxItemGroup != null).Count()) return false;


                // check if the combo properties are the same
                var articleCodes1 = Properties.Where(x => x.Properties_Key == Common.PropertyArticleCode).Select(x => x.Properties_Value);
                var articleCodes2 = part.Properties.Where(x => x.Properties_Key == Common.PropertyArticleCode).Select(x => x.Properties_Value);
                var itemTexts1 = Properties.Where(x => x.Properties_Key == Common.PropertyComboItemText).Select(x => x.Properties_Value);
                var itemTexts2 = part.Properties.Where(x => x.Properties_Key == Common.PropertyComboItemText).Select(x => x.Properties_Value);


                IEnumerable<string> differenceAC = articleCodes1.Except(articleCodes2);
                IEnumerable<string> differenceIT = itemTexts1.Except(itemTexts2);

                if (differenceAC.Any() || differenceIT.Any()) return false;
            }

            // if no check has failed >> return true
            return true;
        }

        // TODO: Do we need these?
        /*
        public string GetHelpText()
        {
            var properties = new List<St4Property>();
            var propertiesKeys = new List<string>
            {
                Common.PropertyInfoDocument,
                Common.PropertyInfoPlcLabel,
                Common.PropertyInfoParameter,
                Common.PropertyInfoPid,
                Common.PropertyInfoSsl,
                Common.PropertyInfo
            };

            foreach (string key in propertiesKeys)
            {
                var p = Properties.Where(x => x.Properties_Key.Equals(key)).OrderBy(x => x.Properties_Position)
                    .ToList();
                properties.AddRange(p);
            }

            // group by machen !
            var groupedProperties = properties.GroupBy(x => x.Properties_Key).ToList();

            var sb = new StringBuilder();
            foreach (var propertyGroup in groupedProperties)
            {
                if (propertyGroup.Key == Common.PropertyInfo)
                {
                    sb.AppendLine(propertyGroup.Key + ":  " + string.Join("\n", propertyGroup.Select(x => x.Properties_Value)));
                }
                else
                {
                    sb.AppendLine(propertyGroup.Key + ":  " + string.Join(Session.CurrentAppSettings.ConfigHelpTextDelimiter, propertyGroup.Select(x => x.Properties_Value)));
                }
            }

            //properties.ForEach(x => sb.AppendLine(x.Properties_Key + ":  " + x.Properties_Value));

            return sb.ToString();
        }

        public List<ComboBoxExItem> GetComboItems()
        {
            var retVal = new List<ComboBoxExItem>();

            var comboItemProperties = Properties.Where(x => x.Properties_ComboBoxItemGroup != null).ToList();
            var distinctIds = comboItemProperties.Select(x => x.Properties_ComboBoxItemGroup).Distinct();
            St4Property itemArticleCodeProperty = null;
            St4Property itemTextProperty = null;

            foreach (var itemId in distinctIds)
            {
                var itemProperties = Properties.Where(x => x.Properties_ComboBoxItemGroup == itemId).ToList();

                itemArticleCodeProperty = itemProperties.FirstOrDefault(x => x.Properties_Key.Equals(Common.PropertyArticleCode));
                itemTextProperty = itemProperties.FirstOrDefault(x => x.Properties_Key.Equals(Common.PropertyComboItemText));

                retVal.Add(new ComboBoxExItem
                {
                    ArticleCode = itemArticleCodeProperty?.Properties_Value,
                    Text = itemTextProperty == null ? "<-- text not found -->" : itemTextProperty.Properties_Value,
                    Position = itemArticleCodeProperty == null ? 999 : itemArticleCodeProperty.Properties_Position,
                    ComboItemGroup = itemId.Value,
                    ItemProperties = itemProperties
                });
            }

            //sortieren
            retVal = retVal.OrderBy(x => x.Position).ToList();

            //insert empty value on 0th position          
            retVal.Insert(0, new ComboBoxExItem
            {
                Text = Common.ComboEmptyValue,
                ArticleCode = null,
                ComboItemGroup = null
            });

            return retVal;
        }
        */

        public Property GetPropertyByKey(string key)
        {
            return Properties.FirstOrDefault(x => x.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase));

        }

        public string GetPropertyValueByKey(string key, string defaultValue = "")
        {
            Property property = Properties.FirstOrDefault(x => x.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            return property == null ? defaultValue : property.Properties_Value;
        }

        /// <summary>
        /// get all property values matching the specified key
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="defaultValue">the value used when no property matches the key</param>
        /// <returns>all property values matching the key or the default value</returns>
        public List<string> GetAllPropertyValuesByKey(string key, string defaultValue = "")
        {
            var props = Properties.Where(p => p.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase)).ToList();
            return props.Any() ? props.Select(p => p.Properties_Value).ToList() : new List<string> { defaultValue };
        }

        /// <summary>
        /// checks if the key matches the expected value
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="expectedValue">the expected value to match against</param>
        /// <param name="operation">how to match <paramref name="key"/> with <paramref name="expectedValue"/></param>
        /// <param name="matchCase">whether to perform case sensitive matching</param>
        /// <returns><see langword="true"/> if the key matches the expected value, <see langword="false"/> otherwise</returns>
        public bool CheckFilter(string key, string expectedValue, St4FilterOperation operation, bool matchCase)
        {
            bool retVal = false;

            StringComparison sc = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            switch (operation)
            {
                case St4FilterOperation.Equals:
                    retVal = Properties.Exists(x => x.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase) && x.Properties_Value.Equals(expectedValue, sc));
                    break;
                case St4FilterOperation.NotEquals:
                    retVal = !Properties.Exists(x => x.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase) && x.Properties_Value.Equals(expectedValue, sc));
                    break;
                case St4FilterOperation.Contains:
                    retVal = Properties.Exists(x => x.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase) && x.Properties_Value?.IndexOf(expectedValue, sc) >= 0);
                    break;
                case St4FilterOperation.NotContains:
                    retVal = Properties.Exists(x => x.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase) && x.Properties_Value.IndexOf(expectedValue, sc) < 0);
                    break;
                case St4FilterOperation.RegEx:
                    var properties = Properties.Where(x => x.Properties_Key.Equals(key, StringComparison.OrdinalIgnoreCase)).ToList();
                    string keyValue = string.Join(";", properties.Select(x => x.Properties_Value));
                    var reg = new Regex(expectedValue, matchCase ? RegexOptions.None : RegexOptions.IgnoreCase);
                    Match match = reg.Match(keyValue);
                    retVal = match.Success;
                    break;
            }

            return retVal;
        }
    }
}