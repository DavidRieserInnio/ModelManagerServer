using ModelManagerServer.St4.Enums;
using System;

namespace ModelManagerServer.St4
{
    internal class Milestone
    {
        public Guid Milestones_Id { get; set; }
        public string Milestones_Name { get; set; }
        public string Milestones_JobDepartement { get; set; }
        public string Milestones_JobName { get; set; }
        public int Milestones_JobWarningOffset { get; set; }
        public St4MilestonesFallbackStrategy Milestones_FallbackStrategy { get; set; }
        public int Milestones_FallbackOffset { get; set; }
        public Guid Milestones_CreatedBy_Users_Id { get; set; }
        public DateTime Milestones_CreationTime { get; set; }
    }
}
