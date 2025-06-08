using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Enemy : MonoBehaviour {
    private float currentHP;
    [SerializeField] float maxHP;

    [SerializeField] ParticleSystem takeDamageParticles;
    [SerializeField] GameObject dieParticlePrefabs;

    [SerializeField] Image fill;

    public Gun gun;

    void Start()
    {
        takeDamageParticles.Stop();
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void StoreGun(Gun gun) {
        this.gun = gun;
    }

    private void UpdateHealthBar() {
        
        fill.fillAmount = currentHP/maxHP;
    }

    [Button("Take Dame", EButtonEnableMode.Always)]
    public void TakeDamage(int damageAmout = 25, Action onDie = null) {
        Debug.Log("take dame");
        currentHP -= damageAmout;
        UpdateHealthBar();
        if (currentHP <=0) {
            Die();
        } else {
            takeDamageParticles.Play();
            AudioManager.Instance.SpawnAndPlay(transform, AudioManager.Instance.shootSound);
        }
    }

    public void ResetHealth() {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    private void Die() {
        gun.CompleteGun();
        GameObject particle = Instantiate(dieParticlePrefabs, transform.position, Quaternion.identity);
        AudioManager.Instance.SpawnAndPlay(transform, AudioManager.Instance.explotionSound);
        Destroy(particle, 2f);
        gameObject.SetActive(false);
    }
}