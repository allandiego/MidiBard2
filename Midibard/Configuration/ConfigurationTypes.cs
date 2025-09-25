public enum PlayMode
{
    Single,
    SingleRepeat,
    ListOrdered,
    ListRepeat,
    Random
}

public enum GuitarToneMode
{
    Off,
    Standard,
    Simple,
    OverrideByTrack,
    ProgramElectricGuitarMode,
    //OverrideByChannel,
}

public class TrackStatus
{
    public bool Enabled = false;
    public int Tone = 0;
    public int Transpose = 0;
}

//public struct ChannelStatus
//{
//    public ChannelStatus(bool enabled = true, int tone = 0, int transpose = 0)
//    {
//        Enabled = enabled;
//        Tone = tone;
//        Transpose = transpose;
//    }

//    public bool Enabled = true;
//    public int Tone = 0;
//    public int Transpose = 0;
//}

public class EnsembleMemberConfig
{
    public long Cid;
    public string Name;
    public string TrackAssignmentRegex;
}

public enum ChatType
{
    Current = 0,
    Say = 1,
    Party = 2,
    Linkshell1 = 3,
    Linkshell2 = 4,
    Linkshell3 = 5,
    Linkshell4 = 6,
    Linkshell5 = 7,
    Linkshell6 = 8,
    Linkshell7 = 9,
    Linkshell8 = 10,
    CrossLinkshell1 = 30,
    CrossLinkshell2 = 31,
    CrossLinkshell3 = 32,
    CrossLinkshell4 = 33,
    CrossLinkshell5 = 34,
    CrossLinkshell6 = 35,
    CrossLinkshell7 = 36,
    CrossLinkshell8 = 37
}

public enum AntiStackType
{
    Off = 0,
    KeepFirstNote = 1,
    KeepShortestNote = 2,
    KeepLongestNote = 3,
}

public enum FilterPlayedSongOptions
{
    ShowAll = 0,
    ShowPlayed = 1,
    ShowUnPlayed = 2,
}

public enum CompensationModes
{
    None = 0,
    ByInstrument = 1,
    ByInstrumentNote = 2,
}
