using UnityEngine;
using System.Collections;

public class cameraKick : MonoBehaviour
{
    public float kickAmount = 0.2f;   // How far to move
    public float kickTime = 0.1f;     // How fast to return

    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    public void Kick(Vector2 dir)
    {
        StopAllCoroutines();
        StartCoroutine(KickRoutine(dir));
    }

    IEnumerator KickRoutine(Vector2 dir)
    {
        transform.position = startPos + (Vector3)(dir * kickAmount);
        yield return new WaitForSeconds(kickTime);
        transform.position = startPos;
    }
}
