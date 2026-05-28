using UnityEngine;
using System.Collections;

public class Lot_Manager : MonoBehaviour
{

    [SerializeField] private GameObject _Plant;
    [SerializeField] private GameObject _Player;

    [SerializeField] private bool _Plant_Still_Growing;
    [SerializeField] private bool _Plant_Done_Growing;

    private int _Current_Amount_Mats;
    private int _Max_Amount_Mats_Grow;

    private void Start()
    {

        _Check_Plant();

    }

    private void _Check_Plant()
    {

        if ( _Plant == null)
        {

            Debug.Log("No plant assigned to lot");

        }

        else if ( _Plant != null)
        {

            if ( _Plant.transform.GetChild(0).gameObject.name == "Plant_Type_1")
            {

                

            }

        }

    }

    private void _Plant_Growth_Plan()
    {

        int Mats_Needed = _Max_Amount_Mats_Grow;



    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject == _Player)
        {

            Player_Growth_Corrosponding_Script player_Script = _Player.GetComponent<Player_Growth_Corrosponding_Script>();

            while (player_Script._Player_Mats_Owned > 0 && _Current_Amount_Mats < _Max_Amount_Mats_Grow)
            {

                player_Script._Player_Mats_Owned -= 1;

                _Current_Amount_Mats += 1;

            }

        }

    }
    
}
