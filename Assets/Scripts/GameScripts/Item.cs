using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{

    private SniperHandler sniperHandler;

    public void Interact()
    {
        sniperHandler = Camera.main.GetComponent<SniperHandler>();
        sniperHandler.is_able_to_shoot = true;
        Destroy(gameObject);
    }


}
