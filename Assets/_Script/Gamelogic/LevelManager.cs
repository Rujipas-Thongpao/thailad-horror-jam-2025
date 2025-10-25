using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<BaseMark> marks = new();

    public void Init(int difficulty)
    {
        // marks.Add(GetRandomMark());
    }

    public void Dispose()
    {
        
    }

    #region event listeners

    private void OnPlayerNearby()
    {
        
    }

    private void OnPicked()
    {
        
    }

    private void OnImpactHit()
    {
        
    }

    #endregion

    // private BaseMark GetRandomMark()
    // {
    //     BaseMark mark;
    //
    //     switch (Random.Range(0, 3))
    //     {
    //         case 0:
    //             mark = new 
    //             break;
    //         default:
    //             break;
    //     }
    //
    //     return mark;
    // }
}
