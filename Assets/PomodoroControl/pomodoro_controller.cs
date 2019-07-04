using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pomodoro_controller : MonoBehaviour
{

    public int break_time = 5;
    public int session_time = 25;
    bool isSession;
    private float timer = 0f;
    private float timer_handler = 0f;
    public Text time_text;
	private int minutes = 0;
	private int seconds = 0;
	public bool isStopped = false;

	// Start is called before the first frame update
	void Start()
    {
        break_time *= 60;
        session_time *= 60;
        isSession = false;
        timer += session_time;
        timer_handler = timer;

    }

    // Update is called once per frame
    void Update()
    {

		if (timer <= 0f)
		{
			isSession = !isSession;
			if (isSession)
			{
				timer = session_time;
				timer_handler = timer;
			}
			else
			{
				timer = break_time;
				timer_handler = timer;
			}
		}
		if (!isStopped)
		{
			timer -= (float)Time.deltaTime;
			if (timer < timer_handler)
			{
				minutes = (int)System.Math.Truncate(timer) / 60;
				seconds = (int)System.Math.Truncate(timer) % 60;
				time_text.text = minutes.ToString() + ":" + seconds.ToString();
				timer_handler -= 1f;
			}
		}
		
	}

}


