using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private RoomController roomController;

    private List<BaseMark> marks = new();

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
        BaseMark mark;
    
        switch (Random.Range(0, 3))
        {
            case 0:
                mark = new FlickerMark();
                break;
            default:
                mark = new FlickerMark();
                break;
        }
    
        return mark;
    }
}
