public class Patroller
{
    private Mover _mover;

    public Patroller (Mover mover)
    {
        _mover = mover;
    }

    public void Patrol ()
    {
        _mover.Move();
    }
}