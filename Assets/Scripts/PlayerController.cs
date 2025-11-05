using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [Header("Color Materials")]
    public Material redMaterial;
    public Material blueMaterial;
    private Renderer Mat;

    [Header("VFX Materials")]
    public GameObject redVFX;
    public GameObject blueVFX;

    [Header("Audio and UI")]
    public AudioSource switchSound;
    public GameObject ScreenUI;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HScoreText;
    private AudioSource Sound;

    private int Score = 0;
    private int MScore;
    private bool isGameOver = false;


    void Start()
    {
        loadScore();
        Sound = GetComponent<AudioSource>();
        Mat = GetComponent<Renderer>();
        Mat.material = blueMaterial;
        this.tag = "Blue";
        blueVFX.SetActive(true);
        redVFX.SetActive(false);
    }

    void Update()
    {
        float speed = GameObject.FindObjectOfType<Generation>().GetComponent<Generation>().speed;
        UnityEngine.Debug.Log(speed);

        infRotation(speed);
        if (Input.GetKeyDown(KeyCode.Q) && !isGameOver) 
        {
            if (this.tag == "Red")
            {
                redVFX.SetActive(false);
                Mat.material = blueMaterial;
                blueVFX.SetActive(true);
                this.tag = "Blue";
            }
            else
            {
                blueVFX.SetActive(false);
                Mat.material = redMaterial;
                redVFX.SetActive(true);
                this.tag = "Red";
            }
            switchSound.Play();
        }
        if (!isGameOver)
        {
            Score += 1;
            ScoreText.text = Score.ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red") || other.CompareTag("Blue") && !isGameOver)
        {
            if (other.tag != this.tag)
            {
                isGameOver = true;

                redVFX.SetActive(false);
                blueVFX.SetActive(false);

                Sound.Play();

                ScreenUI.SetActive(true);

                savemaxScore();
                loadScore();

                HScoreText.text = "Personal Record:\n" + MScore.ToString();

                Time.timeScale = 0.7f;
            }
        }
    }
    
    void infRotation(float speed)
    {
        transform.Rotate(Vector3.right * speed * 50 * Time.deltaTime, Space.World);
    }

    void loadScore()
    {
        MScore = PlayerPrefs.GetInt("MaxScore", 0);
    }
    void savemaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        if (Score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", Score);
        }
    }
}


