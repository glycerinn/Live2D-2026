using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public int trashThrown;

    private void Awake()
    {
        Instance = this;
    }

    public void AddTrash()
    {
        trashThrown++;
    }
}