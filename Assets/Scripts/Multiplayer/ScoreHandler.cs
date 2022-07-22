using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviourPunCallbacks
{
    public int score;
    public ScoreManager scoreManager;
    public int personalScore;
    private PhotonView view;
    public int maxScore;
    
    private void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Chest")) return;
        //Debug.Log(other.name);
        other.gameObject.SetActive(false);
        score++;


        if (photonView.IsMine)
        {
            personalScore++;
            if (scoreManager == null) scoreManager = GetComponent<ScoreManager>();
            scoreManager.scoreText.text = personalScore.ToString();
        }
     

        if (score == 6)
        {
            if (personalScore > maxScore - personalScore)
            {
                scoreManager.gameWon.SetActive(true);
            }
            photonView.RPC("PlayerLost", RpcTarget.Others);
            Time.timeScale = 0;
        }
        
    }
    
    [PunRPC]
    public void PlayerLost()
    {
        if (scoreManager == null) scoreManager = GetComponent<ScoreManager>();
        scoreManager.gameLost.SetActive(true);
        Time.timeScale = 0;
    }
}