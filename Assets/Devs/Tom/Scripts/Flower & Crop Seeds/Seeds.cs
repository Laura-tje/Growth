using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Seeds : MonoBehaviour
{
    [SerializeField] public Lot_Manager.TypePlant typePlant;

    [SerializeField] private float hitLives;

    private float hitLivesPercentage;

    [SerializeField] private float maxHitLives;

    [SerializeField] private Image HpBar;

    private bool colliderTriggered;

    [SerializeField] public Canvas mainCanvas;

    private Coroutine storedCoroutine;

    //private Coroutine lookCoroutine;

    private Animator animator;

    public float speed;

    private void Awake()
    {
        hitLives = maxHitLives;
    }

    public virtual void Test()
    {
        Debug.Log("Work");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponentInChildren<Inventory>().InventoryItems.Count < other.GetComponentInChildren<Inventory>().InventorySlots.Count && GetComponent<Seeds>().enabled != false)
        {
            //other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
            colliderTriggered = true;
            if (mainCanvas != null)
            {
                mainCanvas.gameObject.SetActive(true);
            }

            Debug.Log("Works");

            animator = other.GetComponentInChildren<Animator>();

            storedCoroutine = StartCoroutine(WhackSeeds(other.gameObject));

            //lookCoroutine = StartCoroutine(PlayerLookAt(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && GetComponent<Seeds>().enabled != false)
        {
            //other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
            colliderTriggered = false;

            if(storedCoroutine != null)
            {
                StopCoroutine(storedCoroutine);
            }

            //StopCoroutine(lookCoroutine);

            if (mainCanvas != null)
            {
                mainCanvas.gameObject.SetActive(false);
                HpBar.fillAmount = 1;
                hitLives = maxHitLives;
            }

            if(animator != null)
            {
                animator.SetBool("Hoe", false);
            }
        }
    }

    //Work in Progress

    //private IEnumerator PlayerLookAt(GameObject Player)
    //{
    //    PlayerControlls playerControlls = Player.GetComponentInChildren<PlayerControlls>();

        
    //    Quaternion lookRotation = Quaternion.LookRotation(transform.position - playerControlls._playerChild.transform.position);
    //    //Quaternion lookRotation = Quaternion.LookRotation(transform.position, Player.GetComponent<PlayerControlls>()._playerChild.transform.position);

    //    float time = 0;

    //    while (time < 1)
    //    {
    //        playerControlls._playerChild.transform.rotation = Quaternion.Slerp(playerControlls._playerChild.transform.rotation, lookRotation, time);

    //        time += Time.deltaTime * speed;

    //        yield return null;
    //    }
    //}

    //

    private IEnumerator WhackSeeds(GameObject player)
    {
        if (colliderTriggered == true)
        {
            while (hitLives > 0 && colliderTriggered)
            {
                hitLives -= 1;
                hitLivesPercentage = hitLives / maxHitLives * 1f;
                //HpBar.fillAmount = hitLivesPercentage;

                //Just Enzo adding some lines of code.
                animator.SetBool("Hoe", true);

                while (HpBar.fillAmount > hitLivesPercentage && colliderTriggered)
                {
                    //HpBar.fillAmount = Mathf.MoveTowards(HpBar.fillAmount, hitLivesPercentage, 1f);

                    HpBar.fillAmount -= Time.deltaTime * 1;

                    //DOTween.To(() => HpBar.fillAmount, x => HpBar.fillAmount = x, hitLives, 1f);

                    yield return new WaitForEndOfFrame();
                }

                HpBar.fillAmount = hitLivesPercentage;

                if (hitLives <= 0)
                {
                    player.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
                    Sound_Manage_III.Instance._Play_Sound(2);
                    animator.SetBool("Hoe", false);
                    if(GetComponentInParent<Lot_Manager>() != null)
                    {
                        GetComponentInParent<Lot_Manager>().ResetAmountOfItems();
                    }
                    Destroy(gameObject);
                    yield return null;
                }

                yield return new WaitForSeconds(1f);

            }
            if (!colliderTriggered)
            {
                yield return null;
            }
        }
        else
        {
            yield return null;
        }
    }
}
