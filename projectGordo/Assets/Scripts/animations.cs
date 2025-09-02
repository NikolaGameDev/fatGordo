using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GordoAnimSimple : MonoBehaviour
{
    Animator anim;
    void Awake() => anim = GetComponent<Animator>();

    void Update()
    {
        float s = Spawner.CurrentSpeed;   // your existing speed variable

        // Drive the Blend Tree (parameter in Animator must be named "runSpeed")
        anim.SetFloat("runSpeed", s, 0.08f, Time.deltaTime);

        // Bonus: subtle speed-up of animation playback
        anim.speed = 1f + (s / 50f);
    }
}
