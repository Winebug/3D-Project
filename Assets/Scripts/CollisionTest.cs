using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"충돌 성공! 상대: {collision.gameObject.name}, 태그: {collision.gameObject.tag}");
    }
}