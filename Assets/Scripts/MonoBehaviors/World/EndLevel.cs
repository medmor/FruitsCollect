using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public void Start()
    {
        EventsManager.Instance.AllItemsCollected?.AddListener(() =>
        {
            var temp = GetComponent<SpriteRenderer>().color;
            temp.a = 255;
            GetComponent<SpriteRenderer>().color = temp;
        });
    }

    public void LoadWinScene()//Called on animation end

    {
        var nextLevel = GameManager.Instance.GameSettings.currentLevel + 1;
        UIManager.Instance.PlayerInventory.Reset(false);
        if (nextLevel <= GameManager.Instance.GameSettings.NumberOfLevels)
        {
            SaveManager.Instance.SetLevel(nextLevel);
            GameManager.Instance.LoadIntro();
        }
        else
        {
            GameManager.Instance.LoadWin();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GetComponent<SpriteRenderer>().color.a == 255)
            {
                GetComponent<Animator>().Play("EndWin");
            }
        }
    }
}
