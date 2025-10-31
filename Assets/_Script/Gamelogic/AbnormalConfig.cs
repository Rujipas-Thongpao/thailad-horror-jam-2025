public static class AbnormalConfig
{
    public static int Count => Infos.Length;

    public static readonly string[] Infos = 
    {
        "By report, residents saw the light flicker on their own.", //Flicker
        "This place was reported to tremble without reason.", //Room Shake
        "By report, this abnormality can induce vivid hallucinations.", //Dizzy
        "Reports state the victims suffered a crawling sensation beneath their skin.", //Hand Tremor
        "By report, residents witnessed object moving by themselves.", //Poltergeist
        "By report, there are dark subjects roaming around in this room, don't look at their eyes." // ghost
    };

    public static readonly string[] Names =
    {
        "Short Circuit", //Flicker
        "Room Quake", //Room Shake
        "Hallucination", //Dizzy
        "Nerve Paralyze", //Hand Tremor
        "Poltergeist",
        "Spectre"
    };

    public static BaseMark Create(int index, int intensity)
    {
        BaseMark mark = index switch
        {
            0 => new FlickerMark(intensity, Names[index]),
            1 => new ShakeMark(intensity, Names[index]),
            2 => new PlayerDizzyMark(intensity, Names[index]),
            3 => new HandTremorsMark(intensity, Names[index]),
            4 => new PoltergeistMark(intensity, Names[index]),
            5 => new GhostMark(intensity, Names[index]),
            _ => new FlickerMark(intensity, Names[0]),
        };

        return mark;
    }
}
