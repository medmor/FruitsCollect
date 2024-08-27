using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer UI references :")]
    [SerializeField] private Image uiFillImage;
    [SerializeField] private Text uiText;

    [SerializeField] private int Duration;

    [SerializeField] private bool IsPaused;

    private int remainingDuration;
    private string currentColor = "green";
    private void Start()
    {
        Begin();
    }

    public void ResetTimer()
    {
        uiText.text = "00:00";
        uiFillImage.fillAmount = 0f;

        remainingDuration = Duration;

        IsPaused = false;
    }

    public void SetPaused(bool paused)
    {
        IsPaused = paused;
    }


    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
        uiFillImage.color = Color.green;
        currentColor = "green";
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration > 0)
        {
            if (!IsPaused)
            {
                UpdateUI(remainingDuration);
                remainingDuration--;
            }
            yield return new WaitForSeconds(1f);
        }
        End();
    }

    private void UpdateUI(int seconds)
    {
        uiText.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
        uiFillImage.fillAmount = Mathf.InverseLerp(0, Duration, seconds);
        if (remainingDuration < 0.25 * Duration)
        {
            if (currentColor != "red")
            {
                uiFillImage.color = Color.red;
                currentColor = "red";
                SoundManager.Instance.playSound("alarm2");
            }
        }
        else if (remainingDuration < .5 * Duration && currentColor != "orange")
        {
            uiFillImage.color = new Color(255, 165, 0);
            currentColor = "orange";
            SoundManager.Instance.playSound("alarm1");
        }
    }

    public void End()
    {
        ResetTimer();
        EventsManager.Instance.TimeOut.Invoke();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
