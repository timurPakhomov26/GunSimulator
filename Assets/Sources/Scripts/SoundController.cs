using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSource;
    [SerializeField] private AudioSource _magazineSource;
    [SerializeField] private AudioSource _loadSource;
    [SerializeField] private AudioSource _shotWithoutSource;
    public void ShotSoundPlay()
    {
        _shotSource.Play();
    }
     public void LoadSoundPlay()
    {
        _loadSource.Play();
    }

    public void MagazineSoundPlay()
    {
        _magazineSource.Play();
    }

    public void ShotWithout()
    {
       _shotWithoutSource.Play();
    }


}
