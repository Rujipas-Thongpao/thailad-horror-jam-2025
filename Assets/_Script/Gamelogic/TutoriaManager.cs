using UnityEngine;

public class TutoriaManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialFurniture;
    private GameplayController gameplayController;

    public void Init(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
        var tutorialFurnitureObj = Instantiate(tutorialFurniture);
        Debug.Log("Tutorial furniture instantiated: " + tutorialFurnitureObj.name);
    }
}
