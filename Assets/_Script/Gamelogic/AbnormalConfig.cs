public static class AbnormalConfig
{
    public static int Count => Infos.Length;

    public static readonly string[] Infos = 
    {
        "Flicker",
        "Room Shake",
        "Dizzy",
        "Hand Tremor",
        "Poltergeist",
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
            _ => new PoltergeistMark(intensity),
        };

        return mark;
    }
}
