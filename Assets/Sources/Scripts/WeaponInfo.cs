using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName  = "Weapon/New weapon")]
public class WeaponInfo : ScriptableObject
{
    [SerializeField] private bool _buyPerRealMoney = false;
    public bool BuyForRealMoney => _buyPerRealMoney;
    [SerializeField] private Sprite _bullet;
    public Sprite BulletSprite => _bullet;
    [SerializeField] private string _name;
    public string WeaponName => _name;
    [SerializeField] private string _weaponClass;
    public string WeaponClass => _weaponClass;
   
    [SerializeField] private int _bulletsCount;
    public int BulletsCount => _bulletsCount;
    
    [SerializeField] private int _price;
    public int Price => _price;
    [SerializeField] private float _rateOfFire;
    public float RateOfFire => _rateOfFire;
    [SerializeField] private ShootingType _type;
    public ShootingType ShootingType => _type;
    [SerializeField] private float _experiencePerShot;
    public float ExperiencePerShot => _experiencePerShot;
    [SerializeField] private int _coinsPerShot;
    public int CoinsPerShot => _coinsPerShot;
    [SerializeField] private int _levelForOpen;
    public int LevelFoOpen => _levelForOpen;
    
    
}