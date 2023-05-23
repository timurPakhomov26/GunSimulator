using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UiController : MonoBehaviour
{
    public static int WeaponIndex = 0;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private TextMeshProUGUI _coinsValueText;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Item[] _items;
    [SerializeField] private TextMeshProUGUI _currentBuletsCountText;
    [SerializeField] private TextMeshProUGUI _bulletsInMagazineText;

    [SerializeField] private Animator _uiAnimator;

    private string _weaponNameString = "Оружие: ";
    private string _weaponClassString = "Класс: ";
    

    [Header("Weapon Info")]
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private TextMeshProUGUI _weaponClass;
    [SerializeField] private GameObject _pricePanel;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _bulletType;
     
    [Header("Level")]
    [SerializeField] private Image _levelView;
    [SerializeField] private GameObject _upLevelBonusPanel;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private float _maxExperienceCount = 10f;
    private float _currentExperienceCount;
    [SerializeField] private Button _upLevelButton;
    [SerializeField] private Color _upLevelButtonColorOnActive;
    [SerializeField] private Color _upLevelButtonColorOnNotActive;
    private bool _isReadyToUpgrade = false;
    private float _levelPoints;

    
    
    private void OnEnable() 
    {
       Weapon.OnShot += AddExperience;  
    }

    private void OnDisable() 
    {
       Weapon.OnShot -= AddExperience;  
      
    }


    private void Start() 
    {
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
       _upLevelBonusPanel.SetActive(false);
       ApplyUiElements();
       SetPricePanel();
       _currentExperienceCount = 0;
       _levelView.fillAmount = _currentExperienceCount;

    }

    private void Update()
    {
      if(_currentExperienceCount >= _maxExperienceCount)
      {
          SetUpLevelButton(true,_upLevelButtonColorOnActive); 
          _uiAnimator.SetTrigger("ActiveUppButton"); 
      }
      else
      {
          SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
          _uiAnimator.SetTrigger("NotActiveUppButton"); 
      }
    }

    public void OnLeftButtonDown()
    {
        if(WeaponIndex <= 0)
        {
           WeaponIndex = _weapon.ItemsLenght - 1;
        }
        else
        {
          WeaponIndex --;
        }

       ApplyUiElements();
       SetPricePanel();
    }


    public void OnRightButtonDown()
    {
       if(WeaponIndex >= _weapon.ItemsLenght - 1)
        {
           WeaponIndex = 0;
        }
        else
        {
          WeaponIndex ++;
        }

        ApplyUiElements();
        SetPricePanel();
    }

    
    public void ApplyUiElements()
    {
       _bulletsInMagazineText.text = _items[WeaponIndex].WeaponInfoo.BulletsCount.ToString(); 
       _currentBuletsCountText.text = _items[WeaponIndex].CurrentBulletsCount.ToString() + "/"; 
       _weaponName.text = _weaponNameString + _items[WeaponIndex].WeaponInfoo.WeaponName.ToString();
       _weaponClass.text = _weaponClassString + _items[WeaponIndex].WeaponInfoo.WeaponClass.ToString();
       _price.text = _items[WeaponIndex].WeaponInfoo.Price.ToString();
       _coinsValueText.text = _playerData.CoinsValue.ToString();
       _levelText.text = "Level: " + _playerData.Level.ToString();
      // _bulletType.sprite = _items[WeaponIndex].WeaponInfoo.BulletSprite;
        _levelView.fillAmount = _currentExperienceCount / _maxExperienceCount;
    }

    private void SetPricePanel()
    {
       if(_items[WeaponIndex].IsBuyed == true)
       {
         _pricePanel.SetActive(false);
         _items[WeaponIndex].Builder.Enabled = true;
       }
       else
       {
         _pricePanel.SetActive(true);
         _items[WeaponIndex].Builder.Enabled = false;
       }
    }

    public void BuyWeapon()
    {
        if(_playerData.CoinsValue >= _items[WeaponIndex].WeaponInfoo.Price)
        {
          _items[WeaponIndex].IsBuyed = true;
          _playerData.CoinsValue -= _items[WeaponIndex].WeaponInfoo.Price;
          SetPricePanel();
          ApplyUiElements();
        }
    }

    public void UppLevel()
    {
      if(_isReadyToUpgrade == true)
      {
       _uiAnimator.SetTrigger("NotActiveUppButton");
       _playerData.Level ++;
       _currentExperienceCount = 0;
       ApplyUiElements();
       OpenBonusPanel();
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);

      }
       
    }

    public void CloseBonusPanel()
    {
       _upLevelBonusPanel.SetActive(false);
    }


    private void OpenBonusPanel()
    {
      _upLevelBonusPanel.SetActive(true);
    }
     

    private void AddExperience()
    {
      if(_currentExperienceCount >= _maxExperienceCount)
      {
        // _levelView.fillAmount = _currentExperienceCount / _maxExperienceCount;
         // SetUpLevelButton(true,_upLevelButtonColorOnActive);
         
      }
      else
      {
         _currentExperienceCount += _items[WeaponIndex].WeaponInfoo.ExperiencePerShot;
         _levelView.fillAmount = _currentExperienceCount / _maxExperienceCount;
          SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
      }
       
    }

    private void SetUpLevelButton(bool isReady,Color color)
    {
       _isReadyToUpgrade = isReady;
       _upLevelButton.GetComponent<Image>().color = color;
    }

    public void SetInfinityBullets()
    {
       StartCoroutine(StartInfinityBulletsEffect());
    }

    private IEnumerator StartInfinityBulletsEffect()
    {
      _bulletsInMagazineText.text = " ";
      _currentBuletsCountText.transform.Rotate(0,0,90f);
      _currentBuletsCountText.text = "8";
       yield return new WaitForSeconds(10f);
    }
    
}
