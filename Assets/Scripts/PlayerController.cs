using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update ()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
