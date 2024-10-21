using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Backup
{
    public int BackupId { get; set; }

    public DateTime BackupDate { get; set; }

    public string BackupFile { get; set; } = null!;
}
