using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    public int CoinsValue = 1000;
    public int Level;
    public float MaxExperienceCount = 10;
    public float UpLevelBonusCoinsValue = 200;
}



public class Init : MonoBehaviour
{
    public PlayerData playerData;
}



