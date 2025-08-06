using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{

    private SniperHandler sniperHandler;
    private SniperSpot sniperSpot;

    public void Interact()
    {
        AudioHandler.Instance.PlaySFX(AudioHandler.Instance.dropItem);
        sniperHandler = Camera.main.GetComponent<SniperHandler>();
        if (sniperHandler.shootingAttempts < 3) return;
        sniperHandler.is_able_to_shoot = true;
        sniperHandler.ResetIsDone();
        Destroy(gameObject);
    }


}
