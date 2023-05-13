using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] int toasterNumber;
    Button button;
    MainManager mainManagerScript;
    bool hasLerped = false;
    [SerializeField] Vector3 positionOffset;
    // Start is called before the first frame update
    void Start()
    {
        mainManagerScript = GameObject.Find("MainManager").GetComponent<MainManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(passVariable);
    }

    public void passVariable()
    {
        mainManagerScript.SpawnPickedToster(toasterNumber, positionOffset);
    }

    IEnumerator showInfoTab(int infoTabNumber, int switchNumber)
    {
        switch (switchNumber)
        {
            case 0:
                float x;
                float duration;
                CanvasGroup canvas;
                if (!hasLerped)
                {
                    x = 0;
                    duration = 0.5f;
                    canvas = mainManagerScript.infoTab[infoTabNumber].gameObject.GetComponent<CanvasGroup>();

                    while (x < duration)
                    {
                        canvas.alpha = Mathf.Lerp(0, 1, x / duration);
                        x += Time.deltaTime;
                        yield return null;
                    }
                    canvas.alpha = 1;
                    hasLerped = true;
                }
                break;

            case 1:
                if (hasLerped)
                {
                    x = 0;
                    duration = 0.5f;
                    canvas = mainManagerScript.infoTab[infoTabNumber].gameObject.GetComponent<CanvasGroup>();

                    while (x < duration)
                    {
                        canvas.alpha = Mathf.Lerp(1, 0, x / duration);
                        x += Time.deltaTime;
                        yield return null;
                    }
                    canvas.alpha = 0;
                    hasLerped = false;
                }

                break;

        }


    }

    public void lerpInfoTab(int number)
    {
        StartCoroutine(showInfoTab(number, 0));
    }
    public void deLerpInfoTab(int number)
    {
        StartCoroutine(showInfoTab(number, 1));
    }

}
