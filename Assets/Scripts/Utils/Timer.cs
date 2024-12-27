using UnityEngine;

public class Timer : MonoBehaviour
{
    public float countdownTime = 60f;

    void Update()
    {
        countdownTime -= Time.deltaTime;
        if (countdownTime <= 0)
        {
            Debug.Log("Time's Up!");
        }
    }
}
