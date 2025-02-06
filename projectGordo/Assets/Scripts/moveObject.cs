using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private float moveSpeed;
    private float destroyX;

    public void Initialize(float speed, float offScreenX)
    {
        moveSpeed = speed;
        destroyX = offScreenX;
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
