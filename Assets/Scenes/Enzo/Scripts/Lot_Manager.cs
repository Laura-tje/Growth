using UnityEngine;
using System.Collections;

public class Lot_Manager : MonoBehaviour
{

    [SerializeField] private GameObject _Plant;
    [SerializeField] private GameObject _Player;

    [SerializeField] private bool _Plant_Still_Growing;
    [SerializeField] private bool _Plant_Done_Growing;
    [SerializeField] private bool _Transfering;
 
    [SerializeField] private int _Current_Amount_Mats;
    private int _Max_Amount_Mats_Grow;

    private void Start()
    {

        _Check_Plant();
        _Plant_Still_Growing = true;
        _Plant_Done_Growing = false;
        _Transfering = false;

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

                _Plant_Growth_Plan_1();

                Debug.Log("Plant type 1 growth plan assigned to lot");

            }

        }

    }

    private void _Plant_Growth_Plan_1()
    {

        int Mats_Needed = 50;

        _Max_Amount_Mats_Grow = Mats_Needed;

    }

     
    private void OnTriggerStay(Collider other)
    {

        if ( other.gameObject == _Player && _Plant_Still_Growing && !_Transfering)
        {

            Debug.Log("Player in lot");

            StartCoroutine(_Transfer_Mats(other));

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if ( other.gameObject == _Player && _Plant_Still_Growing && _Transfering)
        {

            StopCoroutine(_Transfer_Mats(other));

            _Transfering = false;

        }

    }

    private IEnumerator _Transfer_Mats(Collider other)
    {

        Debug.Log("Attempting to transfer mats");

        _Transfering = true;

        Player_Growth_Corrosponding_Script player_Script = _Player.GetComponent<Player_Growth_Corrosponding_Script>();

        if ( other != null && player_Script._Player_Mats_Owned > 0)
        {

            if ( _Current_Amount_Mats >= _Max_Amount_Mats_Grow)
            {

                yield return null;

                _Transfering = false;

                Debug.Log("Transfering mats stopped");

            }

            else if ( _Current_Amount_Mats <= _Max_Amount_Mats_Grow)
            {

                while ( _Current_Amount_Mats <= _Max_Amount_Mats_Grow)
                {

                    player_Script._Player_Mats_Owned --;

                    _Current_Amount_Mats ++;

                    yield return new WaitForSeconds(0.1f);  

                    Debug.Log("Transferring mats");

                    _Transfering = true;

                }

            }

        }

    }

    private void Update()
    {

        if ( _Current_Amount_Mats >= _Max_Amount_Mats_Grow)
        {

            _Plant_Still_Growing = false;

            _Plant_Done_Growing = true;

            Debug.Log("Plant done growing");

        }

    }

}
