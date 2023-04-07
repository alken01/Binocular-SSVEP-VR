using UnityEngine;
using UnityEngine.UI;

public class DisplayParentName : MonoBehaviour
{
    private Text textComponent;

    void Start()
    {
        GetComponent<TextMesh>().text = transform.parent.name;
    }
}
