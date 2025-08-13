using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; 
    public float currentHealth; 

    // UI�� ������ �����̴� ����
    public Slider hpSlider;

    void Start()
    {
        // ���� ���� �� ���� ü���� �ִ� ü������ ����
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // ü���� 0 ���Ϸ� �������� �ʵ��� ��
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // ü�� UI ������Ʈ
        UpdateHealthUI();

        Debug.Log("�÷��̾� ü��: " + currentHealth);
    }

    // HP �����̴��� ���� ������Ʈ�ϴ� �Լ�
    void UpdateHealthUI()
    {
        hpSlider.value = currentHealth / maxHealth;
    }
}