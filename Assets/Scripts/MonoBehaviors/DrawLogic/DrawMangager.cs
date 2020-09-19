using UnityEngine;
using UnityEngine.UI;



[DisallowMultipleComponent]
public class DrawMangager : MonoBehaviour
{
	public GameObject linePrefab;
	public LayerMask groundLayer;

	[HideInInspector]
	public Line currentLine;
	public Transform lineParent;
	public RigidbodyType2D lineRigidBodyType = RigidbodyType2D.Kinematic;
	public LineEnableMode lineEnableMode = LineEnableMode.ON_CREATE;
	public Image lineLife;
	public bool enableLineLife;
	public bool isRunning;
	public string[] colorPicker;

	// Use this for initialization
	void Start()
	{
		if (lineParent == null)
		{
			lineParent = GameObject.Find("Lines").transform;
		}

		if (lineLife != null)
		{
			if (enableLineLife)
			{
				lineLife.gameObject.SetActive(true);
			}
			else
			{
				lineLife.gameObject.SetActive(false);
			}
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (!isRunning)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			CreateNewLine();
		}
		else if (Input.GetMouseButtonUp(0) && currentLine != null)
		{
			RelaseCurrentLine();
		}

		if (currentLine != null)
		{
			var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentLine.AddPoint(mousePosition);
			UpdateLineLife();
			if (currentLine.ReachedPointsLimit())
			{
				RelaseCurrentLine();
			}
			if (Physics2D.OverlapBox(mousePosition, new Vector2(.1f, .1f), 0, groundLayer))
			{
				RelaseCurrentLine();
			}
		}
	}
	Color currColor = Color.white;
	private void CreateNewLine()
	{
		currentLine = (Instantiate(linePrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Line>();

		ColorUtility.TryParseHtmlString(colorPicker[Random.Range(0, colorPicker.Length)], out currColor);
		currentLine.lineColorMat.color = currColor;
		currentLine.name = "Line";
		currentLine.transform.SetParent(lineParent);

		if (lineEnableMode == LineEnableMode.ON_CREATE)
		{
			EnableLine();
		}
	}

	private void EnableLine()
	{
		currentLine.EnableCollider();
		currentLine.SetColliderAndRenderShape();

	}

	private void RelaseCurrentLine()
	{
		if (lineEnableMode == LineEnableMode.ON_RELASE)
		{
			EnableLine();
		}

		currentLine = null;
	}

	private void UpdateLineLife()
	{
		if (!enableLineLife)
		{
			return;
		}

		if (lineLife == null)
		{
			return;
		}

		lineLife.fillAmount = 1 - (currentLine.points.Count / currentLine.maxPoints);
	}

	public enum LineEnableMode
	{
		ON_CREATE,
		ON_RELASE
	}

	;
}
