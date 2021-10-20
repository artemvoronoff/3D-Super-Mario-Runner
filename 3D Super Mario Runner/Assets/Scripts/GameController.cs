using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Player player;
    public Text scoreText;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        player.onCollectCoin = OnCollectCoin;
      /*
       * same as line above (different way)
      player.onCollectCoin = () =>
      {
      }
      */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollectCoin()
    {
        score++;
        scoreText.text = "Score: " + score;
    }


}
