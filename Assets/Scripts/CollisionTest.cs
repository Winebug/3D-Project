using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"�浹 ����! ���: {collision.gameObject.name}, �±�: {collision.gameObject.tag}");
    }
}