using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Seeds : MonoBehaviour
{
    [SerializeField] public Lot_Manager.TypeFlowers flowerSeed;

    [SerializeField] private float hitLives;

    private float hitLivesPercentage;

    [SerializeField] private float maxHitLives;

    [SerializeField] private Image HpBar;

    private bool colliderTriggered;

    [SerializeField] private Canvas mainCanvas;

    private Coroutine storedCoroutine;

    private Animator animator;
    private enum CropSeeds
    {

    }

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
        if (other.gameObject.tag == "Player" && other.GetComponentInChildren<Inventory>().InventoryItems.Count < other.GetComponentInChildren<Inventory>().InventorySlots.Count)
        {
            //other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
            colliderTriggered = true;
            if (mainCanvas != null)
            {
                Debug.Log("Active");
                mainCanvas.gameObject.SetActive(true);
            }


            animator = other.GetComponentInChildren<Animator>();
            Debug.Log(animator);

            storedCoroutine = StartCoroutine(WhackSeeds(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
            colliderTriggered = false;

            StopCoroutine(storedCoroutine);

            if (mainCanvas != null)
            {
                mainCanvas.gameObject.SetActive(false);
                HpBar.fillAmount = 1;
                hitLives = maxHitLives;
            }
            animator.SetBool("Hoe", false);
        }
    }

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
                    Debug.Log("Dies");
                    player.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
                    animator.SetBool("Hoe", false);
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
