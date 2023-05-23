using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{ 
   public  bool  IsBuyed = false;
   public WeaponInfo WeaponInfoo;
   public GameObject WeaponGameobject;
   public Transform muzzleFlash;
   public TextMeshProUGUI PriceCount;
   public TextMeshProUGUI LevelForOpen;
   
   public Animator Animator;
   public GameObject UIweaponPanel;
   
   public bool IsLoaded = false;
   public BuilderBase Builder;
   public Animator Magazine;
   public int CurrentBulletsCount;

   public Transform BulletSpawn;

   [Header("Sound")]
   public AudioSource _shotSound;
   
}
