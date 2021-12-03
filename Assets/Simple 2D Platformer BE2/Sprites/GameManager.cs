using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;

    public GameObject[] stages;

    public Image[] UIhealth;
    public Text UIpoint;
    public Text UIStage;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIpoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        //stage 바뀌게하는 로직
        if (stageIndex < stages.Length-1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex+1);
        }
        else
        {
            //게임성공

            //컨트롤 락
            Time.timeScale = 0;
            Debug.Log("게임 클리어");
           
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear";
            UIRestartBtn.SetActive(true);
        }
        

        totalPoint += stagePoint;
        
        stagePoint = 0;

    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            //All health uioff
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);

            player.OnDie();

            Debug.Log("You Died");

            UIRestartBtn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //return player
            if (health > 1)
            {
                PlayerReposition();           
            }
            HealthDown();

        }
        
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-10, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
