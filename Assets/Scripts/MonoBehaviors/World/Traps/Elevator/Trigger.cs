
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool isTriggred = false;
    public float onY;
    public float offY;

    void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggred = true;
        transform.localPosition = new Vector3(transform.localPosition.x, onY);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isTriggred = false;
        transform.localPosition = new Vector3(transform.localPosition.x, offY);
    }

}
