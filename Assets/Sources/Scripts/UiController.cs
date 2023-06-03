using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UiController : MonoBehaviour
{
    public static int WeaponIndex = 0;
    [SerializeField] private Init _init;
    [SerializeField] private TextMeshProUGUI _coinsValueText;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Item[] _items;
    [SerializeField] private TextMeshProUGUI _currentBuletsCountText;
    [SerializeField] private TextMeshProUGUI _bulletsInMagazineText;

    [SerializeField] private Animator _uiAnimator;


     
    [SerializeField] private GameObject _bulletsCountPanel;
    [SerializeField] private GameObject _infinityBulletsSign;
    [SerializeField] private TextMeshProUGUI _infinityTimeText;
    [SerializeField] private GameObject _infinityButton;


   
    [Header("ForTranslate")]
     private const string InfinityBulletsString = "Бесконечные патроны: ";
     private const string SecondsString = " Секунд";
    [SerializeField] private float _infinityTime = 30f;
    private float _startInfinityTime;
    public static bool InfinityBulletsActive = false;
    private float _timeForInfinityBullets = 15f;
    private const string WeaponNameString = "Оружие: ";
    private const string WeaponClassString = "Класс: ";
    private const string _500dollarsString = "500 долларов";

    [Header("Weapon Info")]
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private TextMeshProUGUI _weaponClass;
    private const string LevelString = "Уровень ";
    [SerializeField] private GameObject _pricePanel;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _needLevelFoOpen;
    

     
    [Header("Level")]
    private  const float MaxExperienceCountKoefficent = 1.2f;
    private const float UpLevelBonusCoinsKoefficent = 1.2f;
    [SerializeField] private Image _levelView;
    [SerializeField] private GameObject _upLevelBonusPanel;
    [SerializeField] private TextMeshProUGUI _upLevelBonusCoins;
    [SerializeField] private TextMeshProUGUI _levelText;
    private float _currentExperienceCount;
    [SerializeField] private Button _upLevelButton;
    [SerializeField] private Color _upLevelButtonColorOnActive;
    [SerializeField] private Color _upLevelButtonColorOnNotActive;




    private bool _isReadyToUpgrade = false;
    private float _levelPoints;

    [Header("Ads")]
    [SerializeField] private TextMeshProUGUI _500dollarsText;
    [SerializeField] private float _timeForAds;
    private int _moneyCountPerAds = 500;
     

    
 
    private void Start() 
    {
       _500dollarsText.text = _500dollarsString;
      _timeForInfinityBullets = _infinityTime;
      _startInfinityTime = _infinityTime;
      _infinityButton.SetActive(true);
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
       _upLevelBonusPanel.SetActive(false);
       ApplyUiElements();
       SetPricePanel();
       _currentExperienceCount = 0;
       _levelView.fillAmount = _currentExperienceCount;
       SetBulletsCountView(false,true);

    }

    private void Update()
    {
      if(_currentExperienceCount >= _init.playerData.MaxExperienceCount)
      {
          SetUpLevelButton(true,_upLevelButtonColorOnActive); 
          _uiAnimator.SetTrigger("ActiveUppButton"); 
      }
      else
      {
          SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
          _uiAnimator.SetTrigger("NotActiveUppButton"); 
      }
       _timeForAds += Time.deltaTime;
      TimeInfinity();
      ShowAdsInterstitial();

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
       _weaponName.text = WeaponNameString + _items[WeaponIndex].WeaponInfoo.WeaponName.ToString();
       _weaponClass.text = WeaponClassString + _items[WeaponIndex].WeaponInfoo.WeaponClass.ToString();
       _price.text = _items[WeaponIndex].WeaponInfoo.Price.ToString();
       _coinsValueText.text = _init.playerData.CoinsValue.ToString();
       _levelText.text = LevelString + _init.playerData.Level.ToString();
        _levelView.fillAmount = _currentExperienceCount / _init.playerData.MaxExperienceCount;
        _upLevelBonusCoins.text = ( Mathf.RoundToInt(_init.playerData.UpLevelBonusCoinsValue)).ToString();
        _needLevelFoOpen.text = LevelString + _items[WeaponIndex].WeaponInfoo.LevelFoOpen.ToString();
    }

    private void SetPricePanel()
    {
       if(_items[WeaponIndex].IsBuyed == true)
       {
         _pricePanel.SetActive(false);
         _items[WeaponIndex].Builder.Enabled= true;
         _items[WeaponIndex].Builder.SetEnable();

       }
       else
       {
         _pricePanel.SetActive(true);
         _items[WeaponIndex].Builder.Enabled = false;
          _items[WeaponIndex].Builder.SetEnable();
       }
    }

    public void BuyWeapon()
    {
        if(_init.playerData.CoinsValue >= _items[WeaponIndex].WeaponInfoo.Price && 
          _init.playerData.Level >= _items[UiController.WeaponIndex].WeaponInfoo.LevelFoOpen)
        {
          _items[WeaponIndex].IsBuyed = true;
          _items[WeaponIndex].GunTriggers.enabled = true;
          _init.playerData.CoinsValue -= _items[WeaponIndex].WeaponInfoo.Price;
          SetPricePanel();
          ApplyUiElements();
        }
    }

    public void UppLevel()
    {
      if(_isReadyToUpgrade == true)
      {
       _uiAnimator.SetTrigger("NotActiveUppButton");
       _init.playerData.Level ++;
       _currentExperienceCount = 0;
       _init.playerData.MaxExperienceCount *= MaxExperienceCountKoefficent;
       _init.playerData.UpLevelBonusCoinsValue *= UpLevelBonusCoinsKoefficent;
       
       ApplyUiElements();
       OpenBonusPanel();
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);

      }
       
    }

    public void CloseBonusPanel()
    {
       _init.playerData.CoinsValue += Mathf.RoundToInt(_init.playerData.UpLevelBonusCoinsValue);
       ApplyUiElements();
       _upLevelBonusPanel.SetActive(false);
    }


    private void OpenBonusPanel()
    {
      _upLevelBonusPanel.SetActive(true);
    }

    public void GetBonusPerNewLevel()
    {
       Debug.Log("Reward Ad");
      _init.playerData.CoinsValue += Mathf.RoundToInt(_init.playerData.UpLevelBonusCoinsValue * 3);
      ApplyUiElements();
      _upLevelBonusPanel.SetActive(false);
    }
     

    public  void AddExperience()
    {  
       _currentExperienceCount += _items[WeaponIndex].WeaponInfoo.ExperiencePerShot;
       _levelView.fillAmount = _currentExperienceCount / _init.playerData.MaxExperienceCount;
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
       
    }

    private void SetUpLevelButton(bool isReady,Color color)
    {
       _isReadyToUpgrade = isReady;
       _upLevelButton.GetComponent<Image>().color = color;
    }

    public void SetInfinityBullets()
    {
       Debug.Log("Reward Add");
       InfinityBulletsActive = true;
      // TimeInfinity();
       StartCoroutine(StartInfinityBulletsEffect());
    }

    private void TimeInfinity()
    {
       if(InfinityBulletsActive == true && _infinityTime > 0)
       {
         _infinityButton.SetActive(false);
         _infinityTime -= Time.deltaTime;
         _infinityTimeText.enabled = true;
         _infinityTimeText.text = InfinityBulletsString +  Mathf.Ceil(_infinityTime).ToString() + SecondsString;
       }
    }

    private IEnumerator StartInfinityBulletsEffect()
    {
       SetBulletsCountView(true,false);
       yield return new WaitForSeconds(_timeForInfinityBullets);
       SetBulletsCountView(false,true);
       InfinityBulletsActive = false;
       _infinityTime = _startInfinityTime;
        _infinityTimeText.enabled = false;
       _infinityButton.SetActive(true);
    }

    private void SetBulletsCountView(bool infinityBulletsActive,bool bulletsCountPanelActive)
    {
        _infinityBulletsSign.SetActive(infinityBulletsActive);
       _bulletsCountPanel.SetActive(bulletsCountPanelActive);
    }

    public void GetMoneyPerAds()
    {
       Debug.Log("Reward Ad");
       _init.playerData.CoinsValue += _moneyCountPerAds;
    }

    private void ShowAdsInterstitial()
    {
       if(_timeForAds >= 60)
       {
         Debug.Log("Interstitial");
         _timeForAds = 0;
       }
    }
    
}
