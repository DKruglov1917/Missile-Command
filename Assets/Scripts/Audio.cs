using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource soundsSource, musicSource;
    public AudioClip explosion, shot;

    private void Awake()
    {
        MissileLaunch.onPlayerShoot += ShotSound;
        Missile.onEnemyMissileDestroy += ExplosionSound;
        Missile.onEnemyCruiseMissileDestroy += ExplosionSound;
        Missile.onCityDestroy += ExplosionSound;
    }

    private void ShotSound()
    {
        soundsSource.PlayOneShot(shot);
    }

    private void ExplosionSound()
    {
        soundsSource.PlayOneShot(explosion);
    }

    private void OnDisable()
    {
        MissileLaunch.onPlayerShoot -= ShotSound;
        Missile.onEnemyMissileDestroy -= ExplosionSound;
        Missile.onEnemyCruiseMissileDestroy -= ExplosionSound;
        Missile.onCityDestroy -= ExplosionSound;
    }
}
