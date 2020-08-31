
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool isTriggred = false;
    public float onY;
    public float offY;
    public LayerMask layer;

    private void Update()
    {
        var collider = Physics2D.OverlapBox(transform.position, new Vector2(1f, .1f), 0, layer);
        if (collider)
        {
            isTriggred = true;
            transform.localPosition = new Vector3(transform.localPosition.x, onY);
        }
        else
        {
            isTriggred = false;
            transform.localPosition = new Vector3(transform.localPosition.x, offY);
        }
    }

}
