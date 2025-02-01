using UnityEngine;

public class MoveRight : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}