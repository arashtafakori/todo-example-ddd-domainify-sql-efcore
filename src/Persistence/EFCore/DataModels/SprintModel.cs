﻿
using Domain.ProjectSettingAggregation;
using System.ComponentModel.DataAnnotations;

namespace Persistence
{
    public class SprintModel
    {
        [Required]
        public required Guid Id { get; set; }
        public required bool IsDeleted { get; set; } = false;

        public required DateTime ModifiedDate { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
        public required string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public static SprintModel InstanceOf(Sprint sprint, string projectId)
        {
            var dataModel = new SprintModel()
            {
                Id = Guid.TryParse(sprint.Id, out var sprintId) ? sprintId : Guid.NewGuid(),
                IsDeleted = sprint.IsDeleted,
                ModifiedDate = sprint.ModifiedDate,

                ProjectId = new Guid(projectId),
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
            };
  
            return dataModel;
        }

        public Sprint ToEntity()
        {
            var sprint = Sprint.NewInstance();
            sprint.SetId(Id.ToString());
            sprint.ModifiedDate = ModifiedDate;
            sprint.IsDeleted = IsDeleted;

            sprint.SetName(Name)
                .SetStartDate(StartDate)
                .SetEndDate(EndDate);

            return sprint;
        }

        public ProjectModel Project { get; private set; }
        public ICollection<TaskModel> Tasks { get; }
    }
}
