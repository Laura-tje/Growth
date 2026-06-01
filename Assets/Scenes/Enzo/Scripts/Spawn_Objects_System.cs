/*using UnityEngine;

public class Spawn_Objects_System : MonoBehaviour
{

    [SerializeField] private GameObject[] _Spawn_Locations;
    [SerializeField] private GameObject[] _Objects_To_Spawn;


    private void Start()
    {

        _Spawn_Locations = GameObject.FindGameObjectsWithTag("Spawn_Location");

        StartCoroutine(Spawn_Objects());

    }

    private IEnumerator Spawn_Objects()
    {

        for each (GameObject spawn_Location in _Spawn_Locations)
        {

            int random_Object_Index = Random.Range(0, _Object_To_Spawn.Length);
            Instantiate(_Object_To_Spawn[random_Object_Index], spawn_Location.transform.position, Quaternion.identity);
            
        }

        yield return new WaitForSeconds(180f);

        for each (GameObject spawn_Location in Spawn_Locations);
        {

            if (gameObject.transform.childCount > 0)
            {

                GameObject _Destroy_Object;

                _Destroy_Object = gameObject.transform.GetChild(0).gameObject;

                Destroy(_DestroyObject);

                int random_Object_Index = Random.Range(0, _Object_To_Spawn.Length);
                Instantiate(_Object_To_Spawn[random_Object_Index], spawn_Location.transform.position, Quaternion.identity);

            }

            if (gameObject.transform.childCount == 0)
            {

                int random_Object_Index = Random.Range(0, _Object_To_Spawn.Length);
                Instantiate(_Object_To_Spawn[random_Object_Index], spawn_Location.transform.position, Quaternion.identity);

            }

        }

    }

}/*

//F this version of vsc i cant find my errors.

using UnityEngine;
using System.Collections;

public class Spawn_Objects_System : MonoBehaviour
{
    [SerializeField] private GameObject[] _Spawn_Locations;
    [SerializeField] private GameObject[] _Objects_To_Spawn;

    private void Start()
    {
        _Spawn_Locations = GameObject.FindGameObjectsWithTag("Spawn_Location");

        StartCoroutine(Spawn_Objects());
    }

    private IEnumerator Spawn_Objects()
    {
        // Initial spawn
        foreach (GameObject spawn_Location in _Spawn_Locations)
        {
            int random_Object_Index = Random.Range(0, _Objects_To_Spawn.Length);

            Instantiate(
                _Objects_To_Spawn[random_Object_Index],
                spawn_Location.transform.position,
                Quaternion.identity,
                spawn_Location.transform
            );
        }

        while (true)
        {
            yield return new WaitForSeconds(180f);

            foreach (GameObject spawn_Location in _Spawn_Locations)
            {
                // If object already exists at spawn point
                if (spawn_Location.transform.childCount > 0)
                {
                    GameObject destroy_Object =
                        spawn_Location.transform.GetChild(0).gameObject;

                    Destroy(destroy_Object);
                }

                // Spawn new object
                int random_Object_Index =
                    Random.Range(0, _Objects_To_Spawn.Length);

                Instantiate(
                    _Objects_To_Spawn[random_Object_Index],
                    spawn_Location.transform.position,
                    Quaternion.identity,
                    spawn_Location.transform
                );
            }
        }
    }
}
