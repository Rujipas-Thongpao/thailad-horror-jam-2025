using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private RoomController roomController;

    private readonly List<BaseMark> marks = new();

    public void Init(int difficulty)
    {
        for (int i = 0; i < 5; i++)
        {
            marks.Add(GetRandomMark());
        }

        roomController.Init(marks);
    }

    public void Dispose()
    {
        
    }

    private BaseMark GetRandomMark()
    {
        BaseMark mark = Random.Range(0, 3) switch
        {
            0 => new FlickerMark(0),
            1 => new ShakeMark(0),
            2 => new PlayerDizzyMark(0),
            _ => new PlayerDizzyMark(0)
        };

        return mark;
    }
}
