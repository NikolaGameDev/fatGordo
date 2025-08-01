using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GordoAnimSimple : MonoBehaviour
{
    public string idle = "Idle";
    public string run = "Run";
    public string run1 = "Run1";
    public string run2 = "Run2";

    Animator anim;
    int lastState = -1; // -1 idle, 0 run, 1 run1, 2 run2

    void Awake() => anim = GetComponent<Animator>();

    void Update()
    {
        float s = Spawner.CurrentSpeed;  // uses your spawner’s current phase speed
        int state = (s > 8f) ? 2 : (s > 5f) ? 1 : (s > 0f ? 0 : -1);

        if (state == lastState) return;   // don’t restart the same clip every frame
        lastState = state;

        if (state == 2) anim.Play(run2);
        else if (state == 1) anim.Play(run1);
        else if (state == 0) anim.Play(run);
        else anim.Play(idle);
    }
}
