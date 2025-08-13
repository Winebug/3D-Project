using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; 
    public float currentHealth; 

    // UI와 연결할 슬라이더 변수
    public Slider hpSlider;

    void Start()
    {
        // 게임 시작 시 현재 체력을 최대 체력으로 설정
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // 체력이 0 이하로 내려가지 않도록 함
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // 체력 UI 업데이트
        UpdateHealthUI();

        Debug.Log("플레이어 체력: " + currentHealth);
    }

    // HP 슬라이더의 값을 업데이트하는 함수
    void UpdateHealthUI()
    {
        hpSlider.value = currentHealth / maxHealth;
    }
}