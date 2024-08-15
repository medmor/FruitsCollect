using UnityEngine;


[AddComponentMenu("Playground/Movement/Patrol")]
[RequireComponent(typeof(Rigidbody2D))]
public class Patrol : MonoBehaviour
{
    [SerializeField] protected PatrolDefinition patrolDefinition;

    [SerializeField] protected Transform target;

    [SerializeField] private bool follower;

    [SerializeField] protected bool orientToDirection = false;
    [SerializeField] protected Enums.Directions lookAxis = Enums.Directions.Up;

    [SerializeField] protected Vector2[] waypoints;

    protected Rigidbody2D rigidbody2;
    protected Vector2[] newWaypoints;
    protected int currentWaypointIndex;
    protected bool targetIn = false;

    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        SetUp();
        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }
    }
    public void FixedUpdate()
    {
        if (!follower || !targetIn)
        {
            Move();
        }
        else
        {
            Follow();
        }
    }
    private void SetUp()
    {
        currentWaypointIndex = 0;

        newWaypoints = new Vector2[waypoints.Length + 1];
        int w = 0;
        for (int i = 0; i < waypoints.Length; i++)
        {
            newWaypoints[i] = waypoints[i];
            w = i;
        }

        int v = (newWaypoints.Length > 1) ? w + 1 : 0;
        newWaypoints[v] = transform.position;

        if (orientToDirection)
        {
            Utils.SetAxisTowards(lookAxis, transform, new Vector3(waypoints[0].x - transform.position.x, 0).normalized);
        }

    }
    protected virtual void Move()
    {
        Vector2 currentTarget = newWaypoints[currentWaypointIndex];

        rigidbody2.MovePosition(
            transform.position +
            ((Vector3)currentTarget - transform.position).normalized *
            patrolDefinition.patrolSpeed * Time.fixedDeltaTime
            );

        if (Vector2.Distance(transform.position, currentTarget) <= .1f)
        {
            currentWaypointIndex = (currentWaypointIndex < newWaypoints.Length - 1) ? currentWaypointIndex + 1 : 0;
            if (orientToDirection)
            {
                currentTarget = newWaypoints[currentWaypointIndex];
                Utils.SetAxisTowards(lookAxis, transform, new Vector3(currentTarget.x - transform.position.x, 0).normalized);
            }
        }
    }
    protected virtual void Follow()
    {
        var targetPos = Vector3.MoveTowards(
            transform.position, target.position,
            patrolDefinition.followSpeed * Time.fixedDeltaTime
            );
        rigidbody2.MovePosition(targetPos);
        transform.right = new Vector3(transform.position.x - target.position.x, 0).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetIn = false;
        }
    }
    public void Reset()
    {
        waypoints = new Vector2[1];
        Vector2 thisPosition = transform.position;
        waypoints[0] = new Vector2(2f, .5f) + thisPosition;
    }
    private void OnDrawGizmos()
    {
        foreach (var point in waypoints)
            Gizmos.DrawCube(point, Vector2.one / 5);
    }
}