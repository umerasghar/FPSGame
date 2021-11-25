using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BloodSplatterEffect : MonoBehaviour
{
    public static BloodSplatterEffect instance;

    public Sprite[] bloodSplatterSprites;
 
    public float duration = 1;

    public Image bloodSplatterImage;

    private bool isRoutineRunning;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
      //  bloodSplatterImage = GetComponentInChildren<Image>();
    }


    public void BloodSplatter()
    { 
        if (bloodSplatterImage == null)
        {
            Debug.LogError("No blood splatter image found!");
            return;
        }

        if (bloodSplatterSprites.Length == 0)
        {
            Debug.LogError("Assign one or more sprites to the array!");
            return;
        }
        if (!isRoutineRunning)
        {
            StartCoroutine(BloodSplatterRoutine());
        }
    }


    IEnumerator BloodSplatterRoutine()
    {
        isRoutineRunning = true;

        bloodSplatterImage.sprite = bloodSplatterSprites[Random.Range(0, bloodSplatterSprites.Length)];

        Color col = bloodSplatterImage.color;

        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            col.a = Mathf.Lerp(1, 0, timer / duration);
            bloodSplatterImage.color = col;

            yield return null;
        }

        isRoutineRunning = false;
    }
}

