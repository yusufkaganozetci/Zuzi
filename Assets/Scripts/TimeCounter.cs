using System.Collections;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private GameHandler gameHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        gameHandler = FindObjectOfType<GameHandler>();
        StartCoroutine(CountDown(3));
    }

    private IEnumerator CountDown(int startValue)
    {
        int currentTime = startValue;
        while (currentTime != 0)
        {
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1);
            currentTime -= 1;
            
        }
        timeText.text = "GO!";
        yield return new WaitForSeconds(1);
        gameHandler.StartTheGame();
        gameObject.SetActive(false);
    }
}
