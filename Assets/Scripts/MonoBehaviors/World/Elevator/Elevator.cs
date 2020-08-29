using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Trigger trigger;
    public GameObject elevator;
    public float minY = 0f;
    public float maxY = 2f;

    void Start()
    {
        elevator.transform.localPosition = new Vector3(elevator.transform.localPosition.x, minY);
        trigger.transform.localPosition = new Vector3(trigger.transform.localPosition.x, trigger.offY);
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.isTriggred)
        {
            if(elevator.transform.localPosition.y < maxY)
            {
                elevator.transform.localPosition = new Vector3(elevator.transform.localPosition.x,
                    elevator.transform.localPosition.y + .005f);
            }
        }
        else
        {
            if(elevator.transform.localPosition.y > minY)
            {
                elevator.transform.localPosition = new Vector3(elevator.transform.localPosition.x,
                    elevator.transform.localPosition.y - .005f);
            }
        }
    }
}
