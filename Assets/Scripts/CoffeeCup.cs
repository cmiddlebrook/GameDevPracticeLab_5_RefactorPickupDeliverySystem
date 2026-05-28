using TMPro;
using UnityEngine;

public class CoffeeCup : MonoBehaviour, IInteractable, IDesctructible
{
    //[SerializeField] private GameObject _interactionUI;
    //[SerializeField] private TMP_Text _interactionText;

    private void Start()
    {
        //_interactionText.text = "Recycle";
    }

    private void Update()
    {
        //_interactionUI.SetActive(false);
    }

    public void Destruct()
    {
        Destroy(this);
    }

    public void Interact()
    {
        //_interactionUI.SetActive(true);
    }

    public void ShowInteractionUI()
    {

    }

}
