using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_control : MonoBehaviour
{

	public GameObject book;
	public GameObject book_model;
	public GameObject pomodoro_controller;

	// Start is called before the first frame update
	void Start()
	{
        closeBook();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			closeBook();
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			openBook();
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			previousPage();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			nextPage();
		}
	}

	public void openBook()
	{
		pomodoro_controller.GetComponent<pomodoro_controller>().isStopped = false;
		book.SetActive(true);
	}

	public void closeBook()
	{
		pomodoro_controller.GetComponent<pomodoro_controller>().isStopped = true;
		book.SetActive(false);
	}

	public void nextPage()
	{
		book_model.GetComponent<Book>().mode = FlipMode.RightToLeft;
		book_model.GetComponent<Book>().TweenForward();
	}

	public void previousPage()
	{
		book_model.GetComponent<Book>().mode = FlipMode.LeftToRight;
		book_model.GetComponent<Book>().TweenForward();
	}
}
