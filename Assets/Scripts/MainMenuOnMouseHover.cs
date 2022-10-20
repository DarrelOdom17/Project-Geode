using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuOnMouseHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject bracket;

    // Start is called before the first frame update
    void Start()
    {
        bracket = gameObject.transform.Find("Bracket").gameObject;
        bracket.SetActive(false);
        bracket.gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bracket.SetActive(true);
        Debug.Log("Mouse hovering");
        bracket.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bracket.SetActive(false);
        Debug.Log("Mouse is not hovering");
    }
}
