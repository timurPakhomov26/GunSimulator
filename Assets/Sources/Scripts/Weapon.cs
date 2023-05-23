using System.Collections;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public static Action OnShot;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Item[] _items;
    [SerializeField] private UiController _uiController;
    public int ItemsLenght => _items.Length;

    private int _index;
    private int _previusItemIndex = -1;
    private float _time;
    private bool _isMobileButtonDown = false;
    


   private void OnEnable() 
   {
      for(int i=0; i < _items.Length;i++)
      {
         _items[i].Builder.OnTrigger += CheckOnMagazineAnimation;
         _items[i].Builder.OnReloadMagazine += ReloadMagazine;
      }
   }


   private void OnDisable() 
   {
      for(int i=0; i < _items.Length;i++)
      {
         _items[i].Builder.OnTrigger -= CheckOnMagazineAnimation;
         _items[i].Builder.OnReloadMagazine -= ReloadMagazine;
      }  
   }

   private void Start() 
   {
      foreach(var item in _items)
      {
        item.gameObject.SetActive(false);
        item.UIweaponPanel.SetActive(false);
        item.IsBuyed = false;
       // item.Builder.enabled = false;
      }

      _items[0].IsBuyed = true;
     // _items[0].Builder.enabled = true;
       
       EquipItem(0);   
   }


    public void ZatvorInput()
    {
       if(_items[UiController.WeaponIndex].Builder.MagazineInWeapon == true)
       {
         _items[UiController.WeaponIndex].IsLoaded = true;
       }

       _items[UiController.WeaponIndex].Animator.SetTrigger("Load");
       StartCoroutine(ComeAnimationToDefault());

    }

    public void ShotInput()
    {
       if(_items[UiController.WeaponIndex].IsLoaded == true)
       {
         if(_items[UiController.WeaponIndex].CurrentBulletsCount > 0)
         {
           _items[UiController.WeaponIndex].Animator.SetTrigger("Shot");
           StartCoroutine(ComeAnimationToDefault()); 
           BulletFly();
           _playerData.CoinsValue += _items[_index].WeaponInfoo.CoinsPerShot;
           
           OnShot?.Invoke();
           _items[UiController.WeaponIndex].CurrentBulletsCount --;
     
           _uiController.ApplyUiElements();

         }
         else
         {
            _items[UiController.WeaponIndex].Animator.SetTrigger("ShotWithout");
            StartCoroutine(ComeAnimationToDefault());
           
           ReloadMagazine();
         }
        
       }

       else
       {
         
       _items[UiController.WeaponIndex].Animator.SetTrigger("ShotWithout");
         StartCoroutine(ComeAnimationToDefault());
       }   
    }


    private IEnumerator ComeAnimationToDefault()
    {
       yield return new WaitForSeconds(0.4f);
       _items[UiController.WeaponIndex].Animator.SetTrigger("Idle");
    }



    private void Update() 
    {
       

      EquipItem(UiController.WeaponIndex);
     // CheckOnMagazineAnimation();

     /* for(int i = 0; i < _items.Length; i++)
      {
         if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Desktop)
         {
            if(Input.GetKeyDown((i + 1).ToString()) == false) 
             continue;
         }
         else 
         {
           EquipItem(_uiController.WeaponIndex);
           break;
         }
         

         EquipItem(i);
         break;
      }

       _time += Time.deltaTime;
       
       if(_items[_index].WeaponInfoo.Type == WeaponType.Automatic)
       {
           if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Desktop)
           {
              AutomaticShoot();

           }
           else
           {
              MobileShoot();
           }
       }
       else if(_items[_index].WeaponInfoo.Type == WeaponType.NonAutomatic)
       {
          if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Desktop)
           {
              NonAutomaticShoot();

           }
           else
           {
              MobileShoot();
           }
       }*/
    }

   /* public void CheckOnMagazineAnimation()
    {
       if(_items[UiController.WeaponIndex].WeaponBuilder.MagazineInTrigger == true)
       {
          _items[UiController.WeaponIndex].Animator.SetTrigger("Magazine");
          _items[UiController.WeaponIndex].WeaponBuilder.MagazineInWeapon = true;
          StartCoroutine(ComeAnimationToDefault());
       }
    }*/

    

   private void CheckOnMagazineAnimation()
    {
       if(_items[UiController.WeaponIndex].Builder.MagazineInTrigger == true)
       {
          _items[UiController.WeaponIndex].Magazine.SetTrigger("Magazine");
          _items[UiController.WeaponIndex].Builder.MagazineInWeapon = true;
          _items[UiController.WeaponIndex].CurrentBulletsCount = _items[UiController.WeaponIndex].WeaponInfoo.BulletsCount;
          _uiController.ApplyUiElements();
         // StartCoroutine(ComeAnimationToDefault());
       }
    }

    private void ReloadMagazine()
    {
       _items[UiController.WeaponIndex].IsLoaded = false;
       _items[UiController.WeaponIndex].Builder.IsCollider = false;
       _items[UiController.WeaponIndex].Builder.MagazineInTrigger = false;
       _items[UiController.WeaponIndex].Builder.MagazineInWeapon = false;
       _items[UiController.WeaponIndex].Magazine.SetTrigger("NewMagazine");
       
    }



    private void AutomaticShoot()
    {
       if(Input.GetMouseButton(0) && _time >= _items[_index].WeaponInfoo.RateOfFire)
       {
          _items[_index]._shotSound.Play();

           BulletFly();
           //OnShot?.Invoke();
           _time = 0;
       }  
       if(Input.GetMouseButtonUp(0))
       {
          _items[_index]._shotSound.Stop();

       } 
    }

    private void NonAutomaticShoot()
    {
       if(Input.GetMouseButtonDown(0) && _time >= _items[_index].WeaponInfoo.RateOfFire)
       {
          _items[_index]._shotSound.Play();

           BulletFly();
          // OnShot?.Invoke();
           _time = 0;
       } 
        if(Input.GetMouseButtonUp(0))
       {
         _items[_index]._shotSound.Stop();

       }  
    }

    public void MobileShootButton()
    {
       _isMobileButtonDown = true;
    }

    private void MobileShoot()
    {
      if(_isMobileButtonDown == true && _time >= _items[_index].WeaponInfoo.RateOfFire)
      {
           _items[_index]._shotSound.Play();

           BulletFly();
          // OnShot?.Invoke();
           _time = 0;
           _isMobileButtonDown = false;
      }
    }

    private void BulletFly()
    {   
       _bulletPool.Create(_items[UiController.WeaponIndex].BulletSpawn.position,Vector3.right,
                         _items[UiController.WeaponIndex].WeaponInfoo.BulletSprite);
       
    }


     private void EquipItem(int index)
     {
        _index = index;

        if(_index == _previusItemIndex)
           return;

        if(_index >= _items.Length)
           return;
       
        _items[_index].WeaponGameobject.SetActive(true);
        _items[_index].UIweaponPanel.SetActive(true); 

        if(_previusItemIndex != -1)
        {
           _items[_previusItemIndex].WeaponGameobject.SetActive(false);
           _items[_previusItemIndex].UIweaponPanel.SetActive(false);
        
        }

        _previusItemIndex = _index;
     }
  
}


public enum ShootingType
{
   Single,
   Automaatic
}

