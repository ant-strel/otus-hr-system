using Domain.Entities.Entities;


namespace Services.Impl.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<JobPosting> JobPostings => new List<JobPosting>() 
        { 
            new (Guid.Parse("76FDA61F-3431-43E4-AE61-E76CB437206E")),
            new (Guid.Parse("004398F0-2A6D-4BD1-B526-263583642C64")),
            new (Guid.Parse("27F24E84-E206-40FF-8C8B-52C25948BA2C"))
        };

        public static IEnumerable<JobPostingStatus> JobPostingStatuses { 
            get 
            {
                List<JobPostingStatus> responseStatuses = new List<JobPostingStatus>();
                var status = Statuses.First(x => x.IsInitial);
                foreach(var response in JobPostings)
                {
                    responseStatuses.Add(new JobPostingStatus() 
                                                            { 
                                                                Id = Guid.NewGuid(), 
                                                                JobPostingId = response.Id, 
                                                                StatusId = status.Id 
                                                            });
                }
                    return responseStatuses;
            } 
        }

        public static IEnumerable<Status> Statuses => new List<Status>() 
        {
            new Status()
            {
               Id = Guid.Empty,
               IsInitial = true,
               Name = "Отклик Создан",
               IsFinal = false,
               Description = ""

            },
            new Status()
            {
               Id = Guid.Parse("E8E8DF1D-A60F-4C55-AE2E-62A24159A9F1"),
               IsInitial = false,
               Name = "Собеседуется",
               IsFinal = false,
               Description = ""

            },
            new Status()
            {
               Id = Guid.Parse("629B6834-72BB-4DEF-A608-E20BE20361A8"),
               IsInitial = false,
               Name = "Принят",
               IsFinal = true,
               Description = ""
            },
            new Status()
            {
               Id = Guid.Parse("50CA2A65-F45C-42FC-9EB4-0542FCF18573"),
               IsInitial = false,
               Name = "Отказ",
               IsFinal = true,
               Description = ""
            },
        };

    }
}
