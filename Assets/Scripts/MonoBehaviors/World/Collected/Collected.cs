using UnityEngine;

public class Collected : MonoBehaviour
{
    public void AnimationEnd()
    {
        Destroy(gameObject);
    }
}
