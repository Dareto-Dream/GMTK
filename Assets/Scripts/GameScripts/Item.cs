using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{

    private SniperHandler sniperHandler;
    private SniperSpot sniperSpot;

    public void Interact()
    {
        sniperHandler = Camera.main.GetComponent<SniperHandler>();
        if (sniperHandler.shootingAttempts < 3) return;
        sniperHandler.is_able_to_shoot = true;
        sniperHandler.ResetIsDone();
        Destroy(gameObject);
    }


}
