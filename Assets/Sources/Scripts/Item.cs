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

   public GameObject IBuilderGameObject;
   public IBuilder Builder;
   
   public bool IsLoaded = false;
   
   public Animator Magazine;
   public int CurrentBulletsCount;
   public int CountOfAddedBullets;

   public Transform BulletSpawn;

   [Header("Sound")]
   public AudioSource _shotSound;


   private void OnEnable() 
   {
     // Builder = IBuilderGameObject.GetComponent<IBuilder>();
   }
   
}
