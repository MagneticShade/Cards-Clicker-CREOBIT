using UnityEngine;

public class RowScript : MonoBehaviour
{
    private CardScript[] cards;
    public void FlipFirstRow(float delayStart)
    {
        float delay = delayStart;
        cards = gameObject.GetComponentsInChildren<CardScript>();
        foreach (CardScript card in cards)
        {
            card.Flip(delay);
            card.SetDragable(true);
            delay += 0.05f;
            card.SetFirsRow(true);
        }
    }
}
