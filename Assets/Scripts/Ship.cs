using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 25f;

    void Update()
    {
        Move();
        Rotate();
        
    }

    void Move()
    { 

        transform.position = transform.position + transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

    }
    void Rotate()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }

    

}
