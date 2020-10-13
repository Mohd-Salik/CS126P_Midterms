using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceStatus : MonoBehaviour
{
    public static bool win = false;
    public static bool onstart_point = false;
    public static bool count_down = true;
    public static float timer = 5000f;
    Text status;

    void Start(){
        status = GetComponent<Text>();
    }

    void updateTextUI(){

        //LEVEL SELECT AND PLAYER ON START POINT
        if (Player.level == 0){
            status.text = "WELCOME TO THE LOBBY\nPLEASE USE THE NUMBER KEYPAD TO SELECT A LEVEL";
        }
        else if ((Player.level > 0)&(Player.stop_movement != true)){
            status.text = "YOU CAN NOW ENTER THE CRACK ON THE WALL" + "\nLEVEL: " + Player.level.ToString() + " SELECTED.";
        }

        if (onstart_point == true){
            status.text = "GET TO FOOD BEFORE OTHER BACTERIA REACHES IT!\nBEWARE OF RED VIRUSES(HIT SPACE)";
            onstart_point = false;
        }

        //IF LEVEL HAS STARTED
        if (Player.start == true){
            if (FoodTrigger.food_eaten != true){
                timer -= 100 * Time.deltaTime;
                status.text = "GET TO THE FOOD!" + 
                            "\nMILLISECONDS LEFT: " + 
                            timer.ToString("0");
                if (timer <= 0){
                    status.text = "5 SECONDS HAS PASSED, FOOD CANNOT BE TAKEN ANYMORE.\n (HIT SPACE TO RETURN TO LOBBY)";
                    count_down = false;
                    FoodTrigger.food_eaten = true;
                    }
            }
            else{
                if (EnemyAI.enemy_win == true){
                    status.text = "FAILED! OTHER BACTERIA GOT TO THE FOOD FIRST\n (HIT SPACE TO RETURN TO LOBBY)";
                }
                else{
                    status.text = "CONGRATULATIONS! ENJOY YOUR FOOD!\n (HIT SPACE TO RETURN TO LOBBY)";
                }
            }
        }
    }

    void Update(){
        updateTextUI();
    }
}
