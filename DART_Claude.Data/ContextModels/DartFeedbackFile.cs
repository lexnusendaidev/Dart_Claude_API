using System;
using System.Collections.Generic;

namespace DART_Claude.Data.ContextModels;

public partial class DartFeedbackFile
{
    public long FfId { get; set; }

    public int FfDartFeedbackId { get; set; }

    public string FfFilePathway { get; set; } = null!;

    public int FfCreateBy { get; set; }

    public DateTime FfCreateDate { get; set; }

    public int? FfUpdatedBy { get; set; }

    public DateTime? FfUpdateDate { get; set; }
}
