using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] int toasterNumber;
    Button button;
    MainManager mainManagerScript;
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
}
