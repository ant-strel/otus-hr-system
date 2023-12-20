using Domain.Entities.Entities;


namespace Services.Impl.Data
{
    public static class StartedWfInit
    {
        public static readonly Guid Created = new Guid("00000000-0000-0000-0000-000000000001");
        public static readonly Guid InProcess = new Guid("00000000-0000-0000-0000-000000000002");
        public static readonly Guid Rejected = new Guid("00000000-0000-0000-0000-000000000003");
        public static readonly Guid Accepted = new Guid("00000000-0000-0000-0000-000000000004");

        public static readonly Guid InPorgressCommand = new Guid("00000000-0000-0000-0000-000000000001");
        public static readonly Guid RejectCommand = new Guid("00000000-0000-0000-0000-000000000002");
        public static readonly Guid AcceptCommand = new Guid("00000000-0000-0000-0000-000000000003");

        public static IEnumerable<Status> Statuses => new List<Status>() 
        {
            new Status()
            {
               Id = Created,
               IsInitial = true,
               Name = "Отклик Создан",
               IsFinal = false,
               Description = ""
            },
            new Status()
            {
               Id = InProcess,
               IsInitial = false,
               Name = "Собеседуется",
               IsFinal = false,
               Description = ""

            },
            new Status()
            {
               Id = Accepted,
               IsInitial = false,
               Name = "Принят",
               IsFinal = true,
               Description = ""
            },
            new Status()
            {
               Id = Rejected,
               IsInitial = false,
               Name = "Отказ",
               IsFinal = true,
               Description = ""
            },
        };

        public static IEnumerable<Command> Command => new List<Command>() 
        {
            new Command()
            {
               Id = InPorgressCommand,
               StartStatusId = Created,
               EndStatusId = InProcess,
               Name = "Принять в работу",
               NeedResolution = false,
            },
            new Command()
            {
               Id = RejectCommand,
               StartStatusId = InProcess,
               EndStatusId = Rejected,
               Name = "Отклонить",
               NeedResolution = false,
            },
            new Command()
            {
               Id = AcceptCommand,
               StartStatusId = InProcess,
               EndStatusId = Accepted,
               Name = "Принять",
               NeedResolution = true,
            }
        };

    }
}
