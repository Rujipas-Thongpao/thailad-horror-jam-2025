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
        "Poltergeist"
    };

    public static BaseMark Create(int index, int intensity)
    {
        BaseMark mark = index switch
        {
            0 => new FlickerMark(intensity),
            1 => new ShakeMark(intensity),
            2 => new PlayerDizzyMark(intensity),
            3 => new HandTremorsMark(intensity),
            4 => new PoltergeistMark(intensity),
            5 => new GhostMark(intensity),
            _ => new FlickerMark(intensity),
        };

        return mark;
    }
}
