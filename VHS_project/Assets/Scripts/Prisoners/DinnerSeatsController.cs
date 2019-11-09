using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat
{
    public Seat(Transform seat)
    {
        SeatPoint = seat;
        isFree = true;
    }

    public Transform SeatPoint;
    public bool isFree;
}

public class DinnerSeatsController : MonoBehaviour
{
    private List<Seat> Seats = new List<Seat>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Seat seat = new Seat (transform.GetChild (i).transform);
            Seats.Add (seat);
        }
    }

    public Seat GetFreeSeat()
    {
        foreach (var item in Seats)
        {
            if (item.isFree)
            {
                item.isFree = false;
                return item;
            }
        }
        
        return null;
    }

    public void SetSeatToFree(Seat item)
    {
        if (Seats.Contains (item))
        {
            int index = Seats.IndexOf (item);
            Seats[index].isFree = true;
        }
    }
}
